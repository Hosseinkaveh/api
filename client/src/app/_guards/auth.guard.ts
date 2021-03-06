import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private accountservice:AccountService,private toast:ToastrService){}
  canActivate(): Observable<boolean> {
   return this.accountservice.curentusers$.pipe(
      map(user => {
        if(user) return true;
        this.toast.error('You shall not pass!');
        return false;
      })
    )
  }

}
