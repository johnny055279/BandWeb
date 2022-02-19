import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { SocialUser } from 'angularx-social-login';
import { ExternalAuthDto } from 'src/app/_models/externalAuth';
import { AccountService } from 'src/app/_services/account.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { ToolbarComponent } from 'src/app/_shared-components/toolbar/toolbar.component';

@Component({
    selector: 'app-login-dialog',
    templateUrl: './login-dialog.component.html',
    styleUrls: ['./login-dialog.component.css']
})
export class LoginDialogComponent implements OnInit {
    hide = true;
    data = {
        account: '',
        password: ''
    }
    constructor(public dialogRef: MatDialogRef<ToolbarComponent>, private authService: AuthenticationService, private accountService: AccountService) { }

    ngOnInit(): void {
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

    externalLogin = () => {
        this.dialogRef.close();
        this.authService.signInWithGoogle()
            .then(res => {
                const user: SocialUser = { ...res };
                const externalAuth: ExternalAuthDto = {
                    provider: user.provider,
                    idToken: user.idToken
                }
                this.validateExternalAuth(externalAuth);
            }, error => console.log(error))
    }

    validateExternalAuth(externalAuth: ExternalAuthDto) {
        this.authService.externalLogin(externalAuth).subscribe(user => {
            if (user) {
                this.accountService.setUser(user);
            }
        });
    }

}
