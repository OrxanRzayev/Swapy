import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginCredential, UserRegistrationCredential, ShopRegistrationCredential, AuthResponse, ResetPasswordCredential, EmailVerifyCredential } from '../models/auth-credentials';
import axios, { AxiosResponse } from 'axios';
import { catchError, from, map, Observable } from 'rxjs';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {

  private readonly apiUrl: string = environment.apiUrl;
  private readonly authApiUrl: string = environment.authApiUrl;

  constructor(private axiosInterceptorService: AxiosInterceptorService) {}

  login(credential: LoginCredential): Observable<AuthResponse> {
    return from(this.axiosInterceptorService.post<AuthResponse>(`${this.apiUrl}/${this.authApiUrl}/Login`, credential))
           .pipe(
              map((response: AxiosResponse<AuthResponse>) => ({
                type: response.data.type,
                userId: response.data.userId,
                accessToken: response.data.accessToken,
                refreshToken: response.data.refreshToken,
                hasUnreadMessages: response.data.hasUnreadMessages
              })),
              catchError(error => { throw error; }));
  }

  userRegistration(credential: UserRegistrationCredential): Observable<any> {
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.authApiUrl}/Register/User`, credential))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  shopRegistration(credential: ShopRegistrationCredential): Observable<any> {
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.authApiUrl}/Register/Shop`, credential))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  checkEmailAvailability(email: string): Observable<boolean> {
    return from(this.axiosInterceptorService.get<boolean>(`${this.apiUrl}/${this.authApiUrl}/Check?Email=${encodeURIComponent(email)}`))
          .pipe(
            map((response: AxiosResponse<boolean>) => { 
              const result: boolean = response.data;
              return result; 
            }),
            catchError(error => { throw error; })
    );
  }

  checkPhoneNumberAvailability(phoneNumber: string): Observable<boolean> {
    return from(this.axiosInterceptorService.get<boolean>(`${this.apiUrl}/${this.authApiUrl}/Check?PhoneNumber=${encodeURIComponent(phoneNumber)}`))
          .pipe(
            map((response: AxiosResponse<boolean>) => { 
              const result: boolean = response.data;
              return result; 
            }),
            catchError(error => { throw error; })
    );
  }

  checkShopNameAvailability(shopName: string): Observable<boolean> {
    return from(this.axiosInterceptorService.get<boolean>(`${this.apiUrl}/${this.authApiUrl}/Check?ShopName=${encodeURIComponent(shopName)}`))
          .pipe(
            map((response: AxiosResponse<boolean>) => { 
              const result: boolean = response.data;
              return result; 
            }),
            catchError(error => { throw error; })
    );
  }

  forgotPassword(email: string) : Observable<any> {
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.authApiUrl}/ForgotPassword`, email))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  resetPassword(credential: ResetPasswordCredential) : Observable<any> {
    return from(this.axiosInterceptorService.patch(`${this.apiUrl}/${this.authApiUrl}/ResetPassword`, credential))
            .pipe(
            catchError(error => { throw error; })
            );
  }

  emailVerify(credential: EmailVerifyCredential) : Observable<any> {
    return from(this.axiosInterceptorService.patch(`${this.apiUrl}/${this.authApiUrl}/ConfirmEmail`, credential))
           .pipe(
            catchError(error => { throw error; })
           );
  }

  logout(): Observable<any> {
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.authApiUrl}/Logout`))
           .pipe(
            catchError(error => { throw error; })
           );
  }
}