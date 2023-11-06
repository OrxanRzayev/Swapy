import { Injectable } from '@angular/core';
import { AuthApiService } from './auth-api.service';
import { LocalStorageService } from 'src/app/core/services/local-storage.service';
import { EmailVerifyCredential, LoginCredential, ResetPasswordCredential, ShopRegistrationCredential, UserRegistrationCredential } from '../models/auth-credentials';
import { SessionStorageService } from 'src/app/core/services/session-storage.service';
import { UserType } from 'src/app/core/enums/user-type.enum';

@Injectable({
  providedIn: 'root'
})
export class AuthFacadeService {

  constructor(private authApi: AuthApiService, private localStorage: LocalStorageService, private sessionStorage: SessionStorageService) { }

  async login(credential: LoginCredential): Promise<void> {
    try {
      const result = await this.authApi.login(credential).toPromise();
      if(result) {
        if (this.localStorage.rememberMe) {
          if (this.localStorage.rememberMe === 'true') {
            this.localStorage.userId = result.userId;
            this.localStorage.userType = result.type.toString();
            this.localStorage.accessToken = result.accessToken;
            this.localStorage.hasUnreadMessage = result.hasUnreadMessages.toString();
          } else {
            this.sessionStorage.userId = result.userId;
            this.sessionStorage.userType = result.type.toString();
            this.sessionStorage.accessToken = result.accessToken;
            this.sessionStorage.hasUnreadMessage = result.hasUnreadMessages.toString();
          }
        }
      }
    } catch(error) { throw error; }
  }
  
  async userRegistration(credential: UserRegistrationCredential): Promise<void> {
    await this.authApi.userRegistration(credential).toPromise();
  }

  async shopRegistration(credential: ShopRegistrationCredential): Promise<void> {
    await this.authApi.shopRegistration(credential).toPromise();
  }

  async checkEmailAvailability(email: string): Promise<boolean> {
    try {
      const result = await this.authApi.checkEmailAvailability(email).toPromise()
      if(result) return result;
      return false;
    } catch (error) { throw error }
  }

  async checkPhoneNumberAvailability(phoneNumber: string): Promise<boolean> {
    try {
      const result = await this.authApi.checkPhoneNumberAvailability(phoneNumber).toPromise()
      if(result) return result;
      return false;
    } catch (error) { throw error }
  }

  async checkShopNameAvailability(shopName: string): Promise<boolean> {
    try {
      const result = await this.authApi.checkShopNameAvailability(shopName).toPromise()
      if(result) return result;
      return false;
    } catch (error) { throw error }
  }

  async forgotPassword(email: string) : Promise<void> {
    try {
      await this.authApi.forgotPassword(email).toPromise()
    } catch (error) { throw error }
  }

  async resetPassword(credential: ResetPasswordCredential) : Promise<void> {
    try {
      await this.authApi.resetPassword(credential).toPromise()
    } catch (error) { throw error; }
  }

  async emailVerify(credential: EmailVerifyCredential) : Promise<void> {
    try {
      await this.authApi.emailVerify(credential).toPromise()
    } catch (error) { throw error; }
  }

  async logout(): Promise<void> {
    try {
      await this.authApi.logout().toPromise();
      
      if(this.localStorage.rememberMe) {
        if(this.localStorage.rememberMe === 'true') {
          this.localStorage.removeToken();
          this.localStorage.removeUserId();
          this.localStorage.removeUserType();
          this.localStorage.removeHasUnreadMessage();
        } else {
          this.sessionStorage.removeToken();
          this.sessionStorage.removeUserId();
          this.sessionStorage.removeUserType();
          this.sessionStorage.removeHasUnreadMessage();
        }
        this.localStorage.removeRememberMe();
      }
    } catch(error) { console.log(error); }
  }

  isAuthenticated(): boolean {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === 'true') {
        if(this.localStorage.accessToken) return true;
        return false;
      } else {
        if(this.sessionStorage.accessToken) return true;
        return false;
      }
    }
    return false;
  }

  getUserId(): string | null {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === 'true') {
        if(this.localStorage.userId) return this.localStorage.userId;
        return null;
      } else {
        if(this.sessionStorage.userId) return this.sessionStorage.userId;
        return null;
      }
    }
    return null;
  }

  getUserType(): UserType | null {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === 'true') {
        if(this.localStorage.userType) return convertToNumber(this.localStorage.userType);
        return null;
      } else {
        if(this.sessionStorage.userType) return convertToNumber(this.sessionStorage.userType);
        return null;
      }
    }
    return null;
  }

  getRememberMe(): boolean {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === 'true') return true;
    }
    return false;
  }

  setRememberMe(value: string): void { this.localStorage.rememberMe = value; }

  getUnreadMesageState() : boolean {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === 'true') {
        return (this.localStorage.hasUnreadMessage === 'true');
      } else {
        return (this.sessionStorage.hasUnreadMessage === 'true');
      }
    }
    return false;
  }

  setUnreadMesageState(value: string) : void {
    if(this.localStorage.rememberMe) {
      if(this.localStorage.rememberMe === 'true') {
        this.localStorage.hasUnreadMessage = value;
      } else {
        this.sessionStorage.hasUnreadMessage = value;
      }
    }
  }
}

function convertToNumber(value: string): number | null { 
  return (!isNaN(parseFloat(value)) && isFinite(Number(value))) ? parseFloat(value) : null; 
}
