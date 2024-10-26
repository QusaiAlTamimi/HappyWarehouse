import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WerahousesComponent } from './werahouses.component';

describe('WerahousesComponent', () => {
  let component: WerahousesComponent;
  let fixture: ComponentFixture<WerahousesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WerahousesComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(WerahousesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
