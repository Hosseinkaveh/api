import { JsonPipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/Member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  @Input() member:Member;
  user:User;
  uploader:FileUploader;
  hasBaseDropZoneOver:false;
  baseUrl= environment.baseUrl;

  constructor(private accountservice:AccountService) {
    this.accountservice.curentusers$.pipe(take(1)).subscribe(user =>this.user = user);
   }

  ngOnInit(): void {
    this.intializeUploader();
  }

  fileOverBase(e:any){
    this.hasBaseDropZoneOver = e;
  }

  intializeUploader(){
    this.uploader = new FileUploader({
      url:this.baseUrl+'users/add-photo',
      authToken:'Bearer '+ this.user.token,
      isHTML5:true,
      allowedFileType:['image'],
      removeAfterUpload:true,
      autoUpload:false,
      maxFileSize:10* 1024 *1024
    });
    this.uploader.onAfterAddingFile =(file)=>{
      file.withCredentials  = false;
    }
    this.uploader.onSuccessItem = (item,response,status,header)=>{
      if(response){
        const photo = JSON.parse(response);
        this.member.photos.push(photo);

      }
    }
  }

}
