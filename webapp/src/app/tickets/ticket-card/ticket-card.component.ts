import { Component, Input, OnInit } from '@angular/core';
import { Ticket } from 'src/app/_models/ticket';

@Component({
    selector: 'app-ticket-card',
    templateUrl: './ticket-card.component.html',
    styleUrls: ['./ticket-card.component.css']
})
export class TicketCardComponent implements OnInit {
    @Input() ticket?: Ticket;
    constructor() { }

    ngOnInit(): void {
    }

}
