import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { News } from '../_models/home/news';

@Component({
    selector: 'app-news',
    templateUrl: './news.component.html',
    styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {

    news!: News;
    constructor(private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.route.data.subscribe(response => {
            this.news = response.news;
        });
    }

}
