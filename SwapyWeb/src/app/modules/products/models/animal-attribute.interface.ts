import { ProductAttribute } from "./product-attribute.interface";

export interface AnimalAttribute extends ProductAttribute {
    breedId: string;
    breed: string;
}