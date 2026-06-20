import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from '../models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  
  private apiUrl = 'http://localhost:5209/api/ticket';

  private http = inject(HttpClient);

  constructor() { }

  getTickets(): Observable<Ticket[]> {
    return this.http.get<Ticket[]>(this.apiUrl);
  }

  createTicket(ticket: Ticket): Observable<Ticket[]> {
    return this.http.post<Ticket[]>(this.apiUrl, ticket);
  }
}