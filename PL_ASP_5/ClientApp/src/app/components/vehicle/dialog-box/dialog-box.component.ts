import { HttpClient } from '@angular/common/http';
import { Component, Inject, Optional } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Dictionary, DictionaryComponent as DictionaryComponent } from '../../dictionary/dialog-edit/color-dialog.component';
import { Vehicle } from '../vehicle.component';



@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.css']
})
export class DialogBoxComponent {
  action: string;
  local_data: any;
  colors: any;
  models: any;
  categories: any;
  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<DialogBoxComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: Vehicle,
    public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
    console.log(data);
    this.local_data = { ...data };
    this.action = this.local_data.action;
    this.getColors()
    this.getModels();
    this.getCategories();
  }
  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }
  closeDialog() {
    this.dialogRef.close({ event: 'Отмена' });
  }

  getColors() {
    this.http.get<Dictionary[]>(this.baseUrl + 'api/Colors').subscribe(result => {
      this.colors = result;
    }, error => console.error(error));
  }
  getModels() {
    this.http.get<Dictionary[]>(this.baseUrl + 'api/Models').subscribe(result => {
      this.models = result;
    }, error => console.error(error));
  }
  getCategories() {
    this.http.get<Dictionary[]>(this.baseUrl + 'api/Categories').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
  }

  onDropdownChange(e) {
    console.log(e)//you will get the id  
    // this.local_data.color.id = e //if you want to bind it to your model
  }
 compareFn(c1: any, c2: any): boolean {
    return c1 && c2 ? c1.id === c2.id : c1 === c2;
  }

  openColors() {
    const dialogRef = this.dialog.open(DictionaryComponent, {
      width: '300px',
      data:{api:'Colors', pickable:false},

    });
    this.getColors();
  }
  openModels(){
    const dialogRef = this.dialog.open(DictionaryComponent, {
      width: '300px',
      data:{api:'Models', pickable:false},

    });
    this.getModels();
  }
  openCategories(){
    const dialogRef = this.dialog.open(DictionaryComponent, {
      width: '300px',
      data:{api:'Categories', pickable:false},
    });
    this.getCategories();
  }
}
// export default  function compareFn(c1: any, c2: any): boolean {
  //     return c1 && c2 ? c1.id === c2.id : c1 === c2;
  //   }