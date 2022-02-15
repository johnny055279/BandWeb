import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { BusyService } from '../_services/busy.service';
import { SnackBarComponent } from '../_shared-components/snack-bar/snack-bar.component';

@Component({
    selector: 'app-regist',
    templateUrl: './regist.component.html',
    styleUrls: ['./regist.component.css']
})
export class RegistComponent implements OnInit {
    baseUrl = environment.baseUrl;
    snackbarConfig = <MatSnackBarConfig>environment.snackBarConfig;
    firstFormGroup = new FormGroup({
        userName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        nickName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        password: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.minLength(8)]),
    });

    secondFormGroup = new FormGroup({
        gender: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.email])
    });

    constructor(private http: HttpClient, private snackBar: MatSnackBar, public busyService: BusyService, private router: Router) { }

    ngOnInit(): void {
    }

    regist() {
        this.firstFormGroup.addControl('gender', this.secondFormGroup.controls['gender']);
        this.firstFormGroup.addControl('email', this.secondFormGroup.controls['email']);

        this.http.post(this.baseUrl + 'account/register', this.firstFormGroup.value).subscribe(() => {
            this.snackBar.openFromComponent(SnackBarComponent, this.snackbarConfig);
            this.router.navigateByUrl('/');
        }, error => console.log(error));
    }



}
