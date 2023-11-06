import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { ChatMessageModel } from 'src/app/modules/chats/models/chat-message.model';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { ChatDetailService } from 'src/app/modules/chats/chat-detail/chat-detail.service';
import { ChatListService } from 'src/app/modules/chats/chat-list/chat-list.service';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/shared/auth/auth.service';
import { MessageToastService } from 'src/app/shared/message-toast/message-toast.service';

@Injectable({
  providedIn: 'root'
})
export class ChatHubService {
  private userId!: string;
  private hubConnection!: signalR.HubConnection;

  constructor(private authFacade: AuthFacadeService, private chatDetail: ChatDetailService, private chatList: ChatListService, private authService: AuthService, private messageToast: MessageToastService) {
    if(!this.isConnected()) {
      var temp =  this.authFacade.getUserId();
      this.userId = temp == null ? "" : temp ;

      if(this.userId !== "") {
          this.hubConnection = new signalR.HubConnectionBuilder()
          .configureLogging(signalR.LogLevel.Debug)
          .withUrl(`${environment.apiUrl}/chatHub`, {
              skipNegotiation: true,
              transport: signalR.HttpTransportType.WebSockets,
              accessTokenFactory: () => this.userId
          })
          .build();
      }
    }
  }

  configureHubConnection() {
    var temp =  this.authFacade.getUserId();
    this.userId = temp == null ? "" : temp ;

    if(this.userId !== "") {
        this.hubConnection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Debug)
        .withUrl(`${environment.apiUrl}/chatHub`, {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
            accessTokenFactory: () => this.userId
        })
        .build();
    }

    this.startConnection();
  }

  startConnection() : boolean {
    if(!this.isConnected() && this.userId !== "") {
      this.hubConnection.start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
      return true;
    }
    else return false;
  }

  sendMessage(model: string) {
    this.hubConnection.invoke('SendMessage', model)
                      .catch(err => console.error(err));
  }

  receiveMessages() {
    this.hubConnection.on('ReceiveMessage', (data: ChatMessageModel) => {
      if(window.location.href.includes('chats')) {
        this.chatDetail.receiveMessage(data);
        this.chatList.receiveMessage(data);
      }
      else {
        this.messageToast.setMessage(data);
        this.authService.changeMessagesState(true);
      }
    });
  }

  isConnected(): boolean {
    return this.hubConnection?.state === signalR.HubConnectionState.Connected;
  }

  disconnect() {
    if (this.isConnected()) {
      this.hubConnection.stop()
                        .then(() => console.log('Connection stopped'))
                        .catch(err => console.error('Error while stopping connection: ' + err));
    }
  }
}