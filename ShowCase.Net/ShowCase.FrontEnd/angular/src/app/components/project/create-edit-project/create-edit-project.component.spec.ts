import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateEditProjectComponent } from './create-edit-project.component';

describe('CreateEditProjectComponent', () => {
  let component: CreateEditProjectComponent;
  let fixture: ComponentFixture<CreateEditProjectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CreateEditProjectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateEditProjectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
