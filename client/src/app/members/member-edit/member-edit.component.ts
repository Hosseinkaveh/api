import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/Member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { MemberListComponent } from '../member-list/member-list.component';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm:NgForm;
  User:User;
  detailMembers:Member;
  @HostListener('window:beforeunload',['$event']) unloadNotification($event:any){
    if(this.editForm.dirty){
      $event.returnValue =  true;
    }
  }

  constructor(private accountService:AccountService,
    private memberService:MembersService,private toast:ToastrService) {

    this.accountService.curentusers$.pipe(take(1)).subscribe( x=>this.User = x);

     }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    this.memberService.getMember(this.User.username).subscribe(x=>this.detailMembers=x);
  }

  updateMember(){
    this.memberService.updateMember(this.detailMembers).subscribe(x =>{
      this.toast.success("update Data successful")
      this.editForm.reset(this.detailMembers);
    })


  }

}