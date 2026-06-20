import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Ticket } from '../../models/ticket.model';
import { TicketService } from '../../services/ticket-service';

@Component({
  selector: 'app-ticket-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './ticket-dashboard.html',
  styleUrl: './ticket-dashboard.scss'
})
export class TicketDashboard implements OnInit {
  // Service ins Dashboard laden
  private ticketService = inject(TicketService);
  
  tickets: Ticket[] = [];

  ngOnInit() {
    this.ticketService.getTickets().subscribe(data => {
      this.tickets = data;
    });
  }
}