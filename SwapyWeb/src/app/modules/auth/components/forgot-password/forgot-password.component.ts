import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpStatusCode } from 'axios';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { AuthFacadeService } from '../../services/auth-facade.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {
  private inputFields = [
    { name: 'email', elementId: 'emailGroup' },
  ];

  emailNotFoundError: boolean = false;
  forgotPasswordSuccess: boolean = false;
  unconfirmedEmailError: boolean = false;

  forgotPasswordForm: FormGroup;
  get email() { return this.forgotPasswordForm.get('email'); }
  
  constructor(private authFacade: AuthFacadeService, private router: Router, private spinnerService: SpinnerService) {
    this.forgotPasswordForm = new FormGroup({
      email: new FormControl(null, [Validators.required, Validators.pattern('^[\\w\\-\\.]+@[\\w-]+\\.[a-z]{2,4}$')]),
    });
  }
  
  async onSubmit() : Promise<void> {
    try {
      if(this.forgotPasswordForm.valid) {
        this.emailNotFoundError = this.unconfirmedEmailError = false;
        this.spinnerService.changeSpinnerState(true);
        await this.authFacade.forgotPassword(this.forgotPasswordForm.value);
        this.spinnerService.changeSpinnerState(false);
        this.forgotPasswordSuccess = true;
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      if(error.response.status === HttpStatusCode.NotFound) this.emailNotFoundError = true;
      else if(error.response.status === HttpStatusCode.Unauthorized) this.unconfirmedEmailError = true;
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

    if (this.forgotPasswordForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }

  backToLogin(): void { this.router.navigate(["/auth/login"]); }
}
