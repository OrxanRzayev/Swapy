import { ProductAttribute } from "./product-attribute.interface";

export interface ItemAttribute extends ProductAttribute {
    isNew: boolean;
}