import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inject, Optional } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { DictionaryComponent } from '../../dictionary/dialog-edit/color-dialog.component';
@Component({
  selector: 'app-license-dialog',
  templateUrl: './license-dialog.component.html',
  styleUrls: ['./license-dialog.component.scss']
})
export class LicenseDialogComponent {
  selectedTakeStrokes: any;
  selectedCategories: any;
  action: string;
  local_data: any;
  allOwners: any;
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
      this.local_data.categories = [];

    }
    this.local_data.returnDate = new Date();

    this.action = this.local_data.action;
    this.getCarOwners();
  }
  getCarOwners() {
    this.http.get(this.baseUrl + 'api/CarOwners').subscribe(result => {
      this.allOwners = result;
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
  deleteStrokes() {
    var selected = this.selectedTakeStrokes;

    var current = this.local_data.takeStrokes
    for (var i = current.length - 1; i >= 0; i--) {
      for (var j = 0; j < selected.length; j++) {
        if (current[i] &&
          (current[i].id === selected[j].id)) {
            current.splice(i, 1);
        }
      }
    }
    console.log();
  }
  deleteCategory() {
    var selected = this.selectedCategories;

    var current = this.local_data.categories
    for (var i = current.length - 1; i >= 0; i--) {
      for (var j = 0; j < selected.length; j++) {
        if (current[i] &&
          (current[i].id === selected[j].id)) {
            current.splice(i, 1);
        }
      }
    }
    console.log();
  }
  compareFn(c1: any, c2: any): boolean {
    return c1 && c2 ? c1.id === c2.id : c1 === c2;
  }

  addCategory() {

    const dialogRef = this.dialog.open(DictionaryComponent, {
      width: '300px',
      data:{api:'Categories', pickable:true},

    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.data != undefined) {
        this.local_data.categories.push(result.data);
        // var vo = new VehicleOffense();
        // vo.penalty = result.penalty;
        // vo.articleOffense = result.data;
        // this.local_data.carDriver.vehicleOffenses.push(vo);
      }
    });
  }

}
