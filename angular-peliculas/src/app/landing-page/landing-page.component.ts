import { Component, OnInit } from '@angular/core';
import { ListadoPeliculasComponent } from "../peliculas/listado-peliculas/listado-peliculas.component";

@Component({
  selector: 'app-landing-page',
  imports: [ListadoPeliculasComponent],
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.css'
})
export class LandingPageComponent implements OnInit {
peliculasEnCines!: any[];
  peliculasProximosEstrenos!: any[];


  procesarVoto(voto: number) {
   alert('Calificación otorgada ${voto}');
  }
  
  ngOnInit(): void {
     setTimeout(() => {
      this.peliculasEnCines= [
    {
    titulo: 'Inside Out 2',
    fechaLanzamiento: new Date(),
    precio: 1400.99,
    poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f7/Inside_Out_2_poster.jpg/250px-Inside_Out_2_poster.jpg',
    },
     {
    titulo: 'Moana 2',
    fechaLanzamiento: new Date("2016-05-03"),
    precio: 300.99,
    poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/7/73/Moana_2_poster.jpg/250px-Moana_2_poster.jpg'
    },
   
];    
   
      this.peliculasProximosEstrenos=[
         {
    titulo: 'Bad Boys: Ride or Die',
    fechaLanzamiento: new Date("2016-05-03"),
    precio: 300.99,
    poster: 'https://upload.wikimedia.org/wikipedia/en/8/8b/Bad_Boys_Ride_or_Die_%282024%29_poster.jpg'
    },
       {
    titulo: 'Deadpool & Wolverine',
    fechaLanzamiento: new Date("2016-05-03"),
    precio: 300.99,
    poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/4/4c/Deadpool_%26_Wolverine_poster.jpg/250px-Deadpool_%26_Wolverine_poster.jpg'
    },
     {
    titulo: 'Oppenheimer',
    fechaLanzamiento: new Date("2017-05-03"),
    precio: 300.99,
    poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/4/4a/Oppenheimer_%28film%29.jpg/250px-Oppenheimer_%28film%29.jpg'
    },
    {
       titulo: 'The Flash',
    fechaLanzamiento: new Date("2016-06-03"),
    precio: 300.99,
    poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/e/ed/The_Flash_%28film%29_poster.jpg/250px-The_Flash_%28film%29_poster.jpg'
    },
      ]

    },2000)
  }

  }
  

  
         




