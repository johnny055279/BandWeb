import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DropDownList } from 'src/app/_models/dropdown';
import { Ticket } from 'src/app/_models/ticket';
import { SystemService } from 'src/app/_services/system.service';

@Component({
    selector: 'app-ticket-detail-admin',
    templateUrl: './ticket-detail-admin.component.html',
    styleUrls: ['./ticket-detail-admin.component.css']
})
export class TicketDetailAdminComponent implements OnInit {
    ticket?: Ticket;
    cities?: DropDownList[];
    constructor(private route: ActivatedRoute, private systemService: SystemService) { }

    ngOnInit(): void {
        this.route.data.subscribe(data => {
            this.ticket = data.ticket;
        });
        this.getcities();
    }

    getcities() {
        this.systemService.dropDownList$.subscribe(dropdown => {
            this.cities = dropdown.find(n => n.type == 'City')?.list;
        });
    }



}
