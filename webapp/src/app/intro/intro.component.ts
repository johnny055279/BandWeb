import { trigger, transition, query, style, stagger, animate } from '@angular/animations';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-intro',
  templateUrl: './intro.component.html',
  styleUrls: ['./intro.component.css'],
  animations: [
    trigger('pageAnimations', [
      transition(':leave', [
        query('.container', [
          style({opacity: 1}),
          stagger(-30, [
            animate('400ms cubic-bezier(0.7,0,0.3,1)', style({ opacity: 0.5, transform: 'translateY(-200px)'})),
            animate('1000ms cubic-bezier(0.7,0,0.3,1)', style({ opacity: 0.1, transform: 'translateY(-100%)'}))
          ])
        ]),
      ])
    ]),
  ]
})
export class IntroComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
