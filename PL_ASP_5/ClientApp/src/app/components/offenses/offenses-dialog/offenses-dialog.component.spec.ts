import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OffensesDialogComponent } from './offenses-dialog.component';

describe('OffensesDialogComponent', () => {
  let component: OffensesDialogComponent;
  let fixture: ComponentFixture<OffensesDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OffensesDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OffensesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
