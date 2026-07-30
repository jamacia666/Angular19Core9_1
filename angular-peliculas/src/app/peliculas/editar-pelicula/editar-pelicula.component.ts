import { Component, Input, numberAttribute, OnInit} from '@angular/core';
import { PeliculasCreacionDto, PeliculasDto } from '../peliculas';
import { FormularioPeliculasComponent } from "../formulario-peliculas/formulario-peliculas.component";
import { SelectorMultipleDto } from '../../compartidos/componentes/selector-multiple/SelectorMultipleModelo';
import { ActorAutoCompleteDto } from '../../actores/actores';
import { inject } from '@angular/core';
import { PeliculasService } from '../peliculas.service';
import { extraerErrores } from '../../compartidos/funciones/extraerErrores';
import { Router } from '@angular/router';
import { MostrarErroresComponent } from "../../compartidos/componentes/mostrar-errores/mostrar-errores.component";
import { CargandoComponent } from "../../compartidos/componentes/cargando/cargando.component";

@Component({
  selector: 'app-editar-pelicula',
  imports: [FormularioPeliculasComponent, MostrarErroresComponent, CargandoComponent],
  templateUrl: './editar-pelicula.component.html',
  styleUrl: './editar-pelicula.component.css'
})
export class EditarPeliculaComponent  implements OnInit {

   ngOnInit(): void {
    this.peliculasService.actualizarGet(this.id).subscribe(modelo => {
      this.pelicula = modelo.pelicula;
      this.actoresSeleccionados = modelo.actores;
      this.cinesNoSeleccionados = modelo.cinesNoSeleccionados.map(cine => {
        return <SelectorMultipleDto>{llave: cine.id, valor: cine.nombre};
      });

      this.cinesSeleccionados = modelo.cinesSeleccionados.map(cine => {
        return <SelectorMultipleDto>{llave: cine.id, valor: cine.nombre};
      });

      this.generosNoSeleccionados = modelo.generosNoSeleccionados.map(genero => {
        return <SelectorMultipleDto>{llave: genero.id, valor: genero.nombre};
      });

      this.generosSeleccionados = modelo.generosSeleccionados.map(genero => {
        return <SelectorMultipleDto>{llave: genero.id, valor: genero.nombre};
      });
    });
  }
  @Input({transform: numberAttribute})
   id!:  number;

   pelicula!: PeliculasDto;
   generosSeleccionados!: SelectorMultipleDto[];
   generosNoSeleccionados!: SelectorMultipleDto[];
   cinesSeleccionados!: SelectorMultipleDto[];
   cinesNoSeleccionados!: SelectorMultipleDto[];
   actoresSeleccionados!: ActorAutoCompleteDto[];

   peliculasService= inject (PeliculasService)
   router = inject(Router);
   errores: string[] = [];


    guardarCambios(pelicula: PeliculasCreacionDto){
    this.peliculasService.actualizar(this.id, pelicula).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: err => {
        const errores = extraerErrores(err);
        this.errores = errores;
      }
    })
  }

}
