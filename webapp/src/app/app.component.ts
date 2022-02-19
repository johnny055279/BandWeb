import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';
import { SystemService } from './_services/system.service';
@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {

    title = 'BandApp';

    constructor(private accountService: AccountService, private systemService: SystemService) { }

    ngOnInit(): void {
        var result = sessionStorage.getItem('user');
        if (result) {
            const user: User = JSON.parse(result);
            this.accountService.setUser(user);
        }

        this.systemService.getCities().subscribe();
    }


}
