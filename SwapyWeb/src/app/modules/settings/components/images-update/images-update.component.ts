import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { SettingsApiService } from '../../services/settings-api.service';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { UserType } from 'src/app/core/enums/user-type.enum';
import { UserData } from '../../models/user-data';
import { ShopData } from '../../models/shop-data';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-images-update',
  templateUrl: './images-update.component.html',
  styleUrls: ['./images-update.component.scss']
})
export class ImagesUpdateComponent implements OnInit {
  isShop: boolean = false;
  
  @ViewChild('logoElement') logoElement!: ElementRef;
  @ViewChild('bannerElement') bannerDiv!: ElementRef<HTMLDivElement>;

  constructor(private authFacade: AuthFacadeService, private spinnerService: SpinnerService, private settingsApi: SettingsApiService) { }

  async ngOnInit(): Promise<void> {
    this.isShop = this.authFacade.getUserType() === UserType.Shop;
    this.spinnerService.changeSpinnerState(true);

    if(this.isShop) {
      this.settingsApi.getShopData().subscribe((result: ShopData) => {
        this.logoLoad(`${environment.blobUrl}/logos/${result.logo}`);
        this.bannerLoad(`${environment.blobUrl}/banners/${result.banner}`);
        this.spinnerService.changeSpinnerState(false);
      });
    } else {
      this.settingsApi.getUserData().subscribe((result: UserData) => {
        this.logoLoad(`${environment.blobUrl}/logos/${result.logo}`);
        this.spinnerService.changeSpinnerState(false);
      });
    }
  }

  async updateLogo(event: any) {
    this.spinnerService.changeSpinnerState(true);
    const file: File = event.target.files[0];
    const allowedExtensions = ['jpg', 'jpeg', 'png'];
    const maxFileSize = 10 * 1024 * 1024;

    if (file.size <= maxFileSize && allowedExtensions.includes(file.type.split('/')[1].toLowerCase())) {
      const img = new Image();
      img.src = URL.createObjectURL(file);
      img.onload = async () => {
        URL.revokeObjectURL(img.src);
        const data = new FormData();
        data.append("image", file);

        if (file) { 
          const reader = new FileReader(); 
          reader.onload = (e: any) => { 
            if (file) { 
              const arrayBuffer = e.target.result; 
              const blob = new Blob([arrayBuffer], { type: file.type }); 
              const imageUrl = URL.createObjectURL(blob); 
              this.logoElement.nativeElement.src = imageUrl; 
              this.settingsApi.updateLogo(data).subscribe(result => { this.spinnerService.changeSpinnerState(false); });
            } 
          }; 
          reader.readAsArrayBuffer(file); 
        }  
        else this.logoElement.nativeElement.src = 'undefined'; 
      };

      img.onerror = () => {
          URL.revokeObjectURL(img.src);
      };

    } else {
      console.log('Invalid file');
      this.spinnerService.changeSpinnerState(false);
    }
    
  }

  async updateBanner(event: any) {
    this.spinnerService.changeSpinnerState(true);
    const file: File = event.target.files[0];
    const allowedExtensions = ['jpg', 'jpeg', 'png'];
    const maxFileSize = 10 * 1024 * 1024;

    if (file.size <= maxFileSize && allowedExtensions.includes(file.type.split('/')[1].toLowerCase())) {
      const img = new Image();
      img.src = URL.createObjectURL(file);
      img.onload = async () => {
        URL.revokeObjectURL(img.src);
        const data = new FormData();
        data.append("image", file);

        if (file) { 
          const reader = new FileReader(); 
          reader.onload = (e: any) => { 
            if (file) { 
              const arrayBuffer = e.target.result; 
              const blob = new Blob([arrayBuffer], { type: file.type }); 
              const imageUrl = URL.createObjectURL(blob); 
              this.bannerDiv.nativeElement.style.backgroundImage = `url(${imageUrl})`;
              this.bannerDiv.nativeElement.style.backgroundColor = 'transparent';
              this.settingsApi.updateBanner(data).subscribe(result => { this.spinnerService.changeSpinnerState(false); });
            } 
          }; 
          reader.readAsArrayBuffer(file); 
        }  
        else this.bannerDiv.nativeElement.style.backgroundColor = 'linear-gradient(to bottom, $secondary, $primary)';
      };

      img.onerror = () => {
          URL.revokeObjectURL(img.src);
      };

    } else {
      console.log('Invalid file');
      this.spinnerService.changeSpinnerState(false);
    }
    
    
  }

  imageLoadError(event: any) {
    event.srcElement.src = "https://mtek3d.com/wp-content/uploads/2018/01/image-placeholder-500x500.jpg";
  }

  logoLoad(imageUrl: string): void {
    const image = new Image();
    image.src = imageUrl;
    
    image.onload = () => {
      this.logoElement.nativeElement.src = `${imageUrl}`;
    };
    
    image.onerror = () => {
      this.logoElement.nativeElement.src = `${environment.blobUrl}/logos/${this.isShop == true ? "default-shop-logo.png" : "default-user-logo.png"}`;
    };
  }

  bannerLoad(imageUrl: string): void {
    if(imageUrl !== `${environment.blobUrl}/banners/default-shop-banner`) {
      const image = new Image();
      image.src = imageUrl;
      
      image.onload = () => {
        this.bannerDiv.nativeElement.style.backgroundImage = `url(${imageUrl})`;
        this.bannerDiv.nativeElement.style.backgroundColor = 'transparent';
      };
      
      image.onerror = () => {
        this.bannerDiv.nativeElement.style.backgroundColor = 'linear-gradient(to bottom, $secondary, $primary)';
      };
    }
  }
}
