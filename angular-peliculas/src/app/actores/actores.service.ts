import { HttpClient, HttpResponse } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ActorAutoCompleteDto, ActorCreacionDto, ActorDto } from './actores';
import { PaginacionDTO } from '../compartidos/modelos/PaginacionDTO';
import { Observable } from 'rxjs';
import { construirQueryParams } from '../compartidos/funciones/construirQueryParams';
import id from '@angular/common/locales/id';
import { IServicioCRUD } from '../compartidos/interfaces/IServicioCRUD';

@Injectable({
  providedIn: 'root'
})
export class ActoresService  implements IServicioCRUD<ActorDto,ActorCreacionDto>{

  constructor() { }
  private http = inject(HttpClient);
  private urlBase = environment.apiUrl + '/actores';

  public obtenerPaginado(paginacion: PaginacionDTO): Observable<HttpResponse<ActorDto[]>> {
      let queryParams = construirQueryParams(paginacion);
      return this.http.get<ActorDto[]>(this.urlBase, {params: queryParams,observe: 'response'});
  }

  public obtenerPorId(id:number) :Observable<ActorDto>
  {
    return this.http.get<ActorDto>(`${this.urlBase}/${id}`);
  }

    public obtenerPorNombre(nombre: string): Observable<ActorAutoCompleteDto[]>{
    return this.http.get<ActorAutoCompleteDto[]>(`${this.urlBase}/${nombre}`);
  }

  public actualizar(id:number,actor: ActorCreacionDto)
  {
    const formData= this.construirFormData(actor);
    return this.http.put(`${this.urlBase}/${id}`,formData);
  }

  public crear(actor: ActorCreacionDto) {
    const formData= this.construirFormData(actor);
    return this.http.post(this.urlBase,formData);
  }

public borrar(id:number) {
  return this.http.delete(`${this.urlBase}/${id}`);
}
  private construirFormData(actor: ActorCreacionDto):FormData {
    const formData = new FormData();
    formData.append('nombre',actor.nombre);
    formData.append('fechaNacimiento',actor.fechaNacimiento.toISOString().split('T')[0]);
    if (actor.foto)
    {
      formData.append('foto',actor.foto)
    }
    return formData;
  }
}
