import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { SettingsApiService } from '../../services/settings-api.service';
import { ShopData } from '../../models/shop-data';

@Component({
  selector: 'app-shop-general',
  templateUrl: './shop-general.component.html',
  styleUrls: ['./shop-general.component.scss']
})
export class ShopGeneralComponent implements OnInit {
  private inputFields = [
    { name: 'shopName', elementId: 'shopNameGroup' },
    { name: 'phoneNumber', elementId: 'phoneNumberGroup' },
    { name: 'description', elementId: 'descriptionGroup' },
    { name: 'tagline', elementId: 'taglineGroup' },
    { name: 'location', elementId: 'locationGroup' },
    { name: 'phoneNumber', elementId: 'phoneNumberGroup' },
  ];
  
  selectedDays = Array<boolean>(7).fill(false);
  daysOfWeek = ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'];

  prevData: ShopData = {
    shopName: '',
    phoneNumber: '',
    description: '',
    location: '',
    slogan: '',
    logo: '',
    banner: '',
    workDays: '',
    startWorkTime: null,
    endWorkTime: null
  };

  sendData: ShopData = {
    shopName: '',
    phoneNumber: '',
    description: '',
    location: '',
    slogan: '',
    logo: '',
    banner: '',
    workDays: '',
    startWorkTime: null,
    endWorkTime: null
  };

  validShopName: boolean = false;
  validPhoneNumber: boolean = false;

  shopGeneralForm: FormGroup;
  get shopName() { return this.shopGeneralForm.get('shopName'); }
  get phoneNumber() { return this.shopGeneralForm.get('phoneNumber'); }
  get description() { return this.shopGeneralForm.get('description'); }
  get tagline() { return this.shopGeneralForm.get('tagline'); }
  get location() { return this.shopGeneralForm.get('location'); }
  get startWorkTime() { return this.shopGeneralForm.get('startWorkTime'); }
  get endWorkTime() { return this.shopGeneralForm.get('endWorkTime'); }
  get mondayCheck() { return this.shopGeneralForm.get('mondayCheck'); }
  get tuesdayCheck() { return this.shopGeneralForm.get('tuesdayCheck'); }
  get wednesdayCheck() { return this.shopGeneralForm.get('wednesdayCheck'); }
  get thursdayCheck() { return this.shopGeneralForm.get('thursdayCheck'); }
  get fridayCheck() { return this.shopGeneralForm.get('fridayCheck'); }
  get saturdayCheck() { return this.shopGeneralForm.get('saturdayCheck'); }
  get sundayCheck() { return this.shopGeneralForm.get('sundayCheck'); }
  
  constructor(private authFacade: AuthFacadeService, private spinnerService: SpinnerService, private settingsApi: SettingsApiService) {
    this.shopGeneralForm = new FormGroup({
      shopName: new FormControl(null, [Validators.required, Validators.pattern(`^([A-ZА-ЯƏÜÖĞİŞÇ]|[0-9])[A-Za-zА-Яа-яƏəÜüÖöĞğİıŞşÇç0-9\\s']{2,31}$`)]),
      phoneNumber: new FormControl(null, [Validators.required, Validators.pattern('^\\+\\d{1,3}\\d{1,3}\\d{7}$')]),
      description: new FormControl(null),
      tagline: new FormControl(null),
      location: new FormControl(null),
      startWorkTime: new FormControl(null),
      endWorkTime: new FormControl(null),
      mon: new FormControl(null),
      tue: new FormControl(null),
      wed: new FormControl(null), 
      thu: new FormControl(null), 
      fri: new FormControl(null), 
      sat: new FormControl(null), 
      sun: new FormControl(null),
    });
  }
  
  async ngOnInit(): Promise<void> {
    this.spinnerService.changeSpinnerState(true);
    this.settingsApi.getShopData().subscribe((result: ShopData) => {
      this.shopGeneralForm.setValue({
        shopName: result.shopName,
        phoneNumber: result.phoneNumber,
        description: result.description,
        tagline: result.slogan || '',
        location: result.location,
        startWorkTime: result.startWorkTime,
        endWorkTime: result.endWorkTime,
        mon: false,
        tue: false,
        wed: false,
        thu: false, 
        fri: false,
        sat: false,
        sun: false,
      });

      this.prevData = result;
      if(result.workDays && result.workDays !== '') this.parseAndCheckDays(result.workDays);

      this.spinnerService.changeSpinnerState(false);

    });
  }

  async onSubmit() : Promise<void> {
    try {
      if(this.shopGeneralForm.valid) {
        if(!this.equalData()) {
          this.changeValidityToDefault();
          this.spinnerService.changeSpinnerState(true);

          var data = new FormData();
          data.append("Logo", this.sendData.logo);
          data.append("Banner", this.sendData.banner);
          if(this.sendData.slogan && this.sendData.slogan.trim().length > 0) data.append("Slogan", this.sendData.slogan);
          if(this.sendData.location && this.sendData.location.trim().length > 0) data.append("Location", this.sendData.location);
          data.append("ShopName", this.sendData.shopName);
          data.append("WorkDays", this.sendData.workDays);
          if(this.sendData.description && this.sendData.description.trim().length > 0) data.append("Description", this.sendData.description);
          data.append("PhoneNumber", this.sendData.phoneNumber);
          if(this.sendData.endWorkTime) data.append("EndWorkTime", this.sendData?.endWorkTime?.toString());
          if(this.sendData.startWorkTime) data.append("StartWorkTime", this.sendData?.startWorkTime?.toString());

          await this.settingsApi.updateShopData(data).toPromise();
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
        if(fieldName === 'phoneNumber' && this.shopGeneralForm.get(fieldName)?.valid) {
          if(this.phoneNumber?.value !== this.prevData.phoneNumber) {
            this.validPhoneNumber = await this.authFacade.checkPhoneNumberAvailability(this.phoneNumber?.value);

            if(this.validPhoneNumber) {
              this.phoneNumber?.setErrors({ 'phoneNumberNotAvailable': true });
              this.shopGeneralForm.setErrors({ 'invalidData': true });
            }
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else if (field.name === 'shopName') {
        if(fieldName === 'shopName' && this.shopGeneralForm.get(fieldName)?.valid) {
          if(this.shopName?.value !== this.prevData.shopName) {
            this.validShopName = await this.authFacade.checkShopNameAvailability(this.shopName?.value);

            if(this.validShopName) {
              this.shopName?.setErrors({ 'shopNameNotAvailable': true });
              this.shopGeneralForm.setErrors({ 'invalidData': true });
            }
          }
        }

        this.changeValidity(field.name, field.elementId);

      } else this.changeValidity(field.name, field.elementId);
    }
  }

  changeValidity(fieldName: string, elementId: string) : void {
    const element = $(`#${elementId}`);

    if (this.shopGeneralForm.get(fieldName)?.valid) {
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

      if (this.shopGeneralForm.get(item.name)?.valid) element.removeClass('custom-valid-card-group');
      else  element.removeClass('custom-invalid-card-group');

    });

    this.sendData.shopName = this.shopName?.value;
    this.sendData.phoneNumber = this.phoneNumber?.value;
    this.sendData.description = this.description?.value;
    this.sendData.slogan = this.tagline?.value;
    this.sendData.location = this.location?.value;
    this.sendData.workDays = this.convertToDaysString();
    this.sendData.logo = this.prevData.logo;
    this.sendData.banner = this.prevData.banner;
    this.sendData.startWorkTime = this.sendData.startWorkTime == null ? this.startWorkTime?.value : `${this.startWorkTime?.value}:00`; 
    this.sendData.endWorkTime = this.sendData.endWorkTime == null ? this.endWorkTime?.value : `${this.endWorkTime?.value}:00`; 

    this.prevData = this.sendData;
  }

  equalData(): boolean {
    return (
      this.prevData.shopName === this.shopName?.value &&
      this.prevData.phoneNumber === this.phoneNumber?.value &&
      this.prevData.description === this.description?.value &&
      this.prevData.slogan === this.tagline?.value &&
      this.prevData.location === this.location?.value &&
      this.prevData.workDays === this.convertToDaysString() &&
      this.prevData.startWorkTime === this.startWorkTime?.value &&
      this.prevData.endWorkTime === this.endWorkTime?.value
    );
  }

  parseAndCheckDays(workDays: string) {
    const parts = workDays.split(', ');
    parts.forEach(part => {
      if (part.includes('-')) {
        const [start, end] = part.split('-');
        const startIndex = this.daysOfWeek.indexOf(start.trim());
        const endIndex = this.daysOfWeek.indexOf(end.trim());
        for (let i = startIndex; i <= endIndex; i++) {
          const controlName = this.daysOfWeek[i].toLowerCase();
          this.shopGeneralForm.get(controlName)?.setValue(true); 
        }
      } else {
        const index = this.daysOfWeek.indexOf(part.trim());
        const controlName = this.daysOfWeek[index].toLowerCase(); 
        this.shopGeneralForm.get(controlName)?.setValue(true);
      }
    });
  }

  convertToDaysString(): string {
    const selectedDays = this.daysOfWeek.filter(day => this.shopGeneralForm.get(day.toLocaleLowerCase())?.value);
    const groupedDays = [];
    let currentGroup = [];
    for (let i = 0; i < selectedDays.length; i++) {
      if (i === 0 || this.daysOfWeek.indexOf(selectedDays[i - 1]) !== this.daysOfWeek.indexOf(selectedDays[i]) - 1) {
        currentGroup = [selectedDays[i]];
        groupedDays.push(currentGroup);
      } else {
        currentGroup.push(selectedDays[i]);
      }
    }

    const daysString = groupedDays
      .map(group => {
        if (group.length > 1) {
          return `${group[0]}-${group[group.length - 1]}`;
        } else {
          return group[0];
        }
      })
      .join(', ');

      return daysString;
  }
}
