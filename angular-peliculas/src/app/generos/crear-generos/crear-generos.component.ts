import { Component,inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { primeraLetraMayuscula } from '../../compartidos/funciones/validaciones';
import { FormularioGeneroComponent } from "../formulario-genero/formulario-genero.component";
import { GeneroCreacionDTO } from '../generos';
import { GenerosService } from '../generos.service';
import { environment } from '../../../environments/environment';
import { extraerErrores } from '../../compartidos/funciones/extraerErrores';
import { MostrarErroresComponent } from "../../compartidos/componentes/mostrar-errores/mostrar-errores.component";


@Component({
  selector: 'app-crear-generos',
  imports: [MatButtonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, FormularioGeneroComponent, MostrarErroresComponent],
  templateUrl: './crear-generos.component.html',
  styleUrl: './crear-generos.component.css'
})
export class CrearGenerosComponent {
  private router = inject(Router);
  private generosService= inject(GenerosService);
  private urlBase = environment.apiUrl + '/generos';
  errores: string[]=[];


 guardarCambios(genero: GeneroCreacionDTO){

this.generosService.crear(genero).subscribe({
  next: () => {
    this.router.navigate(['generos']);
  },
  error: err => {
    const errores = extraerErrores(err);
    this.errores= errores;
  }
});
 }



}
