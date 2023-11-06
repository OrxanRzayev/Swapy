import { UserType } from "src/app/core/enums/user-type.enum";

export interface UserDetail {
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    registrationDate: Date;
    logo: string;
    likesCount: number;
    productsCount: number;
    subscriptionsCount: number;
    type: UserType;
}
  