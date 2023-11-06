import { ProductAttribute } from "./product-attribute.interface";

export interface AutoAttribute extends ProductAttribute {
    isNew: boolean;
    miliage: number;
    engineCapacity: number;
    releaseYear: Date;
    fuelTypeId: string;
    fuelType: string;
    colorId: string;
    color: string;
    transmissionTypeId: string;
    transmissionType: string;
    autoBrandId: string;
    autoBrand: string;
    autoModelId: string;
    autoModel: string;
}