import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { TicketListAdminComponent } from 'src/app/tickets/ticket-list-admin/ticket-list-admin.component';
import { DropDownList } from 'src/app/_models/dropdown';
import { Ticket } from 'src/app/_models/ticket';
import { SystemService } from 'src/app/_services/system.service';

@Component({
    selector: 'app-ticket-add-dialog',
    templateUrl: './ticket-add-dialog.component.html',
    styleUrls: ['./ticket-add-dialog.component.css']
})
export class TicketAddDialogComponent implements OnInit {
    ticket: Ticket = {
        id: '',
        showTime: new Date(),
        cityId: '',
        cityName: '',
        price: 0,
        title: '',
        subTitle: '',
        remainNumber: 0,
        soldOut: false,
        completeShow: false,
        open: false,
        imageUrl: '',
        purchaseDeadLine: new Date()
    }
    cities?: DropDownList[];
    constructor(public dialogRef: MatDialogRef<TicketListAdminComponent>, private systemService: SystemService) { }

    ngOnInit(): void {
        this.systemService.dropDownList$.subscribe(dropdown => {
            this.cities = dropdown.find(n => n.type == 'City')?.list;
        });
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

}
