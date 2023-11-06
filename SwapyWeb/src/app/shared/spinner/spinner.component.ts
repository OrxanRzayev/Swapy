import { Component, OnInit } from '@angular/core';
import { SpinnerService } from './spinner.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss']
})
export class SpinnerComponent implements OnInit{
  oldScroll: number;
  spinnerState: boolean = false;
  
  constructor(private spinnerService: SpinnerService) { this.oldScroll = 0; }
  
  ngOnInit(): void { this.spinnerService.setSpinnerComponent(this); }

  changeSpinnerState(value: boolean): void {
    this.spinnerState = value;

    if(this.spinnerState) this.showSpinner();
    else this.hideSpinner();
  }

  showSpinner(): void { document.body.style.overflow = 'hidden'; }

  hideSpinner(): void { document.body.style.overflow = 'auto'; }
}
