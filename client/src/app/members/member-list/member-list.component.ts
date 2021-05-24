import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/Member';
import { Pageination } from 'src/app/_models/Pageination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/UserParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {
  members:Member[];
  pageination:Pageination;
  userParams:UserParams;
  user:User;
  genderList = [{value:'male',display:'Male'},{value:'female',display:'Female'}]

  constructor(private memberservice:MembersService,private accountService:AccountService) {
    this.accountService.curentusers$.pipe(take(1)).subscribe(user=>{
      this.user = user;
      this.userParams = new UserParams(user);

    })
   }

  ngOnInit(): void {
    this.loadMember();



  }

  loadMember(){
    this.memberservice.getMembers(this.userParams).subscribe(response =>{
      this.members = response.result;
      this.pageination = response.pageination;

    })
  }

  resetFilters(){
    this.userParams = new UserParams(this.user);
    this.loadMember();

  }


  pageChanged(event:any){
    this.userParams.pageNumber = event.page;
    this.loadMember();
  }

}
