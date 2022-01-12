import { Component, OnInit } from '@angular/core';
import * as Flickity from 'flickity';
import { faArrowLeft } from '@fortawesome/free-solid-svg-icons'
@Component({
  selector: 'app-shop-main',
  templateUrl: './shop-main.component.html',
  styleUrls: ['./shop-main.component.css']
})
export class ShopMainComponent implements OnInit {
  faArrowLeft = faArrowLeft
  constructor() { }

  ngOnInit(): void {

    var options = {
      accessibility: true,
      prevNextButtons: true,
      pageDots: true,
      setGallerySize: false,
      wrapAround: true,
      autoPlay: 5000,
      arrowShape: {
        x0: 10,
        x1: 60,
        y1: 50,
        x2: 60,
        y2: 45,
        x3: 15
      }
    };
    
    var carousel: any = document.querySelector('[data-carousel]');
    var slides: any = document.getElementsByName('carousel-cell');
    if(carousel){
      var flkty: any = new Flickity(carousel, options);
      flkty.on('scroll', function () {
        slides.forEach((element: { target: any; style: { backgroundPosition: string; }; }) => {
          var x = (element.target + flkty.x) * -1/3;
          element.style.backgroundPosition = x + 'px';
        });
      });
    }
   
  }
}
