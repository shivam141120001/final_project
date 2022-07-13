import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetCustomersAssignedComponent } from './get-customers-assigned.component';

describe('GetCustomersAssignedComponent', () => {
  let component: GetCustomersAssignedComponent;
  let fixture: ComponentFixture<GetCustomersAssignedComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GetCustomersAssignedComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GetCustomersAssignedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
