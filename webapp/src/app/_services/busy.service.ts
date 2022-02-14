import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class BusyService {
    loadingSource = new BehaviorSubject<boolean>(false);
    loading$ = this.loadingSource.asObservable();
    requestCoiunt = 0;
    constructor() { }

    onRequest() {
        this.requestCoiunt++;
        this.loadingSource.next(true);
    }

    onResponse() {
        this.requestCoiunt--;

        if (this.requestCoiunt <= 0) {
            this.requestCoiunt = 0;
            this.loadingSource.next(false);
        }
    }
}
