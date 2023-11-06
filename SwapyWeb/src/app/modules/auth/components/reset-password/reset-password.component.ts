import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpStatusCode } from 'axios';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { ResetPasswordCredential } from '../../models/auth-credentials';
import { AuthFacadeService } from '../../services/auth-facade.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  private inputFields = [
    { name: 'newPassword', elementId: 'newPasswordGroup' },
    { name: 'confirmPassword', elementId: 'confirmPasswordGroup' },
  ];

  token: string | null = null;
  userId: string | null = null;
  showNewPassword: boolean = false;
  resetPasswordProgress: number = 1;
  showConfirmPassword: boolean = false;
  unconfirmedEmailError: boolean = false;
  passwordEqualityError: boolean = false;
  regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;

  resetPasswordForm: FormGroup;
  get newPassword() { return this.resetPasswordForm.get('newPassword'); }
  get confirmPassword() { return this.resetPasswordForm.get('confirmPassword'); }
  
  constructor(private authFacade: AuthFacadeService, private router: Router, private route: ActivatedRoute, private spinnerService: SpinnerService) {
    this.resetPasswordForm = new FormGroup({
      newPassword: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')]),
      confirmPassword: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')])
    });
  }
  
  async ngOnInit(): Promise<void> {
    this.userId = this.route.snapshot.queryParamMap.get('userid');
    this.token = this.route.snapshot.queryParamMap.get('token');

    if (!this.token) {
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return;
    }
    
    if(!this.userId || !this.regex.test(this.userId)) {
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return;
    }
  }

  async onSubmit() : Promise<void> {
    try {
      if(this.resetPasswordForm.valid) {
        if(this.newPassword?.value !== this.confirmPassword?.value) {
          this.passwordEqualityError = true;
          this.resetPasswordForm.setErrors({ 'invalidData': true });
        } else {
          this.passwordEqualityError = this.unconfirmedEmailError = false;

          if(this.userId && this.token) { 
            this.spinnerService.changeSpinnerState(true);
            const credential: ResetPasswordCredential = {
              userId: this.userId,
              token: this.token,
              password: this.newPassword?.value
            };

            await this.authFacade.resetPassword(credential);
            this.spinnerService.changeSpinnerState(false);
            this.resetPasswordProgress = 2;
          }
        }
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      if(error.response.status === HttpStatusCode.NotFound) this.router.navigateByUrl('/404', { skipLocationChange: true });
      else if(error.response.status === HttpStatusCode.BadRequest) this.resetPasswordProgress = 3;
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

    if (this.resetPasswordForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }

  onShowPassword(id: string) {
    if(id === "newPasswordInput") {
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

  backToLogin(): void { this.router.navigate(["/auth/login"]); }
}
