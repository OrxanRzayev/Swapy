import { UserType } from "src/app/core/enums/user-type.enum";

export interface LoginCredential {
    emailOrPhone: string,
    password: string
}

export interface UserRegistrationCredential {
    firstName: string,
    lastName: string,
    email: string,
    phoneNumber: string,
    password: string
}

export interface ShopRegistrationCredential {
    shopName: string,
    email: string,
    phoneNumber: string,
    password: string
}

export interface EmailVerifyCredential {
    userId: string;
    token: string;
}

export interface ResetPasswordCredential {
    userId: string;
    token: string;
    password: string;
}

export interface AuthResponse {
    type: UserType;
    userId: string;
    accessToken: string;
    refreshToken: string;
    hasUnreadMessages: boolean;
}