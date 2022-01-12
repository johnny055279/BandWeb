import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {

  constructor() {}

  ngOnInit(): void {
  }

  currentSection = 'home';

  onSectionChange(sectionId: string) {
   
    this.currentSection = sectionId;
  }

  scrollTo(section: string) {
    if(section == this.currentSection){
      const sectionCollection = document.querySelectorAll('section');
      sectionCollection.forEach(element => {
        element.children[0].classList.remove('focus-in-contract-bck');
        element.children[1].classList.remove('slide-right');
      });
    }
    document.querySelector('#' + section)?.scrollIntoView();
    document.querySelector('#' + section)?.children[0].classList.add('focus-in-contract-bck');
    document.querySelector('#' + section)?.children[1].classList.add('slide-right');
  }
}

