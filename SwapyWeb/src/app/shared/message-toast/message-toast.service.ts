import { Injectable } from '@angular/core';
import { MessageToastComponent } from './message-toast.component';
import { ChatMessageModel } from 'src/app/modules/chats/models/chat-message.model';

@Injectable({
  providedIn: 'root'
})
export class MessageToastService {
  private messageToastComponent: MessageToastComponent | null = null;

  setMessageToastComponent(component: MessageToastComponent) {
    this.messageToastComponent = component;
  }

  setMessage(newMessage: ChatMessageModel) {
    if (this.messageToastComponent) {
      this.messageToastComponent.setMessage(newMessage);
    }
  }
}