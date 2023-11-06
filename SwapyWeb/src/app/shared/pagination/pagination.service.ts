import { Injectable } from '@angular/core';
import { PaginationComponent } from './pagination.component';

@Injectable({
  providedIn: 'root'
})
export class PaginationService {
  private paginationComponent: PaginationComponent | null = null;

  setPaginationComponent(component: PaginationComponent) {
    this.paginationComponent = component;
  }

  updatePagination(selectedPage : number, total_pages : number): void {
    if (this.paginationComponent) {
      this.paginationComponent.updatePagination(selectedPage, total_pages);
    }
  }
}