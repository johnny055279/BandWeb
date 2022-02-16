import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Ticket } from '../_models/ticket';

@Injectable({
    providedIn: 'root'
})
export class TicketService {
    baseUrl = environment.baseUrl;
    constructor(private http: HttpClient) { }

    getTickets(soldOut: boolean, completeShow: boolean) {
        return this.http.get<Ticket[]>(this.baseUrl + 'ticket?soldOut=' + soldOut + '&completeShow=' + completeShow);
    };

    getTicketById(id: string) {
        return this.http.get<Ticket>(this.baseUrl + `ticket/${id}`);
    }

    addTicket(ticket: Ticket) {
        return this.http.post(this.baseUrl + 'ticket', ticket);
    }
}
