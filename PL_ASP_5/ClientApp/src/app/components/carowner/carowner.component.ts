import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { MatDialog, MatTable, MAT_DIALOG_DATA } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { mergeMap } from 'rxjs/operators';
import { DialogCarOwnerComponent } from './dialog/dialog.component';
import { UserService } from 'src/app/services/user/user.service';

export class CarOwner {
  fio: string;
  passportData: number;
  id: number;
  constructor(data:any){
    this.id = data.id;
    this.fio = data.fio;
    this.passportData = data.passportData;
  }
}

@Component({
  selector: 'app-carowner',
  templateUrl: './carowner.component.html',
  styleUrls: ['./carowner.component.scss']
})
export class CarownerComponent implements OnInit {


  displayedColumns: string[] = ['fio','passportData','vehicles'];
  public entity: any;

  dataSource: any;
  api: string;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(
    public dialog: MatDialog,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public userService:UserService
  ) {
    this.api = "CarOwners";
    this.get();
  }
  private get() {
    this.http.get(this.baseUrl + 'api/' + this.api).subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  isInspector = false;
  ngOnInit(): void {
    this.userService.isAuth().subscribe(
      (data: any) => {
        if (data.roleName == "inspector"){
          this.isInspector = true;
          if (!this.displayedColumns.includes('action'))
          this.displayedColumns.push('action');

        } else {
          this.isInspector = false;

        };
      },
      error => {
        console.log(error)
      }
    )
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogCarOwnerComponent, {
      width: '300px',
      data: obj
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Добавить') {
        this.addRowData(result.data);
      } else if (result.event == 'Обновить') {
        this.updateRowData(result.data);
      } else if (result.event == 'Удалить') {
        this.deleteRowData(result.data);
      }
    });
  }

  addRowData(newRow) {

    // this.entity = new CarOwner(newRow);
    // this.dataSource.push(this.entity);

    // this.dataSource.push({
    //   id: newRow.id,
    //   name: newRow.name,
    // });

    // this.entity = {
    //   id: newRow.id,
    //   name: newRow.name,
    // };
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(newRow);
    this.http.post(this.baseUrl + 'api/' + this.api, body, { 'headers': headers })
      .pipe(mergeMap(async () => this.get()))
      .subscribe(data => {
    })
    this.table.renderRows();

  }
  updateRowData(newRow) {
    this.dataSource = this.dataSource.filter((value, key) => {

      if (value.id == newRow.id) {
        //newRow.update(value)
      }
      const headers = { 'content-type': 'application/json' }
      // const body = JSON.stringify(new CarOwner(newRow));
      const body = JSON.stringify(newRow);
      this.http.put(this.baseUrl + 'api/' + this.api + '/' + newRow.id, body, { 'headers': headers })
        .pipe(mergeMap(async () => this.get()))
        .subscribe(data => {
      })
      return true;
    });
  }
  deleteRowData(newRow) {
    this.dataSource = this.dataSource.filter((value, key) => {
      this.http.delete<any>(this.baseUrl + 'api/' + this.api + '/' + newRow.id).subscribe(data => {
      })
      return value.id != newRow.id;
    });
  }
}
