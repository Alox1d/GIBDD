import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ArticlesDialogComponent } from './articles-dialog.component';

describe('ArticlesDialogComponent', () => {
  let component: ArticlesDialogComponent;
  let fixture: ComponentFixture<ArticlesDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ArticlesDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ArticlesDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
