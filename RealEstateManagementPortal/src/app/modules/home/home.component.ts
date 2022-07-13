import { Component, Input, OnInit } from '@angular/core';
import { IcarouselImage } from './interfaces/carouselImage';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {

  constructor() { }

// images:IcarouselImage[]=[];
  selectedIndex=0;
  indicators=true;
  controls=true;
  autoSlide=true;
  slideInterval=3000;

  selectImage(index:number):void{
    this.selectedIndex=index;
  }

  onPrevClick():void{
    if(this.selectedIndex ===0 ){
      this.selectedIndex = this.images.length-1;
    }else{
      this.selectedIndex--;
    }

  }

  onNextClick():void{
    if(this.selectedIndex === this.images.length-1){
      this.selectedIndex = 0;
    }else{
      this.selectedIndex++;
    }

  }

  ngOnInit(): void {
    if(this.autoSlide){
      this.autoSlideImages();
    }
  }

autoSlideImages():void{
  setInterval(() =>{
    this.onNextClick();
  },this.slideInterval);
}

  images:IcarouselImage[] = [
    {
      imgSrc:
        './assets/images/bg_1.jpg',
      imgAlt: 'property1',
    },
    {
      imgSrc:
        './assets/images/bg_2.jpg',
      imgAlt: 'property2',
    },
    // {
    //   imgSrc:
    //     'https://images.unsplash.com/photo-1640844444545-66e19eb6f549?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1032&q=80',
    //   imgAlt: 'person1',
    // },
    // {
    //   imgSrc:
    //     'https://images.unsplash.com/photo-1490730141103-6cac27aaab94?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=870&q=80',
    //   imgAlt: 'person2',
    // },
  ];

}
