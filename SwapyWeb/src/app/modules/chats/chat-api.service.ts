import { Injectable } from '@angular/core';
import { AxiosError, AxiosResponse } from 'axios';
import { Observable, catchError, from, map } from 'rxjs';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';
import { environment } from 'src/environments/environment';
import { ChatListResponseDTO } from './models/chat-list-response-dto';
import { DetailChatResponseDTO } from './models/detail-chat-response-dto';
import { ChatType } from './enums/chat-type.enum';

@Injectable({
  providedIn: 'root'
})
export class ChatApiService {

  private readonly apiUrl: string = environment.apiUrl;
  private readonly chatsApiUrl: string = environment.chatsApiUrl;

  constructor(private axiosInterceptorService: AxiosInterceptorService) {}

  getBuyersChats(): Observable<ChatListResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.chatsApiUrl}/Chats/Buyers`)).pipe(
      map((response: AxiosResponse<any>) => {
        let chatListResponseDTO: ChatListResponseDTO = response.data;
        chatListResponseDTO.items = response.data.items.$values;
        chatListResponseDTO.items.forEach(i => { i.logo = environment.blobUrl + '/product-images/' + i.image; });
        return chatListResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getSellersChats(): Observable<ChatListResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.chatsApiUrl}/Chats/Sellers`)).pipe(
      map((response: AxiosResponse<any>) => {
        let chatListResponseDTO: ChatListResponseDTO = response.data;
        chatListResponseDTO.items = response.data.items.$values;
        chatListResponseDTO.items.forEach(i => i.logo = `${environment.blobUrl}/logos/${i.logo}`);
        return chatListResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getDetailChat(chatId: string): Observable<DetailChatResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.chatsApiUrl}/Chats/${chatId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        let detailChatResponseDTO: DetailChatResponseDTO = response.data;
        detailChatResponseDTO.messages = response.data.messages.$values;
        detailChatResponseDTO.messages.forEach(m => m.image = m.image ? `${environment.blobUrl}/messages/${m.image}` : null);
        detailChatResponseDTO.image = environment.blobUrl + (detailChatResponseDTO.type == ChatType.Buyer ? '/product-images/' : '/logos/') + detailChatResponseDTO.image;
        return detailChatResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getDetailChatByProductId(productId: string): Observable<DetailChatResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.chatsApiUrl}/ChatByProductId/${productId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        let detailChatResponseDTO: DetailChatResponseDTO = response.data;
        detailChatResponseDTO.messages = response.data.messages.$values;
        detailChatResponseDTO.messages.forEach(m => m.image = m.image ? `${environment.blobUrl}/messages/${m.image}` : null)
        detailChatResponseDTO.image = environment.blobUrl + '/product-images/' + detailChatResponseDTO.image;
        return detailChatResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getTemporaryChat(productId: string): Observable<DetailChatResponseDTO>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.chatsApiUrl}/TemporaryChat/${productId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        let detailChatResponseDTO: DetailChatResponseDTO = response.data;
        detailChatResponseDTO.image = environment.blobUrl + '/product-images/' + detailChatResponseDTO.image
        return detailChatResponseDTO;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  SendMessageAsync(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.chatsApiUrl}/Messages`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  CreateChatAsync(data: FormData): Observable<string>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.chatsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  readChatAsync(chatId: string): Observable<boolean> {
    return from(this.axiosInterceptorService.patch(`${this.apiUrl}/${this.chatsApiUrl}?ChatId=${chatId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
}


