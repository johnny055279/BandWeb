import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SnackbarService {
    snackbarConfig = <MatSnackBarConfig>environment.snackBarConfig;
    constructor(private snackBar: MatSnackBar) { }

    onSuccess() {
        this.snackBar.open('Success!', undefined, this.snackbarConfig);
    }

    onError() {

    }

    onWarning() {

    }

    onInfo() {

    }

}
