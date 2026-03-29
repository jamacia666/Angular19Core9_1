import { CurrencyPipe, DatePipe, NgOptimizedImage, UpperCasePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ListadoPeliculasComponent } from './peliculas/listado-peliculas/listado-peliculas.component';
import { MenuComponent } from "./compartidos/componentes/menu/menu.component";
import { RatingComponent } from "./compartidos/componentes/rating/rating.component";
import { MatButton, MatButtonModule } from "@angular/material/button";
import { RouterOutlet } from '@angular/router';


@Component({
  selector: 'app-root', 
  imports: [ListadoPeliculasComponent, MenuComponent, RatingComponent, MatButtonModule,RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit  {
  peliculasEnCines!: any[];
  peliculasProximosEstrenos!: any[];


  procesarVoto(voto: number) {
   alert('Calificación otorgada ${voto}');
  }
  
  ngOnInit(): void {
     
  }

  }
  

  
         


