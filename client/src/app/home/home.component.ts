import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
 RegisterMode=false;

  constructor(public accountservice:AccountService) { }

  ngOnInit(): void {
  }

 RegisterToggle(){
  this.RegisterMode=!this.RegisterMode;
 }

 cancelRegisterFrom(event:boolean){
   this.RegisterMode = event;
 }
}
