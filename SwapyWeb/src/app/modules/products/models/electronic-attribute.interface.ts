import { ProductAttribute } from "./product-attribute.interface";

export interface ElectronicAttribute extends ProductAttribute {
    isNew: boolean;
    colorId: string;
    color: string;
    memoryId: string;
    memory: string;
    modelId: string;
    model: string;
    electronicBrandId: string;
    electronicBrand: string;
}