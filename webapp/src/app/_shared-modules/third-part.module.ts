import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeagoCustomFormatter, TimeagoFormatter, TimeagoIntl, TimeagoModule } from 'ngx-timeago';



@NgModule({
    declarations: [],
    imports: [
        CommonModule,
        TimeagoModule.forRoot({
            intl: { provide: TimeagoIntl },
            formatter: { provide: TimeagoFormatter, useClass: TimeagoCustomFormatter }
        })
    ],
    exports: [
        TimeagoModule
    ]
})
export class ThirdPartModule { }
