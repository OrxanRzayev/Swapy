import { UserType } from "src/app/core/enums/user-type.enum";
import { CategoryNode } from "src/app/core/models/category-node.interface";
import { Specification } from "src/app/core/models/specification";

export interface ProductAttribute {
    id: string;
    city: string;
    cityId: string;
    currency: string;
    currencyId: string;
    currencySymbol: string;
    userId: string;
    sellerName: string;
    phoneNumber: string;
    shopId: string;
    isDisable: boolean;
    isFavorite: boolean;
    userType: UserType;
    productId: string;
    title: string;
    description: string;
    views: number;
    price: number;
    dateTime: Date;
    categories: Specification<string>[];
    images: string[];
}
  