import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { CategoryNode } from 'src/app/core/models/category-node.interface';
import { Specification } from 'src/app/core/models/specification';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.scss']
})
export class CategoryCardComponent implements OnInit {

  @Input() category!: CategoryNode;
  categoryImage!: string;

  constructor(private router: Router) { 
  }

  ngOnInit(): void {
    switch(this.category.type) {
      case CategoryType.AnimalsType: {
        this.categoryImage = `${environment.blobUrl}/categories/animals.png`;
        break;
      }
      case CategoryType.AutosType: {
        this.categoryImage = `${environment.blobUrl}/categories/auto.png`;
        break;
      }
      case CategoryType.ClothesType: {
        this.categoryImage = `${environment.blobUrl}/categories/clothes.png`;
        break;
      }
      case CategoryType.ElectronicsType: {
        this.categoryImage = `${environment.blobUrl}/categories/electronics.png`;
        break;
      }
      case CategoryType.ItemsType: {
        this.categoryImage = `${environment.blobUrl}/categories/hobby.png`;
        break;
      }
      case CategoryType.RealEstatesType: {
        this.categoryImage = `${environment.blobUrl}/categories/real_estate.png`;
        break;
      }
      case CategoryType.TVsType: {
        this.categoryImage = `${environment.blobUrl}/categories/tv.png`;
        break;
      }
    }
  }

  moveToCategory(): void{
    window.location.replace(`products/search?category=${this.category.id}`);
  }
}
