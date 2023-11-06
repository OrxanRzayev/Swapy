import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }

  public get accessToken() : string {
    return localStorage.getItem('accessToken') as string;
  }

  public set accessToken(token : string) {
    localStorage.setItem('accessToken', token);
  }

  public removeToken() : void {
    localStorage.removeItem('accessToken');
  }

  ////////////////////////////////////////////////////////////

  public get userId() : string {
    return localStorage.getItem('userId') as string;
  }

  public set userId(id : string) {
    localStorage.setItem('userId', id);
  }

  public removeUserId() : void {
    localStorage.removeItem('userId');
  }

  ////////////////////////////////////////////////////////////

  public get userType(): string {
    return localStorage.getItem('userType') as string;
  }

  public set userType(type: string) {
    localStorage.setItem('userType', type);
  }

  public removeUserType() {
    localStorage.removeItem('userType');
  }

  ////////////////////////////////////////////////////////////

  public get rememberMe() : string {
    return localStorage.getItem('rememberMe') as string;
  }

  public set rememberMe(type : string) {
    localStorage.setItem('rememberMe', type);
  }

  public removeRememberMe() : void {
    localStorage.removeItem('rememberMe');
  }

  ////////////////////////////////////////////////////////////

  public get hasUnreadMessage() : string {
    return localStorage.getItem('hasUnreadMessage') as string;
  }

  public set hasUnreadMessage(type : string) {
    localStorage.setItem('hasUnreadMessage', type);
  }

  public removeHasUnreadMessage() : void {
    localStorage.removeItem('hasUnreadMessage');
  }
}