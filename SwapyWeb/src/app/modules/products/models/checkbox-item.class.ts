export class CheckboxItem<T> {
    selected: boolean = false;
    value!: T;

    constructor(value: T) { this.value = value; }
}