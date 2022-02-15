import { Component, OnInit } from '@angular/core';
import { Ticket } from 'src/app/_models/ticket';
import { TicketService } from 'src/app/_services/ticket.service';

@Component({
    selector: 'app-ticket-list',
    templateUrl: './ticket-list.component.html',
    styleUrls: ['./ticket-list.component.css']
})
export class TicketListComponent implements OnInit {
    tickets?: Ticket[];
    constructor(private ticketService: TicketService) { }

    ngOnInit(): void {
        this.getTickets();
    }

    getTickets() {
        this.ticketService.getTickets(false, true).subscribe((tickets) => {
            this.tickets = tickets;
        });
    }

}
