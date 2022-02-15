import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { AccountService } from '../_services/account.service';
import { BusyService } from '../_services/busy.service';
import { SnackbarService } from '../_services/snackbar.service';

@Component({
    selector: 'app-regist',
    templateUrl: './regist.component.html',
    styleUrls: ['./regist.component.css']
})
export class RegistComponent implements OnInit {
    baseUrl = environment.baseUrl;
    hide = true;
    firstFormGroup = new FormGroup({
        userName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        nickName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        password: new FormControl('', [Validators.required, Validators.maxLength(20), Validators.minLength(8)]),
    });

    secondFormGroup = new FormGroup({
        gender: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.email])
    });

    constructor(
        public busyService: BusyService,
        private router: Router,
        private accountService: AccountService,
        private snackbarService: SnackbarService) { }

    ngOnInit(): void {
    }

    regist() {
        this.firstFormGroup.addControl('gender', this.secondFormGroup.controls['gender']);
        this.firstFormGroup.addControl('email', this.secondFormGroup.controls['email']);

        this.accountService.regist(this.firstFormGroup.value).subscribe(() => {
            this.snackbarService.onSuccess('Success!');
            this.router.navigateByUrl('/');
        }, error => console.log(error));
    }



}
