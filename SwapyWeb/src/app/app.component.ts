import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ChatHubService } from './core/services/chat-hub.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title: string = 'SwapyWeb';
  IsShowHeader: boolean = true;
  IsShowFooter: boolean = true;

  constructor(private router: Router, private chatHub: ChatHubService) {
    this.router.events.subscribe(event => this.onShowUI(event));
  }

  ngOnInit(): void {
    if(this.chatHub.startConnection()) {
      this.chatHub.receiveMessages();
    }
  }

  onShowUI(location: any): void {
    this.isShowUI(true)
    // if (location instanceof NavigationEnd) {
    //   if (location.url === '/') this.isShowUI(true);
    //   else if (location.url.includes('/chats')) this.showUIWithOutFooter();
    //   else if (location.url.includes('/products/add')) this.isShowUI(false);
    //   else if (location.url.includes('/products/edit')) this.isShowUI(false);
    //   else if (location.url.includes('/products')) this.isShowUI(true);
    //   else if (location.url.includes('/settings')) this.showUIWithOutFooter();
    //   else if (location.url.includes('/shops')) this.isShowUI(true);
    //   else if (location.url.includes('/users')) this.isShowUI(true);
    //   else this.isShowUI(false);
    // }
  }

  isShowUI(value: boolean): void { this.IsShowHeader = this.IsShowFooter = value; }

  showUIWithOutFooter(): void {
    this.IsShowHeader = true;
    this.IsShowFooter = false;
  }

  showUIWithOutHeader(): void {
    this.IsShowFooter = true;
    this.IsShowHeader = false;
  }
}