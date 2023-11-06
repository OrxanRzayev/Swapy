import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phoneNumber'
})
export class PhoneNumberPipe implements PipeTransform {
  transform(value: string): string {
    if (!value) {
      return '';
    }
    const cleanedNumber = value.replace(/\D/g, '');
    const countryCode = cleanedNumber.slice(0, 3);
    const firstPart = cleanedNumber.slice(3, 5);
    const secondPart = cleanedNumber.slice(5, 8);
    const thirdPart = cleanedNumber.slice(8, 10);
    const fourthPart = cleanedNumber.slice(10);

    return `+${countryCode} ${firstPart} ${secondPart} ${thirdPart} ${fourthPart}`;
  }
}
