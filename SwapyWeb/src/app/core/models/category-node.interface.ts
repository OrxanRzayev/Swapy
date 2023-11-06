import { CategoryType } from "../enums/category-type.enum";
import { SubcategoryType } from "../enums/subcategory-type.enum";
import { Specification } from "./specification";

export interface CategoryNode {
    id: string;
    value: string;
    type: CategoryType;
    subType: SubcategoryType | null;
    isFinal: boolean;
    parent: Specification<string>;
}