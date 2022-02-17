import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SocialAuthService, GoogleLoginProvider } from "angularx-social-login";
import { environment } from 'src/environments/environment';
import { ExternalAuthDto } from '../_models/externalAuth';
import { User } from '../_models/user';
@Injectable({
    providedIn: 'root'
})
export class AuthenticationService {
    baseUrl = environment.baseUrl;
    constructor(private http: HttpClient, private externalAuthService: SocialAuthService) { }

    signInWithGoogle = () => {
        return this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
    }
    signOutExternal = () => {
        this.externalAuthService.signOut();
    }

    refreshToken(): void {
        this.externalAuthService.refreshAuthToken(GoogleLoginProvider.PROVIDER_ID);
    }

    externalLogin(externalAuth: ExternalAuthDto) {
        return this.http.post<User>(this.baseUrl + 'account/extra-signin', externalAuth);
    }
}
