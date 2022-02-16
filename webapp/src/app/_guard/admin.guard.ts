import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from '../_services/account.service';
import { SnackbarService } from '../_services/snackbar.service';

@Injectable({
    providedIn: 'root'
})
export class AdminGuard implements CanActivate {
    constructor(private accountService: AccountService, private snackbarService: SnackbarService) { }
    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot): Observable<boolean> {
        return this.accountService.account$.pipe(map(user => {
            if (user.roles.includes('Admin')) return true;
            this.snackbarService.onError('You are not admin!');
            return false;
        }))
    }

}
