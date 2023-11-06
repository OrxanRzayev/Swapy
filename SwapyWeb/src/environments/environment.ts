// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  //Debug Api URL
  //apiUrl: "https://localhost:7083",
  
  //Release Api URL
  apiUrl: "https://swapyapi.azurewebsites.net",
  animalsApiUrl: "api/v1/Products/Animals",
  authApiUrl: "api/v1/Auth",
  autosApiUrl: "api/v1/Products/Autos",
  categoriesApiUrl: "api/v1/Categories",
  chatsApiUrl: "api/v1/Chats",
  clothesApiUrl: "api/v1/Products/Clothes",
  electronicsApiUrl: "api/v1/Products/Electronics",
  itemsApiUrl: "api/v1/Products/Items",
  productsApiUrl: "api/v1/Products",
  realEstatesApiUrl: "api/v1/Products/RealEstates",
  shopsApiUrl: "api/v1/Shops",
  tvsApiUrl: "api/v1/Products/TVs",
  usersApiUrl: "api/v1/Users",
  blobUrl: "https://swapyblob.blob.core.windows.net"
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
