import { ProductAttribute } from "./product-attribute.interface";

export interface TVAttribute extends ProductAttribute {
    isNew: boolean;
    isSmart: boolean;
    tvBrandId: string;
    tvBrand: string;
    tvTypeId: string;
    tvType: string;
    screenDiagonal: number;
    screenDiagonalId: string;
    screenResolutionId: string;
    screenResolution: string;
}