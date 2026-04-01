import { Component } from '@angular/core';
import { PeliculasCreacionDto } from '../peliculas';
import { FormularioPeliculasComponent } from "../formulario-peliculas/formulario-peliculas.component";
import { SelectorMultipleComponent } from '../../compartidos/componentes/selector-multiple/selector-multiple.component';
import { SelectorMultipleDto } from '../../compartidos/componentes/selector-multiple/SelectorMultipleModelo';
import { ActorAutoCompleteDto } from '../../actores/actores';

@Component({
  selector: 'app-crear-pelicula',
  imports: [FormularioPeliculasComponent],
  templateUrl: './crear-pelicula.component.html',
  styleUrl: './crear-pelicula.component.css'
})
export class CrearPeliculaComponent {

  generosSeleccionados: SelectorMultipleDto[]= [];
  generosNoSeleccionados: SelectorMultipleDto[] = [
    {llave: 1, valor: 'Drama'},
    {llave: 2, valor: 'Acción'},
    {llave: 3, valor: 'Comedia'}
  ];

  cinesSeleccionados: SelectorMultipleDto[]= [];
  cinesNoSeleccionados: SelectorMultipleDto[] = [
    {llave: 1, valor: 'Agora Mall'},
    {llave: 2, valor: 'Blue Mall'},
    {llave: 3, valor: 'Acrópolis'}
  ];

  actoresSeleccionados: ActorAutoCompleteDto[]=[];

  guardarCambios(pelicula: PeliculasCreacionDto) {
    console.log('creando película', pelicula);
  }
}
