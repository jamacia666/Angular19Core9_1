import { Component, Input, numberAttribute } from '@angular/core';
import { ActorCreacionDto, ActorDto } from '../actores';
import { FormularioActoresComponent } from "../formulario-actores/formulario-actores.component";

@Component({
  selector: 'app-editar-actor',
  imports: [FormularioActoresComponent],
  templateUrl: './editar-actor.component.html',
  styleUrl: './editar-actor.component.css'
})
export class EditarActorComponent {

   @Input({transform: numberAttribute})
   id!:  number;

   actor: ActorDto= {id: 1, nombre: 'Tom Holland', fechaNacimiento: new Date('1999-01-25'),foto: 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg/250px-Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg'}

   guardarCambios(actor: ActorCreacionDto) {
    console.log('editando el actor',actor)
   }
}
