import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/Member';
import { PageinatedResult } from '../_models/Pageination';
import { UserParams } from '../_models/UserParams';




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
  memberCatch = new Map();



  constructor(private http:HttpClient) { }

  getMembers(userParams:UserParams){
    var response = this.memberCatch.get(Object.values(userParams).join('-'))
   if(response){
     return of(response);
   }
    let params = this.getPageinationHeaders(userParams.pageNumber,userParams.pageSize);

    params = params.append('minAge',userParams.minAge.toString());
    params = params.append('maxAge',userParams.maxAge.toString());
    params = params.append('gender',userParams.gender);
    params = params.append('orderBy',userParams.orderBy);


   return this.getPageinatedResult<Member[]>(this.Url+'users',params)
    .pipe(map(respose=>{
    this.memberCatch.set(Object.values(userParams).join('-'),respose)
    return respose;
   }))
  }



  getMember(username:string){
    const member = [...this.memberCatch.values()]
    .reduce((arr,elem)=>arr.concat(elem.result),[])
    .find((member:Member)=>member.userName === username);
    if(member){
      return of(member)
    }
    console.log(member)

    // const member = this.Members.find(x=>x.userName === username);
    // if(member !== undefined) return of(member);
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


  addLike(usernaem:string){
    return this.http.post(this.Url+'likes/'+usernaem,{});
  }
  getLikes(predicate:string,pageNumber,pageSize){
    let params = this.getPageinationHeaders(pageNumber,pageSize);
    params = params.append('predicate',predicate)
      return this.getPageinatedResult<Partial<Member[]>>(this.Url+'likes',params);
    //return this.http.get<Partial<Member[]>>(this.Url+'Likes?predicate='+predicate);
  }



  private getPageinatedResult<T>(url,params) {

    const pageinatedResult : PageinatedResult<T> = new PageinatedResult<T>();

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        pageinatedResult.result = response.body;
        if (response.headers.get('Pageination') !== null) {
          pageinatedResult.pageination = JSON.parse(response.headers.get('Pageination'));

        }
        return pageinatedResult;
      })

    );
  }

  private getPageinationHeaders(pageNumber:number,pageSize:number){

    let params = new HttpParams();

      params = params.append("PageNumber",pageNumber.toString());
      params = params.append("PageSize",pageSize.toString());

      return params;
  }
}
