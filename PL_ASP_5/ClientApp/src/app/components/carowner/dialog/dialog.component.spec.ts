import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogCarOwnerComponent } from './dialog.component';

describe('DialogComponent', () => {
  let component: DialogCarOwnerComponent;
  let fixture: ComponentFixture<DialogCarOwnerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DialogCarOwnerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogCarOwnerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
