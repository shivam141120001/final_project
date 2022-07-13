import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetExecutiveDetailsComponent } from './get-executive-details.component';

describe('GetExecutiveDetailsComponent', () => {
  let component: GetExecutiveDetailsComponent;
  let fixture: ComponentFixture<GetExecutiveDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetExecutiveDetailsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GetExecutiveDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
