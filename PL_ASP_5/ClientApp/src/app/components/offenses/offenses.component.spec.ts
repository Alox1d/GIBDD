import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OffensesComponent } from './offenses.component';

describe('OffensesComponent', () => {
  let component: OffensesComponent;
  let fixture: ComponentFixture<OffensesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OffensesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OffensesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
