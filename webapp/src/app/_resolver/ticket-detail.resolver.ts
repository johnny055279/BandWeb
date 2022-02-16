import { Injectable } from '@angular/core';
import {
    Router, Resolve,
    RouterStateSnapshot,
    ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Ticket } from '../_models/ticket';
import { TicketService } from '../_services/ticket.service';

@Injectable({
    providedIn: 'root'
})
export class TicketDetailResolver implements Resolve<Ticket> {
    constructor(private ticketService: TicketService) { }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Ticket> {
        return this.ticketService.getTicketById(route.paramMap.get('id') || '');
    }
}
