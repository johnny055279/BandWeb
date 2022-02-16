import { AfterViewInit, Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { startWith, switchMap, catchError, map } from 'rxjs/operators';
import { TicketAddDialogComponent } from 'src/app/dialogs/ticket-add-dialog/ticket-add-dialog.component';
import { Ticket } from 'src/app/_models/ticket';
import { TicketService } from 'src/app/_services/ticket.service';

@Component({
    selector: 'app-ticket-list-admin',
    templateUrl: './ticket-list-admin.component.html',
    styleUrls: ['./ticket-list-admin.component.css']
})
export class TicketListAdminComponent implements AfterViewInit {
    displayedColumns: string[] = ['number', 'showTime', 'title', 'location', 'price', 'purchaseDeadLine', 'operate'];
    data: Ticket[] = [];

    resultsLength = 0;
    isLoadingResults = true;
    isRateLimitReached = false;

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;
    constructor(private ticketService: TicketService, public dialog: MatDialog) { }

    ngAfterViewInit(): void {

        // If the user changes the sort order, reset back to the first page.
        this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

        merge(this.sort.sortChange, this.paginator.page)
            .pipe(
                startWith({}),
                switchMap(() => {
                    this.isLoadingResults = true;
                    return this.ticketService.getTickets(false, false).pipe(catchError(() => observableOf(null)));
                }),
                map(data => {
                    // Flip flag to show that loading has finished.
                    this.isLoadingResults = false;
                    this.isRateLimitReached = data === null;

                    if (data === null) {
                        return [];
                    }
                    // Only refresh the result length if there is new data. In case of rate
                    // limit errors, we do not want to reset the paginator to zero, as that
                    // would prevent users from re-triggering requests.
                    this.resultsLength = data.length;
                    return data;
                }),
            )
            .subscribe(data => { (this.data = data); console.log(data) });

    }


    openDialog() {
        const dialogRef = this.dialog.open(TicketAddDialogComponent, {
            width: '500px',
        });

        dialogRef.afterClosed().subscribe(response => {

        });
    }

}
