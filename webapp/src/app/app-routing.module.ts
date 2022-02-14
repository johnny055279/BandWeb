import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { ShopMainComponent } from './shop/shop-main/shop-main.component';
import { TicketDetailComponent } from './tickets/ticket-detail/ticket-detail.component';
import { TicketListComponent } from './tickets/ticket-list/ticket-list.component';
import { Page404Component } from './_errors/page404/page404.component';

const routes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        children: [
            { path: 'login', component: LoginComponent },
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
