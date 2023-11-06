import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'chatDateEn'
})
export class ChatDatePipeEn implements PipeTransform {
    transform(value: Date): string {
        value = new Date(value);
        const today = new Date();
        const yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);
        
        const valueDate = value.getDate();
        const todayDate = today.getDate();
        const yesterdayDate = yesterday.getDate();
        
        if (valueDate === todayDate && value.getMonth() === today.getMonth() && value.getFullYear() === today.getFullYear()) {
          return 'Today';
        } else if (valueDate === yesterdayDate && value.getMonth() === yesterday.getMonth() && value.getFullYear() === yesterday.getFullYear()) {
          return 'Yesterday';
        } else {
          return `${value.getDate()}.${value.getMonth() + 1}.${value.getFullYear()}`;
        }
    }
}