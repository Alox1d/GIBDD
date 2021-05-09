import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MatTable, MAT_DIALOG_DATA } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { NameEditDialogComponent } from '../../dictionary/dialog-edit/dialog/color-dialog.component';
import { mergeMap } from 'rxjs/operators';

export class Dictionary {
  name: string;
  id: number;
  constructor(data:any){
    this.id = data.id;
    this.name = data.name;
  }
}

@Component({
  selector: 'app-color-dialog',
  templateUrl: './color-dialog.component.html',
  styleUrls: ['./color-dialog.component.scss']
})
export class DictionaryComponent implements OnInit {

  displayedColumns: string[] = ['name', 'action'];
  dataSource: any;
  local_data: any;
  public entity: Dictionary;
  api: string;
  pickable: boolean;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<DictionaryComponent>,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.local_data = {...data};
    this.api = this.local_data.api;
    this.pickable = this.local_data.pickable;
    this.get();
  }
  private get() {
    this.http.get<Dictionary[]>(this.baseUrl + 'api/' + this.api).subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {

  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(NameEditDialogComponent, {
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
    // this.entity = {
    //   id: newRow.id,
    //   name: newRow.name,
    // };
    this.entity = new Dictionary(newRow);
    this.dataSource.push(this.entity);

    // this.dataSource.push({
    //   id: newRow.id,
    //   name: newRow.name,
    // });

    // this.entity = {
    //   id: newRow.id,
    //   name: newRow.name,
    // };
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(this.entity);
    this.http.post(this.baseUrl + 'api/' + this.api, body, { 'headers': headers })
      .pipe(mergeMap(async () => this.get()))
      .subscribe(data => {
    })
    this.table.renderRows();

  }
  updateRowData(newRow:Dictionary) {
    this.dataSource = this.dataSource.filter((value, key) => {

      if (value.id == newRow.id) {
        //newRow.update(value)
      }

      // this.entity = {
      //   id: newRow.id,
      //   name: newRow.name,
      // };
      const headers = { 'content-type': 'application/json' }
      const body = JSON.stringify(new Dictionary(newRow));
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
  accept(element) {

    this.dialogRef.close({ data: element});
  }
}
