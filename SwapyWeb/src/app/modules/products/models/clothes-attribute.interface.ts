import { ProductAttribute } from "./product-attribute.interface";

export interface ClothesAttribute extends ProductAttribute {
    isNew: boolean;
    clothesSizeId: string;
    clothesSize: string;
    isShoe: boolean;
    isChild: boolean;
    genderId: string;
    gender: string;
    clothesSeasonId: string;
    clothesSeason: string;
    clothesBrandId: string;
    clothesBrand: string;
    clothesViewId: string;
    clothesView: string;
}