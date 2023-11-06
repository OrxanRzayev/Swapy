import { Injectable } from '@angular/core';
import { CategoryTreeComponent } from './category-tree.component';

@Injectable({
  providedIn: 'root'
})
export class CategoryTreeService {
  private categoryTreeComponent: CategoryTreeComponent | null = null;

  setCategoryTreeComponent(component: CategoryTreeComponent) {
    this.categoryTreeComponent = component;
  }

  toggleAnimation() {
    if (this.categoryTreeComponent) {
      this.categoryTreeComponent.toggleAnimation();
    }
  }

  disableCategoryMenu() {
    if (this.categoryTreeComponent) {
      this.categoryTreeComponent.disableCategoryMenuAnimationAsync();
    }
  }
}