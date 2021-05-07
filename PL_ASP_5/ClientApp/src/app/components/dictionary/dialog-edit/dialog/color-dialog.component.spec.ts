import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NameEditDialogComponent } from './color-dialog.component';

describe('ColorDialogComponent', () => {
  let component: NameEditDialogComponent;
  let fixture: ComponentFixture<NameEditDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NameEditDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NameEditDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
