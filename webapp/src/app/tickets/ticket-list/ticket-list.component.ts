import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Ticket } from 'src/app/_models/ticket';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-ticket-list',
    templateUrl: './ticket-list.component.html',
    styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
    baseUrl = environment.baseUrl;
    tickets?: Ticket[];
    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.getTickets();
    }

    getTickets() {
        this.http.get<Ticket[]>(this.baseUrl + "ticket?soldOut=" + false + "&completeShow=" + "false").subscribe((tickets) => {
            this.tickets = tickets;
        });
    }

}
