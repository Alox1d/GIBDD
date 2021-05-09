import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DriverLicensesComponent } from './driver-licenses.component';

describe('DriverLicensesComponent', () => {
  let component: DriverLicensesComponent;
  let fixture: ComponentFixture<DriverLicensesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DriverLicensesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DriverLicensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
