import { Component, Input, numberAttribute } from '@angular/core';
import { PeliculasCreacionDto, PeliculasDto } from '../peliculas';
import { FormularioPeliculasComponent } from "../formulario-peliculas/formulario-peliculas.component";
import { SelectorMultipleDto } from '../../compartidos/componentes/selector-multiple/SelectorMultipleModelo';
import { ActorAutoCompleteDto } from '../../actores/actores';

@Component({
  selector: 'app-editar-pelicula',
  imports: [FormularioPeliculasComponent],
  templateUrl: './editar-pelicula.component.html',
  styleUrl: './editar-pelicula.component.css'
})
export class EditarPeliculaComponent {
  @Input({transform: numberAttribute})
   id!:  number;

   pelicula: PeliculasDto = { id: 1,titulo: 'Spider-Man', trailer: 'ABC', fechaLanzamiento: new Date('2018-07-25'), poster: 'https://upload.wikimedia.org/wikipedia/en/thumb/f/f7/Inside_Out_2_poster.jpg/250px-Inside_Out_2_poster.jpg' }

     generosSeleccionados: SelectorMultipleDto[]= [
        {llave: 2, valor: 'Acción'}
     ];
     generosNoSeleccionados: SelectorMultipleDto[] = [
       {llave: 1, valor: 'Drama'},
       {llave: 3, valor: 'Comedia'}
     ];

   cinesSeleccionados: SelectorMultipleDto[]= [
      {llave: 2, valor: 'Blue Mall'},
   ];
   cinesNoSeleccionados: SelectorMultipleDto[] = [
    {llave: 1, valor: 'Agora Mall'},
    {llave: 3, valor: 'Acrópolis'}
  ];

    actoresSeleccionados : ActorAutoCompleteDto[] = [
      {id: 1, nombre:'Tom Holland', personaje: 'Forrest Gump', foto: 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg/250px-Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg'}
    ]
    guardarCambios(pelicula: PeliculasCreacionDto) {
       console.log('editando película', pelicula)
     }
  }

