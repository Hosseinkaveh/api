import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/Member';

// const httpOption={
//   headers:new HttpHeaders({
//     Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token
//   })
// }

@Injectable({
  providedIn: 'root'
})
export class MembersService {

  Url:string = environment.baseUrl;
  Members:Member[] =[];



  constructor(private http:HttpClient) { }

  getMembers(){
    if (this.Members.length > 0) return of(this.Members)
   return this.http.get<Member[]>(this.Url+'Users').pipe(
     map(members => {
       this.Members = members;
       return members;
     })
   )
  }

  getMember(username:string){
    const member = this.Members.find(x=>x.userName === username);
    if(member !== undefined) return of(member);
   return this.http.get<Member>(this.Url + 'Users/' + username)
  }
  updateMember(member:Member)
  {
    return this.http.put(this.Url+'Users',member).pipe(
      map(()=>{
        const index = this.Members.indexOf(member);
        this.Members[index]= member;
      })
    );
  }
  setMainPhoto(photoid:number){
    return this.http.put(this.Url + 'Users/set-main-photo/'+photoid,{});
  }

  deletePhoto(photoid:number){
    return this.http.delete(this.Url+'users/delete-photo/'+photoid);
  }
}
