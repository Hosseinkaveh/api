import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/Member';
import { Pageination } from '../_models/Pageination';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members:Partial<Member[]>;
  predicate='liked'
  pageNumber = 1;
  PageSize = 2;
  pageination:Pageination;

  constructor( private memberService:MembersService) { }

  ngOnInit(): void {
    this.loadLikes();
  }
  loadLikes(){
    this.memberService.getLikes(this.predicate,this.pageNumber,this.PageSize).subscribe(response=>{
      this.members = response.result;
      this.pageination = response.pageination;
    })

  }
  pageChanged(event:any){
    this.pageNumber = event.page;
    this.loadLikes();
  }

}
