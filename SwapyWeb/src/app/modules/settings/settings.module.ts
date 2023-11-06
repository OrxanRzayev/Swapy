import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserGeneralComponent } from './components/user-general/user-general.component';
import { ShopGeneralComponent } from './components/shop-general/shop-general.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { ImagesUpdateComponent } from './components/images-update/images-update.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SettingsRoutingModule } from './settings-routing.module';

@NgModule({
  declarations: [
    UserGeneralComponent,
    ShopGeneralComponent,
    ChangePasswordComponent,
    ImagesUpdateComponent,
    DashboardComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    HttpClientModule,
    SettingsRoutingModule,
    ReactiveFormsModule
  ]
})
export class SettingsModule { }
