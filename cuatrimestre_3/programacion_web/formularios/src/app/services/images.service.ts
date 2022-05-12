import { Injectable } from '@angular/core';
import { Bike } from '../models/bike';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  allImages: Bike[] ;

  constructor() {
    this.allImages = IMAGESDETAILS;
  }
  //este metodo nos retorna un listado completo de imagenes
  getImages() {
    return this.allImages = IMAGESDETAILS.slice(0);
  }

  //este metodo recibe un id por parametro y buscara dicho id dentro del array de imagenes
  getImageById(id:number) {
    return  IMAGESDETAILS.slice(0).find(image => image.id = id);
  }
}

const IMAGESDETAILS = [
  { "id": 1, "brand": "adultorc", "url": "" },
  { "id": 2, "brand": "adultorc", "url": "" },
  { "id": 3, "brand": "adultorc", "url": "" },
  { "id": 4, "brand": "adultorc", "url": "" },
  { "id": 5, "brand": "bkeco", "url": "" },
  { "id": 6, "brand": "bkeco", "url": "" },
  { "id": 7, "brand": "bkeco", "url": "" },
  { "id": 8, "brand": "bkeco", "url": "" },
  { "id": 9, "brand": "bkmotor", "url": "" },
  { "id": 10, "brand": "bkmotor", "url": "" },
  { "id": 11, "brand": "bkmotor", "url": "" },
  { "id": 12, "brand": "bkmotor", "url": "" },
  { "id": 13, "brand": "bkmotor", "url": "" },
  { "id": 14, "brand": "bknene", "url": "" },
  { "id": 15, "brand": "bknene", "url": "" },
  { "id": 16, "brand": "bknene", "url": "" },
  { "id": 17, "brand": "bknene", "url": "" }
]