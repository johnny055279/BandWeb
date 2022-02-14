import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BusyService } from '../_services/busy.service';
import { NewsService } from '../_services/news.service';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
    constructor(public newsService: NewsService, public busyService: BusyService) { }

    ngOnInit(): void {
        this.newsService.getNews().subscribe(response => { });
    }
}

