import { Component, Input ,numberAttribute} from '@angular/core';
import { CineCreacionDto, CineDto } from '../cines';
import { FormularioCinesComponent } from "../formulario-cines/formulario-cines.component";

@Component({
  selector: 'app-editar-cine',
  imports: [FormularioCinesComponent],
  templateUrl: './editar-cine.component.html',
  styleUrl: './editar-cine.component.css'
})
export class EditarCineComponent {
   @Input({transform: numberAttribute})
   id!:  number;

   cine: CineDto = { id: 1, nombre:'Acrópolis',latitud: 40.41961226545802,longitud: 40.41961226545802}

   guardarCambios(cine: CineCreacionDto) {
     console.log('editar cine', cine);
   }
}
