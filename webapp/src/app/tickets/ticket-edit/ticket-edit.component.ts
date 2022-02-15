import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, Observable, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { Ticket } from 'src/app/_models/ticket';
import { TicketService } from 'src/app/_services/ticket.service';

@Component({
    selector: 'app-ticket-edit',
    templateUrl: './ticket-edit.component.html',
    styleUrls: ['./ticket-edit.component.css']
})
export class TicketEditComponent implements AfterViewInit {
    displayedColumns: string[] = ['created', 'state', 'number', 'title'];
    data: Ticket[] = [];

    resultsLength = 0;
    isLoadingResults = true;
    isRateLimitReached = false;

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    constructor(private ticketService: TicketService) { }
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
            .subscribe(data => (this.data = data));

    }
}
