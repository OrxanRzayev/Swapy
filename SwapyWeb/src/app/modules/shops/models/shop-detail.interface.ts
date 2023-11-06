import { UserType } from "src/app/core/enums/user-type.enum";

export interface ShopDetail {
    id: string;
    userId: string;
    shopName: string;
    description: string;
    email: string;
    location: string;
    phoneNumber: string;
    slogan: string;
    views: number;
    banner: string;
    workDays: string;
    logo: string;
    likesCount: number;
    productsCount: number;
    subscriptionsCount: number;
    endWorkTime: Date | null;   
    startWorkTime: Date | null; 
    registrationDate: Date; 
    type: UserType;
  }