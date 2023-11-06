import { Component, OnInit } from '@angular/core';
import { ChatMessageModel } from 'src/app/modules/chats/models/chat-message.model';
import { MessageToastService } from './message-toast.service';

@Component({
  selector: 'app-message-toast',
  templateUrl: './message-toast.component.html',
  styleUrls: ['./message-toast.component.scss']
})
export class MessageToastComponent {
  incomeMessage: ChatMessageModel = {
    chatId: "",
    recepientId: "",
    senderId: "",
    senderName: "",
    message: "",
    image: "",
    dateTime: new Date()
  };

  constructor(private messageToast: MessageToastService) {
    messageToast.setMessageToastComponent(this);
  }

  showToast(): void { 
    const toastElement = document.getElementById('message-toast');
      const toast = new (window as any).bootstrap.Toast(toastElement as HTMLElement);
      toast.show();
      
      setTimeout(() => {
        toast.hide();
      }, 3000);
  }

  setMessage(newMessage: ChatMessageModel): void {
    console.log(newMessage);
    this.incomeMessage = newMessage;
    if((newMessage.message === null || newMessage.message === "") && newMessage.image != null) this.incomeMessage.message = "📎 Photo";
    this.showToast();
  }
}
