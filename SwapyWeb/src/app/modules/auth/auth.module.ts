import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AuthRoutingModule } from './auth-routing.module';
import { LoginComponent } from './components/login/login.component';
import { UserRegistrationComponent } from './components/user-registration/user-registration.component';
import { ShopRegistrationComponent } from './components/shop-registration/shop-registration.component';
import { EmailVerifyComponent } from './components/email-verify/email-verify.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

@NgModule({
  declarations: [
    LoginComponent,
    ShopRegistrationComponent,
    UserRegistrationComponent,
    EmailVerifyComponent,
    ForgotPasswordComponent,
    ResetPasswordComponent
  ],
  imports: [
    FormsModule,
    CommonModule,
    HttpClientModule,
    AuthRoutingModule,
    ReactiveFormsModule
  ],
})
export class AuthModule { }