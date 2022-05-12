import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
//impor el servicio a utilizar
import { ImagesService } from 'src/app/services/images.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  validMessage: string = "";

  //listado de moledos de bicicletas 
  models: string[] = [
    'Bike Mistica 023',
    'Bike Model 456',
    'Bike Pro Magic',
    'Bike Golden',
    'Bike Ami',
    'Bike Led Zeppe',
  ];

  //como la mayoria de los formulario tienen mas de una entrada manejaremos 
  //todas ellas bajo un FormGroup 
  bikeForm: FormGroup;

  //inyecto el servicio a utilizar 
  constructor(private servicioImages: ImagesService) {

     //utilizamos el servicio previamente inyecto 
     console.log("esto me responde el servicio inyectado ");
     console.dir(servicioImages.getImages());
     console.dir(servicioImages.getImageById(5));

    this.bikeForm = new FormGroup({
      //validamos que el input del form sea required y que cumpla con una 
      //determinada Expresion Regular 
      name: new FormControl('', [Validators.pattern('@([A-Za-z0-9_]{1,15})'), Validators.required]),
      email: new FormControl('', [Validators.email, Validators.required]),
      contact: new FormControl('', Validators.required),
      //validamos el numeor de telefono con una regExp
      //el formato valido sera el siguiente +54 0223 1512345678
      phone: new FormControl('', [Validators.required, Validators.pattern('\\+54\\s[0-9]{1,4}\\s15[0-9]{8}')]),
      model: new FormControl('',Validators.required),
      serialNumber:  new FormControl('',[Validators.required ,Validators.min(11), Validators.max(9999999)]),
      purchasePrice:  new FormControl('',[Validators.required ,Validators.min(1000), Validators.max(100000)]),
      //la fecha deberia tener el formato  : mm/dd/aaaa ej 30/12/2022
      purchaseDate:  new FormControl('', [Validators.required, Validators.pattern(/^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]|(?:Jan|Mar|May|Jul|Aug|Oct|Dec)))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2]|(?:Jan|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec))\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)(?:0?2|(?:Feb))\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9]|(?:Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep))|(?:1[0-2]|(?:Oct|Nov|Dec)))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/)]),
    });
  }

  //creamos un getters para que nos sea mas comodo y prolijo acceder a cada
  //uno de los FormControls desde el template (home.component.html)
  get controles() {
    return this.bikeForm.controls;
  }

  ngOnInit(): void {
  }

  submitRegistration() {
    console.log("el objeto form Group Tiene lo siguiente : ");
    console.dir(this.bikeForm);
    console.log("controles :");
    console.dir(this.controles);

    console.log("disparaste la funcion submitRegistration()");
    console.dir("El formulario es valido : " + this.bikeForm.valid);

    this.validMessage = (this.bikeForm.valid) ? "Su Garantia Fue Registrada Correctamente en Nuestro Sistema" : "Por Favor Completa Correctamente El Formulario";

    // if (this.bikeForm.valid) {
    //   this.validMessage = "Su Garantia Fue Registrada Correctamente en Nuestro Sistema";
    // } else {
    //   this.validMessage = "Por Favor Completa Correctamente El Formulario";
    // }
  }
}
