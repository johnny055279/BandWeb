import { Component, Input, OnInit } from '@angular/core';
import { Ticket } from 'src/app/_models/ticket';

@Component({
    selector: 'app-ticket-detail',
    templateUrl: './ticket-detail.component.html',
    styleUrls: ['./ticket-detail.component.css']
})
export class TicketDetailComponent implements OnInit {
    @Input() ticket?: Ticket;
    constructor() { }

    ngOnInit(): void {
    }

}
