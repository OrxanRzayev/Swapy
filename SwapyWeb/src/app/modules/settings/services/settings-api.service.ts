import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AxiosResponse } from 'axios';
import { catchError, from, map, Observable, tap } from 'rxjs';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';
import { UserData } from '../models/user-data';
import { ShopData } from '../models/shop-data';
import { AuthResponse } from '../../auth/models/auth-credentials';
import { PasswordData } from '../models/password-data';

@Injectable({
  providedIn: 'root'
})
export class SettingsApiService {

  private readonly usersApiUrl: string = environment.usersApiUrl;
  private readonly shopsApiUrl: string = environment.shopsApiUrl;
  private readonly apiUrl: string = environment.apiUrl;
  private readonly authApiUrl: string = environment.authApiUrl;

  constructor(private axiosInterceptorService: AxiosInterceptorService) {}

  getUserData(): Observable<UserData> {
    return from(this.axiosInterceptorService.get<UserData>(`${this.apiUrl}/${this.usersApiUrl}/UserData`))
           .pipe(
              map((response: AxiosResponse<UserData>) => ({
                firstName: response.data.firstName,
                lastName: response.data.lastName,
                phoneNumber: response.data.phoneNumber,
                logo: response.data.logo,
                isSubscribed: response.data.isSubscribed
              })),
            catchError(error => { throw error; }));
  }
  
  getShopData(): Observable<ShopData> {
    return from(this.axiosInterceptorService.get<ShopData>(`${this.apiUrl}/${this.shopsApiUrl}/ShopData`))
           .pipe(
              map((response: AxiosResponse<ShopData>) => ({
                 shopName: response.data.shopName,
                 phoneNumber: response.data.phoneNumber,
                 description: response.data.description,
                 slogan: response.data.slogan,
                 location: response.data.location,
                 workDays : response.data.workDays,
                 startWorkTime: response.data.startWorkTime,
                 endWorkTime: response.data.endWorkTime,
                 logo: response.data.logo,
                 banner: response.data.banner
              })),
            catchError(error => { throw error; }));
  }

  updateUserData(data: UserData) : Observable<any> {
    return from(this.axiosInterceptorService.put<any>(`${this.apiUrl}/${this.usersApiUrl}`, data))
    .pipe(
      catchError(error => { throw error; })
    );
  }

  updateShopData(data: FormData) : Observable<any> {
    return from(this.axiosInterceptorService.put<any>(`${this.apiUrl}/${this.shopsApiUrl}`, data))
    .pipe(
      catchError(error => { throw error; })
    );
  }

  changePassword(data: PasswordData) : Observable<any> {
    return from(this.axiosInterceptorService.patch<any>(`${this.apiUrl}/${this.authApiUrl}/ChangePassword`, data))
    .pipe(
      catchError(error => { throw error; })
    );
  }

  updateLogo(data: FormData): Observable<any> {
    return from(this.axiosInterceptorService.post<any>(`${this.apiUrl}/${this.usersApiUrl}/UploadLogo`, data))
    .pipe(
      catchError(error => { throw error; })
    );
  }

  updateBanner(data: FormData): Observable<any> {
    return from(this.axiosInterceptorService.post<any>(`${this.apiUrl}/${this.shopsApiUrl}/UploadBanner`, data))
    .pipe(
      catchError(error => { throw error; })
    );
  }
}