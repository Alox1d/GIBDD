import { Component, OnInit } from '@angular/core';
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})


export class RegisterComponent {

  Roles: any = ['Admin', 'Author', 'Reader'];

  constructor(private http: HttpClient){ }
 
  postData(user: User){
        
      const body = {email: user.email, password: user.password, passwordConfirm: user.passwordConfirm};
      return this.http.post('https://localhost:44314/api/Account/Register', body); 
  }

  user: User=new User(); // данные вводимого пользователя
      
  receivedUser: User; // полученный пользователь
  done: boolean = false;
  error:string;
  submit(user: User){
      this.postData(user)
              .subscribe(
                (data: any) => {
                  this.done = true; this.error = data.message;
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
