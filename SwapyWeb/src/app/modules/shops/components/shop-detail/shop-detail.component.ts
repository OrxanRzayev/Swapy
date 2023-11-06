import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopApiService } from '../../services/shop-api.services';
import { ShopDetail } from '../../models/shop-detail.interface';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, concat, filter, forkJoin, map, mergeMap, of, toArray } from 'rxjs';
import { HttpStatusCode } from 'axios';
import { environment } from 'src/environments/environment';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { Product } from 'src/app/modules/products/models/product.model';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { UserApiService } from 'src/app/modules/users/services/user-api.services';
import { UserType } from 'src/app/core/enums/user-type.enum';

@Component({
  selector: 'app-shop-detail',
  templateUrl: './shop-detail.component.html',
  styleUrls: ['./shop-detail.component.scss']
})
export class ShopDetailComponent implements OnInit {
  userId!: string | null;
  shopDetail!: ShopDetail;
  shopProducts!: Product[];
  allPages: number | null = 0;
  currentPage: number = 0;
  shopProductsCount: number | null = 0;
  shopDisabledProducts!: Product[];
  allDisabledPages: number | null = 0;
  currentDisabledPage: number = 0;
  shopDisabledProductsCount: number | null = 0;
  isMe: boolean;
  isIShop: boolean;
  isLike!: boolean;
  isSubscription!: boolean;
  pageSize: number = 10;
  selectedFilter: string = '1';
  sortByPrice: boolean = false;
  reverseSort: boolean = true;
  isLoadingProducts: boolean = true;
  isNotFoundProducts: boolean = false;
  selectedDisabledFilter: string = '1';
  sortDisabledByPrice: boolean = false;
  reverseDisabledSort: boolean = true;
  isLoadingDisabledProducts: boolean = false;
  isNotFoundDisabledProducts: boolean = false;

  @ViewChild('shopBannerDiv') shopBannerDiv!: ElementRef<HTMLDivElement>;

  @ViewChild('dialogElement') dialogElementRef!: ElementRef;

  constructor(private authFacade : AuthFacadeService , private shopApiService : ShopApiService, private userApiService : UserApiService,private sharedApiService : SharedApiService, private route: ActivatedRoute, private router: Router) { 
    this.userId = this.route.snapshot.paramMap.get('id');
    this.isMe = this.authFacade.getUserId() ===  this.userId;
    this.isIShop = this.authFacade.getUserType() === UserType.Shop;
  }

  ngOnInit(): void {
    const regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;
    if (!((this.userId) && regex.test(this.userId))) {
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return; 
    } 
    if(!this.authFacade.isAuthenticated()) { this.isLike = false; this.isSubscription = false; }
    else{
      this.userApiService.checkLike(this.userId as string).subscribe(
        (result) => { this.isLike = result; }
      );
      this.userApiService.checkSubscription(this.userId as string).subscribe(
        (result) => { this.isSubscription = result; }
      );
    }
    this.shopApiService.getShopById(this.userId as string).subscribe(
      (result) => {
        this.shopDetail = result;
        if(result.type !== UserType.Shop) { this.router.navigateByUrl('/404', { skipLocationChange: true }); }
        result.banner = `${environment.blobUrl}/banners/${result.banner}`;
        result.logo = `${environment.blobUrl}/logos/${result.logo}`
        this.shopBannerLoad(result.banner);
        if(this.isMe){
          setTimeout(() => {
            this.dialogElementRef.nativeElement.showModal();
          }, Math.floor(Math.random() * (60000 - 5000 + 1) + 5000));  
        }
      },
      (error) => {
        if(error.response.status === HttpStatusCode.NotFound){
          this.router.navigateByUrl('/404', { skipLocationChange: true }); 
        }
      }
    );
    this.loadShopProducts();
    this.loadShopDisabledProducts();
  }

  like(): void{
    if(!this.authFacade.isAuthenticated()) { this.router.navigate(['/auth/login']); return; }
    if(this.isLike){
      this.userApiService.removeLike(this.userId as string).subscribe(
        (result) => { this.isLike = false; }
      );
    }
    else{
      this.userApiService.addLike(this.userId as string).subscribe(
        (result) => { this.isLike = true; }
      );
    }
  }

  subscribe(): void{
    if(!this.authFacade.isAuthenticated()) { this.router.navigate(['/auth/login']); return; }
    if(this.isSubscription){
      this.userApiService.removeSubscription(this.userId as string).subscribe(
        (result) => { this.isSubscription = false; }
      );
    }
    else{
      this.userApiService.addSubscription(this.userId as string).subscribe(
        (result) => { this.isSubscription = true; }
      );
    }
  }

  transferToSettings(): void{
    if(this.isMe){ this.router.navigateByUrl('settings'); }
  }

  shopBannerLoad(imageUrl: string): void {
    const image = new Image();
    image.src = imageUrl;
    image.onload = () => {
      this.shopBannerDiv.nativeElement.style.backgroundImage = `url(${imageUrl})`;
      this.shopBannerDiv.nativeElement.style.backgroundColor = 'transparent';
    };
    image.onerror = () => {
      this.shopBannerDiv.nativeElement.style.backgroundColor = 'linear-gradient(to bottom, $secondary, $primary)';
    };
  }

  shopLogoLoadError(event: any) {
    event.srcElement.src = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAb1BMVEXg4OAAAAD////d3d3a2trl5eXt7e3Y2Nj09PTPz8/7+/vU1NTn5+fi4uLq6urLy8taWlqtra1CQkJhYWG0tLSCgoJRUVGjo6MhISGdnZ01NTUZGRl5eXmPj4/AwMBvb29ISEgTExOUlJQqKiq7u7vMOI2cAAAK10lEQVR4nOWd65qjIAyGcYSxVqg9jj13puv9X+Nqj7YCGggW2+/XPjvPLL5LTEIMQALn2qzH290xm+T7NE4ooUmc7vNJdtxtx+uN++GJw397M50d5/uEjzhnjImTCCHnPxR/w4ufJPv5cTZ1CeqKcHrI0oRxdoZSq2TlLEmzw9TRk7ggnC7mESnhtGyPnIyTaL5wQYlOOMsiBqKrUrIom2E/ECrh8jAv6cBwFczi9+eHJeZD4REWeCG3obtR8hATEovwJ6PcwDQVjILT7AfpyXAIFymzMk4JJGPpAuXZEAj/rRiKddYgOVv984DwZ4I9fRVGxibWxmpJ+JMz5gjvLMZyS0Yrwt/c2fTdJVj++yLCde7k9ZMw8nz9AsLlauTWPqtio5VxgDQlXFDeGV8pTk1jhxnhT9yRgd4leGzmcowIsw4cjISRZR0Rzjo20Ls4NVh5gAn/JqNXTOBZYjT5c004TrrzoDKxZOyW8PiSN7AqwY4OCZdp5y5UgshTUGyEEI7J6/lKCQKxVADh7uUWepVgOxeEcw8s9CrB5+iEm/RVQVAunratIrcknNLXBom6GG1ZXG1HOPPEx1QlSLsEpxXh1hsfU5VgWyzChUc+pirB26yoWhDuPAUsEVtEjWbCo19O9FG8OYVrJPQasA1iE+HOb8ACsclQGwgXvgMWiA3uRk+49dbJ3CW4PmhoCWdexsFnCaYN/TrCqYeZjEyC6BI4DeGG9gOwQKSaNFxDmPqWbKvFUhPCuf9u9C7NelFJ6H0gfJQ6LKoIx71wo3cJpqrdKAiXPXGjdwmiqMApCNO+ARaICm8jJ/Q83ZZLkYRLCfv2Ep6leBVlhH9JHwELxOSvJeGkP6H+UWzSjnA2evWTGmskycElhL1JR+sStA1h1kc/ehWvfwivEf709SU8i9XaGWqEcX9ttJSImwh7UJjRq1a2eSJc9tjNnCXoUku46vsUFpO40hGu+xsK7xqtNYR5vx3pWSxXE/7230ZL8V8lYd53N3OWyFWEPQ/2dz2E/Srhm0zh0yRWCN9mCh8nsUJosiwMu5AB4URG+A8MGIb8e+Bc398CDsn+SQhXUMLw+6sjDRkUka0khMDqU0iGXQEW+gYiClYnBC4qugWEI96XGDdCYA04HHQK+PU1giHe68NXQmCoCHnHgF9fQHdzCxhXwgxGSLu10VJAO71tXbgQAle+Iekc8GtIQYS3lfCF8AD0M1cjHUaJa93+M2GEhB8eCOcwP0NH11GTyLXia9ilQF8zrxIuQyCh4agGCqPrWEBXI8JlhRBopF0Smo91MVNiYqT9ILyY6ZkQmpL2gpCwO+EMWp/pByGf3QiB4V49auEMRJHBmyzpwGM16xz0T4QRtHwhHzUU3+dUZ/hN8BjNCUV0JZyC176yUcOH9SJ0LQAbq6XY9EII/xojGfV5OTXAQrQgPC2hSsI5whyG4deThkiINnM4vxCCX0PJqJLFBtIsWhCeXkRi1ChbG1VaswEuWtuO1V6n1loCT9lko9ZsFM9ObWJvmbgRg2hYHzUcyQi/OAaiDWEZEYlRl97zqFRetRkAl3StxoKorNaQYGPQ41UjlBc12izLw6a31Yow2RSE8HhfG7UeKq6EjQ9V2HeDz7XKgfm0IASn3fVRzQlPL7Ae0Y5wVhAeEeZQaaUND3XxUFpEK0J2LAihq1/JqGEkJxxE+n/m5oJ1iFaExSqYBHt7QhLJfem3nrASYzSIdoT7gtCkXbbmS4WUkGh96UMQVSPaESYB2Zj0X9QifiIz00Gie6anLEGJaFdP4Bti1CRUGzWSFcFD3RTW0iAVoh3haE3GGHNYTGI99ea6KZTkeQpEyzkcky0KIaHJs7P5BgKqEC0Jt2Rn0oEhWQHT+PGheax5IkWmLkW0I2Q7YhLwpVUMGlfy70GUwAHliJaER2KwdlJUomgUUz4YDocDHsWRpqKoBJQiWhJmZGLSCKWoJtIoiUslke5pNIAyRDtCMSFGrV7qinBIKdU3+WgBJYiWhDkxSdpsRm0ArCNaEu6J0T4881EbAWuIloQpMdp9YDxqC8BnREvCmBjtUzMdtRXgE6IlYaJP/1UyHLUl4COi5Zc8w2JYi1FDUftRa8AHROtvlY7mMBx8PX9hAwBWEa3n0M17WLa9DR8RQYAVROv30IkvPWeoD4hAwDuitS91EQ+vKXgFEQx4Q7SOhw5ymkpn5hXRAPD6TcA6p0HNS58Br7NoAnitRVrnpYhrizrgBdEIEIlwgrc+vP7oqZpRIJoB4hAW60OsNb4CsPxOagaIRHjEqtNchNn+jUO4w6q14QPiEPItUr308teoDfw4hGOkmrcDQBzC0Rrnu8VJ2FswcOZwg/PtyQUgCmH57Qnl+6ELQBzCPc43YIL+DqIRznG+4zsBRCE8fcdH6MVws88Lg/DUi4HRT+NkkxDKHE5xeqJU/TQvJzz3RCH0tflLmCL1JnpLeOlNtO8v9Zbw0l9q3yPsK+G1R9i+z9tbwgirV99XwluvvvV+C18Jb/strPfM+Ep42zNjve/JU8L7vifrvWshLS9PRxZntjXvyt416/2H1MWW7sj2u0Vl/6H9HlKH59JYzOFH7QN+/73cHu/HNx7rcT++8ZkKqDt+FWOZEj6cqQA1U3o9F2NAnfiYB13jYwQjfDoXw/hsk6Fz3UZq2LzxpOezTYBBP4yaEhJ8NWzeeNbz+TTQM4Yiw2+CFqKwzp/aGUPAag1V7ANyJ21jfF31c6KAS6gwot0CDhLYFErO+gKe1xYmTtYTGkDYFErOa4OeuRcmtR0W7sRjYKiQnrkHPTcxjOLo1JrvWgORAGdQcW4i+OzLW2u+ayW6rQ1yQOnZlwbnl546850rNDj6Un5+6fufQfs+h9AqzxF+l0lUnwX9Aed5v/+Z7B9wrv77343wAfdbvP8dJR9wz0zfw36Lu4Le/76nD7iz6wPuXXv/u/M+4P7D97/D8gPuIf2Au2Q/4D7g3r2K4DudP+Be7g+4W73wNv0J/EzhZRoIN71JUAXdGBGatNa+RKdGWSPCYNYLhyqYJN9uSRhsuf+Igm+1DHrCPpRtaoUZGKH/YVEdCFsS+p6EK9JtCKHfiM2ALQgLQ/XV3YhGE21HWLgbPxFFk5NpTRhsvYyLgunDBIQwmHmY3QiiDfRAwmBKfUvDGdWlanDCYJP65VJ5qkm2jQjL9aI/lio060FzwmDnjb8RrEWUMCAMxp74G0FUNRlbwmCZemCpgqeKqhoCYZHCvdxSBWtO1GwIg3Hy2rDBEoiFmhAGf5PR66ZRjCZ/0AcGExYJDn1VaOS0XRpjS1huXXjFNApW/4TtijD4iTt3qoLHtSYEh4TFiqpjU+W0zUoJkzBYrkbdeVU2WoFiIAphEKzzjkxV8Hzd/DgOCIPgN+/A5QiW/zY/iiPCwuXkzK2tMpabORgswoJxwpxNpGBsYsmHQBgE/1bMyQspOFv9ax6+A8JCixR7IovpS03jw6NwCAtjzSgXWJBCcJpZm+dFWIRFgDzMQxRrFTycH4zDX014hMEJknErcxXF72PiBciEpWZZVFLCMUVJF2UGqwe90AkLTRfziIAoSzoSzRctS6AguSAsNT1kacJLTD2nKOF4kmYHF3SlXBGW2kxnx/k+4SNeHl8jTjpDlVysIBvxZD8/zqZtq7smckl40WY93u6O2STfp3FCCU3idJ9PsuNuO167RLvoP6oh3SkvURrLAAAAAElFTkSuQmCC";
  }

  loadShopProducts(isNewRequest: boolean = false): void {
    this.isLoadingProducts = true;
    if(isNewRequest){
      this.shopProductsCount = null;
      this.allPages = null;
      this.shopProducts = [];
      this.currentPage = 1;
    }
    else { this.currentPage++; }
    this.sharedApiService.getProducts(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort, this.userId).subscribe((response: PageResponse<Product>) => { 
      response.items.forEach(item => {
        if (Array.isArray(item.images)) {
          item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
        }
      });
      this.shopProductsCount = response.count;
      this.allPages = response.allPages;
      if(this.shopProducts != null) { this.shopProducts.push(...response.items); }
      else { this.shopProducts = response.items; }
      this.isLoadingProducts = false;
    },
    (error) => {
      this.isLoadingProducts = false;
      this.isNotFoundProducts = true;
    });
  }

  loadShopDisabledProducts(isNewRequest: boolean = false): void {
    if(!this.isMe)
    {
      this.isNotFoundDisabledProducts = true;
      return;
    }
    this.isLoadingDisabledProducts = true;
    if(isNewRequest){
      this.shopDisabledProductsCount = null;
      this.allDisabledPages = null;
      this.shopDisabledProducts = [];
      this.currentDisabledPage = 1;
    }
    else { this.currentDisabledPage++; }
    this.sharedApiService.getDisabledProducts(this.currentDisabledPage, this.pageSize, this.sortDisabledByPrice, this.reverseDisabledSort, this.userId).subscribe((response: PageResponse<Product>) => { 
      response.items.forEach(item => {
        if (Array.isArray(item.images)) {
          item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
        }
      });
      this.shopDisabledProductsCount = response.count;
      this.allDisabledPages = response.allPages;
      if(this.shopDisabledProducts != null) { this.shopDisabledProducts.push(...response.items); }
      else { this.shopDisabledProducts = response.items; }
      this.isLoadingDisabledProducts = false;
      this.isNotFoundDisabledProducts = false;
    },
    (error) => {
      this.isLoadingDisabledProducts = false;
      this.isNotFoundDisabledProducts = true;
    });
  }

  trackByProductId(index: number, product: Product): string {
    return product.id;
  }

  onSelectFilterChange(): void{
    switch(this.selectedFilter) {
      case '1':{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
      case '2':{
        this.sortByPrice = false;
        this.reverseSort = false;
        break;
      }
      case '3':{
        this.sortByPrice = true;
        this.reverseSort = false;
        break;
      }
      case '4':{
        this.sortByPrice = true;
        this.reverseSort = true;
        break;
      }
      default:{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
    }
    this.loadShopProducts(true);
  }

  onSelectDisabledFilterChange(): void{
    switch(this.selectedDisabledFilter) {
      case '1':{
        this.sortDisabledByPrice = false;
        this.reverseDisabledSort = true;
        break;
      }
      case '2':{
        this.sortDisabledByPrice = false;
        this.reverseDisabledSort = false;
        break;
      }
      case '3':{
        this.sortDisabledByPrice = true;
        this.reverseDisabledSort = false;
        break;
      }
      case '4':{
        this.sortDisabledByPrice = true;
        this.reverseDisabledSort = true;
        break;
      }
      default:{
        this.sortDisabledByPrice = false;
        this.reverseDisabledSort = true;
        break;
      }
    }
    this.loadShopDisabledProducts(true);
  }

  copyToClipboard(event: Event) {
    const target = event.target as HTMLElement;
    const text = target.innerText;
    if (navigator.clipboard && typeof navigator.clipboard.writeText === 'function') {
      const el = document.createElement('textarea');
      el.value = text;
      document.body.appendChild(el);
      el.select();
      document.execCommand('copy');
      document.body.removeChild(el);

      //Toast
      const toastElement = document.getElementById('footer-toast');
      const toast = new (window as any).bootstrap.Toast(toastElement as HTMLElement);
      toast.show();
      
      setTimeout(() => {
        toast.hide();
      }, 1500);
    }
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }
}
