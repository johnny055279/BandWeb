import { HttpClient } from '@angular/common/http';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { merge, of as observableOf } from 'rxjs';
import { startWith, switchMap, catchError, map } from 'rxjs/operators';
import { NewsAddDialogComponent } from 'src/app/dialogs/news-add-dialog/news-add-dialog.component';
import { News } from 'src/app/_models/news';
import { NewsService } from 'src/app/_services/news.service';
import { SnackbarService } from 'src/app/_services/snackbar.service';
import { environment } from 'src/environments/environment';

@Component({
    selector: 'app-news-list',
    templateUrl: './news-list.component.html',
    styleUrls: ['./news-list.component.css']
})
export class NewsListComponent implements AfterViewInit {

    displayedColumns: string[] = ['number', 'title', 'subtitle', 'content', 'postDate'];
    data: News[] = [];
    resultsLength = 0;
    isLoadingResults = true;
    isRateLimitReached = false;
    baseUrl = environment.baseUrl;
    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;
    constructor(private newsService: NewsService, public dialog: MatDialog, private http: HttpClient, private snackbarService: SnackbarService) { }

    ngAfterViewInit(): void {

        // If the user changes the sort order, reset back to the first page.
        this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

        merge(this.sort.sortChange, this.paginator.page)
            .pipe(
                startWith({}),
                switchMap(() => {
                    this.isLoadingResults = true;
                    return this.newsService.newList$.pipe(catchError(() => observableOf(null)));
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
        const dialogRef = this.dialog.open(NewsAddDialogComponent, {
            width: '500px',
        });

        dialogRef.afterClosed().subscribe((ticket: News) => {
            this.createTicket(ticket);
        });
    }

    createTicket(ticket: News) {
        this.http.post(this.baseUrl + "ticket", ticket).subscribe(() => {
            this.snackbarService.onSuccess('Success!');
        });
    }

}

