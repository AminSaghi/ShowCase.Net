import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditFeatureComponent } from './create-edit-feature.component';

describe('CreateEditFeatureComponent', () => {
  let component: CreateEditFeatureComponent;
  let fixture: ComponentFixture<CreateEditFeatureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditFeatureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditFeatureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
