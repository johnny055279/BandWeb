import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
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
    constructor(public dialogRef: MatDialogRef<ToolbarComponent>) { }

    ngOnInit(): void {
    }

    onNoClick(): void {
        this.dialogRef.close();
    }

}
