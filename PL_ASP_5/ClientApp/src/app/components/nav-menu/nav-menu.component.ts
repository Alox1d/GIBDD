import { Component, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  isExpanded = false;
  loginTitle = "Вход";
  isInspector = false;
constructor (private userService:UserService){

}
user:Observable<any>

  ngOnInit(): void {
    
      this.user = this.userService.isAuth();
      this.user.subscribe(
      (data: any) => {
        if (data.roleName != "guest"){
          this.loginTitle = data.userName;
          if (data.roleName != "inspector"){
            this.isInspector = false;
          } else {
            this.isInspector = true;
          };
        } else {
          this.loginTitle = "Вход";
        };
        
      },
      error => {
        console.log(error)
      }
    )
  }
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
