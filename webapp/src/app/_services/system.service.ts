import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { City } from '../_models/city';

@Injectable({
    providedIn: 'root'
})
export class SystemService {
    baseUrl = environment.baseUrl;

    private citiesSource = new BehaviorSubject<City[]>([]);

    cities$ = this.citiesSource.asObservable();

    constructor(private http: HttpClient) { }

    getCities() {
        return this.http.get<City[]>(this.baseUrl + "system/dropdown-list?type=city").pipe(map(cities => {
            this.citiesSource.next(cities);
        }));
    }


}
