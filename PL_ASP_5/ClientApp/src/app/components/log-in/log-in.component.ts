import { AfterContentInit, AfterViewInit, Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements AfterContentInit {

  Roles: any = ['Admin', 'Author', 'Reader'];
  user: User = new User(); // данные вводимого пользователя

  done: boolean = false;
  msg: string;
  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string, private userService: UserService) {
  }

  ngAfterContentInit(): void {
    this.userService.isAuth();
    this.userService.response.subscribe(
      (data: any) => {
        this.msg = data.message;
      },
      error => {
        console.log(error)
      }
    )
  }


  postData(user: User) {
    const body = {
      email: user.email, password: user.password, rememberMe: true,
      returnUrl: "/"
    };
    return this.http.post(this.baseUrl + 'api/Account/Login', body);
  }



  submit(user: User) {
    this.postData(user)
      .subscribe(
        (data: any) => {
          this.done = true;
          this.msg = data.message;
          this.userService.isAuth();

        },
        error => {
          console.log(error)
        }
      );
  }
  logout() {

    this.http.post(this.baseUrl + 'api/Account/LogOff', "")
      .subscribe(
        (data: any) => {
          this.msg = data.message;
          this.userService.isAuth()

        },
        error => {
          console.log(error)
        }
      );
    return
  }

}

class User {
  email: string;
  password: string;
}
