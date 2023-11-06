import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { AuthFacadeService } from 'src/app/modules/auth/services/auth-facade.service';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private authFacade: AuthFacadeService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(this.authFacade.isAuthenticated()) return true;
      else {
        this.router.navigate(['/auth/login']);
        return false;
      }
    }
}