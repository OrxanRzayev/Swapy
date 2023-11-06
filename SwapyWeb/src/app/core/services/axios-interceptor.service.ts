import { Injectable } from '@angular/core';
import jwt_decode, { JwtPayload } from 'jwt-decode';
import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse, HttpStatusCode } from 'axios';
import { catchError, from, map, mergeMap, Observable, of, tap, throwError } from 'rxjs';
import { LocalStorageService } from './local-storage.service';
import { SessionStorageService } from './session-storage.service';
import { Router } from '@angular/router';
import { AuthResponse } from 'src/app/modules/auth/models/auth-credentials';
import { environment } from 'src/environments/environment';
import { UserType } from '../enums/user-type.enum';

@Injectable({
  providedIn: 'root'
})
export class AxiosInterceptorService {
    private axiosInstance: AxiosInstance;
    private readonly apiUrl: string = environment.apiUrl;
    private readonly authpiUrl: string = environment.authApiUrl;

    constructor(private localStorage: LocalStorageService, private sessionStorage: SessionStorageService, public router: Router) {
        this.axiosInstance = axios.create();

        this.axiosInstance.interceptors.request.use(
            async (config: any) => {
                config.ContentType = "application/json";
                config = await this.addAccessToken(config);
                return config;
            },
            (error) => { return Promise.reject(error); }
        );
    }

    //Headers
    private addAccessToken(config: AxiosRequestConfig): AxiosRequestConfig {
        const token = this.getAccessToken();

        if(token != null) {
            if (!config.headers) config.headers = {};
            config.headers['Authorization'] = `Bearer ${token}`;
        }

        return config;
    }

    public getAccessToken(): string | null {
        var accessToken: string | null;
        
        if(this.localStorage.rememberMe) {
            accessToken = this.localStorage.rememberMe === 'true'
            ? this.localStorage.accessToken
            : this.sessionStorage.accessToken;
        } else accessToken = null;
        
        return accessToken;
    }

    private isTokenExpired(token: string): boolean {
        const decodedToken = jwt_decode<JwtPayload>(token);
  
        if (!decodedToken.exp) return false;

        const expirationTime = decodedToken.exp * 1000;
        const currentTime = new Date().getTime();
        return expirationTime < currentTime;
    }

    //Logout method
    private logout(): void {
        if(this.localStorage.rememberMe) {
            if(this.localStorage.rememberMe === 'true') {
              this.localStorage.removeToken();
              this.localStorage.removeUserId();
              this.localStorage.removeUserType();
            } else {
              this.sessionStorage.removeToken();
              this.sessionStorage.removeUserId();
              this.sessionStorage.removeUserType();
            }
            this.localStorage.removeRememberMe();
        }
        this.router.navigate(['/auth/login']);
    }

    private refreshToken(token: string): Observable<AuthResponse> {
        try {
            const instance = axios.create();
            instance.interceptors.request.use(
                (config: any) => {
                    config.headers['Authorization'] = `Bearer ${token}`;
                    return config;
                },
                (error) => {
                    return Promise.reject(error);
                }
            );
    
            return from(instance.put<AuthResponse>(`${this.apiUrl}/RefreshAccessToken`)).pipe(
                map((response: AxiosResponse<AuthResponse>) => ({
                    type: response.data.type,
                    userId: response.data.userId,
                    accessToken: response.data.accessToken,
                    refreshToken: response.data.refreshToken,
                    hasUnreadMessages: response.data.hasUnreadMessages
                })),
                catchError((error) => {
                    if (error.response?.status === HttpStatusCode.Unauthorized) this.logout();
                    return throwError(error);
                })
            );
        } catch (error: any) {
            if (error.response.status === HttpStatusCode.Unauthorized) this.logout();
            return throwError(error);
        }
    }

    private tryRefreshAccessToken(): Observable<AuthResponse> {
        const token = this.getAccessToken();
    
        if (token != null) {
            if (this.isTokenExpired(token)) {
                alert();
                return this.refreshToken(token).pipe(
                    catchError((error) => {
                        if (error.response?.status === HttpStatusCode.Unauthorized) this.logout();
                        return throwError(error);
                    })
                );
            }
        }
        
        const emptyResponse: AuthResponse = { type: UserType.Empty, userId: "", accessToken: "", refreshToken: "", hasUnreadMessages: false };
        return of(emptyResponse);
    }

    private sendRequest<T>(
        method: 'get' | 'post' | 'put' | 'delete' | 'patch',
        url: string,
        params?: any,
        config?: AxiosRequestConfig
    ): Observable<AxiosResponse<T>> {
        return this.tryRefreshAccessToken().pipe(
            tap((result: AuthResponse) => {
                if(result.type != UserType.Empty) {
                    if (this.localStorage.rememberMe && this.localStorage.rememberMe === 'true') {
                        this.localStorage.userId = result.userId;
                        this.localStorage.userType = result.type.toString();
                        this.localStorage.accessToken = result.accessToken;
                    } else {
                        this.sessionStorage.userId = result.userId;
                        this.sessionStorage.userType = result.type.toString();
                        this.sessionStorage.accessToken = result.accessToken;
                    }
                }
            }),
            mergeMap(() => {
                return from(this.axiosInstance[method]<T>(url, params, { ...config }));
            }),
            catchError((error) => { throw(error); })
        );
    }

    get<T>(url: string, params?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
        return this.sendRequest('get', url, params, config);
    }

    post<T>(url: string, params?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
        return this.sendRequest('post', url, params, config);
    }

    put<T>(url: string, params?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
        return this.sendRequest('put', url, params, config);
    }

    delete<T>(url: string, params?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
        return this.sendRequest('delete', url, params, config);
    }

    patch<T>(url: string, params?: any, config?: AxiosRequestConfig): Observable<AxiosResponse<T>> {
        return this.sendRequest('patch', url, params, config);
    }
}