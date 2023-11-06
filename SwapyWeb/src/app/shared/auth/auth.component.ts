import { Router } from '@angular/router';
import { AfterContentInit, AfterViewInit, Component, OnInit, Renderer2 } from '@angular/core';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { CategoryTreeService } from 'src/app/shared/category-tree/category-tree.service';
import { HeaderService } from '../header/header.service';
import { UserType } from 'src/app/core/enums/user-type.enum';
import { AuthService } from './auth.service';
import { ChatHubService } from 'src/app/core/services/chat-hub.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit, AfterViewInit{
  hasUnreadMessages: boolean = false;

  constructor(private chatHub: ChatHubService, public authFacade: AuthFacadeService, public router: Router, private renderer: Renderer2, private headerService: HeaderService, private categoryTreeService: CategoryTreeService, private authService: AuthService) 
  {
    this.authService.setAuthComponent(this);
  } 

  ngOnInit(): void {
    this.hasUnreadMessages = this.authFacade.getUnreadMesageState();
  }

  ngAfterViewInit(): void {
    if(this.hasUnreadMessages) $('.new-message-icon').removeClass('collapse');
    else $('.new-message-icon').addClass('collapse');
  }

  async onLogout(): Promise<void> {
    await this.authFacade.logout();
    this.chatHub.disconnect();
    window.location.replace("/");
  }

  toggleCategoryMenuAnimation(){
    this.headerService.disableCollapsedMenu();
    this.categoryTreeService.toggleAnimation();
  }

  transferToProfile(): void {
    this.router.navigate([this.authFacade.isAuthenticated() ? ('/' + (this.authFacade.getUserType() === UserType.Shop ? 'shops' : 'users') + '/' + this.authFacade.getUserId()) : '/auth/login']);
  }

  transferToFavorites(): void{
    this.router.navigate(['/products/favorites']);
  }
  
  transferToAddProduct(): void{
    this.router.navigate(['/products/add']);
  }

  transferToLogin(): void{
    this.router.navigate(['/auth/login']);
  }

  transferToChats(): void{
    this.router.navigate(['/chats']);
  }

  changeMessagesState(value: boolean) {
    this.hasUnreadMessages = value;

    if(this.hasUnreadMessages) $('.new-message-icon').removeClass('collapse');
    else $('.new-message-icon').addClass('collapse');
   
    this.authFacade.setUnreadMesageState(value.toString());
  }
}
