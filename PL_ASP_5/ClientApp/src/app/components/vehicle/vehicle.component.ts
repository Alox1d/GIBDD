import { Component, ViewChild, Inject, OnInit } from '@angular/core';
import { MatDialog, MatTable } from '@angular/material';
import { DialogBoxComponent } from './dialog-box/dialog-box.component';
import { HttpClient } from '@angular/common/http';
import { UserService } from 'src/app/services/user/user.service';

export interface Vehicle {
  id: number;
  registrationNumber: string;
  color: string;
  model: string;
  category: string;
  maintenanceDate: Date;
  maintenanceSuccess: boolean;
}

@Component({
  selector: 'none',
  templateUrl: './vehicle.component.html',
})

export class VehicleComponent implements OnInit {
  displayedColumns: string[] = ['registrationNumber', "color", "model", "category", 'maintenanceDate', 'maintenanceSuccess'];
  dataSource: any;
  public vehicle: Vehicle;
  isInspector = false;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(public dialog: MatDialog, public http: HttpClient, @Inject('BASE_URL') public baseUrl: string,  private userService : UserService) {
    http.get<Vehicle[]>(baseUrl + 'api/Vehicles').subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));

  }
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
    this.vehicle = {
      id: newRow.id,
      registrationNumber: newRow.registrationNumber,
      color: newRow.color,
      model: newRow.model,
      category: newRow.category,
      maintenanceDate: new Date(newRow.maintenanceDate),
      maintenanceSuccess: newRow.maintenanceSuccess,
    };
    this.dataSource.push(this.vehicle);


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
        value.color = newRow.color,
          value.model = newRow.model,
          value.category = newRow.category,
        value.maintenanceDate = new Date(newRow.maintenanceDate);
        value.maintenanceSuccess = newRow.maintenanceSuccess;
      }

      this.vehicle = {
        id: newRow.id,
        registrationNumber: newRow.registrationNumber,
        color: newRow.color,
        model: newRow.model,
        category: newRow.category,
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
