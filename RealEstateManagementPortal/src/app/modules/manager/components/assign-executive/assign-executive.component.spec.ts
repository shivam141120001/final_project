import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignExecutiveComponent } from './assign-executive.component';

describe('AssignExecutiveComponent', () => {
  let component: AssignExecutiveComponent;
  let fixture: ComponentFixture<AssignExecutiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AssignExecutiveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignExecutiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
