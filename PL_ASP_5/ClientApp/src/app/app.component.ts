import { Component, ViewChild, Inject } from '@angular/core';
import { MatDialog, MatTable } from '@angular/material';
import { DialogBoxComponent } from './components/dialog-box/dialog-box.component';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})

export class AppComponent {
  displayedColumns: string[] = ['registrationNumber', 'maintenanceDate', 'maintenanceSuccess', 'action'];
  dataSource: any;
  public vehicle: Vehicle;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(public dialog: MatDialog, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    http.get<Vehicle[]>(baseUrl + 'api/Vehicles').subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(DialogBoxComponent, {
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

    this.dataSource.push({
      id: newRow.id,
      registrationNumber: newRow.registrationNumber,
      maintenanceDate: new Date(newRow.maintenanceDate),
      maintenanceSuccess: newRow.maintenanceSuccess,
    });

    this.vehicle = {
      id: newRow.id,
      registrationNumber: newRow.registrationNumber,
      maintenanceDate: new Date(newRow.maintenanceDate),
      maintenanceSuccess: newRow.maintenanceSuccess,
    };
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(this.vehicle);
    this.http.post<Vehicle>(this.baseUrl + 'api/Vehicles', body, { 'headers': headers }).subscribe(data => {
    })

    this.table.renderRows();

  }
  updateRowData(newRow) {
    this.dataSource = this.dataSource.filter((value, key) => {

      if (value.id == newRow.id) {
        value.registrationNumber = newRow.registrationNumber;
        value.maintenanceDate = new Date(newRow.maintenanceDate);
        value.maintenanceSuccess = newRow.maintenanceSuccess;
      }

      this.vehicle = {
        id: newRow.id,
        registrationNumber: newRow.registrationNumber,
        maintenanceDate: new Date(newRow.maintenanceDate),
        maintenanceSuccess: newRow.maintenanceSuccess,
      };
      const headers = { 'content-type': 'application/json' }
      const body = JSON.stringify(this.vehicle);
      this.http.put<Vehicle[]>(this.baseUrl + 'api/Vehicles/' + newRow.id, body, { 'headers': headers }).subscribe(data => {
      })
      return true;
    });
  }
  deleteRowData(newRow) {
    this.dataSource = this.dataSource.filter((value, key) => {
      this.http.delete<any>(this.baseUrl + 'api/Vehicles/' + newRow.id).subscribe(data => {
      })
      return value.id != newRow.id;
    });
  }
}


interface Vehicle {
  id: number;
  registrationNumber: string;
  maintenanceDate: Date;
  maintenanceSuccess: boolean;
}
