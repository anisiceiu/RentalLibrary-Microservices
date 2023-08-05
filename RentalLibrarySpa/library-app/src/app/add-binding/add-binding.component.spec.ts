import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddBindingComponent } from './add-binding.component';

describe('AddBindingComponent', () => {
  let component: AddBindingComponent;
  let fixture: ComponentFixture<AddBindingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddBindingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddBindingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
