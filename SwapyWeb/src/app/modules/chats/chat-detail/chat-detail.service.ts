import { Injectable } from '@angular/core';
import { ChatDetailComponent } from './chat-detail.component';
import { ChatMessageModel } from '../models/chat-message.model';

@Injectable({
  providedIn: 'root'
})
export class ChatDetailService {
  private chatDetailComponent: ChatDetailComponent | null = null;

  setChatDetailComponent(component: ChatDetailComponent) {
    this.chatDetailComponent = component;
  }

  changeSelectedChat(chatId: string) {
    if (this.chatDetailComponent) {
        this.chatDetailComponent.changeSelectedChat(chatId);
    }
  }

  receiveMessage(message: ChatMessageModel): void {
    if (this.chatDetailComponent) {
      this.chatDetailComponent.receiveMessage(message);
    }
  }
}