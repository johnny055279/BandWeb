import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { HomeComponent } from './home/home.component';
import { AngularMaterialModule } from './_shared-modules/angular-material.module';
import { Page404Component } from './_errors/page404/page404.component';
import { Page500Component } from './_errors/page500/page500.component';
import { ToolbarComponent } from './_shared-components/toolbar/toolbar.component';
import { SidebarComponent } from './_shared-components/sidebar/sidebar.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ShopMainComponent } from './shop/shop-main/shop-main.component';
import { ShopCdComponent } from './shop/shop-music/shop-music.component';
import { ShopApparelComponent } from './shop/shop-apparel/shop-apparel.component';
import { ShopAccessoriesComponent } from './shop/shop-accessories/shop-accessories.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TicketListComponent } from './tickets/ticket-list/ticket-list.component';
import { TicketPurchaseComponent } from './tickets/ticket-purchase/ticket-purchase.component';
import { RegistComponent } from './regist/regist.component';
import { SnackBarComponent } from './_shared-components/snack-bar/snack-bar.component';
import { RequestInterceptor } from './_interceptors/request.interceptor';
import { LoginDialogComponent } from './dialogs/login-dialog/login-dialog.component';
import { TicketCardComponent } from './tickets/ticket-card/ticket-card.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NewsEditComponent } from './news/news-edit/news-edit.component';
import { TicketDetailComponent } from './tickets/ticket-detail/ticket-detail.component';
import { TicketListAdminComponent } from './tickets/ticket-list-admin/ticket-list-admin.component';
import { TicketDetailAdminComponent } from './tickets/ticket-detail-admin/ticket-detail-admin.component';
import { CheckRoleDirective } from './_directives/check-role.directive';
import { TicketAddDialogComponent } from './dialogs/ticket-add-dialog/ticket-add-dialog.component';
import { GoogleLoginProvider, SocialAuthServiceConfig } from 'angularx-social-login';
import { SocialLoginModule } from 'angularx-social-login';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';
import { NewsAddDialogComponent } from './dialogs/news-add-dialog/news-add-dialog.component';

@NgModule({
    declarations: [
        AppComponent,
        HomeComponent,
        Page404Component,
        Page500Component,
        ToolbarComponent,
        SidebarComponent,
        ShopMainComponent,
        ShopCdComponent,
        ShopApparelComponent,
        ShopAccessoriesComponent,
        TicketListComponent,
        TicketPurchaseComponent,
        RegistComponent,
        SnackBarComponent,
        LoginDialogComponent,
        TicketCardComponent,
        NewsDetailComponent,
        NewsListComponent,
        NewsEditComponent,
        TicketDetailComponent,
        TicketListAdminComponent,
        TicketDetailAdminComponent,
        CheckRoleDirective,
        TicketAddDialogComponent,
        NewsAddDialogComponent,
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        BrowserAnimationsModule,
        AngularMaterialModule,
        FormsModule,
        LayoutModule,
        FontAwesomeModule,
        ReactiveFormsModule,
        HttpClientModule,
        SocialLoginModule
    ],
    providers: [{ provide: HTTP_INTERCEPTORS, useClass: RequestInterceptor, multi: true },
    {
        provide: 'SocialAuthServiceConfig',
        useValue: {
            autoLogin: false,
            providers: [
                {
                    id: GoogleLoginProvider.PROVIDER_ID,
                    provider: new GoogleLoginProvider(
                        '510583077153-j9qcbi5b8lfd1alqeitc8jcahao56qiq.apps.googleusercontent.com'
                    )
                },
            ],
        } as SocialAuthServiceConfig
    },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
