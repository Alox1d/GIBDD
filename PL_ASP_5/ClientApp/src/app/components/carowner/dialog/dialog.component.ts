import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Inject, Optional } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogCarOwnerComponent {

  action: string;
  local_data: any;
  colors: any;
  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<any>,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
    public http: HttpClient, 
    @Inject('BASE_URL') public baseUrl: string) {
    console.log(data);
    this.local_data = { ...data };
    this.action = this.local_data.action;
  }



  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }
  closeDialog() {
    this.dialogRef.close({ event: 'Отмена' });
  }
}
