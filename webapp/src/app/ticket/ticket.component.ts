import { Component, OnInit } from '@angular/core';
import { faTicketAlt, faLocationArrow, faCalendarAlt, faArrowLeft } from '@fortawesome/free-solid-svg-icons'
@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.css']
})
export class TicketComponent implements OnInit {
  faTicketAlt = faTicketAlt;
  faLocationArrow = faLocationArrow;
  faCalendarAlt = faCalendarAlt;
  faArrowLeft = faArrowLeft;
  constructor() { }

  ngOnInit(): void {
  }

}
