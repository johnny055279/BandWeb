import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { TicketListAdminComponent } from 'src/app/tickets/ticket-list-admin/ticket-list-admin.component';
import { FileTransform } from 'src/app/_helper/fileTransform';
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
        id: undefined,
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
        imageBase64: '',
        purchaseDeadLine: new Date(),
    }
    minDate = new Date();
    cities?: DropDownList[];
    tempImage?: SafeUrl;
    fileTransform = new FileTransform();
    constructor(public dialogRef: MatDialogRef<TicketListAdminComponent>, private systemService: SystemService, private domSanitizer: DomSanitizer) { }

    ngOnInit(): void {
        this.systemService.dropDownList$.subscribe(dropdown => {
            this.cities = dropdown.find(n => n.type == 'City')?.list;
        });
    }

    onNoClick(): void {
        this.dialogRef.close();
        this.tempImage = undefined;
    }

    setImageUrl(event: any) {
        const file = event.target.files[0];
        if (!file) return;
        this.fileTransform.FileToBase64(this, file, this.GetBase64);

    }

    GetBase64(component: any, base64: string) {
        component.tempImage = component.domSanitizer.bypassSecurityTrustUrl('data:image/jpeg;base64,' + base64);
        component.ticket.imageBase64 = base64;
    }
}
