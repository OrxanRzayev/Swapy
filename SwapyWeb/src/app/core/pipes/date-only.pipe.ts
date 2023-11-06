import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateOnly'
})
export class DateOnlyPipe implements PipeTransform {
  transform(value: Date): string {
    // Проверяем, что значение даты не является нулевым или неопределенным
    if (!value) {
      return '';
    }

    // Получаем только дату из объекта типа Date
    const date = new Date(value);
    const year = date.getFullYear();
    const month = this.formatNumber(date.getMonth() + 1);
    const day = this.formatNumber(date.getDate());

    return `${day}.${month}.${year}`;
  }

  private formatNumber(value: number): string {
    // Форматируем числа так, чтобы однозначные числа имели ведущий ноль
    return value.toString().padStart(2, '0');
  }
}
