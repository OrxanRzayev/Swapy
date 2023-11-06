export interface PageResponse<T> {
    items: T[];
    count: number;
    allPages: number;
    minPrice: number | null;
    maxPrice: number | null;
}