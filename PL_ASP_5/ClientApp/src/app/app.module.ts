import { BrowserModule } from '@angular/platform-browser';
import { NgModule , CUSTOM_ELEMENTS_SCHEMA} from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { VehicleComponent } from './components/vehicle/vehicle.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
/* Angular material */
import { AngularMaterialModule } from './angular-material.module';
import { MatMomentDateModule, MomentDateAdapter, MAT_MOMENT_DATE_ADAPTER_OPTIONS  } from "@angular/material-moment-adapter";
import {
  MatTableModule,
  MatDialogModule,
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule,
  MatCheckboxModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MAT_DATE_LOCALE
} from '@angular/material';
import { DialogBoxComponent } from './components/vehicle/dialog-box/dialog-box.component';
import { LogInComponent } from './components/log-in/log-in.component';
import { RegisterComponent } from './components/register/register.component';
import { FlexLayoutModule } from '@angular/flex-layout';
import { UserService } from './services/user/user.service';
import { NameEditDialogComponent } from './components/dictionary/dialog-edit/dialog/color-dialog.component';
import { DictionaryComponent } from './components/dictionary/dialog-edit/color-dialog.component';
import { CarownerComponent } from './components/carowner/carowner.component';
import { DialogCarOwnerComponent } from './components/carowner/dialog/dialog.component';
import { OffensesComponent } from './components/offenses/offenses.component';
import { OffensesDialogComponent } from './components/offenses/offenses-dialog/offenses-dialog.component';
import {  ArticlesDialogComponent } from './components/offenses/articles-dialog/articles-dialog.component';
import { ArticlesEditDialogComponent } from './components/offenses/articles-dialog/edit/articles-dialog.component';
import { DriverLicensesComponent } from './components/driver-licenses/driver-licenses.component';
import { LicenseDialogComponent } from './components/driver-licenses/license-dialog/license-dialog.component';
import { OffensesReportComponent } from './components/offenses-report/offenses-report.component';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    VehicleComponent,
    DialogBoxComponent,
    LogInComponent,
    RegisterComponent,
    DictionaryComponent,
    NameEditDialogComponent,
    CarownerComponent,
    DialogCarOwnerComponent,
    OffensesComponent,
    OffensesDialogComponent,
    ArticlesEditDialogComponent,
    ArticlesDialogComponent,
    DriverLicensesComponent,
    LicenseDialogComponent,
    OffensesReportComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AngularMaterialModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDatepickerModule,
    ReactiveFormsModule,
    MatNativeDateModule,
    MatMomentDateModule,
    FlexLayoutModule,
    RouterModule.forRoot([
      { path: 'carowners', component: CarownerComponent, pathMatch: 'full' },
      { path: 'offensesReport', component: OffensesReportComponent, pathMatch: 'full' },
      { path: 'offenses', component: OffensesComponent},
      { path: 'driverLicenses', component: DriverLicensesComponent},
      { path: 'vehicles', component: VehicleComponent, pathMatch: 'full' },
      { path: '', redirectTo:'vehicles',pathMatch: 'full' },
      { path: 'login', component: LogInComponent },
      { path: 'register', component: RegisterComponent },
    ])
  ],
  entryComponents: [
    DialogBoxComponent,
    DictionaryComponent,
    NameEditDialogComponent,
    DialogCarOwnerComponent,
    OffensesDialogComponent,
    ArticlesEditDialogComponent,
    ArticlesDialogComponent,
    LicenseDialogComponent
  ],
  providers: [
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } },
    {provide: MAT_DATE_LOCALE, useValue: 'ru'},
    UserService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
