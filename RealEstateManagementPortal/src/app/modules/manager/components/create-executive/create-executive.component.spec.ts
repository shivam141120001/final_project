import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateExecutiveComponent } from './create-executive.component';

describe('CreateExecutiveComponent', () => {
  let component: CreateExecutiveComponent;
  let fixture: ComponentFixture<CreateExecutiveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateExecutiveComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateExecutiveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
