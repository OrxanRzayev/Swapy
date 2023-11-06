import { Pipe, PipeTransform } from '@angular/core';

@Pipe({  
  name: 'timespanToHoursMinutes'
})

export class TimeSpanToHoursMinutesPipe implements PipeTransform {
  transform(value: Date): string {  
    value = new Date(value);
    const hours = value.getHours();
    const minutes = value.getMinutes();
    
    const formattedHours = this.padNumber(hours);
    const formattedMinutes = this.padNumber(minutes);
    
    return `${formattedHours}:${formattedMinutes}`;  
  }

  private padNumber(number: number): string {
    return number.toString().padStart(2, '0');
  }
}