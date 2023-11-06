import { Injectable } from '@angular/core';
import { ProductsSearchComponent } from './products-search.component';

@Injectable({
  providedIn: 'root'
})
export class ProductsSearchService {
    private productsSearchComponent: ProductsSearchComponent | null = null;

    setCategoryTreeComponent(component: ProductsSearchComponent) {
        this.productsSearchComponent = component;
    }

    changeTitleFilter(title: string | null) {
        if (this.productsSearchComponent) {
            this.productsSearchComponent.changeTitleFilter(title);
        }
    }
}