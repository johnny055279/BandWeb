import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SocialUser } from 'angularx-social-login';
import { LoginDialogComponent } from 'src/app/dialogs/login-dialog/login-dialog.component';
import { ExternalAuthDto } from 'src/app/_models/externalAuth';
import { AccountService } from 'src/app/_services/account.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { SnackbarService } from 'src/app/_services/snackbar.service';

@Component({
    selector: 'app-toolbar',
    templateUrl: './toolbar.component.html',
    styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {

    constructor(public accountService: AccountService, public dialog: MatDialog, private snackbarService: SnackbarService, private authService: AuthenticationService) { }

    ngOnInit(): void {
    }

    login(model: any) {
        this.accountService.login(model).subscribe(() => {
            this.snackbarService.onSuccess('Welcome back!');
        });
    }

    logout() {
        this.accountService.logout();
    }

    openDialog() {
        const dialogRef = this.dialog.open(LoginDialogComponent);

        dialogRef.afterClosed().subscribe(response => {
            this.login(response);
        });
    }

    externalLogin = () => {
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
