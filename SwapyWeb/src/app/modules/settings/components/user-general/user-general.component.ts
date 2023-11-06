import { FormControl, FormGroup, Validators} from '@angular/forms';
import { Component, OnInit} from '@angular/core';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { SettingsApiService } from '../../services/settings-api.service';
import { UserData } from '../../models/user-data';

@Component({
  selector: 'app-user-general',
  templateUrl: './user-general.component.html',
  styleUrls: ['./user-general.component.scss']
})
export class UserGeneralComponent implements OnInit{
  private inputFields = [
    { name: 'firstName', elementId: 'firstNameGroup' },
    { name: 'lastName', elementId: 'lastNameGroup' },
    { name: 'phoneNumber', elementId: 'phoneNumberGroup' },
  ];
  
  prevData: UserData = {
    firstName: '',
    lastName: '',
    phoneNumber: '',
    logo: '',
    isSubscribed: false,
  };

  sendData: UserData = {
    firstName: '',
    lastName: '',
    phoneNumber: '',
    logo: '',
    isSubscribed: false,
  };

  validPhoneNumber: boolean = false;

  userGeneralForm: FormGroup;
  get firstName() { return this.userGeneralForm.get('firstName'); }
  get lastName() { return this.userGeneralForm.get('lastName'); }
  get phoneNumber() { return this.userGeneralForm.get('phoneNumber'); }
  get isSubscribed() { return this.userGeneralForm.get('isSubscribed'); }
  
  constructor(private authFacade: AuthFacadeService, private settingsApi: SettingsApiService, private spinnerService: SpinnerService) {
    this.userGeneralForm = new FormGroup({
      firstName: new FormControl(null, [Validators.required, Validators.pattern(`^[A-ZА-ЯƏÜÖĞİŞÇ][A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç\s']{2,31}$`)]),
      lastName: new FormControl(null, [Validators.required, Validators.pattern(`^[A-ZА-ЯƏÜÖĞİŞÇ][A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç\s']{2,31}$`)]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern('^\\+\\d{1,3}\\d{1,3}\\d{7}$')]),
      isSubscribed: new FormControl(null),
    });
  }

  async ngOnInit(): Promise<void> {
    this.spinnerService.changeSpinnerState(true);
    this.settingsApi.getUserData().subscribe((result: UserData) => {
      this.userGeneralForm.setValue({
        firstName: result.firstName,
        lastName: result.lastName,
        phoneNumber: result.phoneNumber,
        isSubscribed: result.isSubscribed
      });

      this.prevData = result;

      this.spinnerService.changeSpinnerState(false);

    });
  }
  
  async onSubmit() : Promise<void> {
    try {
      if(this.userGeneralForm.valid) {
        if(!this.equalData()) {
          this.changeValidityToDefault();
          this.spinnerService.changeSpinnerState(true);
          await this.settingsApi.updateUserData(this.sendData).toPromise();
          this.spinnerService.changeSpinnerState(false);
        } 
      }
    } catch(error : any) {
      this.spinnerService.changeSpinnerState(false);
      console.log(error);
    }
  }

  async onInputBlur(fieldName: string) : Promise<void> {
    const field = this.inputFields.find(f => f.name === fieldName);
    if (field) {
      if (field.name === 'phoneNumber') {
        if(fieldName === 'phoneNumber' && this.userGeneralForm.get(fieldName)?.valid) {
          if(this.phoneNumber?.value !== this.prevData.phoneNumber) {
            this.validPhoneNumber = await this.authFacade.checkPhoneNumberAvailability(this.phoneNumber?.value);

            if(this.validPhoneNumber) {
              this.phoneNumber?.setErrors({ 'phoneNumberNotAvailable': true });
              this.userGeneralForm.setErrors({ 'invalidData': true });
            }
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else this.changeValidity(field.name, field.elementId);
    }
  }

  changeValidity(fieldName: string, elementId: string) : void {
    const element = $(`#${elementId}`);

    if (this.userGeneralForm.get(fieldName)?.valid) {
      element.removeClass('custom-invalid-card-group');
      element.addClass('custom-valid-card-group');
    } else {
      element.removeClass('custom-valid-card-group');
      element.addClass('custom-invalid-card-group');
    }
  }

  changeValidityToDefault() {
    this.inputFields.forEach(item => {
      const element = $(`#${item.elementId}`);

      if (this.userGeneralForm.get(item.name)?.valid) element.removeClass('custom-valid-card-group');
      else  element.removeClass('custom-invalid-card-group');

    });

    this.sendData.firstName = this.firstName?.value
    this.sendData.lastName = this.lastName?.value
    this.sendData.phoneNumber = this.phoneNumber?.value;
    this.sendData.logo = this.prevData.logo;
    this.sendData.isSubscribed = this.isSubscribed?.value;

    this.prevData = this.sendData;
  }

  equalData(): boolean {
    return (
        this.prevData.firstName === this.firstName?.value &&
        this.prevData.lastName === this.lastName?.value &&
        this.prevData.phoneNumber === this.phoneNumber?.value &&
        this.prevData.isSubscribed === this.isSubscribed?.value
    );
  }
}
