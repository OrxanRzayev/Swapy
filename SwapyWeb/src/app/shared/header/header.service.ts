import { Injectable } from "@angular/core";
import { HeaderComponent } from "./header.component";


@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  private headerComponent: HeaderComponent | null = null;

  setHeaderComponent(component: HeaderComponent) {
    this.headerComponent = component;
  }

  disableCollapsedMenu() {
    if (this.headerComponent) {
      this.headerComponent.disableCollapsedMenuAnimation();
    }
  }
}