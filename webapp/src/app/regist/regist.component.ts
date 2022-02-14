import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-regist',
    templateUrl: './regist.component.html',
    styleUrls: ['./regist.component.css']
})
export class RegistComponent implements OnInit {
    firstFormGroup = new FormGroup({
        userName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        nickName: new FormControl('', [Validators.required, Validators.maxLength(20)]),
        password: new FormControl('', [Validators.required, Validators.maxLength(10), Validators.minLength(8)]),
    });

    secondFormGroup = new FormGroup({
        gender: new FormControl('', Validators.required),
        email: new FormControl('', [Validators.required, Validators.email])
    });

    constructor() { }

    ngOnInit(): void {
    }



}
