import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatDetailComponent } from './chat-detail/chat-detail.component';
import { ChatListComponent } from './chat-list/chat-list.component';
import { ChatPanelComponent } from './chat-panel/chat-panel.component';
import { CustomDatePipeEn } from 'src/app/core/pipes/custom-date-en.pipe';
import { SharedModule } from 'src/app/shared/shared.module';
import { FormsModule } from '@angular/forms';
import { NgxDropzoneModule } from 'ngx-dropzone';

@NgModule({
  declarations: [
    ChatDetailComponent,
    ChatListComponent,
    ChatPanelComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    NgxDropzoneModule
  ]
})
export class ChatsModule { }
