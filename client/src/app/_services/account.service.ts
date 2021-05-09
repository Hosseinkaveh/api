import { Injectable } from '@angular/core';
import {HttpClient, JsonpClientBackend} from '@angular/common/http'
import { Observable, ReplaySubject } from 'rxjs';

import {map } from 'rxjs/operators'
import { User } from '../_models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

   baseUrl=environment.baseUrl;
   private CurentUserSource = new ReplaySubject<User>(1);
   curentusers$ = this.CurentUserSource.asObservable();


  constructor(private http:HttpClient) { }

  login(model:any){
    return this.http.post<User>(this.baseUrl+'Account/Login',model).pipe(
      map((response:User) =>{
        const user = response;
        if(user){
          this.setCurnetUser(user);
        }
      })
    )

    }
    Register(model:any)
    {
      return this.http.post(this.baseUrl+'Account/Register',model).pipe(
        map((response:User)=>
        {
          const user =response;
          if (user){
            this.setCurnetUser(user);
          }
        })
      )

    }

    setCurnetUser(user:User)
    {
      localStorage.setItem('user',JSON.stringify(user));
         this.CurentUserSource.next(user);
    }

logOUt(){
localStorage.removeItem('user');
this.CurentUserSource.next(null);


}

}
