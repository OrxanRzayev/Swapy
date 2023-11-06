import { Component, OnInit } from '@angular/core';
import { UserType } from 'src/app/core/enums/user-type.enum';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  userType: boolean;
  currentPage: number = 0;

  constructor(private authFacade: AuthFacadeService) {
    this.userType = this.authFacade.getUserType() == UserType.Shop ? true : false;
  }
  
  onMenuClick(value: number): void {
    this.currentPage = value;

    const buttons = document.getElementsByClassName('settings-button');
  
    const activeButton = document.querySelector('.settings-button.active-button');
    if (activeButton) {
      activeButton.classList.remove('active-button');
      activeButton.removeAttribute('disabled');
    }
  
    buttons[value].classList.add('active-button');
    buttons[value].setAttribute('disabled', 'true');
  }
}
