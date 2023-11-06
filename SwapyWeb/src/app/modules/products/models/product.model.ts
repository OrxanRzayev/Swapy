import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { UserType } from 'src/app/core/enums/user-type.enum';

export class Product {
    id: string = 'none';
    title: string = 'none';
    price: number = 0;
    city: string = 'none';
    currency: string = 'none';
    currencySymbol: string = 'none';
    dateTime: Date = new Date();
    images: string[] = [];
    userType!: UserType;
    userId!: string;
    type!: CategoryType;
    isFavorite: boolean = false;
    isDisable: boolean = true;
}