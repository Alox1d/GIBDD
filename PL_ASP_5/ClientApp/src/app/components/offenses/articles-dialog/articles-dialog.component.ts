import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { MatDialog, MatDialogRef, MatTable, MAT_DIALOG_DATA } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { NameEditDialogComponent } from '../../dictionary/dialog-edit/dialog/color-dialog.component';
import { mergeMap } from 'rxjs/operators';
import { ArticlesEditDialogComponent } from './edit/articles-dialog.component';
@Component({
  selector: 'app-articles-dialog',
  templateUrl: './articles-dialog.component.html',
  styleUrls: ['./articles-dialog.component.scss']
})
export class ArticlesDialogComponent implements OnInit {


  displayedColumns: string[] = ['number', 'description', 'penalty', 'action'];
  dataSource: any;
  public entity: any;
  api: string;
  public penalty:number = 0;
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(
    public dialog: MatDialog,
    public dialogRef: MatDialogRef<ArticlesDialogComponent>,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    @Optional() @Inject(MAT_DIALOG_DATA) public data: string,
  ) {
    this.api = "ArticleOffenses";
    this.get();
  }
  private get() {
    this.http.get(this.baseUrl + 'api/' + this.api).subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

  ngOnInit(): void {

  }

  openDialog(action, obj) {
    obj.action = action;
    const dialogRef = this.dialog.open(ArticlesEditDialogComponent, {
      width: '600px',
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
      this.http.delete(this.baseUrl + 'api/' + this.api + '/' + newRow.id).subscribe(data => {
      })
      return value.id != newRow.id;
    });
  }
  selectedRowIndex = -1;

  highlight(row) {
    console.log(row)
    this.selectedRowIndex = row.id;
  }
  accept(element) {

    this.dialogRef.close({ data: element, penalty: this.penalty});
  }
}
