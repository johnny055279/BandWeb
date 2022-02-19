import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { City } from 'src/app/_models/city';
import { Ticket } from 'src/app/_models/ticket';
import { SystemService } from 'src/app/_services/system.service';

@Component({
    selector: 'app-ticket-detail-admin',
    templateUrl: './ticket-detail-admin.component.html',
    styleUrls: ['./ticket-detail-admin.component.css']
})
export class TicketDetailAdminComponent implements OnInit {
    ticket?: Ticket;
    cities?: City[];
    constructor(private route: ActivatedRoute, private systemService: SystemService) { }

    ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.ticket = data.ticket;
        });
        this.getcities();
    }

    getcities() {
        this.systemService.cities$.subscribe(cities => {
            this.cities = cities;
        })
    }



}
