import { FormControl, FormGroup, Validators} from '@angular/forms';
import { AuthFacadeService } from '../../services/auth-facade.service';
import { Component} from '@angular/core';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';

@Component({
  selector: 'app-shop-registration',
  templateUrl: './shop-registration.component.html',
  styleUrls: ['./shop-registration.component.scss']
})
export class ShopRegistrationComponent {
  private inputFields = [
    { name: 'shopName', elementId: 'shopNameGroup' },
    { name: 'email', elementId: 'emailGroup' },
    { name: 'phoneNumber', elementId: 'phoneNumberGroup' },
    { name: 'password', elementId: 'passwordGroup' }
  ];
  
  validEmail: boolean = false;
  showPassword: boolean = false;
  validShopName: boolean = false;
  validPhoneNumber: boolean = false;
  shopRegistratiomSuccess: boolean = false;

  registrationForm: FormGroup;
  get shopName() { return this.registrationForm.get('shopName'); }
  get email() { return this.registrationForm.get('email'); }
  get phoneNumber() { return this.registrationForm.get('phoneNumber'); }
  get password() { return this.registrationForm.get('password'); }
  get rememberMe() { return this.registrationForm.get('rememberMe'); }
  
  constructor(private authFacade: AuthFacadeService, private spinnerService: SpinnerService) {
    this.registrationForm = new FormGroup({
      shopName: new FormControl(null, [Validators.required, Validators.pattern(`^([A-ZА-ЯƏÜÖĞİŞÇ]|[0-9])[A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç0-9\\s']{2,31}$`)]),
      email: new FormControl(null, [Validators.required, Validators.pattern('^[\\w\\-\\.]+@[\\w-]+\\.[a-z]{2,4}$')]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern('^\\+\\d{1,3}\\d{1,3}\\d{7}$')]),
      password: new FormControl(null, [Validators.required, Validators.pattern('^(?=.*\\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\\w\\d\\s:])([^\\s]){8,32}$')]),
      rememberMe: new FormControl(false)
    });
  }
  
  async onSubmit() : Promise<void> {
    try{
      if(this.registrationForm.valid) {
        this.spinnerService.changeSpinnerState(true);
        await this.authFacade.shopRegistration(this.registrationForm.value);
        this.spinnerService.changeSpinnerState(false);
        this.shopRegistratiomSuccess = true;
      }
    } catch (error : any) {
      this.spinnerService.changeSpinnerState(false);
      console.log(error);
    }
  }

  async onInputBlur(fieldName: string) : Promise<void> {
    const field = this.inputFields.find(f => f.name === fieldName);
    if (field) {
      if(field.name === 'email') {
        if(this.registrationForm.get(fieldName)?.valid) {
          this.validEmail = await this.authFacade.checkEmailAvailability(this.email?.value);
          
          if(this.validEmail) {
            this.email?.setErrors({ 'emailNotAvailable': true });
            this.registrationForm.setErrors({ 'invalidData': true });
          }
        }
        
        this.changeValidity(field.name, field.elementId);

      } else if (field.name === 'phoneNumber') {
        if(fieldName === 'phoneNumber' && this.registrationForm.get(fieldName)?.valid) {
          this.validPhoneNumber = await this.authFacade.checkPhoneNumberAvailability(this.phoneNumber?.value);

          if(this.validPhoneNumber) {
            this.phoneNumber?.setErrors({ 'phoneNumberNotAvailable': true });
            this.registrationForm.setErrors({ 'invalidData': true });
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else if (field.name === 'shopName') {
        if(fieldName === 'shopName' && this.registrationForm.get(fieldName)?.valid) {
          this.validShopName = await this.authFacade.checkShopNameAvailability(this.shopName?.value);

          if(this.validShopName) {
            this.shopName?.setErrors({ 'shopNameNotAvailable': true });
            this.registrationForm.setErrors({ 'invalidData': true });
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else this.changeValidity(field.name, field.elementId);
    }
  }

  changeValidity(fieldName: string, elementId: string) : void {
    const element = $(`#${elementId}`);

    if (this.registrationForm.get(fieldName)?.valid) {
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
