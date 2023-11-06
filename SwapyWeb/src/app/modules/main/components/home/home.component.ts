import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import 'node_modules/slick-carousel/slick/slick.min.js';
import 'node_modules/slick-carousel';
import * as $ from 'jquery';
import { SharedApiService } from '../../services/shared-api.service';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { Product } from 'src/app/modules/products/models/product.model';
import { Observable, finalize, take, tap } from 'rxjs';
import { Router } from '@angular/router';
import { Specification } from 'src/app/core/models/specification';
import { Shop } from 'src/app/modules/shops/models/shop.model';
import { ChangeDetectorRef } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CategoryNode } from 'src/app/core/models/category-node.interface';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  categories!: CategoryNode[];
  popularShops!: PageResponse<Shop>;
  latestProducts!: Product[];
  allPages: number = 0;
  currentPage: number = 0;
  latestProductsCount: number = 0;
  pageSize: number = 10;
  isLoadingCategories: boolean = true;
  isLoadingShops: boolean = true;
  isLoadingProducts: boolean = true;

  constructor(private sharedApiService : SharedApiService, private router: Router, private changeDetectorRef : ChangeDetectorRef){}

  ngOnInit(): void {
    this.loadCategories();
    this.loadShops();
    this.loadLatestProducts();
  }

  initShopSliders(): void{
    (<any>$('.shop-slider')).slick({
      lazyLoad: 'ondemand',
      arrows:true,
      prevArrow: $('.shop-slider-controls .prev'),
      nextArrow: $('.shop-slider-controls .next'),
      dots:false,
      adaptiveHeight:true,
      slidesToShow: 5,
      slidesToScroll: 1,
      speed:500,
      easing:'ease',
      infinite:false,
      initialSlide:0,
      autoplay:false,
      autoplaySpeed:3000,
      pauseOnFocus:true,
      pauseOnHover:true,
      pauseOnDotsHover:false,
      draggable:true,
      swipe:true,
      touchThreshold:25,
      touchMove:true,
      waitForAnimate:true,
      centerMode:false,
      variableWidth:false,
      rows:1,
      slidesPerRow:1,
      vertical:false,
      verticalSwiping:false,
      appendArrows: $('.shop-slider-controls'),
      responsive: [
        {
          breakpoint: 2300, 
          settings: {
            slidesToShow: 4 
          }
        },
        {
          breakpoint: 1680, 
          settings: {
            slidesToShow: 3 
          }
        },
        {
          breakpoint: 1230, 
          settings: {
            slidesToShow: 2 
          }
        },
        {
          breakpoint: 840, 
          settings: {
            slidesToShow: 1 
          }
        }
      ],
    });
  }

  initCategorySliders(): void{
    (<any>$('.category-slider')).slick({
      arrows:true,
      prevArrow: $('.category-slider-controls .prev'), 
      nextArrow: $('.category-slider-controls .next'), 
      dots:false,
      adaptiveHeight:true,
      slidesToShow: 13,
      slidesToScroll: 13,
      speed:1000,
      easing:'ease',
      infinite:true,
      initialSlide:0,
      autoplay:false,
      autoplaySpeed:3000,
      pauseOnFocus:true,
      pauseOnHover:true,
      pauseOnDotsHover:false,
      draggable:true,
      swipe:true,
      touchThreshold:25,
      touchMove:true,
      waitForAnimate:true,
      centerMode:false,
      variableWidth:false,
      rows:1,
      slidesPerRow:1,
      vertical:false,
      verticalSwiping:false,
      appendArrows: $('.category-slider-controls'),
      responsive: [
        {
          breakpoint: 2300, 
          settings: {
            slidesToShow: 12,
            slidesToScroll: 12
          }
        },
        {
          breakpoint: 1670, 
          settings: {
            slidesToShow: 10,
            slidesToScroll: 10
          }
        },
        {
          breakpoint: 1480, 
          settings: {
            slidesToShow: 8,
            slidesToScroll: 8
          }
        },
        {
          breakpoint: 1100, 
          settings: {
            slidesToShow: 6,
            slidesToScroll: 6,
          }
        },
        {
          breakpoint: 800, 
          settings: {
            slidesToShow: 4,
            slidesToScroll: 4
          }
        },
        {
          breakpoint: 550, 
          settings: {
            slidesToShow: 3,
            slidesToScroll: 3
          }
        },
        {
          breakpoint: 380, 
          settings: {
            slidesToShow: 2,
            slidesToScroll: 2
          }
        }
      ]
    });
  }

  loadCategories(): void{
    this.isLoadingCategories = true;
    this.sharedApiService.getCategories().subscribe((result) => { 
      this.categories = result;
      this.changeDetectorRef.detectChanges();
      this.initCategorySliders(); 
      this.isLoadingCategories = false;
    });

  }

  loadShops(): void{
    this.isLoadingShops = true;
    this.sharedApiService.getShops(1, 10, true, true).subscribe((result) => {
      this.popularShops = result;
      this.changeDetectorRef.detectChanges();
      this.initShopSliders(); 
      this.isLoadingShops = false;
    });
  }

  loadLatestProducts(): void {
    this.isLoadingProducts = true;
    this.sharedApiService.getProducts(++this.currentPage, this.pageSize, false, true).subscribe((response: PageResponse<Product>) => { 
      response.items.forEach(item => {
        if (Array.isArray(item.images)) {
          item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
        }
      });
      
      this.latestProductsCount = response.count;
      this.allPages = response.allPages;
      if(this.latestProducts != null) { this.latestProducts.push(...response.items); }
      else { this.latestProducts = response.items; }
      this.isLoadingProducts = false;
    });
  }

  trackByProductId(index: number, product: Product): string {
    return product.id;
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }
}