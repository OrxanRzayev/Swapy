import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'shortNumber'
})
export class ShortNumberPipe implements PipeTransform {
  transform(value: number): string {
    if (isNaN(value)) {
      return '';
    }

    const suffixes = ['', 'K', 'M', 'B', 'T'];
    const exp = Math.floor(Math.log(value) / Math.log(1000));

    if (exp < 1) {
      return value.toString();
    }

    const shortNumber = (value / Math.pow(1000, exp)).toFixed(1);
    return shortNumber + suffixes[exp];
  }
}