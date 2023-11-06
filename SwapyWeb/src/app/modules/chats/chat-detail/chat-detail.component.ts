import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ChatListService } from '../chat-list/chat-list.service';
import { DetailChatResponseDTO } from '../models/detail-chat-response-dto';
import { ChatApiService } from '../chat-api.service';
import { ChatDetailService } from './chat-detail.service';
import { AuthFacadeService } from '../../auth/services/auth-facade.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpStatusCode } from 'axios';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';  
import { ChatHubService } from 'src/app/core/services/chat-hub.service';
import { ChatMessageModel } from '../models/chat-message.model';
import { MessageResponseDTO } from '../models/message-response-dto';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-chat-detail',
  templateUrl: './chat-detail.component.html',
  styleUrls: ['./chat-detail.component.scss']
})
export class ChatDetailComponent implements OnInit {
  myId!: string;
  selectedChat: DetailChatResponseDTO | null = null;
  inputTextToSend: string = '';
  productId: string | null = null;
  fileInputValue: File | undefined = undefined;
  selectedFileToSend: File | undefined = undefined;
  @ViewChild('imageElement') previewImageElement!: ElementRef;

  constructor(private route: ActivatedRoute, private chatDetailService: ChatDetailService, private chatListService: ChatListService, private chatApiService: ChatApiService, private authFacadeService: AuthFacadeService, private spinnerService: SpinnerService, private router: Router, private chatHub: ChatHubService) {
    chatDetailService.setChatDetailComponent(this);
    let userId = authFacadeService.getUserId()
    if(userId === null){ this.router.navigateByUrl('/404', { skipLocationChange: true }); return; }
    this.myId = userId;
    
  }
  
  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.productId = params['productId'] ? params['productId'] : null;
    });
    if(this.productId !== null) {
      this.chatApiService.getDetailChatByProductId(this.productId).subscribe(
        (response: DetailChatResponseDTO) => {
          this.chatListService.changeSelectedChat(response.chatId);
          this.selectedChat = response;
        },
        (error) => {
          if(error.response.status === HttpStatusCode.NotFound){
            if(this.productId !== null) {
              this.chatApiService.getTemporaryChat(this.productId).subscribe(
                (responseTemporaryChat: DetailChatResponseDTO) => {
                  this.selectedChat = responseTemporaryChat; 
                },
                error => {
                  this.productId = null;
                }
              )
            }
          }
        }
      );
    }
  }

  changeSelectedChat(chatId: string): void {  
    this.chatApiService.getDetailChat(chatId).subscribe(
      (response: DetailChatResponseDTO) => { 
        this.selectedChat = response;
        this.productId = null;
      }
    );
  }

  sendMessage(): void {
    if(this.productId){
      this.spinnerService.changeSpinnerState(true);
      let formData = new FormData();
      formData.append("ProductId", this.productId); 
      this.chatApiService.CreateChatAsync(formData).subscribe(
        (response: string) => { 
          this.chatListService.changeSelectedChat(response);
          this.chatApiService.getDetailChat(response).subscribe(
            (response: DetailChatResponseDTO) => { 
              this.selectedChat = response;
              if(this.selectedChat !== null && (this.inputTextToSend.trim().length > 0 || this.selectedFileToSend !== undefined)){
                let formData = new FormData();
                formData.append("Text", this.inputTextToSend); 
                formData.append("ChatId", this.selectedChat.chatId);
                if(this.selectedFileToSend) { formData.append("image", this.selectedFileToSend); }
                this.inputTextToSend = '';
                this.selectedFileToSend = undefined;
                this.chatApiService.SendMessageAsync(formData).subscribe(
                  (response) => {
                    this.inputTextToSend = '';
                    this.selectedFileToSend = undefined;
                    this.spinnerService.changeSpinnerState(false);
                  },
                  (error) => { this.spinnerService.changeSpinnerState(false); }
                )
              }
            },
            (error) => { this.spinnerService.changeSpinnerState(false); }
          );
        },
        (error) => { this.spinnerService.changeSpinnerState(false); }
      ) 
      this.productId = null; 
    }
    else if(this.selectedChat !== null && (this.inputTextToSend.trim().length > 0 || this.selectedFileToSend !== undefined)){
      this.spinnerService.changeSpinnerState(true);
      let formData = new FormData();
      formData.append("Text", this.inputTextToSend); 
      formData.append("ChatId", this.selectedChat.chatId);
      if(this.selectedFileToSend) { formData.append("image", this.selectedFileToSend); }
      this.inputTextToSend = '';
      this.selectedFileToSend = undefined;
      this.chatApiService.SendMessageAsync(formData).subscribe(
        (response) => {
          this.inputTextToSend = '';
          this.selectedFileToSend = undefined;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => { this.spinnerService.changeSpinnerState(false); }
      )
    }
  }

  selectFile(event: any): void {
    this.selectedFileToSend = event.target.files[0];
    if (this.selectedFileToSend) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        if (this.selectedFileToSend) {
          const arrayBuffer = e.target.result;
          const blob = new Blob([arrayBuffer], { type: this.selectedFileToSend.type });
          const imageUrl = URL.createObjectURL(blob);
          this.previewImageElement.nativeElement.src = imageUrl;
        }
      };
      reader.readAsArrayBuffer(this.selectedFileToSend);
    } 
    else {
      this.previewImageElement.nativeElement.src = 'undefined';
    }
  }

  closeFile(): void {
    this.fileInputValue = undefined;
    this.selectedFileToSend = undefined;
  }

  equalsDatesWithoutTime(date1: Date, date2: Date): boolean {
    const dateObject1 = new Date(date1);
    const dateObject2 = new Date(date2);
    return dateObject1.getDate() == dateObject2.getDate()
  }

  backToList(): void {
    this.chatListService.toggleAnimation();
  }

  receiveMessage(data: ChatMessageModel): void {
    if(this.selectedChat?.chatId === data.chatId) {
      var newMessage: MessageResponseDTO = {
        id: "",
        text: data.message,
        image: data.image == null ? null : `${environment.blobUrl}/messages/${data.image}`,
        chatId: data.chatId,
        senderId: data.senderId,
        senderLogo: "",
        dateTime: data.dateTime 
      }
      
      this.selectedChat?.messages.push(newMessage);
    }
  }
}
