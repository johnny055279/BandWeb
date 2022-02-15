import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
    providedIn: 'root'
})
export class AccountService {
    baseUrl = environment.baseUrl;
    accountSource = new ReplaySubject<User>(1);
    account$ = this.accountSource.asObservable();
    constructor(private http: HttpClient) { }

    login(model: any) {
        return this.http.post<User>(this.baseUrl + "account/login", model).pipe(map(user => {
            if (user) {
                this.setUser(user);
            }
        }));
    }

    logout() {
        this.accountSource.next(undefined);
        sessionStorage.clear();
    }

    regist(model: any) {
        return this.http.post<User>(this.baseUrl + 'account/regist', model).pipe(map(user => {
            this.setUser(user);
        }));
    }

    setUser(user: User) {
        user.roles = [];
        const roles = this.getDecodedToken(user.token).role;
        Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);
        sessionStorage.setItem('user', JSON.stringify(user));
        this.accountSource.next(user);
    }

    getDecodedToken(token: string) {
        return JSON.parse(atob(token.split('.')[1]));
    }
}
