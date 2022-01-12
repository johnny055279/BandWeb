import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { IntroComponent } from './intro/intro.component';
import { LoginComponent } from './login/login.component';
import { ShopMainComponent } from './shop/shop-main/shop-main.component';
import { TicketComponent } from './ticket/ticket.component';
import { Page404Component } from './_errors/page404/page404.component';

const routes: Routes = [
  { path: '', component: IntroComponent, data: {animation: 'intro'} },
  {
    path: '',
    children:[
      { path: 'login', component: LoginComponent },
      { path: 'home', component: HomeComponent, data: {animation: 'home'} },
      { path: 'tour', component: TicketComponent },
      { path: 'shop', component: ShopMainComponent }
    ]
  },
  { path: '**', component: Page404Component }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
