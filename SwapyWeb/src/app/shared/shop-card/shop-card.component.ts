import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Shop } from 'src/app/modules/shops/models/shop.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-shop-card',
  templateUrl: './shop-card.component.html',
  styleUrls: ['./shop-card.component.scss']
})
export class ShopCardComponent implements OnInit {

  @Input() shop!: Shop;

  constructor(private router: Router) {}
  
  ngOnInit(): void {
    this.shop.logo = `${environment.blobUrl}/logos/${this.shop.logo}`;
  }

  shopLogoLoadError(event: any) {
    event.srcElement.src = `${environment.blobUrl}/logos/default-shop-logo.png`;
  }

  moveToShop(): void {
    window.location.replace(`shops/${this.shop.userId}`);
  }
}
