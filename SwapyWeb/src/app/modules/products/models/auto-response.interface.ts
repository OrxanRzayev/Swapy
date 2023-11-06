import { PageResponse } from "src/app/core/models/page-response.interface";
import { Product } from "./product.model";

export interface AutoResponse extends PageResponse<Product> {
    maxMiliage: number;
    minMiliage: number;
    maxEngineCapacity: number;
    minEngineCapacity: number;
    newerReleaseYear : number;
    olderReleaseYear: number;
}