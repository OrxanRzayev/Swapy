import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './components/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MainRoutingModule } from './main-routing.module';
import { CustomDatePipeEn } from 'src/app/core/pipes/custom-date-en.pipe';
import { ProductCardComponent } from 'src/app/shared/product-card/product-card.component';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    HttpClientModule,
    MainRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
})
export class MainModule { }
