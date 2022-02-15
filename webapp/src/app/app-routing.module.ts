import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewsComponent } from './news/news.component';
import { RegistComponent } from './regist/regist.component';
import { ShopMainComponent } from './shop/shop-main/shop-main.component';
import { TicketDetailComponent } from './tickets/ticket-detail/ticket-detail.component';
import { TicketListComponent } from './tickets/ticket-list/ticket-list.component';
import { Page404Component } from './_errors/page404/page404.component';
import { NewsDetailResolver } from './_resolver/news-detail.resolver';

const routes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        children: [
            { path: 'regist', component: RegistComponent },
            { path: 'news/:id', component: NewsComponent, resolve: { news: NewsDetailResolver } },
            { path: 'shop', component: ShopMainComponent },
            { path: 'tickets', component: TicketListComponent },
            { path: 'tickets/:id', component: TicketDetailComponent }
        ]
    },
    { path: '**', component: Page404Component }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
