import { Injectable } from "@angular/core";
import { AuthComponent } from "./auth.component";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private authComponent: AuthComponent | null = null;

  setAuthComponent(component: AuthComponent) {
    this.authComponent = component;
  }

  changeMessagesState(value: boolean) {
    if (this.authComponent) {
      this.authComponent.changeMessagesState(value);
    }
  }
}