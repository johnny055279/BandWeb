import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  baseUrl = environment.baseUrl;
  ngFormInput = new FormGroup({
    account: new FormControl(''),
    password: new FormControl('')
  });
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  login() {
    this.http.post(this.baseUrl + "api/account/login", this.ngFormInput.value).subscribe((response) => {
      console.log(response);
    })
  }

}
