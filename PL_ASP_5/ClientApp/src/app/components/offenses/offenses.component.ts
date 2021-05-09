import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { MatDialog, MatTable, MAT_DIALOG_DATA } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { mergeMap } from 'rxjs/operators';
import { OffensesDialogComponent } from './offenses-dialog/offenses-dialog.component';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-offenses',
  templateUrl: './offenses.component.html',
  styleUrls: ['./offenses.component.scss']
})
export class OffensesComponent implements OnInit {



  displayedColumns: string[] = ['address','date','sum'];
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
    this.api = "Offenses";
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
    const dialogRef = this.dialog.open(OffensesDialogComponent, {
      width: '400px',
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
    // newRow.carDriver.vehicle.carOwnerId = newRow.carDriver.vehicle.carOwner.id
    // newRow.carDriver.vehicle.carOwner.id = undefined
    // newRow.carDriver.vehicleId = newRow.carDriver.vehicle.id
    // newRow.carDriver.vehicle.id = undefined
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
      
      const headers = { 'content-type': 'application/json' }
      // const body = JSON.stringify(new CarOwner(newRow));
      const body = JSON.stringify(newRow);
      this.http.put(this.baseUrl + 'api/' + this.api + '/' + newRow.id, body, { 'headers': headers })
        .pipe(mergeMap(async () => this.get()))
        .subscribe(data => {
        })
    }
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
