import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard {
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    if (localStorage.getItem('user_token')) {
      let routeRole = route.data['role'];
      let user = JSON.parse(<string>localStorage.getItem('user_token'));

      if (user.roles && (user.roles.includes(routeRole) || user.roles.includes('Administrator'))) {
        return true;
      }

    }

    return false;

  }

}
