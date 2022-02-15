import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';
import { SnackbarService } from '../_services/snackbar.service';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private accountService: AccountService, private snackbarService: SnackbarService) { }

    canActivate(): Observable<boolean> {
        return this.accountService.account$.pipe(map(user => {
            if (user) return true;
            this.snackbarService.onError('Auth fail!');
            return false;
        }));
    }

}
