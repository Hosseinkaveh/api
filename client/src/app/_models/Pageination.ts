export interface Pageination{
  currentPge:number;
  itemPerPage:number;
  totalItem:number;
  totalPage:number;

}
 export class PageinatedResult<T>{
   result:T;
   pageination:Pageination;
 }
