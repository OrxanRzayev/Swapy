import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateToTime'
})
export class DateToTimePipe implements PipeTransform {
  transform(value: Date): string {
    const date = value.toString().split(':');

    return `${date[0]}:${date[1]}`;
  }
}
