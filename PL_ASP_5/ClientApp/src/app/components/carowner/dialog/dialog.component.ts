import { Component} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inject, Optional } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ViewChild } from '@angular/core';
@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogCarOwnerComponent {
  @ViewChild('vehiclesView',{static:false}) selectedVehicles: any;
  action: string;
  local_data: any;
  vehicles: any;
  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<any>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    public http: HttpClient, 
    @Inject('BASE_URL') public baseUrl: string) {
    console.log(data);
    this.local_data = { ...data };
    if (this.local_data.vehicles == undefined) this.local_data.vehicles = [];
    this.action = this.local_data.action;
    this.getVehicles();
  }
  getVehicles() {
    this.http.get(this.baseUrl + 'api/Vehicles').subscribe(result => {
      this.vehicles = (result as any[]).filter(item=>item.carOwner == null);
    }, error => console.error(error));
  }
  onDropdownChange(e) {
    console.log(e)//you will get the id  
    this.local_data.vehicles.push(e.value); //if you want to bind it to your model
  }

  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }
  closeDialog() {
    this.dialogRef.close({ event: 'Отмена' });
  }
  delete(vehiclesView){
    var selected = this.selectedVehicles.selectedOptions.selected;
    //this.local_data.vehicles = this.local_data.vehicles
    //  .filter(item =>
    //    !selected.includes(item));
    for (var i = this.local_data.vehicles.length - 1; i >= 0; i--) {
      for (var j = 0; j < selected.length; j++) {
        if (this.local_data.vehicles[i] &&
          (this.local_data.vehicles[i].id === selected[j].value.id)) {
          this.local_data.vehicles.splice(i, 1);
        }
      }
    }
  }
  compareFn(c1: any, c2: any): boolean {
    return c1 && c2 ? c1.id === c2.id : c1 === c2;
  }
}
