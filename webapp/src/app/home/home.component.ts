import { DatePipe } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NewsList } from '../_models/home/newsList';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
    baseUrl = environment.baseUrl;
    newsListSource = new BehaviorSubject<NewsList[]>([]);
    newList$ = this.newsListSource.asObservable();
    constructor(private http: HttpClient) { }

    ngOnInit(): void {
        this.getNews();
    }

    getNews() {
        this.http.get<NewsList[]>(this.baseUrl + "news").subscribe((response) => {
            this.newsListSource.next(response);
        });
    }
}

