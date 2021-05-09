import { Component, Inject, OnInit, Optional, ViewChild } from '@angular/core';
import { MatDialog, MatTable, MAT_DIALOG_DATA } from '@angular/material';
import { HttpClient } from '@angular/common/http';
import { mergeMap } from 'rxjs/operators';
import { UserService } from 'src/app/services/user/user.service';
import * as moment from 'moment';

export class ReportModel{
  startDate = new Date("2010-01-01");
  endDate = new Date();
}

@Component({
  selector: 'app-offenses-report',
  templateUrl: './offenses-report.component.html',
  styleUrls: ['./offenses-report.component.scss']
})

export class OffensesReportComponent implements OnInit {


  displayedColumns: string[] = ['number','sumOfPenalty'];
  public entity: any;

  dataSource: any;
  public reportModel = new ReportModel();
  api: string;

  licenseCountStr:string;
  licenseCount:number;

  @ViewChild(MatTable, { static: true }) table: MatTable<any>;

  constructor(
    public dialog: MatDialog,
    public http: HttpClient,
    @Inject('BASE_URL') public baseUrl: string,
    public userService:UserService
  ) {
    this.api = "Offenses/MakeReport";
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
    this.makeReport();

  }


  makeReport() {
    this.reportModel.startDate = new Date(this.reportModel.startDate)
    this.reportModel.endDate = new Date(this.reportModel.endDate)
    this.http.get(this.baseUrl + 'api/' + this.api, {
      params:{
        startDate: moment(this.reportModel.startDate).format("YYYY/MM/DD"),
        endDate: moment(this.reportModel.endDate).format("YYYY/MM/DD")
      },
    }).subscribe(result => {
      this.dataSource = result;
    }, error => console.error(error));
  }

}
