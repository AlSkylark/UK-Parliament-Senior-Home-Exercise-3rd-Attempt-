import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaybandCardComponent } from './payband-card.component';

describe('PaybandCardComponent', () => {
  let component: PaybandCardComponent;
  let fixture: ComponentFixture<PaybandCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PaybandCardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PaybandCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
