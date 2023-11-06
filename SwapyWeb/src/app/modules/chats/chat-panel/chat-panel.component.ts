import { Component, OnInit } from '@angular/core';
import { ChatHubService } from 'src/app/core/services/chat-hub.service';

@Component({
  selector: 'app-chat-panel',
  templateUrl: './chat-panel.component.html',
  styleUrls: ['./chat-panel.component.scss']
})
export class ChatPanelComponent implements OnInit {

  constructor(private chatHub: ChatHubService) { }

  ngOnInit(): void {
  }
}
