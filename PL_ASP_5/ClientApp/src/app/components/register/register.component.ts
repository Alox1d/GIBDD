import { Component, Inject, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent {

  Roles: any = ['Admin', 'Author', 'Reader'];

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }
 
  postData(user: User){
        
      const body = {email: user.email, password: user.password, passwordConfirm: user.passwordConfirm};
      return this.http.post(this.baseUrl + 'api/Account/Register', body); 
  }

  user: User=new User(); // данные вводимого пользователя
      
  receivedUser: User; // полученный пользователь
  done: boolean = false;
  msg:string;
  submit(user: User){
      this.postData(user)
              .subscribe(
                (data: any) => {
                  this.done = true; this.msg = data.message;
                },
                  error => { console.log(error)
                  }
              );
  }
}
class User {
  email: string;
  password: string;
  passwordConfirm: string;
}
