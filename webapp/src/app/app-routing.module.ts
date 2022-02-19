import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';
import { RegistComponent } from './regist/regist.component';
import { ShopMainComponent } from './shop/shop-main/shop-main.component';
import { TicketDetailAdminComponent } from './tickets/ticket-detail-admin/ticket-detail-admin.component';
import { TicketDetailComponent } from './tickets/ticket-detail/ticket-detail.component';
import { TicketListAdminComponent } from './tickets/ticket-list-admin/ticket-list-admin.component';
import { TicketListComponent } from './tickets/ticket-list/ticket-list.component';
import { Page404Component } from './_errors/page404/page404.component';
import { AdminGuard } from './_guard/admin.guard';
import { NewsDetailResolver } from './_resolver/news-detail.resolver';
import { TicketDetailResolver } from './_resolver/ticket-detail.resolver';

const routes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        children: [
            { path: 'regist', component: RegistComponent },
            { path: 'news/:id', component: NewsDetailComponent, resolve: { news: NewsDetailResolver } },
            { path: 'shop', component: ShopMainComponent },
            { path: 'tickets', component: TicketListComponent },
            { path: 'ticket/:id', component: TicketDetailComponent, resolve: { news: TicketDetailResolver } },
            { path: 'admin/tickets', component: TicketListAdminComponent },
            { path: 'admin/tickets/:id', component: TicketDetailAdminComponent, resolve: { ticket: TicketDetailResolver } }

        ]
    },
    { path: '**', component: Page404Component }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
