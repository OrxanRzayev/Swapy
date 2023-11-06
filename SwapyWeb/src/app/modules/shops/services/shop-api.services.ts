import { Injectable } from '@angular/core';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { EMPTY, Observable, catchError, from, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ShopDetail } from '../models/shop-detail.interface';

@Injectable({
  providedIn: 'root'
})
export class ShopApiService {
  private readonly apiUrl : string = environment.apiUrl;
  private readonly shopsApiUrl : string = environment.shopsApiUrl;

  getShopById(id: string): Observable<ShopDetail> {
    return from(axios.get(`${this.apiUrl}/${this.shopsApiUrl}/${id}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const shopDetail: ShopDetail = response.data;
        return shopDetail;
      }),
      catchError((error: AxiosError) => {
        throw error; 
      })
    );
  }
}