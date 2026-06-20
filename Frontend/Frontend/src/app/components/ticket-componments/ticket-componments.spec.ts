import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketComponments } from './ticket-componments';

describe('TicketComponments', () => {
  let component: TicketComponments;
  let fixture: ComponentFixture<TicketComponments>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TicketComponments],
    }).compileComponents();

    fixture = TestBed.createComponent(TicketComponments);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
