import { Injectable } from '@angular/core'; 
import { SpinnerComponent } from './spinner.component'; 
 
@Injectable({ 
  providedIn: 'root' 
}) 
export class SpinnerService { 
  private spinnerComponent: SpinnerComponent | null = null; 
 
  setSpinnerComponent(component: SpinnerComponent) { 
    this.spinnerComponent = component; 
  } 
 
  changeSpinnerState(value: boolean) { 
    if (this.spinnerComponent) { 
      this.spinnerComponent.changeSpinnerState(value); 
    } 
  } 
}