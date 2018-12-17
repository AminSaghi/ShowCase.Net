import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ListFeaturesComponent } from './list-features.component';

describe('ListFeaturesComponent', () => {
  let component: ListFeaturesComponent;
  let fixture: ComponentFixture<ListFeaturesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ListFeaturesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ListFeaturesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
