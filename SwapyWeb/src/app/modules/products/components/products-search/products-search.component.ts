import { AfterViewInit, ChangeDetectorRef, Component, HostListener, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { environment } from 'src/environments/environment';
import { PageResponse } from 'src/app/core/models/page-response.interface';
import { CategoryNode } from 'src/app/core/models/category-node.interface';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { ProductApiService } from '../../services/product-api.service';
import { Specification } from 'src/app/core/models/specification';
import { Observable, forkJoin, map } from 'rxjs';
import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { CurrencyResponse } from 'src/app/core/models/currency-response.interface';
import { CheckboxItem } from '../../models/checkbox-item.class';
import { SubcategoryType } from 'src/app/core/enums/subcategory-type.enum';
import { AutoResponse } from '../../models/auto-response.interface';
import { RealEstateResponse } from '../../models/real-estate-response.interface';
import { ActivatedRoute } from '@angular/router';
import { ProductsSearchService } from './products-search.service';

@Component({
  selector: 'app-products-search',
  templateUrl: './products-search.component.html',
  styleUrls: ['./products-search.component.scss']
})
export class ProductsSearchComponent implements OnInit, AfterViewInit {
  suitableProducts!: Product[];
  allPages: number | null = 0;
  currentPage: number = 0;
  suitableProductsCount: number | null = 0;
  pageSize: number = 10;
  isLoadingProducts: boolean = true;
  isNotFoundProducts: boolean = false;

  isRangeSliderMouseDown: boolean = false;

  selectedFilter: string = '1';
  sortByPrice: boolean = false;
  reverseSort: boolean = true;

  categories!: CategoryNode[];
  selectedCategoryId!: string;
  subcategoriesHierarchy: CategoryNode[][] = [];
  selectedSubcategoriesId: string[] = [];
  currentSubcategoryNesting: number = -1;

  currencies!: CurrencyResponse[];
  selectedCurrencyId!: string;
  selectedCurrencySymbol!: string;

  minPrice!: number;
  maxPrice!: number;
  priceSliderOptions: any = {
    floor: NaN, 
    ceil: NaN, 
    step: 1
  };

  cities!: Specification<string>[];
  selectedCityId!: string;

  breeds!: CheckboxItem<Specification<string>>[];

  selectedIsNewFilter: number = 0;

  minMiliage!: number;
  maxMiliage!: number;
  miliageSliderOptions: any = {
    floor: NaN, 
    ceil: NaN, 
    step: 1
  };

  minEngineCapacity!: number;
  maxEngineCapacity!: number;
  engineCapacitySliderOptions: any = {
    floor: NaN, 
    ceil: NaN, 
    step: 1
  };

  olderReleaseYear!: number;
  newerReleaseYear!: number;
  releaseYearSliderOptions: any= {
    floor: NaN, 
    ceil: NaN, 
    step: 1
  };

  fuelTypes!: CheckboxItem<Specification<string>>[];

  colors!: CheckboxItem<Specification<string>>[];

  transmissionTypes!: CheckboxItem<Specification<string>>[];

  brands!: CheckboxItem<Specification<string>>[];

  private _clotheIsShoe: boolean = false;

  get clotheIsShoe(): boolean  { return this._clotheIsShoe; }

  set clotheIsShoe(value: boolean) {
    this._clotheIsShoe = value;
    forkJoin([
      this.productApiService.getClothesSizes(this.selectedClothesTypeFilter == -1, this.clotheIsShoe)
    ]).subscribe(
      ([clothesSizes]: [Specification<string>[]]) => {
        this.clothesSizes = clothesSizes.map(item => new CheckboxItem<Specification<string>>(item));
        this.spinnerService.changeSpinnerState(false);
        return;
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
        return;
      }
    );
  }

  selectedClothesTypeFilter: number = 0;

  clothesSizes!: CheckboxItem<Specification<string>>[];

  genders!: Specification<string>[];
  selectedGenderId!: string;

  clothesSeasons!: CheckboxItem<Specification<string>>[];

  clothesViews!: Specification<string>[];
  selectedClothesViewId!: string;

  memories!: CheckboxItem<Specification<string>>[];

  models!: Specification<string>[];
  selectedModelId!: string;

  minArea!: number;
  maxArea!: number;
  areaSliderOptions: any = {
    floor: NaN, 
    ceil: NaN, 
    step: 1
  };

  minRooms!: number;
  maxRooms!: number;
  roomsSliderOptions: any = {
    floor: NaN, 
    ceil: NaN, 
    step: 1
  };

  selectedIsRentFilter: number = 0;

  tvTypes!: CheckboxItem<Specification<string>>[];

  selectedIsSmartFilter: number = 0;

  screenResolutions!: CheckboxItem<Specification<string>>[];

  screenDiagonals!: CheckboxItem<Specification<number>>[];

  selectedTitle: string | null = null;

  private _selectedCategoryType!: CategoryType | undefined;

  get selectedCategoryType(): CategoryType | undefined { return this._selectedCategoryType; }

  set selectedCategoryType(value: CategoryType | undefined) {
    this.spinnerService.changeSpinnerState(true);
    this.clearSelectionsInAdditionalFilters();
    switch(value){
      case CategoryType.AnimalsType: {
        this.sharedApiService.getBreeds(this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null).subscribe((response : Specification<string>[]) => {
          this.breeds = response.map(breed => new CheckboxItem<Specification<string>>(breed));
          this._selectedCategoryType = value;
          this.spinnerService.changeSpinnerState(false);
          this.loadSuitableProducts(true);
          return;
        })
        break;
      }
      case CategoryType.AutosType: {
        forkJoin([
          this.productApiService.getFuelTypes(),
          this.productApiService.getColors(),
          this.productApiService.getTransmissionTypes(),
          this.productApiService.getAutoBrands(this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null),
          this.productApiService.getAutoModels(this.brands !== undefined ? (this.brands.filter(brand => brand.selected).map(brand => brand.value.id).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null) : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null)
        ]).subscribe(
          ([fuelTypes, colors, transmissionTypes, brands, models]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.fuelTypes = fuelTypes.map(item => new CheckboxItem<Specification<string>>(item));
            this.colors = colors.map(item => new CheckboxItem<Specification<string>>(item));
            this.transmissionTypes = transmissionTypes.map(item => new CheckboxItem<Specification<string>>(item));
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.models = models;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadSuitableProducts(true);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ClothesType: {
        forkJoin([
          this.productApiService.getClotheBrands(this.selectedClothesViewId !== 'undefined' ? [this.selectedClothesViewId] : null),
          this.productApiService.getClothesSizes(this.selectedClothesTypeFilter == -1, this.clotheIsShoe),
          this.productApiService.getGenders(),
          this.productApiService.getClothesSeasons(),
          this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== 'undefined' ? this.selectedGenderId : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
        ]).subscribe(
          ([brands, clothesSizes, genders, clothesSeasons, clothesViews]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.clothesSizes = clothesSizes.map(item => new CheckboxItem<Specification<string>>(item));
            this.genders = genders;
            this.clothesSeasons = clothesSeasons.map(item => new CheckboxItem<Specification<string>>(item));
            this.clothesViews = clothesViews;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadSuitableProducts(true);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        forkJoin([
          this.productApiService.getElectronicColors(this.selectedModelId !== 'undefined' ? this.selectedModelId : null),
          this.productApiService.getElectronicBrands(this.selectedSubcategoriesId.length > 0 && this.selectedSubcategoriesId[this.selectedSubcategoriesId.length - 1] != 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null),
          this.productApiService.getElectronicMemories(this.selectedModelId !== 'undefined' ? this.selectedModelId : null),
          this.productApiService.getElectronicModels(this.brands !== undefined ? (this.brands.filter(brand => brand.selected).map(brand => brand.value.id).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null) : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
        ]).subscribe(
          ([colors, brands, memories, models]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.colors = colors.map(item => new CheckboxItem<Specification<string>>(item));
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.memories = memories.map(item => new CheckboxItem<Specification<string>>(item));
            this.models = models;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadSuitableProducts(true);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ItemsType: {
        this._selectedCategoryType = value;
        this.spinnerService.changeSpinnerState(false);
        this.loadSuitableProducts(true);
        return;
        break;
      }
      case CategoryType.RealEstatesType: {
        this._selectedCategoryType = value;
        this.spinnerService.changeSpinnerState(false);
        this.loadSuitableProducts(true);
        return;
        break;
      }
      case CategoryType.TVsType: {
        forkJoin([
          this.productApiService.getTVBrands(),
          this.productApiService.getTVTypes(),
          this.productApiService.getTVScreenResolutions(),
          this.productApiService.getTVScreenDiagonals()
        ]).subscribe(
          ([brands, tvTypes, screenResolutions, screenDiagonals]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<number>[]]) => {
            this.brands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.tvTypes = tvTypes.map(item => new CheckboxItem<Specification<string>>(item));
            this.screenResolutions = screenResolutions.map(item => new CheckboxItem<Specification<string>>(item));
            this.screenDiagonals = screenDiagonals.map(item => new CheckboxItem<Specification<number>>(item));
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadSuitableProducts(true);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      default: {
        this._selectedCategoryType = undefined;
        this.spinnerService.changeSpinnerState(false);
        this.loadSuitableProducts(true);
        return;
        break;
      }
    }
  }

  isUpdateProductsButtonDisabled: boolean = true;

  isCategoryChanged: boolean = false;

  constructor(private productsSearchService: ProductsSearchService, private productApiService: ProductApiService, private sharedApiService : SharedApiService, private spinnerService: SpinnerService, private route: ActivatedRoute) {
    this.productsSearchService.setCategoryTreeComponent(this);
    this.spinnerService.changeSpinnerState(true);
  }
  
  ngOnInit(): void {
    this.spinnerService.changeSpinnerState(true);
    this.minPrice = this.priceSliderOptions.floor;
    this.maxPrice = this.priceSliderOptions.ceil;
    this.minMiliage = this.miliageSliderOptions.floor;
    this.maxMiliage = this.miliageSliderOptions.ceil;
    this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
    this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
    this.olderReleaseYear = this.releaseYearSliderOptions.floor;
    this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
    this.minArea = this.areaSliderOptions.floor;
    this.maxArea = this.areaSliderOptions.ceil;
    this.minRooms = this.roomsSliderOptions.floor;
    this.maxRooms = this.roomsSliderOptions.ceil;

    forkJoin([
      this.sharedApiService.getCategories(),
      this.sharedApiService.getCurrencies(),
      this.sharedApiService.GetCities()
    ]).subscribe(
      async ([categories, currencies, cities]: [CategoryNode[], CurrencyResponse[], Specification<string>[]]) => {
        this.categories = categories;
        this.currencies = currencies;
        this.selectedCurrencyId = currencies[0].id;
        this.selectedCurrencySymbol = currencies[0].symbol;
        this.cities = cities;

        this.route.queryParams.subscribe(params => {
          this.selectedTitle = params['title'];
          this.selectedFilter = params['sort'] ? params['sort'] : this.selectedFilter;
          this.onSelectSortChange(false);
          this.selectedCurrencyId = params['currency'] ? params['currency'] : this.selectedCurrencyId;
          this.onSelectCurrencyChange(false);
          this.minPrice = params['minPrice'] ? params['minPrice'] : this.minPrice;
          this.maxPrice = params['maxPrice'] ? params['maxPrice'] : this.maxPrice;
          this.selectedCityId = params['city'] ? params['city'] : this.selectedCityId;
        });
        
        let sentCategory : string | undefined;
        this.route.queryParams.subscribe(params => {
          sentCategory = params['category'];
        });
        
        let sentSubcategory : string | undefined;
        this.route.queryParams.subscribe(params => {
          sentSubcategory = params['subcategory'];
        });
        
        if(sentSubcategory != undefined){
          this.sharedApiService.getSubcategoryPath(sentSubcategory).subscribe(
            (mainResponse: Specification<string>[]) => {
              this.selectedCategoryId = mainResponse.shift()?.id as string;
              this.subcategoriesHierarchy.splice(-this.currentSubcategoryNesting - 1);
              this.selectedSubcategoriesId.splice(-this.currentSubcategoryNesting);
              this.currentSubcategoryNesting = -1;
              if(this.selectedCategoryId == 'undefined'){
                this.selectedCategoryType = undefined;
                this.spinnerService.changeSpinnerState(false);
                this.loadSuitableProducts(true);
                return;
              }
              this.sharedApiService.GetSubcategoriesByCategoryAsync(this.selectedCategoryId).subscribe(
                (response: CategoryNode[]) => {
                  this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;
                  
                  let subcategoriesThreads : Observable<CategoryNode[]>[] = [];

                  let subcategoriesBySubcategoryIndex = 0;
                  while (mainResponse.length > 0) {
                    let currentSubcategoryId = mainResponse.shift()?.id as string;
                    this.selectedSubcategoriesId.push(currentSubcategoryId); 
                    subcategoriesThreads.push(this.sharedApiService.GetSubcategoriesBySubcategoryAsync(this.selectedSubcategoriesId[subcategoriesBySubcategoryIndex++]));
                  }

                  forkJoin(subcategoriesThreads.map((observable, index) => observable.pipe(map(response => ({ index, response })))))
                  .subscribe(
                    (results: { index: number, response: CategoryNode[] }[]) => {
                      results.forEach(result => {
                        const index = result.index;
                        const response = result.response;
                        
                        if(this.selectedSubcategoriesId[index] == undefined || this.selectedSubcategoriesId[index] == 'undefined'){
                          return;
                        }
                    
                        
                        if(this.subcategoriesHierarchy[index].find(item => item.id === this.selectedSubcategoriesId[index])?.isFinal){
                          ++this.currentSubcategoryNesting;
                          return;
                        }
                        
                        this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;
                      });
                      this.spinnerService.changeSpinnerState(false);

                      this.onChangeSubcategory(true);

                      return;
                    },
                    error => {
                      this.subcategoriesHierarchy.splice(-this.currentSubcategoryNesting - 1);
                      this.selectedSubcategoriesId.splice(-this.currentSubcategoryNesting);
                      this.currentSubcategoryNesting = 0;
                      this.spinnerService.changeSpinnerState(false);
                      this.onSelectCategoryChange();
                      return;
                    }
                  );
                }
              );       
            }
          );
        }
        else if(sentCategory != undefined && categories.map(c => c.id).includes(sentCategory)){
          this.selectedCategoryId = sentCategory;
          this.subcategoriesHierarchy.splice(-this.currentSubcategoryNesting - 1);
          this.selectedSubcategoriesId.splice(-this.currentSubcategoryNesting);
          this.currentSubcategoryNesting = -1;
          if(this.selectedCategoryId == 'undefined'){
            this.selectedCategoryType = undefined;
            this.spinnerService.changeSpinnerState(false);
            this.loadSuitableProducts(true);
            return;
          }
          else{
            this.selectedCategoryType = this.categories.find(c => c.id.toLowerCase() === this.selectedCategoryId.toLowerCase())?.type;
          }
          this.sharedApiService.GetSubcategoriesByCategoryAsync(this.selectedCategoryId).subscribe(
            (response: CategoryNode[]) => {
              this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          );       
        }
        else{
          this.spinnerService.changeSpinnerState(false);
          this.loadSuitableProducts(true);
        }
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
      }
    );
  }
    
  ngAfterViewInit(): void {}

  changeUrlByFilterParams(): void {
    const currentURL = new URL(window.location.href);
    const searchParams = new URLSearchParams(currentURL.search);
  
    if(this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined') {
      searchParams.delete('category');
      searchParams.set('subcategory', this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1])
    }
    else if(this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined') {
      searchParams.delete('subcategory');
      searchParams.set('category', this.selectedCategoryId)
    }
    else {
      searchParams.delete('category');
      searchParams.delete('subcategory');
    }
    this.selectedTitle ? searchParams.set('title', this.selectedTitle) : searchParams.delete('title');
    this.selectedFilter ? searchParams.set('sort', this.selectedFilter) : searchParams.delete('sort');
    this.selectedCurrencyId ? searchParams.set('currency', this.selectedCurrencyId) : searchParams.delete('currency');
    this.minPrice ? searchParams.set('minPrice', this.minPrice.toString()) : searchParams.delete('minPrice');
    this.maxPrice ? searchParams.set('maxPrice', this.maxPrice.toString()) : searchParams.delete('maxPrice');
    this.selectedCityId && this.selectedCityId != 'undefined' ? searchParams.set('city', this.selectedCityId.toString()) : searchParams.delete('city');

    currentURL.search = searchParams.toString();
    window.history.replaceState({}, '', currentURL.toString());
  }

  onChangeFilter(): void {
    this.isUpdateProductsButtonDisabled = false;
  }

  onSelectSortChange(loadProducts: boolean = true): void{
    switch(this.selectedFilter) {
      case '1':{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
      case '2':{
        this.sortByPrice = false;
        this.reverseSort = false;
        break;
      }
      case '3':{
        this.sortByPrice = true;
        this.reverseSort = false;
        break;
      }
      case '4':{
        this.sortByPrice = true;
        this.reverseSort = true;
        break;
      }
      default:{
        this.sortByPrice = false;
        this.reverseSort = true;
        break;
      }
    }
    if(loadProducts){ this.onChangeFilter(); } 
  }

  loadSuitableProducts(isNewRequest: boolean = false): void {
    this.isCategoryChanged = false;
    this.spinnerService.changeSpinnerState(true);
    this.isUpdateProductsButtonDisabled = true;
    this.isLoadingProducts = true;
    this.changeUrlByFilterParams();
    if(isNewRequest){
      this.suitableProductsCount = null;
      this.allPages = null;
      this.suitableProducts = [];
      this.currentPage = 1;
      this.isNotFoundProducts = false;
    }
    else { this.currentPage++; }

    switch(this.selectedCategoryType){
      case CategoryType.AnimalsType: {   
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredAnimals(
          this.currentPage,
          this.pageSize,
          this.sortByPrice,
          this.reverseSort,
          this.selectedTitle ? this.selectedTitle : null,
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null,
          !Number.isNaN(this.minPrice) ? this.minPrice : null,
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.breeds.filter(breed => breed.selected).length > 0 ? this.breeds.filter(breed => breed.selected).map(breed => breed.value.id) : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      case CategoryType.AutosType: {
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        var tempOlderReleaseYearDate: Date =  new Date();
        tempOlderReleaseYearDate.setFullYear(this.olderReleaseYear, 0, 1);
        var tempNewerReleaseYearDate: Date =  new Date();
        tempNewerReleaseYearDate.setFullYear(this.newerReleaseYear + 1, 0, 1);
        tempNewerReleaseYearDate.setDate(tempNewerReleaseYearDate.getDate() - 1);

        this.productApiService.getFilteredAutos(
          this.currentPage, 
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, 
          !Number.isNaN(this.minMiliage) ? this.minMiliage : null, 
          !Number.isNaN(this.maxMiliage) ? this.maxMiliage : null, 
          !Number.isNaN(this.minEngineCapacity) ? this.minEngineCapacity : null, 
          !Number.isNaN(this.maxEngineCapacity) ? this.maxEngineCapacity : null, 
          !Number.isNaN(this.olderReleaseYear) ? tempOlderReleaseYearDate : null, 
          !Number.isNaN(this.newerReleaseYear) ? tempNewerReleaseYearDate : null, 
          this.fuelTypes.filter(fuelType => fuelType.selected).length > 0 ? this.fuelTypes.filter(fuelType => fuelType.selected).map(fuelType => fuelType.value.id) : null, 
          this.colors.filter(color => color.selected).length > 0 ? this.colors.filter(color => color.selected).map(color => color.value.id) : null, 
          this.transmissionTypes.filter(transmissionType => transmissionType.selected).length > 0 ? this.transmissionTypes.filter(transmissionType => transmissionType.selected).map(transmissionType => transmissionType.value.id) : null, 
          this.brands.filter(brand => brand.selected).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null).subscribe((response: AutoResponse) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          console.log(response);
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          this.miliageSliderOptions = {
            floor: response.minMiliage, 
            ceil: response.maxMiliage, 
            step: 1
          };
          this.minMiliage = !Number.isNaN(this.minMiliage) ? this.minMiliage : this.miliageSliderOptions.floor;
          this.maxMiliage = !Number.isNaN(this.maxMiliage) ? this.maxMiliage : this.miliageSliderOptions.ceil;
          this.engineCapacitySliderOptions = {
            floor: response.minEngineCapacity, 
            ceil: response.maxEngineCapacity, 
            step: 1
          };
          this.minEngineCapacity = !Number.isNaN(this.minEngineCapacity) ? this.minEngineCapacity : this.engineCapacitySliderOptions.floor;
          this.maxEngineCapacity = !Number.isNaN(this.maxEngineCapacity) ? this.maxEngineCapacity : this.engineCapacitySliderOptions.ceil;
          this.releaseYearSliderOptions = {
            floor: response.newerReleaseYear, 
            ceil: response.olderReleaseYear, 
            step: 1
          };
          this.olderReleaseYear = !Number.isNaN(this.olderReleaseYear) ? this.olderReleaseYear : this.releaseYearSliderOptions.floor;
          this.newerReleaseYear = !Number.isNaN(this.newerReleaseYear) ? this.newerReleaseYear : this.releaseYearSliderOptions.ceil;
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      case CategoryType.ClothesType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredClothes(
          this.currentPage,
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, 
          this.clothesSeasons.filter(clothesSeason => clothesSeason.selected).length > 0 ? this.clothesSeasons.filter(clothesSeason => clothesSeason.selected).map(clothesSeason => clothesSeason.value.id) : null, 
          this.clothesSizes.filter(clothesSize => clothesSize.selected).length > 0 ? this.clothesSizes.filter(clothesSize => clothesSize.selected).map(clothesSize => clothesSize.value.id) : null,
          this.brands.filter(brand => brand.selected).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null,
          this.selectedClothesViewId !== 'undefined' ? [this.selectedClothesViewId] : null, 
          null, 
          this.selectedGenderId !== 'undefined' ? [this.selectedGenderId] : null,
          this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      case CategoryType.ElectronicsType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredElectronics(
          this.currentPage, 
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, 
          this.memories.filter(memory => memory.selected).length > 0 ? this.memories.filter(memory => memory.selected).map(memory => memory.value.id) : null, 
          this.colors.filter(color => color.selected).length > 0 ? this.colors.filter(color => color.selected).map(color => color.value.id) : null, 
          this.selectedModelId !== 'undefined' ? [this.selectedModelId] : null, 
          this.brands.filter(brand => brand.selected).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      case CategoryType.ItemsType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;
        this.productApiService.getFilteredItems(
          this.currentPage, 
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      case CategoryType.RealEstatesType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;

        this.productApiService.getFilteredRealEstates(
          this.currentPage,
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.selectedIsRentFilter !== 0 ? this.selectedIsRentFilter === 1 : null, 
          !Number.isNaN(this.maxArea) ? this.maxArea : null,
          !Number.isNaN(this.minArea) ? this.minArea : null,
          !Number.isNaN(this.minRooms) ? this.minRooms : null,
          !Number.isNaN(this.maxRooms) ? this.maxRooms : null).subscribe((response: RealEstateResponse) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          this.areaSliderOptions = {
            floor: response.minArea, 
            ceil: response.maxArea, 
            step: 1
          };
          this.minArea = !Number.isNaN(this.minArea) ? this.minArea : this.areaSliderOptions.floor;
          this.maxArea = !Number.isNaN(this.maxArea) ? this.maxArea : this.areaSliderOptions.ceil;
          this.roomsSliderOptions = {
            floor: response.minRooms, 
            ceil: response.maxRooms, 
            step: 1
          };
          this.minRooms = !Number.isNaN(this.minRooms) ? this.minRooms : this.roomsSliderOptions.floor;
          this.maxRooms = !Number.isNaN(this.maxRooms) ? this.maxRooms : this.roomsSliderOptions.ceil;
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      case CategoryType.TVsType: {
        this.minMiliage = this.miliageSliderOptions.floor;
        this.maxMiliage = this.miliageSliderOptions.ceil;
        this.minEngineCapacity = this.engineCapacitySliderOptions.floor;
        this.maxEngineCapacity = this.engineCapacitySliderOptions.ceil;
        this.olderReleaseYear = this.releaseYearSliderOptions.floor;
        this.newerReleaseYear = this.releaseYearSliderOptions.ceil;
        this.minArea = this.areaSliderOptions.floor;
        this.maxArea = this.areaSliderOptions.ceil;
        this.minRooms = this.roomsSliderOptions.floor;
        this.maxRooms = this.roomsSliderOptions.ceil;

        this.productApiService.getFilteredTVs(
          this.currentPage, 
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null, 
          this.selectedIsNewFilter !== 0 ? this.selectedIsNewFilter === 1 : null, 
          this.selectedIsSmartFilter !== 0 ? this.selectedIsSmartFilter === 1 : null, 
          this.tvTypes.filter(tvType => tvType.selected).length > 0 ? this.tvTypes.filter(tvType => tvType.selected).map(tvType => tvType.value.id) : null, 
          this.brands.filter(brand => brand.selected).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null, 
          this.screenResolutions.filter(screenResolution => screenResolution.selected).length > 0 ? this.screenResolutions.filter(screenResolution => screenResolution.selected).map(screenResolution => screenResolution.value.id) : null, 
          this.screenDiagonals.filter(screenDiagonal => screenDiagonal.selected).length > 0 ? this.screenDiagonals.filter(screenDiagonal => screenDiagonal.selected).map(screenDiagonal => screenDiagonal.value.id) : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
      default: {
        this.productApiService.getFilteredProducts(
          this.currentPage, 
          this.pageSize, 
          this.sortByPrice, 
          this.reverseSort, 
          this.selectedTitle ? this.selectedTitle : null, 
          this.selectedCurrencyId !== 'undefined' ? this.selectedCurrencyId : null, 
          !Number.isNaN(this.minPrice) ? this.minPrice : null, 
          !Number.isNaN(this.maxPrice) ? this.maxPrice : null, 
          this.currentSubcategoryNesting >= 0 && this.selectedCategoryId !== 'undefined' ? this.selectedCategoryId : null, 
          this.currentSubcategoryNesting > 0 && this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null, 
          this.selectedCityId !== 'undefined' ? this.selectedCityId : null).subscribe((response: PageResponse<Product>) => { 
          response.items.forEach(item => {
            if (Array.isArray(item.images)) {
              item.images = item.images.map((image) => `${environment.blobUrl}/product-images/${image}`);
            }
          });
          this.suitableProductsCount = response.count;
          this.allPages = response.allPages;
          this.priceSliderOptions = {
            floor: response.minPrice, 
            ceil: response.maxPrice, 
            step: 1
          };
          this.route.queryParams.subscribe(params => {
            this.minPrice = !Number.isNaN(this.minPrice) ? this.minPrice : params['minPrice'] ? params['minPrice'] : this.priceSliderOptions.floor;
            this.maxPrice = !Number.isNaN(this.maxPrice) ? this.maxPrice : params['maxPrice'] ? params['maxPrice'] : this.priceSliderOptions.ceil;
          });
          if(this.suitableProducts != null) { this.suitableProducts.push(...response.items); }
          else { this.suitableProducts = response.items; }
          this.isLoadingProducts = false;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.isLoadingProducts = false;
          this.isNotFoundProducts = true;
          this.spinnerService.changeSpinnerState(false);
        });
        break;
      }
    }
  }

  trackByProductId(index: number, product: Product): string {
    return product.id;
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }

  filterBlockCollapse(event: MouseEvent): void {
    let thisObj = event.currentTarget as HTMLElement;
    const dataWidget = thisObj.getAttribute('dataWidget');
    const targetElement = jQuery(`#${dataWidget}`);

    if (thisObj.classList.contains('filter-title-container-collapse')) {
        thisObj.classList.remove('filter-title-container-collapse');
        targetElement.removeClass("collapse");
    } else {
        thisObj.classList.add('filter-title-container-collapse');
        targetElement.addClass("collapse");
    }
  }

  onSelectCategoryChange(clearSubcategories: boolean = true): void {
    this.isCategoryChanged = true;
    this.priceSliderOptions = {
      floor: NaN, 
      ceil: NaN, 
      step: 1
    };
    this.minPrice = NaN;
    this.maxPrice = NaN;
    if(clearSubcategories) {
      this.subcategoriesHierarchy.splice(-this.currentSubcategoryNesting - 1);
      this.selectedSubcategoriesId.splice(-this.currentSubcategoryNesting);
      this.currentSubcategoryNesting = -1;
    }
    if(this.selectedCategoryId == 'undefined'){
      this.selectedCategoryType = undefined;
      this.onChangeFilter();
      return;
    }
    else{
      this.selectedCategoryType = this.categories.find(c => c.id.toLowerCase() === this.selectedCategoryId.toLowerCase())?.type;
      this.onChangeFilter();
    }

    if(clearSubcategories) {
      this.spinnerService.changeSpinnerState(true);
      this.sharedApiService.GetSubcategoriesByCategoryAsync(this.selectedCategoryId).subscribe(
        (response: CategoryNode[]) => {
          this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;
          this.spinnerService.changeSpinnerState(false);
        },
        (error) => {
          this.spinnerService.changeSpinnerState(false);
        }
      );
    }
  }

  onSelectSubcategoryChange(index: number): void {
    if(-this.currentSubcategoryNesting + index < 0){
      this.subcategoriesHierarchy.splice((index + 1), this.subcategoriesHierarchy.length - (index + 1));
      this.selectedSubcategoriesId.splice((index + 1), this.selectedSubcategoriesId.length - (index + 1));
      this.currentSubcategoryNesting = index;
    }
    if(this.selectedSubcategoriesId[index] == undefined || this.selectedSubcategoriesId[index] == 'undefined'){
      if(!this.isCategoryChanged) { this.onChangeSubcategory(); }
      return;
    }

    if(this.subcategoriesHierarchy[index].find(item => item.id === this.selectedSubcategoriesId[index])?.isFinal){
      ++this.currentSubcategoryNesting;
      if(!this.isCategoryChanged) { this.onChangeSubcategory(); }
      return;
    }
    this.spinnerService.changeSpinnerState(true);
    this.sharedApiService.GetSubcategoriesBySubcategoryAsync(this.selectedSubcategoriesId[index]).subscribe(
      (response: CategoryNode[]) => {
        this.subcategoriesHierarchy[++this.currentSubcategoryNesting] = response;     
        this.clotheIsShoe = this.subcategoriesHierarchy[index].find(item => item.id === this.selectedSubcategoriesId[index])?.subType === SubcategoryType.Shoe;
        if(!this.isCategoryChanged) { this.onChangeSubcategory(); }
        this.spinnerService.changeSpinnerState(false);
      },
      (error) => {
        this.spinnerService.changeSpinnerState(false);
        this.onChangeFilter();
      }
    );
  }

  onChangeSubcategory(loadProducts: boolean = false): void {
    this.spinnerService.changeSpinnerState(true);
    var tempType = this.categories.find(c => c.id.toLowerCase() === this.selectedCategoryId.toLowerCase())?.type
    switch(tempType){
      case CategoryType.AnimalsType: {
        this.sharedApiService.getBreeds(this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null).subscribe((response : Specification<string>[]) => {
          let newBreeds = response.map(item => new CheckboxItem<Specification<string>>(item));
          this.breeds = this.breeds ? newBreeds.map(newItem => ({
            ...newItem,
            selected: this.breeds.find(oldItem => oldItem.value.id === newItem.value.id)?.selected || newItem.selected
          })) : newBreeds;
          this.spinnerService.changeSpinnerState(false);
          loadProducts ? this.onSelectCategoryChange(false) : this.onChangeFilter();
        },
        (error) => {
          this.spinnerService.changeSpinnerState(false);
          loadProducts ? this.onSelectCategoryChange(false) : this.onChangeFilter();
        })
        break;
      }
      case CategoryType.AutosType: {
        forkJoin([
          this.productApiService.getAutoBrands(this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null),
        ]).subscribe(
          ([brands]: [Specification<string>[]]) => {
            let newBrands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.brands = this.brands ? newBrands.map(newItem => ({
              ...newItem,
              selected: this.brands.find(oldItem => oldItem.value.id === newItem.value.id)?.selected || newItem.selected
            })) : newBrands;
            this.spinnerService.changeSpinnerState(false);
            this.onSelectedBrandChange(loadProducts, true);
          },
          (error) => {
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? this.onSelectCategoryChange(false) : this.onChangeFilter();
          }
        );
        break;
      }
      case CategoryType.ClothesType: {
        this.clotheIsShoe = this.subcategoriesHierarchy[this.subcategoriesHierarchy.length - 1].find(item => item.id === this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1])?.subType === SubcategoryType.Shoe;
        forkJoin([
          this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== undefined ? this.selectedGenderId : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null),
        ]).subscribe(
          ([clothesViews]: [Specification<string>[]]) => {
            this.clothesViews = clothesViews;
            this.selectedClothesViewId = this.clothesViews.map(i => i.id).indexOf(this.selectedClothesViewId) !== -1 ? this.selectedClothesViewId : 'undefined';
            this.spinnerService.changeSpinnerState(false);
            this.onSelectedClothesViewChange(loadProducts, true);
          },
          error => {
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? this.onSelectCategoryChange(false) : this.onChangeFilter();
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        forkJoin([
          this.productApiService.getElectronicBrands(this.selectedSubcategoriesId.length > 0 && this.selectedSubcategoriesId[this.selectedSubcategoriesId.length - 1] != 'undefined' ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
        ]).subscribe(
          ([brands]: [Specification<string>[]]) => {
            let newBrands = brands.map(item => new CheckboxItem<Specification<string>>(item));
            this.brands = this.brands ? newBrands.map(newItem => ({
              ...newItem,
              selected: this.brands.find(oldItem => oldItem.value.id === newItem.value.id)?.selected || newItem.selected
            })) : newBrands;
            this.spinnerService.changeSpinnerState(false);
            this.onSelectedBrandChange(loadProducts, true);
          },
          error => {
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? this.onSelectCategoryChange(false) : this.onChangeFilter();
          }
        );
        break;
      }
      default: {
        this.spinnerService.changeSpinnerState(false);
        loadProducts ? this.onSelectCategoryChange(false) :this.onChangeFilter();
      }
    }
  }

  onSelectCurrencyChange(loadProducts: boolean = true): void {
    const currentURL = new URL(window.location.href);
    const searchParams = new URLSearchParams(currentURL.search);
    searchParams.delete('minPrice');
    searchParams.delete('maxPrice');
    currentURL.search = searchParams.toString();
    window.history.replaceState({}, '', currentURL.toString());
    this.selectedCurrencySymbol = this.currencies.find(currency => currency.id == this.selectedCurrencyId)?.symbol as string;
    this.minPrice = NaN;
    this.maxPrice = NaN;
    if(loadProducts){ this.loadSuitableProducts(true); } 
  }

  onSelectedModelChange(loadProducts: boolean = false, changeSubcategory: boolean = false): void {
    this.spinnerService.changeSpinnerState(true);
    var tempType: CategoryType | undefined = changeSubcategory ? this.categories.find(c => c.id.toLowerCase() === this.selectedCategoryId.toLowerCase())?.type : this.selectedCategoryType;

    switch (tempType) {
      case CategoryType.ElectronicsType: {
        forkJoin([
          this.productApiService.getElectronicColors(this.selectedModelId !== 'undefined' ? this.selectedModelId : null),
          this.productApiService.getElectronicMemories(this.selectedModelId !== 'undefined' ? this.selectedModelId : null)
        ]).subscribe(
          ([colors, memories]: [Specification<string>[], Specification<string>[]]) => {
            let newColors = colors.map(item => new CheckboxItem<Specification<string>>(item));
            this.colors = this.colors ? newColors.map(newItem => ({
              ...newItem,
              selected: this.colors.find(oldItem => oldItem.value.id === newItem.value.id)?.selected || newItem.selected
            })) : newColors;
            let newMemories = memories.map(item => new CheckboxItem<Specification<string>>(item));
            this.memories = this.memories ? newMemories.map(newItem => ({
              ...newItem,
              selected: this.memories.find(oldItem => oldItem.value.id === newItem.value.id)?.selected || newItem.selected
            })) : newMemories;
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? changeSubcategory ? this.onSelectCategoryChange(false) : this.loadSuitableProducts(true) : this.onChangeFilter();
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? this.loadSuitableProducts(true) :this.onChangeFilter();
            return;
          }
        );
        break;
      }
      default: {
        this.spinnerService.changeSpinnerState(false);
        loadProducts ? changeSubcategory ? this.onSelectCategoryChange(false) : this.loadSuitableProducts(true) : this.onChangeFilter();
        break;
      }
    }
  }

  onSelectedBrandChange(loadProducts: boolean = false, changeSubcategory: boolean = false): void {
    this.spinnerService.changeSpinnerState(true);
    var tempType: CategoryType | undefined = changeSubcategory ? this.categories.find(c => c.id.toLowerCase() === this.selectedCategoryId.toLowerCase())?.type : this.selectedCategoryType;

    switch (tempType) {
      case CategoryType.AutosType: {
        forkJoin([
          this.productApiService.getAutoModels(this.brands.filter(brand => brand.selected).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? [this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1]] : null)
        ]).subscribe(
          ([models]: [Specification<string>[]]) => {
            this.models = models;
            this.selectedModelId = this.models.map(i => i.id).indexOf(this.selectedModelId) !== -1 ? this.selectedModelId : 'undefined';
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? changeSubcategory ? this.onSelectCategoryChange(false) : this.loadSuitableProducts(true) : this.onChangeFilter();
          },
          (error) => {
            console.error('Error fetching data:', error);
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? this.loadSuitableProducts(true) :this.onChangeFilter();
            return;
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        forkJoin([
          this.productApiService.getElectronicModels(this.brands.filter(brand => brand.selected).length > 0 ? this.brands.filter(brand => brand.selected).map(brand => brand.value.id) : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
        ]).subscribe(
          ([models]: [Specification<string>[]]) => {
            this.models = models;
            this.selectedModelId = this.models.map(i => i.id).indexOf(this.selectedModelId) !== -1 ? this.selectedModelId : 'undefined';
            this.spinnerService.changeSpinnerState(false);
            this.onSelectedModelChange(loadProducts, changeSubcategory);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this.spinnerService.changeSpinnerState(false);
            loadProducts ? this.loadSuitableProducts(true) : this.onChangeFilter();
            return;
          }
          );
          break;
        }
        default: {
          this.spinnerService.changeSpinnerState(false);
          loadProducts ? changeSubcategory ? this.onSelectCategoryChange(false) : this.loadSuitableProducts(true) : this.onChangeFilter();
        //loadProducts ? this.loadSuitableProducts(true) :this.onChangeFilter();
      }
    }
  }

  onSelectedGenderChange(loadProducts: boolean = false, changeSubcategory: boolean = false): void {
    this.spinnerService.changeSpinnerState(true);
    forkJoin([
      this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== undefined ? this.selectedGenderId : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
    ]).subscribe(
      ([clothesViews]: [Specification<string>[]]) => {
        this.clothesViews = clothesViews;
        this.selectedClothesViewId = this.clothesViews.map(i => i.id).indexOf(this.selectedClothesViewId) !== -1 ? this.selectedClothesViewId : 'undefined';
        this.spinnerService.changeSpinnerState(false);
        this.onSelectedClothesViewChange(loadProducts, changeSubcategory);
        return;
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
        loadProducts ? this.loadSuitableProducts(true) : this.onChangeFilter();
        return;
      }
    );
  }

  onSelectedClothesTypeChange(loadProducts: boolean = false, changeSubcategory: boolean = false): void {
    this.spinnerService.changeSpinnerState(true);
    forkJoin([
      this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== undefined ? this.selectedGenderId : null, this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] !== undefined ? this.selectedSubcategoriesId[this.currentSubcategoryNesting - 1] : null)
    ]).subscribe(
      ([clothesViews]: [Specification<string>[]]) => {
        this.clothesViews = clothesViews;
        this.selectedClothesViewId = this.clothesViews.map(i => i.id).indexOf(this.selectedClothesViewId) !== -1 ? this.selectedClothesViewId : 'undefined';
        this.spinnerService.changeSpinnerState(false);
        this.onSelectedClothesViewChange(loadProducts, changeSubcategory);
        return;
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
        loadProducts ? this.loadSuitableProducts(true) : this.onChangeFilter();
        return;
      }
    );
  }

  onSelectedClothesViewChange(loadProducts: boolean = false, changeSubcategory: boolean = false): void {
    this.spinnerService.changeSpinnerState(true);
    forkJoin([
      this.productApiService.getClotheBrands(this.selectedClothesViewId !== 'undefined' ? [this.selectedClothesViewId] : null),
    ]).subscribe(
      ([brands]: [Specification<string>[]]) => {
        let newBrands = brands.map(item => new CheckboxItem<Specification<string>>(item));
        this.brands = this.brands ? newBrands.map(newItem => ({
          ...newItem,
          selected: this.brands.find(oldItem => oldItem.value.id === newItem.value.id)?.selected || newItem.selected
        })) : newBrands;
        this.spinnerService.changeSpinnerState(false);
        loadProducts ? changeSubcategory ? this.onSelectCategoryChange(false) : this.loadSuitableProducts(true) : this.onChangeFilter();
        return;
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
        loadProducts ? this.loadSuitableProducts(true) :this.onChangeFilter();
        return;
      }
    );
  }

  changeTitleFilter(newTitle: string | null): void {
    this.selectedTitle = newTitle;
    this.loadSuitableProducts(true);
  }

  clearSelectionsInAdditionalFilters(): void {
    this.breeds?.forEach(obj => { obj.selected = false; });
    this.selectedIsNewFilter = 0;
    this.minMiliage = NaN;
    this.maxMiliage = NaN;
    this.minEngineCapacity = NaN;
    this.maxEngineCapacity = NaN;
    this.fuelTypes?.forEach(obj => { obj.selected = false; });
    this.colors?.forEach(obj => { obj.selected = false; });
    this.transmissionTypes?.forEach(obj => { obj.selected = false; });
    this.brands?.forEach(obj => { obj.selected = false; });
    this.selectedClothesTypeFilter = 0;
    this.clothesSizes?.forEach(obj => { obj.selected = false; });
    this.selectedGenderId = 'undefined';
    this.clothesSeasons?.forEach(obj => { obj.selected = false; });
    this.selectedClothesViewId = 'undefined';
    this.memories?.forEach(obj => { obj.selected = false; });
    this.selectedModelId = 'undefined';
    this.minArea = NaN;
    this.maxArea = NaN;
    this.minRooms = NaN;
    this.maxRooms = NaN;
    this.selectedIsRentFilter = 0;
    this.tvTypes?.forEach(obj => { obj.selected = false; });
    this.selectedIsSmartFilter = 0;
    this.screenResolutions?.forEach(obj => { obj.selected = false; });
    this.screenDiagonals?.forEach(obj => { obj.selected = false; });
  }

  handlePositiveNumberInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    let value = inputElement.value.replace(/[^0-9]/g, '');
    value = parseInt(value, 10).toString();
    inputElement.value = value;
    switch(inputElement.id){
      case 'price-input-min': { this.minPrice = Number.isNaN(parseInt(value)) ? this.priceSliderOptions.floor : parseInt(value); break; }
      case 'price-input-max': { this.maxPrice = Number.isNaN(parseInt(value)) ? this.priceSliderOptions.floor : parseInt(value); break; }
      case 'miliage-input-min': { this.minMiliage = Number.isNaN(parseInt(value)) ? this.miliageSliderOptions.floor : parseInt(value); break; }
      case 'miliage-input-max': { this.maxMiliage = Number.isNaN(parseInt(value)) ? this.miliageSliderOptions.floor : parseInt(value); break; }
      case 'engine-capacity-input-min': { this.minEngineCapacity = Number.isNaN(parseInt(value)) ? this.engineCapacitySliderOptions.floor : parseInt(value); break; }
      case 'engine-capacity-input-max': { this.maxEngineCapacity = Number.isNaN(parseInt(value)) ? this.engineCapacitySliderOptions.floor : parseInt(value); break; }
      case 'release-year-input-min': { this.olderReleaseYear = Number.isNaN(parseInt(value)) ? this.releaseYearSliderOptions.floor : parseInt(value); break; }
      case 'release-year-input-max': { this.newerReleaseYear = Number.isNaN(parseInt(value)) ? this.releaseYearSliderOptions.floor : parseInt(value); break; }
      case 'area-input-min': { this.minArea = Number.isNaN(parseInt(value)) ? this.areaSliderOptions.floor : parseInt(value); break; }
      case 'area-input-max': { this.maxArea = Number.isNaN(parseInt(value)) ? this.areaSliderOptions.floor : parseInt(value); break; }
      case 'rooms-input-min': { this.minRooms = Number.isNaN(parseInt(value)) ? this.roomsSliderOptions.floor : parseInt(value); break; }
      case 'rooms-input-max': { this.maxRooms = Number.isNaN(parseInt(value)) ? this.roomsSliderOptions.floor : parseInt(value); break; }
    }
  }

  isRangeSliderMouseDownEvent(): void {
    this.isRangeSliderMouseDown = true;
  }

  @HostListener('document:mouseup', ['$event'])
  isRangeSliderMouseUpEvent(): void {
    if(this.isRangeSliderMouseDown){
      this.isRangeSliderMouseDown = false;
      this.onChangeFilter();
    }
  }
}
