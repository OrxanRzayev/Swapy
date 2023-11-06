import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ProductApiService } from '../../services/product-api.service';
import { SharedApiService } from 'src/app/modules/main/services/shared-api.service';
import { SpinnerService } from 'src/app/shared/spinner/spinner.service';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { CategoryType } from 'src/app/core/enums/category-type.enum';
import { Product } from '../../models/product.model';
import { Specification } from 'src/app/core/models/specification';
import { CurrencyResponse } from 'src/app/core/models/currency-response.interface';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { environment } from 'src/environments/environment';
import { HttpStatusCode } from 'axios';
import { ProductSubcategory } from '../../models/product-subcategory-response.interface';
import { SubcategoryType } from 'src/app/core/enums/subcategory-type.enum';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
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

  currencies!: CurrencyResponse[];
  selectedCurrencyId!: string;
  selectedCurrencySymbol!: string;

  minPrice!: number;
  maxPrice!: number;
  price!: number;

  cities!: Specification<string>[];
  selectedCityId!: string;

  breeds!: Specification<string>[];
  selectedBreedId!: string;

  selectedIsNewFilter: number = 0;

  minMiliage!: number;
  maxMiliage!: number;
  miliage!: number;

  minEngineCapacity: number = 0;
  maxEngineCapacity: number = 18200;
  engineCapacity!: number;

  olderReleaseYear!: number;
  newerReleaseYear!: number;
  releaseYear!: number;

  fuelTypes!: Specification<string>[];
  selectedFuelTypeId!: string;

  colors!: Specification<string>[];
  selectedColorId!: string;

  transmissionTypes!: Specification<string>[];
  selectedTransmissionTypeId!: string;

  brands!: Specification<string>[];
  selectedBrandId!: string;

  private _clotheIsShoe: boolean = false;

  get clotheIsShoe(): boolean  { return this._clotheIsShoe; }

  set clotheIsShoe(value: boolean) {
    this._clotheIsShoe = value;
    forkJoin([
      this.productApiService.getClothesSizes(this.selectedClothesTypeFilter == -1, this.clotheIsShoe)
    ]).subscribe(
      ([clothesSizes]: [Specification<string>[]]) => {
        this.clothesSizes = clothesSizes;
        this.selectedClothesSizeId = this.clothesSizes.map(x => x.id).includes(this.selectedClothesSizeId) ? this.selectedClothesSizeId : 'undefined';
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

  clothesSizes!: Specification<string>[];
  selectedClothesSizeId!: string;

  genders!: Specification<string>[];
  selectedGenderId!: string;

  clothesSeasons!: Specification<string>[];
  selectedClothesSeasonId!: string;

  clothesViews!: Specification<string>[];
  selectedClothesViewId!: string;

  memories!: Specification<string>[];
  selectedMemoryId!: string;

  models!: Specification<string>[];
  selectedModelId!: string;

  minArea!: number;
  maxArea!: number;
  area!: number;

  minRooms!: number;
  maxRooms!: number;
  rooms!: number;

  selectedIsRentFilter: number = 0;

  tvTypes!: Specification<string>[];
  selectedTvTypeId!: string;

  selectedIsSmartFilter: number = 0;

  screenResolutions!: Specification<string>[];
  selectedScreenResolutionId!: string;

  screenDiagonals!: Specification<number>[];
  selectedScreenDiagonalId!: string;

  selectedTitle: string = "";

  selectedDescription: string | null = null;

  newSelectedImages: File[] = [];

  currentImages: string[] = [];

  imagesToRemove: string[] = [];

  private _selectedCategoryType!: CategoryType | undefined;

  get selectedCategoryType(): CategoryType | undefined { return this._selectedCategoryType; }

  set selectedCategoryType(value: CategoryType | undefined) {
    this.spinnerService.changeSpinnerState(true);
    this.clearSelectionsInAdditionalFilters();
    switch(value){
      case CategoryType.AnimalsType: {
        this.sharedApiService.getBreeds(this.productSubcategory?.Id ? this.productSubcategory?.Id : null).subscribe((response : Specification<string>[]) => {
          this.breeds = response;
          this._selectedCategoryType = value;
          this.spinnerService.changeSpinnerState(false);
          this.loadProductDetailByCategory();
          return;
        })
        break;
      }
      case CategoryType.AutosType: {
        forkJoin([
          this.productApiService.getFuelTypes(),
          this.productApiService.getColors(),
          this.productApiService.getTransmissionTypes(),
          this.productApiService.getAutoBrands(this.productSubcategory?.Id ? [this.productSubcategory?.Id] : null),
          this.productApiService.getAutoModels(this.selectedBrandId !== 'undefined' ? [this.selectedBrandId] : null, this.productSubcategory?.Id ? [this.productSubcategory?.Id] : null)
        ]).subscribe(
          ([fuelTypes, colors, transmissionTypes, brands, models]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.fuelTypes = fuelTypes;
            this.colors = colors;
            this.transmissionTypes = transmissionTypes;
            this.brands = brands;
            this.models = models;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadProductDetailByCategory();
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
          this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== 'undefined' ? this.selectedGenderId : null, this.productSubcategory?.Id ? this.productSubcategory?.Id : null)
        ]).subscribe(
          ([brands, clothesSizes, genders, clothesSeasons, clothesViews]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.brands = brands;
            this.clothesSizes = clothesSizes;
            this.genders = genders;
            this.clothesSeasons = clothesSeasons;
            this.clothesViews = clothesViews;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadProductDetailByCategory();
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
          this.productApiService.getElectronicColors(this.selectedModelId !== undefined ? this.selectedModelId : null),
          this.productApiService.getElectronicBrands(this.productSubcategory?.Id ? this.productSubcategory?.Id : null),
          this.productApiService.getElectronicMemories(this.selectedModelId !== undefined ? this.selectedModelId : null),
          this.productApiService.getElectronicModels(this.selectedBrandId !== 'undefined' ? [this.selectedBrandId] : null, this.productSubcategory?.Id ? this.productSubcategory?.Id : null)
        ]).subscribe(
          ([colors, brands, memories, models]: [Specification<string>[], Specification<string>[], Specification<string>[], Specification<string>[]]) => {
            this.colors = colors;
            this.brands = brands;
            this.memories = memories;
            this.models = models;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadProductDetailByCategory();
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
        this.loadProductDetailByCategory();
        return;
        break;
      }
      case CategoryType.RealEstatesType: {
        this._selectedCategoryType = value;
        this.spinnerService.changeSpinnerState(false);
        this.loadProductDetailByCategory();
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
            this.brands = brands;
            this.tvTypes = tvTypes;
            this.screenResolutions = screenResolutions;
            this.screenDiagonals = screenDiagonals;
            this._selectedCategoryType = value;
            this.spinnerService.changeSpinnerState(false);
            this.loadProductDetailByCategory();
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
        return;
        break;
      }
    }
  }

  productSubcategory!: ProductSubcategory;

  productId!: string;
  product!: any;

  errorText: string | null = null;

  isUpdatedSuccessful: boolean = false;

  constructor(private router: Router, private authFacade : AuthFacadeService, private productApiService: ProductApiService, private sharedApiService : SharedApiService, private spinnerService: SpinnerService, private changeDetectorRef : ChangeDetectorRef, private route: ActivatedRoute) {
    if(!authFacade.isAuthenticated()){
      this.router.navigateByUrl('auth/login');
      return;
    }
    let id = this.route.snapshot.paramMap.get('id')
    if(id == null){
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return;
    }
    this.productId = id;
  }

  ngOnInit(): void {
    this.spinnerService.changeSpinnerState(true);

    const regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;
    if (!((this.productId) && regex.test(this.productId))) {
      this.spinnerService.changeSpinnerState(false);
      this.router.navigateByUrl('/404', { skipLocationChange: true });
      return; 
    } 

    forkJoin([
      this.sharedApiService.getCurrencies(),
      this.sharedApiService.GetCities()
    ]).subscribe(
      ([currencies, cities]: [CurrencyResponse[], Specification<string>[]]) => {
        this.currencies = currencies;
        this.selectedCurrencyId = currencies[0]?.id;
        this.selectedCurrencySymbol = currencies[0]?.symbol;
        this.cities = cities;

        this.productApiService.GetProductSubcategory(this.productId).subscribe(
          (result) => {
            this.productSubcategory = new ProductSubcategory(result.Id, result.Name, result.Type, result.CategoryId, result.SubType);
            this.clotheIsShoe = this.productSubcategory.SubType === SubcategoryType.Shoe;
            this.selectedCategoryType = this.productSubcategory.Type;
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );

        this.spinnerService.changeSpinnerState(false);
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
      }
    );
  }

  loadProductDetailByCategory(): void {
    switch(this.selectedCategoryType){
      case CategoryType.AnimalsType: {
        this.productApiService.GetAnimalDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.selectedBreedId = result.breedId;
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.AutosType: {
        this.productApiService.GetAutoDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.selectedFuelTypeId = result.fuelTypeId;
            this.selectedColorId = result.colorId;
            this.selectedTransmissionTypeId = result.transmissionTypeId;
            this.selectedBrandId = result.autoBrandId;
            this.selectedModelId = result.autoModelId;
            this.selectedIsNewFilter = result.isNew ? 1 : -1;
            this.miliage = result.miliage;
            this.engineCapacity = result.engineCapacity;
            this.releaseYear = new Date(result.releaseYear).getFullYear();
            this.onSelectedBrandChange();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ClothesType: {
        this.productApiService.GetClothesDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.selectedBrandId = result.clothesBrandId;
            this.selectedClothesSizeId = result.clothesSizeId;
            this.selectedGenderId = result.genderId;
            this.selectedClothesSeasonId = result.clothesSeasonId;
            this.selectedClothesViewId = result.clothesViewId;
            this.selectedIsNewFilter = result.isNew ? 1 : -1;
            this.selectedClothesTypeFilter = result.isChild ? -1 : 1;
            this.onSelectedClothesTypeChange();
            this.onSelectedGenderChange();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        this.productApiService.GetElectronicDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.selectedColorId = result.colorId;
            this.selectedBrandId = result.electronicBrandId;
            this.selectedMemoryId = result.memoryId;
            this.selectedModelId = result.modelId;
            this.selectedIsNewFilter = result.isNew ? 1 : -1;
            this.onSelectedBrandChange();
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ItemsType: {
        this.productApiService.GetItemDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.selectedIsNewFilter = result.isNew ? 1 : -1;
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.RealEstatesType: {
        this.productApiService.GetRealEstateDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.area = result.area;
            this.rooms = result.rooms;
            this.selectedIsRentFilter = result.isRent ? 1 : -1; 
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      case CategoryType.TVsType: {
        this.productApiService.GetTVDetail(this.productId as string).subscribe(
          (result) => {
            result.images = result.images && result.images.length > 0 ? result.images.map(i => environment.blobUrl + '/product-images/' + i) : [environment.blobUrl + '/product-images/default-product-image.png'];
            this.product = result;
            this.selectedTitle = result.title;
            this.selectedDescription = result.description;
            this.selectedCityId = result.cityId;
            this.currentImages = result.images;
            this.price = result.price;
            this.selectedCurrencyId = result.currencyId;
            this.selectedBrandId = result.tvBrandId;
            this.selectedTvTypeId = result.tvTypeId;
            this.selectedScreenResolutionId = result.screenResolutionId;
            this.selectedScreenDiagonalId = result.screenDiagonalId;
            this.selectedIsNewFilter = result.isNew ? 1 : -1;
            this.selectedIsSmartFilter = result.isSmart ? 1 : -1;
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.router.navigateByUrl('/404', { skipLocationChange: true }); 
              return;
            }
          }
        );
        break;
      }
      default: {
        this.router.navigateByUrl('/404', { skipLocationChange: true }); 
        return;
      }
    }
  }

  generateRange(count: number): number[] { return Array.from({length: count}, (_, i) => i + 1); }

  onSelectedModelChange(): void {
    switch (this.selectedCategoryType) {
      case CategoryType.ElectronicsType: {
        this.spinnerService.changeSpinnerState(true);
        forkJoin([
          this.productApiService.getElectronicColors(this.selectedModelId !== undefined ? this.selectedModelId : null),
          this.productApiService.getElectronicMemories(this.selectedModelId !== undefined ? this.selectedModelId : null)
        ]).subscribe(
          ([colors, memories]: [Specification<string>[], Specification<string>[]]) => {
            this.colors = colors;
            this.selectedColorId = this.colors.map(i => i.id).indexOf(this.selectedColorId) !== -1 ? this.selectedColorId : 'undefined';
            this.memories = memories;
            this.selectedMemoryId = this.memories.map(i => i.id).indexOf(this.selectedMemoryId) !== -1 ? this.selectedMemoryId : 'undefined';
            this.spinnerService.changeSpinnerState(false);
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
    }
  }

  onSelectedBrandChange(): void {
    this.spinnerService.changeSpinnerState(true);
    switch (this.selectedCategoryType) {
      case CategoryType.AutosType: {
        forkJoin([
          this.productApiService.getAutoModels(this.selectedBrandId !== 'undefined' ? [this.selectedBrandId] : null, this.productSubcategory?.Id ? [this.productSubcategory?.Id] : null)
        ]).subscribe(
          ([models]: [Specification<string>[]]) => {
            this.models = models;
            this.selectedModelId = this.models.map(i => i.id).indexOf(this.selectedModelId) !== -1 ? this.selectedModelId : 'undefined';
            this.spinnerService.changeSpinnerState(false);
          },
          (error) => {
            console.error('Error fetching data:', error);
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        forkJoin([
          this.productApiService.getElectronicModels(this.selectedBrandId !== 'undefined' ? [this.selectedBrandId] : null, this.productSubcategory?.Id ? this.productSubcategory?.Id : null)
        ]).subscribe(
          ([models]: [Specification<string>[]]) => {
            this.models = models;
            this.selectedModelId = this.models.map(i => i.id).indexOf(this.selectedModelId) !== -1 ? this.selectedModelId : 'undefined';
            this.spinnerService.changeSpinnerState(false);
            this.onSelectedModelChange();
            return;
          },
          error => {
            console.error('Error fetching data:', error);
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      default: {
        this.spinnerService.changeSpinnerState(false);
      }
    }
  }

  onSelectedGenderChange(): void {
    this.spinnerService.changeSpinnerState(true);
    forkJoin([
      this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== 'undefined' ? this.selectedGenderId : null, this.productSubcategory?.Id ? this.productSubcategory?.Id : null)
    ]).subscribe(
      ([clothesViews]: [Specification<string>[]]) => {
        this.clothesViews = clothesViews;
        this.selectedClothesViewId = this.clothesViews.map(i => i.id).indexOf(this.selectedClothesViewId) !== -1 ? this.selectedClothesViewId : 'undefined';
        this.spinnerService.changeSpinnerState(false);
        this.onSelectedClothesViewChange();
        return;
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
        return;
      }
    );
  }

  onSelectedClothesTypeChange(): void {
    this.spinnerService.changeSpinnerState(true);
    forkJoin([
      this.productApiService.getClothesViews(this.selectedClothesTypeFilter != 0 ? this.selectedClothesTypeFilter == -1 : null, this.selectedGenderId !== 'undefined' ? this.selectedGenderId : null, this.productSubcategory?.Id ? this.productSubcategory?.Id : null),
      this.productApiService.getClothesSizes(this.selectedClothesTypeFilter == -1, this.clotheIsShoe)
    ]).subscribe(
      ([clothesViews, clothesSizes]: [Specification<string>[], Specification<string>[]]) => {
        this.clothesSizes = clothesSizes;
        this.selectedClothesSizeId = this.clothesSizes.map(x => x.id).includes(this.selectedClothesSizeId) ? this.selectedClothesSizeId : 'undefined';
        this.clothesViews = clothesViews;
        this.selectedClothesViewId = this.clothesViews.map(i => i.id).indexOf(this.selectedClothesViewId) !== -1 ? this.selectedClothesViewId : 'undefined';
        this.spinnerService.changeSpinnerState(false);
        this.onSelectedClothesViewChange();
        return;
      },
      error => {
        console.error('Error fetching data:', error);
        this.spinnerService.changeSpinnerState(false);
        return;
      }
    );
  }

  onSelectedClothesViewChange(): void {
    this.spinnerService.changeSpinnerState(true);
    forkJoin([
      this.productApiService.getClotheBrands(this.selectedClothesViewId !== 'undefined' ? [this.selectedClothesViewId] : null),
    ]).subscribe(
      ([brands]: [Specification<string>[]]) => {
        this.brands = brands;
        this.selectedBrandId = this.brands.map(i => i.id).indexOf(this.selectedBrandId) !== -1 ? this.selectedBrandId : 'undefined';
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

  clearSelectionsInAdditionalFilters(): void {
    this.selectedBreedId = 'undefined';
    this.selectedIsNewFilter = 0;
    this.miliage = this.minMiliage;
    this.engineCapacity = this.minEngineCapacity;
    this.selectedFuelTypeId = 'undefined';
    this.selectedColorId = 'undefined';
    this.selectedTransmissionTypeId = 'undefined';
    this.selectedBrandId = 'undefined';
    this.selectedClothesTypeFilter = 0;
    this.selectedClothesSizeId = 'undefined';
    this.selectedGenderId = 'undefined';
    this.selectedClothesSeasonId = 'undefined';
    this.selectedClothesViewId = 'undefined';
    this.selectedMemoryId = 'undefined';
    this.selectedModelId = 'undefined';
    this.area = this.minArea;
    this.rooms = this.minRooms;
    this.selectedIsRentFilter = 0;
    this.selectedTvTypeId = 'undefined';
    this.selectedIsSmartFilter = 0;
    this.selectedScreenResolutionId = 'undefined';
    this.selectedScreenDiagonalId = 'undefined';
  }

  updateProduct(): void {
    this.spinnerService.changeSpinnerState(true);

    const regex = /^[0-9A-Fa-f]{8}[-]?([0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}$/;
    const titleRegex = /^[A-ZА-ЯƏÜÖĞİŞÇ][A-ZА-ЯƏÜÖĞİŞÇa-zа-яəüöğışç0-9\s'""':;,.\(\)\*\-_]{2,127}$/;

    this.errorText = this.selectedTitle.trim().length === 0 ? "Title must not be empty" :
    this.selectedTitle.length > 128 || this.selectedTitle.length < 3 ? "Title must be at least 3 characters and no more than 128" :
    !titleRegex.test(this.selectedTitle) ? "Invalid title" :
    this.selectedDescription && this.selectedDescription?.length > 500 ? "Description must be no more than 500 characters" :
    !regex.test(this.selectedCityId) ? "Wrong city" : 
    this.price < 0 ? "Price cannot be negative" : 
    !regex.test(this.selectedCurrencyId) ? "Wrong currency" : null;

    if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

    let imagesToSend: FormData = new FormData();
    if (this.newSelectedImages) {
      for (let i = 0; i < this.newSelectedImages.length; i++) {
        imagesToSend.append("Files", this.newSelectedImages[i]);
      }
    }

    let formData = new FormData();
    formData.append("ProductId", this.productId);
    formData.append("Title", this.selectedTitle);
    if (this.selectedDescription) { formData.append("Description", this.selectedDescription); }
    formData.append("Price", this.price.toString());
    formData.append("CurrencyId", this.selectedCurrencyId);
    formData.append("CategoryId", this.product.categories[0].id);
    formData.append("SubcategoryId", this.productSubcategory.Id);
    formData.append("CityId", this.selectedCityId);
    this.imagesToRemove.forEach(x => {
      formData.append("OldPaths", x)
    });
    imagesToSend.forEach(x => {
      formData.append("image", x)
    });

    switch(this.selectedCategoryType) {
      case CategoryType.AnimalsType: {
        this.errorText = !regex.test(this.selectedBreedId) ? "Breed field is required" : null;

        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        formData.append("AnimalBreedId", this.selectedBreedId);

        this.productApiService.updateAnimal(formData).subscribe(
          (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
          (error) => {
            if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
              this.errorText = "Invalid request, please try again."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
            else if(error.response.status === HttpStatusCode.Unauthorized){
              this.spinnerService.changeSpinnerState(false);
              this.authFacade.logout();
              this.router.navigateByUrl('auth/login');
              return;
            }
            else if(error.response.status === HttpStatusCode.InternalServerError){
              this.errorText = "Server error, please try again later."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          }
        );
        break;
      }
      case CategoryType.AutosType: {
        this.errorText = this.selectedIsNewFilter === 0 ? "Is new field is required" :
        this.miliage < 0 ? "Miliage cannot be negative" :
        this.engineCapacity < 0 ? "Engine capacity cannot be negative" :
        this.engineCapacity > 18200 ? "Engine capacity cannot be more than 18200" :
        this.releaseYear < 0 ? "Release year cannot be negative" :
        !regex.test(this.selectedBrandId) ? "Brand field is required" :
        !regex.test(this.selectedFuelTypeId) ? "Fuel type field is required" : 
        !regex.test(this.selectedColorId) ? "Color field is required" : 
        !regex.test(this.selectedTransmissionTypeId) ? "Wrong transmission type" : 
        !regex.test(this.selectedModelId) ? "Model field is required" : null;

        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        var tempReleaseYearDate: Date =  new Date();
        tempReleaseYearDate.setFullYear(this.releaseYear, 0, 1);

        formData.append("Miliage", `${this.miliage}`); 
        formData.append("EngineCapacity", `${this.engineCapacity}`); 
        formData.append("ReleaseYear", `${tempReleaseYearDate.toISOString()}`); 
        formData.append("IsNew", `${this.selectedIsNewFilter === 1}`);
        formData.append("FuelTypeId", `${this.selectedFuelTypeId}`);  
        formData.append("AutoColorId", `${this.selectedColorId}`); 
        formData.append("TransmissionTypeId", `${this.selectedTransmissionTypeId}`); 
        formData.append("AutoModelId", `${this.selectedModelId}`); 

        this.productApiService.updateAuto(formData).subscribe(
          (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
          (error) => {
            if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
              this.errorText = "Invalid request, please try again."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
            else if(error.response.status === HttpStatusCode.Unauthorized){
              this.spinnerService.changeSpinnerState(false);
              this.authFacade.logout();
              this.router.navigateByUrl('auth/login');
              return;
            }
            else if(error.response.status === HttpStatusCode.InternalServerError){
              this.errorText = "Server error, please try again later."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ClothesType: {
        this.errorText = this.selectedIsNewFilter === 0 ? "Is new field is required" : 
        this.selectedClothesTypeFilter === 0 ? "Clothes type field is required" : 
        !regex.test(this.selectedGenderId) ? "Gender field is required" : 
        !regex.test(this.selectedClothesSeasonId) ? "Clothes season field is required" :  
        !regex.test(this.selectedBrandId) ? "Brand field is required" : 
        !regex.test(this.selectedClothesSizeId) ? "Clothes size field is required" :  
        !regex.test(this.selectedClothesViewId) ? "Clothes view field is required" : null;

        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        this.productApiService.getClothesBrandViewId(this.selectedBrandId, this.selectedClothesViewId).subscribe(
          (result: string) => {
            formData.append("IsNew", `${this.selectedIsNewFilter === 1}`); 
            formData.append("ClothesSeasonId", `${this.selectedClothesSeasonId}`); 
            formData.append("ClothesSizeId", `${this.selectedClothesSizeId}`); 
            formData.append("ClothesBrandViewId", `${result}`); 

            this.productApiService.updateClothes(formData).subscribe(
              (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
              (error) => {
                if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
                  this.errorText = "Invalid request, please try again."
                  this.spinnerService.changeSpinnerState(false);
                  return;
                }
                else if(error.response.status === HttpStatusCode.Unauthorized){
                  this.spinnerService.changeSpinnerState(false);
                  this.authFacade.logout();
                  this.router.navigateByUrl('auth/login');
                  return;
                }
                else if(error.response.status === HttpStatusCode.InternalServerError){
                  this.errorText = "Server error, please try again later."
                  this.spinnerService.changeSpinnerState(false);
                  return;
                }
              }
            );
          },
          (error) => {
            if(error.response.status === HttpStatusCode.NotFound){
              this.errorText = "Brand and view of clothing are incompatible"
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          }
        );
        break;
      }
      case CategoryType.ElectronicsType: {
        this.errorText = this.selectedIsNewFilter === 0 ? "Is new field is required" : 
        !regex.test(this.selectedBrandId) ? "Brand field is required" :  
        !regex.test(this.selectedModelId) ? "Model field is required" :  
        !regex.test(this.selectedColorId) ? "Color field is required" :  
        !regex.test(this.selectedMemoryId) ? "Memory field is required" : null;

        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        forkJoin([
          this.productApiService.getMemoryModelId(this.selectedMemoryId, this.selectedModelId),
          this.productApiService.getModelColorId(this.selectedColorId, this.selectedModelId)
        ]).subscribe(
          ([memoryModelId, modelColorId]: [string, string]) => {
            formData.append("IsNew", `${this.selectedIsNewFilter === 1}`); 
            formData.append("MemoryModelId", `${memoryModelId}`); 
            formData.append("ModelColorId", `${modelColorId}`); 

            this.productApiService.updateElectronics(formData).subscribe(
              (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
              (error) => {
                if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
                  this.errorText = "Invalid request, please try again."
                  this.spinnerService.changeSpinnerState(false);
                  return;
                }
                else if(error.response.status === HttpStatusCode.Unauthorized){
                  this.spinnerService.changeSpinnerState(false);
                  this.authFacade.logout();
                  this.router.navigateByUrl('auth/login');
                  return;
                }
                else if(error.response.status === HttpStatusCode.InternalServerError){
                  this.errorText = "Server error, please try again later."
                  this.spinnerService.changeSpinnerState(false);
                  return;
                }
              }
            );
          },
          error => {
            this.errorText = "This model does not have this amount of memory or color";
            this.spinnerService.changeSpinnerState(false);
            return;
          }
        );
        break;
      }
      case CategoryType.ItemsType: {
        this.errorText = this.selectedIsNewFilter === 0 ? "Is new field is required" : null;
        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        formData.append("IsNew", `${this.selectedIsNewFilter === 1}`); 
        formData.append("ItemTypeId", `${this.productSubcategory?.Id ? this.productSubcategory?.Id : null}`); 

        this.productApiService.updateItem(formData).subscribe(
          (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
          (error) => {
            if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
              this.errorText = "Invalid request, please try again."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
            else if(error.response.status === HttpStatusCode.Unauthorized){
              this.spinnerService.changeSpinnerState(false);
              this.authFacade.logout();
              this.router.navigateByUrl('auth/login');
              return;
            }
            else if(error.response.status === HttpStatusCode.InternalServerError){
              this.errorText = "Server error, please try again later."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          }
        );
        break;
      }
      case CategoryType.RealEstatesType: {
        this.errorText = this.area < 0 ? "Area cannot be negative" :
        this.rooms < 0 ? "Rooms cannot be negative" :
        this.selectedIsRentFilter === 0 ? "Is rent field is required" : null;

        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        formData.append("Area", `${this.area}`); 
        formData.append("Rooms", `${this.rooms}`); 
        formData.append("IsRent", `${this.selectedIsRentFilter === 1}`); 
        formData.append("RealEstateTypeId", `${this.productSubcategory?.Id ? this.productSubcategory?.Id : null}`); 

        this.productApiService.updateRealEstate(formData).subscribe(
          (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
          (error) => {
            if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
              this.errorText = "Invalid request, please try again."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
            else if(error.response.status === HttpStatusCode.Unauthorized){
              this.spinnerService.changeSpinnerState(false);
              this.authFacade.logout();
              this.router.navigateByUrl('auth/login');
              return;
            }
            else if(error.response.status === HttpStatusCode.InternalServerError){
              this.errorText = "Server error, please try again later."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          }
        );
        break;
      }
      case CategoryType.TVsType: {
        this.errorText = this.selectedIsNewFilter === 0 ? "Is new field is required" :
        this.selectedIsSmartFilter === 0 ? "Is smart field is required" :
        !regex.test(this.selectedBrandId) ? "Brand field is required" : 
        !regex.test(this.selectedTvTypeId) ? "Tv type field is required" : 
        !regex.test(this.selectedScreenResolutionId) ? "Screen resolution field is required" : 
        !regex.test(this.selectedScreenDiagonalId) ? "Screen diagonal field is required" : null;

        if(this.errorText != null) { this.spinnerService.changeSpinnerState(false); return; }

        formData.append("IsNew", `${this.selectedIsNewFilter === 1}`); 
        formData.append("IsSmart", `${this.selectedIsSmartFilter === 1}`); 
        formData.append("TvTypeId", `${this.selectedTvTypeId}`); 
        formData.append("TvBrandId", `${this.selectedBrandId}`); 
        formData.append("ScreenResolutionId", `${this.selectedScreenResolutionId}`); 
        formData.append("ScreenDiagonalId", `${this.selectedScreenDiagonalId}`); 

        this.productApiService.updateTv(formData).subscribe(
          (result) => {this.spinnerService.changeSpinnerState(false);this.isUpdatedSuccessful = true;},
          (error) => {
            if(error.response.status === HttpStatusCode.BadRequest || error.response.status === HttpStatusCode.NotFound){
              this.errorText = "Invalid request, please try again."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
            else if(error.response.status === HttpStatusCode.Unauthorized){
              this.spinnerService.changeSpinnerState(false);
              this.authFacade.logout();
              this.router.navigateByUrl('auth/login');
              return;
            }
            else if(error.response.status === HttpStatusCode.InternalServerError){
              this.errorText = "Server error, please try again later."
              this.spinnerService.changeSpinnerState(false);
              return;
            }
          }
        );
        break;
      }
      default: {
        this.errorText = "Wrong category";
        this.spinnerService.changeSpinnerState(false);
        return;
      }
    }
  }

  onSelectNewImage(event: any): void {
    event.addedFiles.forEach((file: File) => {
      const img = new Image();
      img.src = URL.createObjectURL(file);
      img.onload = () => {
        this.newSelectedImages.push(file);
        URL.revokeObjectURL(img.src);
      };
      img.onerror = () => {
        console.log(`Invalid image: ${file.name}`);
        URL.revokeObjectURL(img.src);
      };
    });
  }

  onRemoveNewImage(image: File): void {
    this.newSelectedImages.splice(this.newSelectedImages.indexOf(image), 1);
  }

  onRemoveCurrentImage(index: number): void {
    this.imagesToRemove.push(this.currentImages[index]);
    this.currentImages.splice(index, 1);
  }

  back(): void{
    window.history.back();
  }

  onSelectCurrencyChange(): void { this.selectedCurrencySymbol = this.currencies.find(currency => currency.id == this.selectedCurrencyId)?.symbol as string; }

  handlePositiveNumberInput(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    let value = inputElement.value.replace(/[^0-9]/g, '');
    value = parseInt(value, 10).toString();
    switch(inputElement.id){
      case 'price-input': {
        if(!Number.isNaN(parseInt(value))){
          value = (parseInt(value) > this.maxPrice ? this.maxPrice : parseInt(value) < this.minPrice ? this.minPrice : value).toString();
          this.price = parseInt(value);
        }
        break; 
      }
      case 'miliage-input': {         
        if(!Number.isNaN(parseInt(value))){
          value = (parseInt(value) > this.maxMiliage ? this.maxMiliage : parseInt(value) < this.minMiliage ? this.minMiliage : value).toString();
          this.miliage = parseInt(value);
        }
        break; 
      }
      case 'engine-capacity-input': { 
        if(!Number.isNaN(parseInt(value))){
          value = (parseInt(value) > this.maxEngineCapacity ? this.maxEngineCapacity : parseInt(value) < this.minEngineCapacity ? this.minEngineCapacity : value).toString();
          this.engineCapacity = parseInt(value);
        }
        break; 
      }
      case 'release-year-input': { 
        if(!Number.isNaN(parseInt(value))){
          value = (parseInt(value) > this.newerReleaseYear ? this.newerReleaseYear : parseInt(value) < this.olderReleaseYear ? this.olderReleaseYear : value).toString();
          this.releaseYear = parseInt(value);
        }
        break; 
      }
      case 'area-input': { 
        if(!Number.isNaN(parseInt(value))){
          value = (parseInt(value) > this.maxArea ? this.maxArea : parseInt(value) < this.minArea ? this.minArea : value).toString();
          this.area = parseInt(value);
        }
        break; 
      }
      case 'rooms-input': { 
        if(!Number.isNaN(parseInt(value))){
          value = (parseInt(value) > this.maxRooms ? this.maxRooms : parseInt(value) < this.minRooms ? this.minRooms : value).toString();
          this.rooms = parseInt(value);
        }
        break; 
      }
    }
    inputElement.value = value;
  }
}
