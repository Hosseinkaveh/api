import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  @Output() cancelRegister = new EventEmitter();
  registerForm:FormGroup;
  maxDate:Date;
  // model:any={}
  validationErrors:string[]=[];

  constructor(private accountservice:AccountService,private toastr:ToastrService,
    private fb:FormBuilder,private router:Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18 );
  }
  initializeForm(){
    this.registerForm =  this.fb.group({
      Gender:['male'],
      username:['',Validators.required],
      KnownAs:['',Validators.required],
      dateOfBirth:['',Validators.required],
      City:['',Validators.required],
      Country:['',Validators.required],
      password:['',[Validators.required,
            Validators.minLength(4),Validators.maxLength(8)]],
        confirmPassword:['',[Validators.required, this.matchValues("password")]],

    })
    // this.registerForm = new FormGroup({
    //   username:new FormControl('',Validators.required),
    //   password:new FormControl('',[Validators.required,
    //     Validators.minLength(4),Validators.maxLength(8)]),
    //   confirmPassword:new FormControl('',[Validators.required, this.matchValues("password")]),
    // });
    this.registerForm.controls.password.valueChanges.subscribe(()=>{
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })

  }
  matchValues(matchTo:string):ValidatorFn{
    return  (control:AbstractControl)=>{
      return control?.value === control?.parent?.controls[matchTo].value
      ? null : { isMatches: true}
    }

  }

  register(){
    this.accountservice.Register(this.registerForm.value).subscribe(response =>{
     this.router.navigateByUrl('/members')
    },error =>{
     this.validationErrors = error;


    });
    // this.accountservice.Register(this.model).subscribe(response =>{
    //   this.cancel();
    // },error =>{
    //   console.log(error);
    // this.toastr.error(error.error)

    // });
  }

  cancel(){
  this.cancelRegister.emit(false);
  }

}
