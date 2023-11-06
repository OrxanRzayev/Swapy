import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import 'node_modules/slick-carousel/slick/slick.min.js';
import 'node_modules/slick-carousel';
import { AfterContentInit, AfterViewInit, ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductApiService } from '../../services/product-api.service';
import { HttpStatusCode } from 'axios';
import { Specification } from 'src/app/core/models/specification';
import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { environment } from 'src/environments/environment';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss']
})
export class ProductDetailComponent implements OnInit, AfterViewInit  {
  productId!: string | null;
  product!: any;
  productCategoryType!: CategoryType;
  isMyProduct!: boolean;
  similarProducts!: Product[];
  allPages: number | null = 0;
  currentPage: number = 0;
  similarProductsCount: number | null = 0;
  pageSize: number = 10;
  isLoadingProducts: boolean = true;
  isNotFoundProducts: boolean = false;
  isMe: boolean = false;

  constructor(private authFacade : AuthFacadeService , private sharedApiService : SharedApiService, private productApiService : ProductApiService, private route: ActivatedRoute, private router: Router, private changeDetectorRef : ChangeDetectorRef, private spinnerService: SpinnerService) {
    this.productId = this.route.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    const regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;
    if (!((this.productId) )) { //&& regex.test(this.productId)
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return; 
    } 

    this.productApiService.GetProductCategoryType(this.productId).subscribe(
      (result) => {
        this.loadProductDetailByCategory(result);
      },
      (error) => {
        if(error.response.status === HttpStatusCode.NotFound){
          this.router.navigateByUrl('/404', { skipLocationChange: true }); 
          return;
        }
      }
    );
    this.loadSimilarProducts();
  }

  ngAfterViewInit(): void {}

  incrementViews(): void{
    if (this.productId && (!this.isMe)) { 
      this.productApiService.incrementViewsAsync(this.productId).subscribe();
    } 
  }

  initProductImagesSliders(): void {
    (<any>$('.product-images-slider')).slick({
      arrows: false,
      dots: false,
      adaptiveHeight: true,
      slidesToShow: 5,
      slidesToScroll: 1,
      speed: 300,
      easing: 'ease',
      infinite: false,
      initialSlide: 0,
      autoplay: false,
      autoplaySpeed: 3000,
      pauseOnFocus: true,
      pauseOnHover: true,
      pauseOnDotsHover: true,
      draggable: false,
      swipe: false, 
      touchThreshold: 25,
      touchMove: false,
      waitForAnimate: false,
      centerMode: true,
      variableWidth: true,
      rows: 1,
      slidesPerRow: 1,
      vertical: false,
      verticalSwiping: false,
      asNavFor: ".product-big-images-slider" 
    });
  
    (<any>$('.product-big-images-slider')).slick({
      arrows: true,
      prevArrow: $('.product-big-images-slider-container .left'), 
      nextArrow: $('.product-big-images-slider-container .right'), 
      adaptiveHeight: true,
      slidesToShow: 1,
      slidesToScroll: 1,
      speed: 300,
      easing: 'ease',
      waitForAnimate: false,
      infinite: false,
      initialSlide: 0,
      autoplay: false,
      autoplaySpeed: 3000,
      pauseOnFocus: true,
      pauseOnHover: true,
      pauseOnDotsHover: true,
      touchMove: false,
      draggable: true,
      swipe: true, 
      touchThreshold: 10,
      fade: true,
      asNavFor: ".product-images-slider" 
    });

    (<any>$('.enlarged-image-slider')).slick({
      arrows: false,
      // prevArrow: $('.product-big-images-slider-container .left'), 
      // nextArrow: $('.product-big-images-slider-container .right'), 
      adaptiveHeight: true,
      slidesToShow: 1,
      slidesToScroll: 1,
      speed: 300,
      easing: 'ease',
      waitForAnimate: false,
      infinite: false,
      initialSlide: 0,
      autoplay: false,
      autoplaySpeed: 3000,
      pauseOnFocus: true,
      pauseOnHover: true,
      pauseOnDotsHover: true,
      touchMove: false,
      draggable: true,
      swipe: true, 
      touchThreshold: 10,
      fade: true,
    });

    $('.product-images-slider-slide').on('click', function () { (<any>$('.product-images-slider')).slick('goTo', $(this).index()); });
  }

  productImageLoadError(event: any) {
    event.srcElement.src = environment.blobUrl + '/product-images/default-product-image.png';
  }

  loadProductDetailByCategory(category: Specification<CategoryType>): void {
    switch(category.value){
      case CategoryType.AnimalsType: {
        this.productApiService.GetAnimalDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.AutosType: {
        this.productApiService.GetAutoDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ClothesType: {
        this.productApiService.GetClothesDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        this.productApiService.GetElectronicDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ItemsType: {
        this.productApiService.GetItemDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.RealEstatesType: {
        this.productApiService.GetRealEstateDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.TVsType: {
        this.productApiService.GetTVDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.productCategoryType = category.value;
            this.isMe = this.authFacade.getUserId() ===  result.userId;
            this.changeDetectorRef.detectChanges();
            this.initProductImagesSliders();
            this.incrementViews();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      default: {
        this.router.navigateByUrl('/404', { skipLocationChange: true }); 
        return;
      }
    }
  }

  changeFavorite(event: any) : void {
    event.preventDefault();
    (this.product.isFavorite ? this.sharedApiService.removeFavorites(this.product.productId) : this.sharedApiService.addToFavorites(this.product.productId))
    .subscribe(
      (result) => { this.product.isFavorite = !this.product.isFavorite },
      (error) => {
        if(error.response.status === HttpStatusCode.Unauthorized){
          this.router.navigate(['/auth/login']);
        }
      }
    );
  }

  async onIsDisableChange(event: any) : Promise<void> {
    event.preventDefault();
    await this.productApiService.switchProductEnabling(this.product.productId).subscribe(
      (response) => { this.product.isDisable = !this.product.isDisable; },
      (error) => {
        if(error.response.status === HttpStatusCode.Unauthorized){
          this.authFacade.logout();
        }
      }
    )
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

  removeProduct() : void {
    this.spinnerService.changeSpinnerState(true);
    this.productApiService.removeProduct(this.product.productId).subscribe(
      (response) => { 
        this.spinnerService.changeSpinnerState(false);
        window.history.back(); 
      },
      (error) => {
        this.spinnerService.changeSpinnerState(false);
        if(error.response.status === HttpStatusCode.Unauthorized){
          this.authFacade.logout();
          this.router.navigateByUrl('auth/login');
          return;
        }
      }
    )
  }

  transferToEditPage() : void {
    this.router.navigateByUrl('products/edit/' + this.product.productId);
  }

  loadSimilarProducts(isNewRequest: boolean = false): void {
    this.isLoadingProducts = true;
    if(isNewRequest){
      this.similarProductsCount = null;
      this.allPages = null;
      this.similarProducts = [];
      this.currentPage = 1;
    }
    else { this.currentPage++; }
    this.productApiService.getSimilarProducts(this.currentPage, this.pageSize, this.productId  as string).subscribe((response: PageResponse<Product>) => { 
      response.items.forEach(item => {
        if (Array.isArray(item.images)) {
          item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
        }
      });
      this.similarProductsCount = response.count;
      this.allPages = response.allPages;
      if(this.similarProducts != null) { this.similarProducts.push(...response.items); }
      else { this.similarProducts = response.items; }
      this.isLoadingProducts = false;
    },
    (error) => {
      if(error.response.status === HttpStatusCode.BadRequest){
        this.router.navigateByUrl('/404', { skipLocationChange: true }); 
      }
      this.isLoadingProducts = false;
      this.isNotFoundProducts = true;
    });
  }

  goToChat(): void {
    const params = { productId: this.productId };
    this.router.navigate(['chats'], { queryParams: params });
  }

  trackByProductId(index: number, product: Product): string {
    return product.id;
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }

}
