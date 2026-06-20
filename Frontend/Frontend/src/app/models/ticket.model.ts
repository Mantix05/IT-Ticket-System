export enum TicketStatus {
  Offen = 'Offen',
  InBearbeitung = 'InBearbeitung',
  Geschlossen = 'Geschlossen'
}

export interface Ticket {
  id?: number;
  title: string;
  description: string;
  status: TicketStatus | string;
}