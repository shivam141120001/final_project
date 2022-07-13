import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailPropertyComponent } from './detail-property.component';

describe('DetailPropertyComponent', () => {
  let component: DetailPropertyComponent;
  let fixture: ComponentFixture<DetailPropertyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailPropertyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DetailPropertyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
