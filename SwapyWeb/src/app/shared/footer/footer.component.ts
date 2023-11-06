import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent {
  constructor() { }

  copyToClipboard(text: string): void {
    if (navigator.clipboard && typeof navigator.clipboard.writeText === 'function') {
      const el = document.createElement('textarea');
      el.value = text;
      document.body.appendChild(el);
      el.select();
      document.execCommand('copy');
      document.body.removeChild(el);

      //Toast
      const toastElement = document.getElementById('footer-toast');
      const toast = new (window as any).bootstrap.Toast(toastElement as HTMLElement);
      toast.show();
      
      setTimeout(() => {
        toast.hide();
      }, 1500);
    }
  }

  onFooterTextClick(event: Event): void {
    const target = event.target as HTMLElement;
    const text = target.innerText;
    this.copyToClipboard(text);
  }
}
