import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LoginDialogComponent } from 'src/app/dialogs/login-dialog/login-dialog.component';
import { AccountService } from 'src/app/_services/account.service';
import { SnackbarService } from 'src/app/_services/snackbar.service';

@Component({
    selector: 'app-toolbar',
    templateUrl: './toolbar.component.html',
    styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {

    constructor(public accountService: AccountService, public dialog: MatDialog, private snackbarService: SnackbarService,) { }

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
}
