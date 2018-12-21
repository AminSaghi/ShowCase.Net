import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListPagesComponent } from './list-pages.component';

describe('ListPagesComponent', () => {
  let component: ListPagesComponent;
  let fixture: ComponentFixture<ListPagesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListPagesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListPagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
