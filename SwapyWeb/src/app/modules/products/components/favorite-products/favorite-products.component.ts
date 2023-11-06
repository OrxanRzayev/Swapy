import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { Router } from '@angular/router';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { HttpStatusCode } from 'axios';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-favorite-products',
  templateUrl: './favorite-products.component.html',
  styleUrls: ['./favorite-products.component.scss']
})
export class FavoriteProductsComponent implements OnInit {
  userId!: string | null;
  favoriteProducts!: Product[];
  allPages: number | null = 0;
  currentPage: number = 0;
  favoriteProductsCount: number | null = 0;
  pageSize: number = 10;
  selectedFilter: string = '1';
  sortByPrice: boolean = false;
  reverseSort: boolean = true;
  isLoadingProducts: boolean = true;
  isNotFoundProducts: boolean = false;

  constructor(private authFacade : AuthFacadeService ,private sharedApiService : SharedApiService, private router: Router) { }

  ngOnInit(): void {
    this.userId = this.authFacade.getUserId()
    const regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;
    if (!(this.userId && regex.test(this.userId))) {
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return; 
    } 
    this.loadFavoriteProducts();
  }


  loadFavoriteProducts(isNewRequest: boolean = false): void {
    this.isLoadingProducts = true;
    if(isNewRequest){
      this.favoriteProductsCount = null;
      this.allPages = null;
      this.favoriteProducts = [];
      this.currentPage = 1;
    }
    else { this.currentPage++; }
    this.sharedApiService.getFavoriteProducts(this.currentPage, this.pageSize, this.sortByPrice, this.reverseSort).subscribe((response: PageResponse<Product>) => { 
      response.items.forEach(f => f.images[0] = `${environment.blobUrl}/product-images/${f.images[0]}`);
      this.favoriteProductsCount = response.count;
      this.allPages = response.allPages;
      if(this.favoriteProducts != null) { this.favoriteProducts.push(...response.items); }
      else { this.favoriteProducts = response.items; }
      this.isLoadingProducts = false;
    },
    (error) => {
      this.isLoadingProducts = false;
      this.isNotFoundProducts = true;
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
    this.loadFavoriteProducts(true);
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }
}