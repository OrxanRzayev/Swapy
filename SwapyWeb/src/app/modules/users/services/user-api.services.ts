import { Injectable } from '@angular/core';
import { Observable, catchError, from, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserDetail } from '../models/user-detail.interface';
import { AxiosError, AxiosResponse } from 'axios';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';


@Injectable({
  providedIn: 'root'
})
export class UserApiService {
  private readonly apiUrl : string = environment.apiUrl;
  private readonly usersApiUrl : string = environment.usersApiUrl;

  constructor(private axiosInterceptor: AxiosInterceptorService) {}

  getUserById(id: string): Observable<UserDetail> {
    return from(this.axiosInterceptor.get(`${this.apiUrl}/${this.usersApiUrl}/${id}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const userDetail: UserDetail = response.data;
        return userDetail;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  checkLike(userId: string): Observable<boolean> {
    return from(this.axiosInterceptor.get(`${this.apiUrl}/${this.usersApiUrl}/Likes/Check/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const isLike: boolean = response.data;
        return isLike;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  checkSubscription(userId: string): Observable<boolean> {
    return from(this.axiosInterceptor.get(`${this.apiUrl}/${this.usersApiUrl}/Subscriptions/Check/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const isSubscription: boolean = response.data;
        return isSubscription;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  addLike(userId: string): Observable<void> {
    return from(this.axiosInterceptor.post(`${this.apiUrl}/${this.usersApiUrl}/Likes/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  addSubscription(userId: string): Observable<void> {
    return from(this.axiosInterceptor.post(`${this.apiUrl}/${this.usersApiUrl}/Subscriptions/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  removeLike(userId: string): Observable<void> {
    return from(this.axiosInterceptor.delete(`${this.apiUrl}/${this.usersApiUrl}/Likes/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

  removeSubscription(userId: string): Observable<void> {
    return from(this.axiosInterceptor.delete(`${this.apiUrl}/${this.usersApiUrl}/Subscriptions/${userId}`)).pipe(
      map((response: AxiosResponse<any>) => {}),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }

}