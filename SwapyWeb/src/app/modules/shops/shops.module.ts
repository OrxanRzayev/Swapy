import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ShopsComponent } from './components/shops/shops.component';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopDetailComponent } from './components/shop-detail/shop-detail.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { PaginationComponent } from 'src/app/shared/pagination/pagination.component';
import { ShortNumberPipe } from 'src/app/core/pipes/short-number.pipe';



@NgModule({
  declarations: [
    ShopsComponent,
    ShopDetailComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    HttpClientModule,
    ShopRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
})

export class ShopsModule { }
