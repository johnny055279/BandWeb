import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { City } from '../_models/city';

@Injectable({
    providedIn: 'root'
})
export class SystemService {
    basUrl = environment.baseUrl;
    constructor(private http: HttpClient) { }

    getCities() {
        return this.http.get<City[]>(this.basUrl + "system/cities").pipe(map(cities => {
            return cities;
        }));
    }


}
