import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/Member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() members:Member;
  constructor(private memberService:MembersService,private toasterService:ToastrService) { }

  ngOnInit(): void {
  }

  addLikes(member:Member){
    this.memberService.addLike(member.userName).subscribe(()=>{
      this.toasterService.success('you have like '+member.knownAs);
    })

  }

}
