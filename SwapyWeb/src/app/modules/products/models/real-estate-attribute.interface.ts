import { ProductAttribute } from "./product-attribute.interface";

export interface RealEstateAttribute extends ProductAttribute {
    area: number;
    rooms: number;
    isRent: boolean;
}