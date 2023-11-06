import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customDateEn'
})
export class CustomDatePipeEn implements PipeTransform {
  transform(value: Date): string {

    const today = new Date();
    const yesterday = new Date(today);
    yesterday.setDate(yesterday.getDate() - 1);

    const inputDate = new Date(value);

    if (isNaN(inputDate.getTime())) {
      return ''; 
    }

    const diffTime = today.getTime() - inputDate.getTime();
    const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24));

    if (diffDays === 0) {
      const options: Intl.DateTimeFormatOptions = { hour: '2-digit', minute: '2-digit', hour12: true };
      return `Today at ${inputDate.toLocaleTimeString('en-EN', options)}`;
    } else if (diffDays === 1) {
      const options: Intl.DateTimeFormatOptions = { hour: '2-digit', minute: '2-digit', hour12: true };
      return `Yesterday at ${inputDate.toLocaleTimeString('en-EN', options)}`;
    } else if (diffDays < 30) {
      const diffMonths = Math.floor(diffDays);
      return `${diffMonths} ${this.getPlural(diffMonths, 'Day', 'Days')} ago`;
    }else if (diffDays < 365) {
      const diffMonths = Math.floor(diffDays / 30);
      return `${diffMonths} ${this.getPlural(diffMonths, 'Month', 'Months')} ago`;
    } else {
      const diffYears = Math.floor(diffDays / 365);
      return `${diffYears} ${this.getPlural(diffYears, 'Year', 'Years')} ago`;
    }
  }

  private getPlural(value: number, singular: string, plural: string): string {
    return value === 1 ? singular : plural;
  }
}