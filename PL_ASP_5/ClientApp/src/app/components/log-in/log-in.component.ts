import { AfterContentInit, AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements AfterContentInit {

  Roles: any = ['Admin', 'Author', 'Reader'];

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  ngAfterContentInit(): void {
    this.isAuth();
  }


  postData(user: User) {
    const body = {
      email: user.email, password: user.password, rememberMe: true,
      returnUrl: "/"
    };
    return this.http.post(this.baseUrl + 'api/Account/Login', body);
  }

  user: User = new User(); // данные вводимого пользователя

  done: boolean = false;
  msg: string;

  submit(user: User) {
    this.postData(user)
      .subscribe(
        (data: any) => {
          this.done = true; this.msg = data.message;
        },
        error => {
          console.log(error)
        }
      );
  }
  logout() {


    return this.http.post(this.baseUrl + 'api/Account/LogOff', "")
      .subscribe(
        (data: any) => {
          this.msg = data.message;
        },
        error => {
          console.log(error)
        }
      );
  }
  isAuth() {


    return this.http.post(this.baseUrl + 'api/Account/isAuthenticated', "")
      .subscribe(
        (data: any) => {
          this.msg = data.message;
        },
        error => {
          console.log(error)
        }
      );
  }
}

class User {
  email: string;
  password: string;
}
