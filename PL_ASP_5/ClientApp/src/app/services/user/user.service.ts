import { HttpClient } from '@angular/common/http';
import {Inject, Injectable}             from '@angular/core';
import {Observable, Subject}                from 'rxjs';

@Injectable()
export class UserService {

    private rSource: Subject<any> = new Subject();
    public response = this.rSource.asObservable();
    msg: string;
    
    constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    }

    /**
     * Use to change user name 
     * @data type: string
     */
    userNameChange(data: string) {
        //this.source.next(data);
    }

    isAuth():Observable<any> {

      this.http.post(this.baseUrl + 'api/Account/isAuthenticated', "").subscribe(
        data => {
          this.rSource.next(data);
        }
      );
      return this.response
    }
}
