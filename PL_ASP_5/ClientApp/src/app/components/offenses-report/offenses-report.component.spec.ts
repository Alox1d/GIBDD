import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OffensesReportComponent } from './offenses-report.component';

describe('OffensesReportComponent', () => {
  let component: OffensesReportComponent;
  let fixture: ComponentFixture<OffensesReportComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OffensesReportComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OffensesReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
