import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'childOrAdult' })
export class ChildOrAdultPipe implements PipeTransform {
  transform(value: boolean): string {
    return value ? 'Child' : 'Adult';
  }
}