import { Component, inject } from '@angular/core';
import { PeliculasCreacionDto } from '../peliculas';
import { FormularioPeliculasComponent } from "../formulario-peliculas/formulario-peliculas.component";
import { SelectorMultipleComponent } from '../../compartidos/componentes/selector-multiple/selector-multiple.component';
import { SelectorMultipleDto } from '../../compartidos/componentes/selector-multiple/SelectorMultipleModelo';
import { ActorAutoCompleteDto } from '../../actores/actores';
import { PeliculasService } from '../peliculas.service';
import { Router } from '@angular/router';
import { extraerErrores } from '../../compartidos/funciones/extraerErrores';
import { MostrarErroresComponent } from "../../compartidos/componentes/mostrar-errores/mostrar-errores.component";
import { CargandoComponent } from "../../compartidos/componentes/cargando/cargando.component";

@Component({
  selector: 'app-crear-pelicula',
  imports: [FormularioPeliculasComponent, MostrarErroresComponent, CargandoComponent],
  templateUrl: './crear-pelicula.component.html',
  styleUrl: './crear-pelicula.component.css'
})
export class CrearPeliculaComponent {

  generosSeleccionados: SelectorMultipleDto[]= [];
  generosNoSeleccionados: SelectorMultipleDto[] = [];
  cinesSeleccionados: SelectorMultipleDto[]= [];
  cinesNoSeleccionados: SelectorMultipleDto[] = [];
  actoresSeleccionados: ActorAutoCompleteDto[]=[];
  peliculasService = inject(PeliculasService);
  router= inject(Router);
  errores: string[]=[];

  constructor() {
    this.peliculasService.CrearGet().subscribe(modelo =>{
      this.generosNoSeleccionados= modelo.generos.map(genero => {
          return <SelectorMultipleDto>{llave: genero.id,valor: genero.nombre}
      })
       this.cinesNoSeleccionados= modelo.cines.map(cine => {
          return <SelectorMultipleDto>{llave: cine.id,valor: cine.nombre}
      })

    })
  }



  guardarCambios(pelicula: PeliculasCreacionDto) {
    this.peliculasService.crear(pelicula).subscribe({
      next: pelicula => {
          console.log(pelicula);
          this.router.navigate(['/'])
      },
      error: err => {
        const errores = extraerErrores(err);
        this.errores= errores;
      }
    })
  }
}
