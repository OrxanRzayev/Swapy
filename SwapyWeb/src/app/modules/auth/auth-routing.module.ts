import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmailVerifyComponent } from './components/email-verify/email-verify.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LoginComponent } from './components/login/login.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';
import { ShopRegistrationComponent } from './components/shop-registration/shop-registration.component';
import { UserRegistrationComponent } from './components/user-registration/user-registration.component';
import { DeauthGuard } from 'src/app/core/guards/deauth.guard';

const routes: Routes = [
  {path: '', redirectTo: 'login', pathMatch: 'full'},
  {path: 'forgot-password', component: ForgotPasswordComponent },
  {path: 'login', component: LoginComponent, canActivate: [DeauthGuard]},
  {path: 'registration/shop', component: ShopRegistrationComponent, canActivate: [DeauthGuard]},
  {path: 'registration/user', component: UserRegistrationComponent, canActivate: [DeauthGuard]},
  {path: 'reset-password', component: ResetPasswordComponent },
  {path: 'verify-email', component: EmailVerifyComponent, canActivate: [DeauthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }