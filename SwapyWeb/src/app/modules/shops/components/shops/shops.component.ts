import { AfterViewInit, Component, OnInit } from '@angular/core';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { PaginationService } from 'src/app/shared/pagination/pagination.service';
import { Shop } from '../../models/shop.model';
import { Observable } from 'rxjs';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
@Component({
  selector: 'app-shops',
  templateUrl: './shops.component.html',
  styleUrls: ['./shops.component.scss'],
})
export class ShopsComponent implements AfterViewInit, OnInit {
  shops!: Shop[];
  allPages: number | null = 0;
  currentPage: number = 0;
  shopsCount: number | null = 0;
  pageSize: number = 10;
  selectedFilter: string = '1';
  sortByViews: boolean = false;
  reverseSort: boolean = false;
  isLoadingShops: boolean = true;
  isNotFoundShops: boolean = false;
  
  constructor(private paginationService: PaginationService, private sharedApiService : SharedApiService) {
  }

  ngOnInit(): void {
    this.loadShops();
  }

  ngAfterViewInit(): void {
  }

  loadShops(isNewRequest: boolean = false): void {
    this.isLoadingShops = true;
    if(isNewRequest){
      this.shopsCount = null;
      this.allPages = null;
      this.shops = [];
      this.currentPage = 1;
    }
    else { this.currentPage++; }
    this.sharedApiService.getShops(this.currentPage, this.pageSize, this.sortByViews, this.reverseSort).subscribe((response: PageResponse<Shop>) => { 
      this.shopsCount = response.count;
      this.allPages = response.allPages;
      if(this.shops != null) { this.shops.push(...response.items); }
      else { this.shops = response.items; }
      this.isLoadingShops = false;
    },
    (error) => {
      this.isLoadingShops = false;
      this.isNotFoundShops = true;
    });
  }

  onSelectFilterChange(): void{
    switch(this.selectedFilter) {
      case '1':{
        this.sortByViews = false;
        this.reverseSort = false;
        break;
      }
      case '2':{
        this.sortByViews = false;
        this.reverseSort = true;
        break;
      }
      case '3':{
        this.sortByViews = true;
        this.reverseSort = true;
        break;
      }
      case '4':{
        this.sortByViews = true;
        this.reverseSort = false;
        break;
      }
      default:{
        this.sortByViews = false;
        this.reverseSort = false;
        break;
      }
    }
    this.loadShops(true);
  }

  trackByShopId(index: number, shop: Shop): string {
    return shop.userId;
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }
}
