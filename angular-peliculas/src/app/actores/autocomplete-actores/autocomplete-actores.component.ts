import { Component, Input, ViewChild } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepicker, MatDatepickerInput, MatDatepickerToggle } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RouterLink, ɵEmptyOutletComponent } from '@angular/router';
import { InputImgComponent } from '../../compartidos/componentes/input-img/input-img.component';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatTable, MatTableModule } from '@angular/material/table';
import { ActorAutoCompleteDto } from '../actores';
import id from '@angular/common/locales/id';
import { CdkDragDrop, DragDropModule, moveItemInArray } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-autocomplete-actores',
  imports: [MatAutocompleteModule,FormsModule, MatButtonModule, ReactiveFormsModule,
     MatFormFieldModule, MatIconModule, MatTableModule, MatInputModule,DragDropModule],
  templateUrl: './autocomplete-actores.component.html',
  styleUrl: './autocomplete-actores.component.css'
})
export class AutocompleteActoresComponent {

eliminar(actor: ActorAutoCompleteDto) {
 const indice = this.actoresSeleccionados.findIndex((a: ActorAutoCompleteDto)=> actor.id === actor.id)
 this.actoresSeleccionados.splice(indice,1);
 this.table.renderRows();
}

finalizarArrastre(event: CdkDragDrop<any[]>) {
   const indicePrevio = this.actoresSeleccionados.findIndex(actor => actor === event.item.data);
   moveItemInArray(this.actoresSeleccionados,indicePrevio,event.currentIndex);
   this.table.renderRows();
}

  control = new FormControl();
  actores: ActorAutoCompleteDto[] = [
    {id: 1, nombre:'Tom Holland', personaje: '', foto: 'https://upload.wikimedia.org/wikipedia/commons/thumb/5/58/Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg/250px-Tom_Holland_during_pro-am_Wentworth_golf_club_2023-2.jpg'},
    {id: 2, nombre:'Tom Hanks', personaje: '', foto: 'https://upload.wikimedia.org/wikipedia/commons/thumb/3/39/TomHanksPrincEdw031223_%2811_of_41%29_%28cropped%29.jpg/250px-TomHanksPrincEdw031223_%2811_of_41%29_%28cropped%29.jpg'},
    {id: 3, nombre:'Samuel L. Jackson', personaje: '', foto: 'https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/SamuelLJackson.jpg/250px-SamuelLJackson.jpg'},
  ]

  @Input({required:true})
  actoresSeleccionados: ActorAutoCompleteDto[]=[];

  columnasAMostrar = ['imagen','nombre','personaje','acciones'];

  @ViewChild(MatTable) table! : MatTable<ActorAutoCompleteDto>;


  actorSeleccionado(event:MatAutocompleteSelectedEvent) {
    this.actoresSeleccionados.push(event.option.value);
    this.control.patchValue('');
    if (this.table != undefined) {
      this.table.renderRows();
    }
  }
}
