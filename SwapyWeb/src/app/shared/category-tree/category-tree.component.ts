
import { Component, ElementRef, HostListener, OnInit, Renderer2 } from '@angular/core';
import { CategoryTreeService } from './category-tree.service';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { Observable, of } from 'rxjs';
import { Specification } from 'src/app/core/models/specification';
import { CategoryNode } from 'src/app/core/models/category-node.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-tree',
  templateUrl: './category-tree.component.html',
  styleUrls: ['./category-tree.component.scss']
})
export class CategoryTreeComponent implements OnInit {
  showElement: boolean;
  oldScroll: number;
  parentCategory$!: Observable<Specification<string>> | null;
  categoryes$!: Observable<CategoryNode[]>;

  constructor(private sharedApiService : SharedApiService, private elementRef: ElementRef, private renderer: Renderer2, private categoryTreeService: CategoryTreeService, private router: Router) {
    this.showElement = false;
    this.oldScroll = 0;
  }

  ngOnInit(): void {
    this.categoryTreeService.setCategoryTreeComponent(this);
    this.parentCategory$ = null;
    this.categoryes$ = this.sharedApiService.getCategories();
  }

  backButtonClick() {
    const categoryTreeMenu = this.elementRef.nativeElement.querySelector('.category-tree-menu');

    this.renderer.addClass(categoryTreeMenu, 'slide-out-animation');
    this.renderer.listen(categoryTreeMenu, 'animationend', () => {
      this.renderer.removeClass(categoryTreeMenu, 'slide-out-animation');
      this.renderer.setStyle(categoryTreeMenu, 'display', 'none');
    });

    this.parentCategory$?.subscribe((response: Specification<string>) => { 
      this.categoryes$ = this.sharedApiService.GetSiblingsByCategoryAsync(response.id);
    });

    this.categoryes$.subscribe((response: CategoryNode[]) => { 
        let categoryNode = response[0];
        if(categoryNode?.parent?.id != null) { this.parentCategory$ = of(categoryNode.parent); }
        else { this.parentCategory$ = null; }
    });

    categoryTreeMenu.addEventListener('animationend', () => {
      this.renderer.setStyle(categoryTreeMenu, 'display', 'flex');
      this.renderer.addClass(categoryTreeMenu, 'slide-in-animation');
      this.renderer.listen(categoryTreeMenu, 'animationend', () => {
        this.renderer.removeClass(categoryTreeMenu, 'slide-in-animation');
        this.renderer.setStyle(categoryTreeMenu, 'display', 'flex');
      });
    });
  }

  categoryButtonClick(category : CategoryNode) {

    if(category.isFinal){
      this.disableCategoryMenuAnimationAsync();
      window.location.replace(`products/search?subcategory=${category.id}`);
      return;
    }

    const categoryTreeMenu = this.elementRef.nativeElement.querySelector('.category-tree-menu');

    this.renderer.addClass(categoryTreeMenu, 'slide-out-animation');
    this.renderer.listen(categoryTreeMenu, 'animationend', () => {
      this.renderer.removeClass(categoryTreeMenu, 'slide-out-animation');
      this.renderer.setStyle(categoryTreeMenu, 'display', 'none');
    });

    this.categoryes$ = this.parentCategory$ == null ? this.sharedApiService.GetSubcategoriesByCategoryAsync(category.id) : this.sharedApiService.GetSubcategoriesBySubcategoryAsync(category.id) ;

    this.parentCategory$ = of(category);

    categoryTreeMenu.addEventListener('animationend', () => {
      this.renderer.setStyle(categoryTreeMenu, 'display', 'flex');
      this.renderer.addClass(categoryTreeMenu, 'slide-in-animation');
      this.renderer.listen(categoryTreeMenu, 'animationend', () => {
        this.renderer.removeClass(categoryTreeMenu, 'slide-in-animation');
        this.renderer.setStyle(categoryTreeMenu, 'display', 'flex');
      });
    });
  }

  // Toggle button
  toggleAnimation() {
    this.showElement = !this.showElement;

    const categoryTreeMenu = this.elementRef.nativeElement.querySelector('.category-tree-menu');

    if (this.showElement) {
      this.renderer.setStyle(categoryTreeMenu, 'display', 'flex');
      this.renderer.addClass(categoryTreeMenu, 'slide-in-animation');
      this.renderer.listen(categoryTreeMenu, 'animationend', () => {
        this.renderer.removeClass(categoryTreeMenu, 'slide-in-animation');
        this.renderer.setStyle(categoryTreeMenu, 'display', 'flex');
      });
    } else {
      this.renderer.addClass(categoryTreeMenu, 'slide-out-animation');
      this.renderer.listen(categoryTreeMenu, 'animationend', () => {
        this.renderer.removeClass(categoryTreeMenu, 'slide-out-animation');
        this.renderer.setStyle(categoryTreeMenu, 'display', 'none');
      });
    }
  }

  async disableCategoryMenuAnimationAsync() {
    if (this.showElement) {
      this.showElement = false;

      const categoryTreeMenu = this.elementRef.nativeElement.querySelector('.category-tree-menu');

      this.renderer.addClass(categoryTreeMenu, 'slide-out-animation');
      this.renderer.listen(categoryTreeMenu, 'animationend', () => {
        this.renderer.removeClass(categoryTreeMenu, 'slide-out-animation');
        this.renderer.setStyle(categoryTreeMenu, 'display', 'none');
      });

      await new Promise<void>((resolve) => {
        this.renderer.addClass(categoryTreeMenu, 'slide-out-animation');
        this.renderer.listen(categoryTreeMenu, 'animationend', () => {
          this.renderer.removeClass(categoryTreeMenu, 'slide-out-animation');
          this.renderer.setStyle(categoryTreeMenu, 'display', 'none');
          resolve();
        });
      });

      this.parentCategory$ = null;
      this.categoryes$ = this.sharedApiService.getCategories();
    }
  }

  // Scroller
  @HostListener('window:scroll', [])
  onWindowScroll() {
    const categoryTreeMenu = this.elementRef.nativeElement.querySelector('.category-tree-menu');
    const currentScroll = window.scrollY;

    this.disableCategoryMenuAnimationAsync();

    //categoryTreeMenu.style.top = `calc(${this.oldScroll - 1}px + 4.5rem)`;

    this.oldScroll = currentScroll;
  }

}

