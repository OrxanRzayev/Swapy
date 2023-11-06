import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SessionStorageService {

  constructor() { }

  public get accessToken(): string {
    return sessionStorage.getItem('accessToken') as string;
  }

  public set accessToken(token: string) {
    sessionStorage.setItem('accessToken', token);
  }

  public removeToken() {
    sessionStorage.removeItem('accessToken');
  }

  ////////////////////////////////////////////////////////////

  public get userId(): string {
    return sessionStorage.getItem('userId') as string;
  }

  public set userId(id: string) {
    sessionStorage.setItem('userId', id);
  }

  public removeUserId() {
    sessionStorage.removeItem('userId');
  }

  ////////////////////////////////////////////////////////////

  public get userType(): string {
    return sessionStorage.getItem('userType') as string;
  }

  public set userType(type: string) {
    sessionStorage.setItem('userType', type);
  }

  public removeUserType() {
    sessionStorage.removeItem('userType');
  }

  ////////////////////////////////////////////////////////////

  public get hasUnreadMessage() : string {
    return sessionStorage.getItem('hasUnreadMessage') as string;
  }

  public set hasUnreadMessage(type : string) {
    sessionStorage.setItem('hasUnreadMessage', type);
  }

  public removeHasUnreadMessage() : void {
    sessionStorage.removeItem('hasUnreadMessage');
  }
}