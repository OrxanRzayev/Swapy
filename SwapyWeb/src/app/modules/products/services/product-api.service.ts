import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EMPTY, Observable, catchError, from, map } from 'rxjs';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { Product } from '../models/product.model';
import axios, { AxiosError, AxiosResponse } from 'axios';
import { Specification } from 'src/app/core/models/specification';
import { AnimalAttribute } from '../models/animal-attribute.interface';
import { AutoAttribute } from '../models/auto-attribute.interface';
import { ClothesAttribute } from '../models/clothes-attribute.interface';
import { ElectronicAttribute } from '../models/electronic-attribute.interface';
import { ItemAttribute } from '../models/item-attribute.interface';
import { RealEstateAttribute } from '../models/real-estate-attribute.interface';
import { TVAttribute } from '../models/tv-attribute.interface';
import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { UserType } from 'src/app/core/enums/user-type.enum';
import { AutoResponse } from '../models/auto-response.interface';
import { RealEstateResponse } from '../models/real-estate-response.interface';
import { AxiosInterceptorService } from 'src/app/core/services/axios-interceptor.service';
import { ProductSubcategory } from '../models/product-subcategory-response.interface';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {

  private readonly apiUrl: string = environment.apiUrl;
  private readonly productsApiUrl: string = environment.productsApiUrl;
  private readonly animalsApiUrl: string = environment.animalsApiUrl;
  private readonly autosApiUrl: string = environment.autosApiUrl;
  private readonly clothesApiUrl: string = environment.clothesApiUrl;
  private readonly electronicsApiUrl: string = environment.electronicsApiUrl;
  private readonly itemsApiUrl: string = environment.itemsApiUrl;
  private readonly realEstatesApiUrl: string = environment.realEstatesApiUrl;
  private readonly tvsApiUrl: string = environment.tvsApiUrl;

  constructor(private axiosInterceptorService: AxiosInterceptorService) {}

  GetAnimalDetail(productId : string): Observable<AnimalAttribute> {
    let url = `${this.apiUrl}/${this.animalsApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: AnimalAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
  
  GetAutoDetail(productId : string): Observable<AutoAttribute> {
    let url = `${this.apiUrl}/${this.autosApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: AutoAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  GetClothesDetail(productId : string): Observable<ClothesAttribute> {
    let url = `${this.apiUrl}/${this.clothesApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: ClothesAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
  
  GetElectronicDetail(productId : string): Observable<ElectronicAttribute> {
    let url = `${this.apiUrl}/${this.electronicsApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: ElectronicAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
  
  GetItemDetail(productId : string): Observable<ItemAttribute> {
    let url = `${this.apiUrl}/${this.itemsApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: ItemAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
  
  GetRealEstateDetail(productId : string): Observable<RealEstateAttribute> {
    let url = `${this.apiUrl}/${this.realEstatesApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: RealEstateAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
  
  GetTVDetail(productId : string): Observable<TVAttribute> {
    let url = `${this.apiUrl}/${this.tvsApiUrl}/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const product: TVAttribute = response.data;
        product.sellerName = response.data.userType === UserType.Shop ? response.data.shop : `${response.data.firstName} ${response.data.lastName}`;
        product.categories = response.data.categories.$values;
        product.images = response.data.images.$values;
        return product;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  GetProductCategoryType(productId : string): Observable<Specification<CategoryType>>{
    let url = `${this.apiUrl}/${this.productsApiUrl}/GetCategoryType/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const category: Specification<CategoryType> = response.data;
        return category;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  GetProductSubcategory(productId : string): Observable<ProductSubcategory>{
    let url = `${this.apiUrl}/${this.productsApiUrl}/GetProductSubcategory/${productId}`
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const productSubcategory: ProductSubcategory  = new ProductSubcategory(response.data.id, response.data.name, response.data.type, response.data.categoryId, response.data.subType);
        return productSubcategory;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getSimilarProducts(page: number, pageSize: number, productId : string): Observable<PageResponse<Product>> {
    let url = `${this.apiUrl}/${this.productsApiUrl}/GetSimilarProductsByProductId?Page=${page}&PageSize=${pageSize}&ProductId=${productId}`
    return from(
      this.axiosInterceptorService.get(url)
    ).pipe(
      map((response: AxiosResponse<any>) => ({
        items: response.data.items.$values.map((item: any) => ({
          ...item,
          images: item.images.$values
        })),
        count: response.data.count,
        allPages: response.data.allPages,
        minPrice: response.data.minPrice,
        maxPrice: response.data.maxPrice
      })),
      catchError(error => {
        throw error;
      })
    );
  }

  getFilteredProducts(
                      page: number,
                      pageSize: number, 
                      sortByPrice: boolean | null = null, 
                      reverseSort: boolean | null = null, 
                      title: string | null = null, 
                      currencyId: string | null = null, 
                      priceMin: number | null = null, 
                      priceMax: number | null = null, 
                      categoryId: string | null = null, 
                      subcategoryId: string | null = null, 
                      cityId: string | null = null): Observable<PageResponse<Product>> 
  {
    let url = `${this.apiUrl}/${this.productsApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    return from(
      this.axiosInterceptorService.get(url)
    ).pipe(
      map((response: AxiosResponse<any>) => ({
        items: response.data.items.$values.map((item: any) => ({
          ...item,
          images: item.images.$values
        })),
        count: response.data.count,
        allPages: response.data.allPages,
        minPrice: response.data.minPrice,
        maxPrice: response.data.maxPrice
      })),
      catchError(error => {
        throw error;
      })
    );
  }

  getFilteredAnimals(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    animalBreedsId: string[] | null = null,
    animalTypesId: string[] | null = null): Observable<PageResponse<Product>> 
  {
    let url = `${this.apiUrl}/${this.animalsApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += animalBreedsId != null ? animalBreedsId.map(value => `&AnimalBreedsId=${encodeURIComponent(value)}`).join("") : '';
    url += animalTypesId != null ? animalTypesId.map(value => `&AnimalTypesId=${encodeURIComponent(value)}`).join("") : '';
    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFilteredAutos(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    isNew: boolean | null = null,
    miliageMin: number | null = null,
    miliageMax: number | null = null,
    engineCapacityMin: number | null = null,
    engineCapacityMax: number | null = null,
    releaseYearOlder: Date | null = null,
    releaseYearNewer: Date | null = null,
    fuelTypesId: string[] | null = null,
    autoColorsId: string[] | null = null,
    transmissionTypesId: string[] | null = null,
    autoBrandsId: string[] | null = null,
    autoTypesId: string[] | null = null,): Observable<AutoResponse> 
  {
    let url = `${this.apiUrl}/${this.autosApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += isNew != null ? `&IsNew=${isNew}` : '';
    url += miliageMin != null ? `&MiliageMin=${miliageMin}` : '';
    url += miliageMax != null ? `&MiliageMax=${miliageMax}` : '';
    url += engineCapacityMin != null ? `&EngineCapacityMin=${engineCapacityMin}` : '';
    url += engineCapacityMax != null ? `&EngineCapacityMax=${engineCapacityMax}` : '';
    url += releaseYearOlder != null ? `&ReleaseYearOlder=${releaseYearOlder.toISOString()}` : '';
    url += releaseYearNewer != null ? `&ReleaseYearNewer=${releaseYearNewer.toISOString()}` : '';
    url += fuelTypesId != null ? fuelTypesId.map(value => `&FuelTypesId=${encodeURIComponent(value)}`).join("") : '';
    url += autoColorsId != null ? autoColorsId.map(value => `&AutoColorsId=${encodeURIComponent(value)}`).join("") : '';
    url += transmissionTypesId != null ? transmissionTypesId.map(value => `&TransmissionTypesId=${encodeURIComponent(value)}`).join("") : '';
    url += autoBrandsId != null ? autoBrandsId.map(value => `&AutoBrandsId=${encodeURIComponent(value)}`).join("") : '';
    url += autoTypesId != null ? autoTypesId.map(value => `&AutoTypesId=${encodeURIComponent(value)}`).join("") : '';
    
    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice,
    maxMiliage: response.data.maxMiliage,
    minMiliage: response.data.minMiliage,
    maxEngineCapacity: response.data.maxEngineCapacity,
    minEngineCapacity: response.data.minEngineCapacity,
    newerReleaseYear: response.data.newerReleaseYear,
    olderReleaseYear: response.data.olderReleaseYear
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFilteredClothes(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    isNew: boolean | null = null,
    clothesSeasonsId: string[] | null = null,
    clothesSizesId: string[] | null = null,
    clothesBrandsId: string[] | null = null,
    clothesViewsId: string[] | null = null,
    clothesTypesId: string[] | null = null,
    clothesGendersId: string[] | null = null,
    isChild: boolean | null = null): Observable<PageResponse<Product>> 
  {
    let url = `${this.apiUrl}/${this.clothesApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += isNew != null ? `&IsNew=${isNew}` : '';
    url += clothesSeasonsId != null ? clothesSeasonsId.map(value => `&ClothesSeasonsId=${encodeURIComponent(value)}`).join("") : '';
    url += clothesSizesId != null ? clothesSizesId.map(value => `&ClothesSizesId=${encodeURIComponent(value)}`).join("") : '';
    url += clothesBrandsId != null ? clothesBrandsId.map(value => `&ClothesBrandsId=${encodeURIComponent(value)}`).join("") : '';
    url += clothesViewsId != null ? clothesViewsId.map(value => `&ClothesViewsId=${encodeURIComponent(value)}`).join("") : '';
    url += clothesTypesId != null ? clothesTypesId.map(value => `&ClothesTypesId=${encodeURIComponent(value)}`).join("") : '';
    url += clothesGendersId != null ? clothesGendersId.map(value => `&ClothesGendersId=${encodeURIComponent(value)}`).join("") : '';
    url += isChild != null ? `&IsChild=${isChild}` : '';

    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFilteredElectronics(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    isNew: boolean | null = null,
    memoriesId: string[] | null = null,
    colorsId: string[] | null = null,
    modelsId: string[] | null = null,
    brandsId: string[] | null = null,
    typesId: string[] | null = null): Observable<PageResponse<Product>> 
  {
    let url = `${this.apiUrl}/${this.electronicsApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += isNew != null ? `&IsNew=${isNew}` : '';
    url += memoriesId != null ? memoriesId.map(value => `&MemoriesId=${encodeURIComponent(value)}`).join("") : '';
    url += colorsId != null ? colorsId.map(value => `&ColorsId=${encodeURIComponent(value)}`).join("") : '';
    url += modelsId != null ? modelsId.map(value => `&ModelsId=${encodeURIComponent(value)}`).join("") : '';
    url += brandsId != null ? brandsId.map(value => `&BrandsId=${encodeURIComponent(value)}`).join("") : '';
    url += typesId != null ? typesId.map(value => `&TypesId=${encodeURIComponent(value)}`).join("") : '';
    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFilteredItems(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    isNew: boolean | null = null,
    itemTypesId: string[] | null = null): Observable<PageResponse<Product>> 
  {
    let url = `${this.apiUrl}/${this.itemsApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += isNew != null ? `&IsNew=${isNew}` : '';
    url += itemTypesId != null ? itemTypesId.map(value => `&ItemTypesId=${encodeURIComponent(value)}`).join("") : '';
    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFilteredRealEstates(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    isRent: boolean | null = null,
    areaMax: number | null = null,
    areaMin: number | null = null,
    roomsMin: number | null = null,
    roomsMax: number | null = null,
    realEstateTypesId: string[] | null = null): Observable<RealEstateResponse> 
  {
    let url = `${this.apiUrl}/${this.realEstatesApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += isRent != null ? `&IsRent=${isRent}` : '';
    url += areaMax != null ? `&AreaMax=${areaMax}` : '';
    url += areaMin != null ? `&AreaMin=${areaMin}` : '';
    url += roomsMin != null ? `&RoomsMin=${roomsMin}` : '';
    url += roomsMax != null ? `&RoomsMax=${roomsMax}` : '';
    url += realEstateTypesId != null ? realEstateTypesId.map(value => `&RealEstateTypesId=${encodeURIComponent(value)}`).join("") : '';

    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice,
    minArea: response.data.minArea,
    maxArea: response.data.maxArea,
    minRooms: response.data.minRooms,
    maxRooms: response.data.maxRooms
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFilteredTVs(
    page: number,
    pageSize: number, 
    sortByPrice: boolean | null = null, 
    reverseSort: boolean | null = null, 
    title: string | null = null, 
    currencyId: string | null = null, 
    priceMin: number | null = null, 
    priceMax: number | null = null, 
    categoryId: string | null = null, 
    subcategoryId: string | null = null, 
    cityId: string | null = null,
    isNew: boolean | null = null,
    isSmart: boolean | null = null,
    tvTypesId: string[] | null = null,
    tvBrandsId: string[] | null = null,
    screenResolutionsId: string[] | null = null,
    screenDiagonalsId: string[] | null = null): Observable<PageResponse<Product>> 
  {
    let url = `${this.apiUrl}/${this.tvsApiUrl}?Page=${page}&PageSize=${pageSize}`
    url += sortByPrice != null ? `&SortByPrice=${sortByPrice}` : '';
    url += reverseSort != null ? `&ReverseSort=${reverseSort}` : '';
    url += title != null ? `&Title=${title}` : '';
    url += currencyId != null ? `&CurrencyId=${currencyId}` : '';
    url += priceMin != null ? `&PriceMin=${priceMin}` : '';
    url += priceMax != null ? `&PriceMax=${priceMax}` : '';
    url += categoryId != null ? `&CategoryId=${categoryId}` : '';
    url += subcategoryId != null ? `&SubcategoryId=${subcategoryId}` : '';
    url += cityId != null ? `&CityId=${cityId}` : '';
    url += isNew != null ? `&IsNew=${isNew}` : '';
    url += isSmart != null ? `&IsSmart=${isSmart}` : '';
    url += tvTypesId != null ? tvTypesId.map(value => `&TvTypesId=${encodeURIComponent(value)}`).join("") : '';
    url += tvBrandsId != null ? tvBrandsId.map(value => `&TvBrandsId=${encodeURIComponent(value)}`).join("") : '';
    url += screenResolutionsId != null ? screenResolutionsId.map(value => `&ScreenResolutionsId=${encodeURIComponent(value)}`).join("") : '';
    url += screenDiagonalsId != null ? screenDiagonalsId.map(value => `&ScreenDiagonalsId=${encodeURIComponent(value)}`).join("") : '';

    return from(
    this.axiosInterceptorService.get(url)
    ).pipe(
    map((response: AxiosResponse<any>) => ({
    items: response.data.items.$values.map((item: any) => ({
    ...item,
    images: item.images.$values
    })),
    count: response.data.count,
    allPages: response.data.allPages,
    minPrice: response.data.minPrice,
    maxPrice: response.data.maxPrice
    })),
    catchError(error => {
    throw error;
    })
    );
  }

  getFuelTypes(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.autosApiUrl}/FuelTypes`)).pipe(
      map((response: AxiosResponse<any>) => {
        const fuelTypes: Specification<string>[] = response.data.$values;
        return fuelTypes;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getColors(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.productsApiUrl}/Colors`)).pipe(
      map((response: AxiosResponse<any>) => {
        const colors: Specification<string>[] = response.data.$values;
        return colors;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getTransmissionTypes(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.autosApiUrl}/TransmissionTypes`)).pipe(
      map((response: AxiosResponse<any>) => {
        const transmissionTypes: Specification<string>[] = response.data.$values;
        return transmissionTypes;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getAutoBrands(AutoTypesId: string[] | null): Observable<Specification<string>[]>{
    AutoTypesId = AutoTypesId?.map(i => 'AutoTypesId=' + i) ? AutoTypesId?.map(i => 'AutoTypesId=' + i) : null;
    let url = AutoTypesId ? `${this.apiUrl}/${this.autosApiUrl}/Brands?${AutoTypesId.join('&')}` : `${this.apiUrl}/${this.autosApiUrl}/Brands`;
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const brands: Specification<string>[] = response.data.$values;
        return brands;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getAutoModels(AutoBrandsId: string[] | null, AutoTypesId: string[] | null): Observable<Specification<string>[]>{
    let url = `${this.apiUrl}/${this.autosApiUrl}/Models`;
    AutoBrandsId = AutoBrandsId?.map(i => 'AutoBrandsId=' + i) ? AutoBrandsId?.map(i => 'AutoBrandsId=' + i) : null;
    AutoTypesId = AutoTypesId?.map(i => 'AutoTypesId=' + i) ? AutoTypesId?.map(i => 'AutoTypesId=' + i) : null;
    if(AutoBrandsId != null && AutoTypesId != null){
      url += `?${AutoBrandsId.join('&')}&${AutoTypesId.join('&')}`;
    }
    else{
      url += AutoBrandsId != null ? `?${AutoBrandsId.join('&')}` : '';
      url += AutoTypesId != null ? `?${AutoTypesId.join('&')}` : '';
    }
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const models: Specification<string>[] = response.data.$values;
        return models;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getClotheBrands(ClothesViewsId: string[] | null): Observable<Specification<string>[]>{
    ClothesViewsId = ClothesViewsId?.map(i => 'ClothesViewsId=' + i) ? ClothesViewsId?.map(i => 'ClothesViewsId=' + i) : null;
    let url = ClothesViewsId ? `${this.apiUrl}/${this.clothesApiUrl}/Brands?${ClothesViewsId.join('&')}` : `${this.apiUrl}/${this.clothesApiUrl}/Brands`;
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const brands: Specification<string>[] = response.data.$values;
        return brands;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getElectronicBrands(ElectronicTypeId: string | null): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(ElectronicTypeId ? `${this.apiUrl}/${this.electronicsApiUrl}/Brands?ElectronicTypeId=${ElectronicTypeId}` : `${this.apiUrl}/${this.electronicsApiUrl}/Brands`)).pipe(
      map((response: AxiosResponse<any>) => {
        const brands: Specification<string>[] = response.data.$values;
        return brands;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getTVBrands(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.tvsApiUrl}/Brands`)).pipe(
      map((response: AxiosResponse<any>) => {
        const brands: Specification<string>[] = response.data.$values;
        return brands;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getClothesSizes(IsChild: boolean, IsShoe: boolean): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.clothesApiUrl}/Sizes?IsChild=${IsChild}&IsShoe=${IsShoe}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const brands: Specification<string>[] = response.data.$values;
        return brands;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getGenders(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.clothesApiUrl}/Genders`)).pipe(
      map((response: AxiosResponse<any>) => {
        const genders: Specification<string>[] = response.data.$values;
        return genders;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getClothesSeasons(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.clothesApiUrl}/Seasons`)).pipe(
      map((response: AxiosResponse<any>) => {
        const clothesSeasons: Specification<string>[] = response.data.$values;
        return clothesSeasons;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getClothesViews(IsChild: boolean | null = null, GenderId: string | null = null, ClothesTypeId: string | null = null): Observable<Specification<string>[]>{
    let url = `${this.apiUrl}/${this.clothesApiUrl}/Views`
    if(GenderId != null && ClothesTypeId != null){
      url += `?GenderId=${GenderId}&ClothesTypeId=${ClothesTypeId}`;
    }
    else{
      url += GenderId != null ? `?GenderId=${GenderId}` : '';
      url += ClothesTypeId != null ? `?ClothesTypeId=${ClothesTypeId}` : '';
    }
    if (IsChild != null) {
      url += (GenderId != null || ClothesTypeId != null) ? `&IsChild=${IsChild}` : `?IsChild=${IsChild}`;
    }
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const clothesViews: Specification<string>[] = response.data.$values;
        return clothesViews;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getElectronicMemories(ModelId: string | null = null): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(ModelId ? `${this.apiUrl}/${this.electronicsApiUrl}/Memories?ModelId=${ModelId}` : `${this.apiUrl}/${this.electronicsApiUrl}/Memories`)).pipe(
      map((response: AxiosResponse<any>) => {
        const electronicMemories: Specification<string>[] = response.data.$values;
        return electronicMemories;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getElectronicColors(ModelId: string | null = null): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(ModelId ? `${this.apiUrl}/${this.electronicsApiUrl}/Colors?ModelId=${ModelId}` : `${this.apiUrl}/${this.electronicsApiUrl}/Colors`)).pipe(
      map((response: AxiosResponse<any>) => {
        const electronicColors: Specification<string>[] = response.data.$values;
        return electronicColors;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getElectronicModels(ElectronicBrandsId: string[] | null = null, ElectronicTypeId: string | null = null): Observable<Specification<string>[]>{
    let url = `${this.apiUrl}/${this.electronicsApiUrl}/Models`;
    if(ElectronicBrandsId != null && ElectronicTypeId != null){
      url += `?ElectronicTypeId=${ElectronicTypeId}`;
      url += ElectronicBrandsId.map(value => `&ElectronicBrandsId=${encodeURIComponent(value)}`).join("");
    }
    else{
      url += ElectronicTypeId != null ? `?ElectronicTypeId=${ElectronicTypeId}` : '';
      if(ElectronicBrandsId != null){
        url += "?";
        url += ElectronicBrandsId.map(value => `ElectronicBrandsId=${encodeURIComponent(value)}`).join("&");
      }
    }
    return from(this.axiosInterceptorService.get(url)).pipe(
      map((response: AxiosResponse<any>) => {
        const electronicModels: Specification<string>[] = response.data.$values;
        return electronicModels;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getTVTypes(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.tvsApiUrl}/Types`)).pipe(
      map((response: AxiosResponse<any>) => {
        const tvTypes: Specification<string>[] = response.data.$values;
        return tvTypes;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getTVScreenResolutions(): Observable<Specification<string>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.tvsApiUrl}/ScreenResolutions`)).pipe(
      map((response: AxiosResponse<any>) => {
        const screenResolutions: Specification<string>[] = response.data.$values;
        return screenResolutions;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getTVScreenDiagonals(): Observable<Specification<number>[]>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.tvsApiUrl}/ScreenDiagonals`)).pipe(
      map((response: AxiosResponse<any>) => {
        const screenDiagonals: Specification<number>[] = response.data.$values;
        return screenDiagonals;
      }),
      catchError((error: AxiosError) => {
        return EMPTY;
      })
    );
  }

  getClothesBrandViewId(brandId: string, clothesViewId: string): Observable<string>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.clothesApiUrl}/GetClothesBrandViewId?BrandId=${brandId}&ClothesViewId=${clothesViewId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const clothesBrandViewId: string = response.data;
        return clothesBrandViewId;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getMemoryModelId(memoryId: string, modelId: string): Observable<string>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.electronicsApiUrl}/GetMemoryModelId?MemoryId=${memoryId}&ModelId=${modelId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const memoryModelId: string = response.data;
        return memoryModelId;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  getModelColorId(colorId: string, modelId: string): Observable<string>{
    return from(this.axiosInterceptorService.get(`${this.apiUrl}/${this.electronicsApiUrl}/GetModelColorId?ColorId=${colorId}&ModelId=${modelId}`)).pipe(
      map((response: AxiosResponse<any>) => {
        const modelColorId: string = response.data;
        return modelColorId;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createAnimal(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.animalsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createAuto(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.autosApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createClothes(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.clothesApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createElectronics(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.electronicsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createItem(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.itemsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createRealEstate(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.realEstatesApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  createTv(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.post(`${this.apiUrl}/${this.tvsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateAnimal(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.animalsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateAuto(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.autosApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateClothes(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.clothesApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateElectronics(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.electronicsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateItem(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.itemsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateRealEstate(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.realEstatesApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  updateTv(data: FormData): Observable<void>{
    return from(this.axiosInterceptorService.put(`${this.apiUrl}/${this.tvsApiUrl}`, data)).pipe(
      map((response: AxiosResponse<any>) => {
        return response.data;
      }),
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  switchProductEnabling(productId: string): Observable<any>{
    return from(this.axiosInterceptorService.patch<any>(`${this.apiUrl}/${this.productsApiUrl}/Enabling/${productId}`)).pipe(
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  removeProduct(productId: string): Observable<any>{
    return from(this.axiosInterceptorService.delete<any>(`${this.apiUrl}/${this.productsApiUrl}/${productId}`)).pipe(
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }

  incrementViewsAsync(productId: string): Observable<any>{
    return from(this.axiosInterceptorService.patch<any>(`${this.apiUrl}/${this.productsApiUrl}/Views/${productId}`)).pipe(
      catchError((error: AxiosError) => {
        throw error;
      })
    );
  }
}


