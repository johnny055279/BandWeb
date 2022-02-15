import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { News } from '../_models/news';

@Injectable({
    providedIn: 'root'
})
export class NewsService {
    baseUrl = environment.baseUrl;
    newsListSource = new BehaviorSubject<News[]>([]);
    newList$ = this.newsListSource.asObservable();
    constructor(private http: HttpClient,) { }

    getNews() {
        return this.http.get<News[]>(this.baseUrl + "news").pipe(map(response => {
            if (response) return this.newsListSource.next(response);
        }));
    };

    getNewsById(id: string) {
        return this.http.get<News>(this.baseUrl + `news/${id}`);
    };

    editNews() {

    }
}
