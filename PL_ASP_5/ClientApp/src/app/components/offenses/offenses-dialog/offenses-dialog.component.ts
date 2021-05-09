import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inject, Optional } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ArticlesDialogComponent } from '../articles-dialog/articles-dialog.component';
class VehicleOffense {
  id: number;
  penalty: number;
  takeLicenseTime: number;
  carDriver: object;
  articleOffense: object;
  articleOffenseId: number

}
class Offense {
  id: number;
  address: number;
  carDriver: CarDriver;
  articleOffense: object
}
class CarDriver {
  id: number;
  vehicle: object;
}

@Component({
  selector: 'app-offenses-dialog',
  templateUrl: './offenses-dialog.component.html',
  styleUrls: ['./offenses-dialog.component.scss']
})
export class OffensesDialogComponent {

  selectedVO: any;
  action: string;
  local_data: any;
  allVehicles: any;
  selectedVehicle: any;
  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<any>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string) {
    console.log(data);
    this.local_data = { ...data };
    if (this.local_data.action == 'Добавить') {
      this.local_data.vehicles = [];
      this.local_data.carDriver = {};
      this.local_data.carDriver.vehicle = {};
      this.local_data.carDriver.vehicleOffenses = [];
    }
    this.local_data.returnDate = new Date();

    this.action = this.local_data.action;
    this.getVehicles();
  }
  getVehicles() {
    this.http.get(this.baseUrl + 'api/Vehicles').subscribe(result => {
      this.allVehicles = result;
    }, error => console.error(error));
  }
  onDropdownChange(e) {
    // console.log(e)//you will get the id  
    // this.local_data.vehicles.push(e.value); //if you want to bind it to your model
  }

  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }
  closeDialog() {
    this.dialogRef.close({ event: 'Отмена' });
  }
  deleteVO() {
    var selected = this.selectedVO;
    //this.local_data.vehicles = this.local_data.vehicles
    //  .filter(item =>
    //    !selected.includes(item));
    for (var i = this.local_data.carDriver.vehicleOffenses.length - 1; i >= 0; i--) {
      for (var j = 0; j < selected.length; j++) {
        if (this.local_data.carDriver.vehicleOffenses[i] &&
          (this.local_data.carDriver.vehicleOffenses[i].id === selected[j].id)) {
          this.local_data.carDriver.vehicleOffenses.splice(i, 1);
        }
      }
    }
    console.log();
  }
  compareFn(c1: any, c2: any): boolean {
    return c1 && c2 ? c1.id === c2.id : c1 === c2;
  }
  getCarOwner(isCarOwner) {
    console.log(isCarOwner)
    if (isCarOwner) {
      //  this.selectedVehicles.carOwner.fio;

      this.http.get(this.baseUrl + 'api/Vehicles').subscribe(result => {
        this.allVehicles = result;
      }, error => console.error(error));
    }
  }
  add() {
    const dialogRef = this.dialog.open(ArticlesDialogComponent, {
      width: '1280px',
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.data != undefined && result.penalty != undefined) {
        var vo = new VehicleOffense();
        vo.penalty = result.penalty;
        vo.articleOffense = result.data;
        // vo.articleOffenseId = result.data.id;
        // result.data.id = undefined;

        this.local_data.carDriver.vehicleOffenses.push(vo);
        // var sum = 0;
        // for (let item of this.local_data.carDriver.vehicleOffenses)
        // {
        //     sum += item.penalty;
        // }
        // this.local_data.sumPenalty = sum;
      }
    });
  }

}
