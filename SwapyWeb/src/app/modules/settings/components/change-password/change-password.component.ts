import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpStatusCode } from 'axios';
import { ResetPasswordCredential } from 'src/app/modules/auth/models/auth-credentials';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { SettingsApiService } from '../../services/settings-api.service';
import { PasswordData } from '../../models/password-data';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent {
  private inputFields = [
    { name: 'oldPassword', elementId: 'oldPasswordGroup' },
    { name: 'newPassword', elementId: 'newPasswordGroup' },
    { name: 'confirmPassword', elementId: 'confirmPasswordGroup' },
  ];

  showOldPassword: boolean = false;
  showNewPassword: boolean = false;
  showConfirmPassword: boolean = false;
  oldPasswordIncorrect: boolean = false;
  oldAndNewPasswordEquals: boolean = false;
  forgotPasswordSuccessful : boolean = false;
  newAndConfirmPasswordNotEquals: boolean = false;
  regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;

  changePasswordForm: FormGroup;
  get oldPassword() { return this.changePasswordForm.get('oldPassword'); }
  get newPassword() { return this.changePasswordForm.get('newPassword'); }
  get confirmPassword() { return this.changePasswordForm.get('confirmPassword'); }
  
  constructor(private authFacade: AuthFacadeService, private router: Router, private route: ActivatedRoute, private spinnerService: SpinnerService, private settingsApi: SettingsApiService) {
    this.changePasswordForm = new FormGroup({
      oldPassword: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')]),
      newPassword: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')]),
      confirmPassword: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')])
    });
  }

  async onSubmit() : Promise<void> {
    try {
      if(this.changePasswordForm.valid) {
        if(this.oldPassword?.value === this.newPassword?.value || this.oldPassword?.value === this.confirmPassword?.value) {
          this.oldAndNewPasswordEquals = true;
          this.changePasswordForm.setErrors({ 'invalidData': true });
        } else if(this.newPassword?.value !== this.confirmPassword?.value) {
          this.newAndConfirmPasswordNotEquals = true;
          this.changePasswordForm.setErrors({ 'invalidData': true });
        } else {
          this.oldAndNewPasswordEquals = this.newAndConfirmPasswordNotEquals = this.oldPasswordIncorrect = false;
          this.spinnerService.changeSpinnerState(true);

          const data : PasswordData = {
            oldPassword: this.oldPassword?.value,
            newPassword: this.newPassword?.value,
          };

          await this.settingsApi.changePassword(data).toPromise();
          this.spinnerService.changeSpinnerState(false);
          
          this.changePasswordForm.reset();
        }
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      if(error.response.status === HttpStatusCode.BadRequest) this.oldPasswordIncorrect = true;
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

    if (this.changePasswordForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }

  onShowPassword(id: string) {
    if(id === "oldPasswordInput") {
      if(this.showOldPassword) {
          $("#old-eye-icon").removeClass("fa-eye-slash")
          $("#old-eye-icon").addClass("fa-eye")
        } 
        else {
          $("#old-eye-icon").removeClass("fa-eye")
          $("#old-eye-icon").addClass("fa-eye-slash")
        }
        this.showOldPassword = !this.showOldPassword;
    }
    else if(id === "newPasswordInput") {
      if(this.showNewPassword) {
          $("#new-eye-icon").removeClass("fa-eye-slash")
          $("#new-eye-icon").addClass("fa-eye")
        } 
        else {
          $("#new-eye-icon").removeClass("fa-eye")
          $("#new-eye-icon").addClass("fa-eye-slash")
        }
        this.showNewPassword = !this.showNewPassword;
    } else {
      if(this.showConfirmPassword) {
        $("#confirm-eye-icon").removeClass("fa-eye-slash")
        $("#confirm-eye-icon").addClass("fa-eye")
      } 
      else {
        $("#confirm-eye-icon").removeClass("fa-eye")
        $("#confirm-eye-icon").addClass("fa-eye-slash")
      }
      this.showConfirmPassword = !this.showConfirmPassword;
    }
  }
}