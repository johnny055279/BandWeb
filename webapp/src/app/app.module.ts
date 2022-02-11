import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AngularMaterialModule } from './_shared-modules/angular-material.module';
import { IntroComponent } from './intro/intro.component';
import { Page404Component } from './_errors/page404/page404.component';
import { Page500Component } from './_errors/page500/page500.component';
import { ToolbarComponent } from './_shared-components/toolbar/toolbar.component';
import { SidebarComponent } from './_shared-components/sidebar/sidebar.component';
import { ScrollSpyDirective } from './_directives/scroll-spy.directive';
import { TicketComponent } from './ticket/ticket.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ShopMainComponent } from './shop/shop-main/shop-main.component';
import { ShopCdComponent } from './shop/shop-music/shop-music.component';
import { ShopApparelComponent } from './shop/shop-apparel/shop-apparel.component';
import { ShopAccessoriesComponent } from './shop/shop-accessories/shop-accessories.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    IntroComponent,
    Page404Component,
    Page500Component,
    ToolbarComponent,
    SidebarComponent,
    ScrollSpyDirective,
    TicketComponent,
    ShopMainComponent,
    ShopCdComponent,
    ShopApparelComponent,
    ShopAccessoriesComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    LayoutModule,
    FontAwesomeModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
