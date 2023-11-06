import { PageResponse } from "src/app/core/models/page-response.interface";
import { Product } from "./product.model";

export interface RealEstateResponse extends PageResponse<Product> {
    maxArea: number;
    minArea: number;
    maxRooms: number;
    minRooms: number;
}