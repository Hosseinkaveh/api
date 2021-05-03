import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
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
  constructor(private http:HttpClient) { }

  getMembers(){
   return this.http.get<Member[]>(this.Url+'Users')
  }

  getMember(username:string){
   return this.http.get<Member>(this.Url + 'Users/' + username)
  }
  updateMember(member:Member)
  {
    return this.http.put(this.Url+'Users',member);
  }
}
