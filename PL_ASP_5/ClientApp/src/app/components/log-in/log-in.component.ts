import { AfterContentInit, AfterViewInit, Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements AfterContentInit{

  Roles: any = ['Admin', 'Author', 'Reader'];

  constructor(private http: HttpClient){ }

  ngAfterContentInit(): void {
    this.isAuth();
  }
  postData(user: User){
        
      const body = {email: user.email, password: user.password, rememberMe: true,
  returnUrl: "/"};
      return this.http.post('https://localhost:44314/api/Account/Login', body); 
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
  logout(){
        

    return this.http.post('https://localhost:44314/api/Account/LogOff', "")
    .subscribe(
      (data: any) => {
        this.error = data.message;
      },
        error => { console.log(error)
        }
    );
}
isAuth(){
        

  return this.http.post('https://localhost:44314/api/Account/isAuthenticated', "")
  .subscribe(
    (data: any) => {
      this.error = data.message;
    },
      error => { console.log(error)
      }
  );
}
}

class User {
  email: string;
  password: string;
}
