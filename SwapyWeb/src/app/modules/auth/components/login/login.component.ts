import { FormControl, FormGroup, Validators} from '@angular/forms';
import { AuthFacadeService } from '../../services/auth-facade.service';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpStatusCode } from '@angular/common/http';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { ChatHubService } from 'src/app/core/services/chat-hub.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  private inputFields = [
    { name: 'emailOrPhone', elementId: 'emailOrPhoneGroup' },
    { name: 'password', elementId: 'passwordGroup' }
  ];

  loginError: boolean = false;
  showPassword: boolean = false;
  emailNotConfirmed : boolean = false;

  loginForm: FormGroup;
  get emailOrPhone() { return this.loginForm.get('emailOrPhone'); }
  get password() { return this.loginForm.get('password'); }
  get rememberMe() { return this.loginForm.get('rememberMe'); }
  
  constructor(private chatHub: ChatHubService, private authFacade: AuthFacadeService, private router: Router, private spinnerService: SpinnerService) {
    this.loginForm = new FormGroup({
      emailOrPhone: new FormControl(null, [Validators.required, Validators.pattern('^([\\w\\-\\.]+@[\\w-]+\\.[a-z]{2,4})|(\\+\\d{1,3}\\d{1,3}\\d{7})$')]),
      password: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')]),
      rememberMe: new FormControl(false)
    });
  }
  
  async onSubmit() : Promise<void> {
    try {
      if(this.loginForm.valid) {
        this.loginError = this.emailNotConfirmed = false;
        this.spinnerService.changeSpinnerState(true);
        this.authFacade.setRememberMe(this.rememberMe?.value);
        await this.authFacade.login(this.loginForm.value);
        this.router.navigate(["/"]);
        
        if(!this.chatHub.isConnected()) { 
          this.chatHub.configureHubConnection();
          this.chatHub.receiveMessages();
        }

        this.spinnerService.changeSpinnerState(false);
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      if(error.response.status === HttpStatusCode.NotFound) this.loginError = true;
      else if(error.response.status === HttpStatusCode.Conflict) this.emailNotConfirmed = true;
    }
  }

  async onInputBlur(fieldName: string) : Promise<void> {
    const field = this.inputFields.find(f => f.name === fieldName);
    if (field) {
        this.changeValidity(field.name, field.elementId);
    }
  }

  changeValidity(fieldName: string, elementId: string) : void {
    const element = $(`#${elementId}`);

    if (this.loginForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }

  onShowPassword() {
    if(this.showPassword) {
      $("#eye-icon").removeClass("fa-eye-slash")
      $("#eye-icon").addClass("fa-eye")
    } 
    else {
      $("#eye-icon").removeClass("fa-eye")
      $("#eye-icon").addClass("fa-eye-slash")
    }

    this.showPassword = !this.showPassword;
  }
}
