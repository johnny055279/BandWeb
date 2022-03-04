import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { DropDown } from '../_models/dropdown';

@Injectable({
    providedIn: 'root'
})
export class SystemService {
    baseUrl = environment.baseUrl;
    dropDown: DropDown[] = [];
    private dropDownSource = new BehaviorSubject<DropDown[]>([]);
    dropDownList$ = this.dropDownSource.asObservable();
    constructor(private http: HttpClient) { }

    getCities() {
        return this.http.get<DropDown>(this.baseUrl + "system/dropdown-list?type=city").pipe(map(cities => {
            console.log(cities)
            if (!this.dropDown.find(n => n.type == 'City')) {
                this.dropDown.push(cities);
                this.dropDownSource.next(this.dropDown);
            }
        }));
    }


}
