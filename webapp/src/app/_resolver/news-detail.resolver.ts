import { Injectable } from '@angular/core';
import {
    Router, Resolve,
    RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { News } from '../_models/news';
import { NewsService } from '../_services/news.service';

@Injectable({
    providedIn: 'root'
})
export class NewsDetailResolver implements Resolve<News> {

    constructor(private newsService: NewsService) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<News> | Promise<News> {
        return this.newsService.getNewsById(route.paramMap.get('id') || '');
    }
}
