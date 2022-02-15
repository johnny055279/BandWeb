import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SnackbarService {
    snackbarConfig = <MatSnackBarConfig>environment.snackBarConfig;
    constructor(private snackBar: MatSnackBar) { }

    onSuccess(message: string) {
        this.snackbarConfig.panelClass = 'snackbarSuccess';
        this.snackBar.open(message, undefined, this.snackbarConfig);
    }

    onError(message: string) {
        this.snackbarConfig.panelClass = 'snackbarError';
        this.snackBar.open(message, undefined, this.snackbarConfig);
    }

    onWarning() {

    }

    onInfo() {

    }

}
