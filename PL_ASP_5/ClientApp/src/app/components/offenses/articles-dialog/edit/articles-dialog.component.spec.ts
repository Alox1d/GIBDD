import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlesEditDialogComponent } from './articles-dialog.component';

describe('ArticlesDialogComponent', () => {
  let component: ArticlesEditDialogComponent;
  let fixture: ComponentFixture<ArticlesEditDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticlesEditDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticlesEditDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
