import { CategoryType } from "src/app/core/enums/category-type.enum";
import { SubcategoryType } from "src/app/core/enums/subcategory-type.enum";

export class ProductSubcategory {
    Id: string;
    Name: string;
    Type: CategoryType;
    CategoryId: string;
    SubType?: SubcategoryType | null;

    constructor(id: string, name: string, type: CategoryType, categoryId: string, subType?: SubcategoryType | null) {
        this.Id = id;
        this.Name = name;
        this.Type = type;
        this.CategoryId = categoryId;
        this.SubType = subType;
    }
}