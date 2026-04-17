import { inject, Injectable } from '@angular/core';
import { IServicioCRUD } from '../compartidos/interfaces/IServicioCRUD';
import { CineCreacionDto, CineDto } from './cines';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginacionDTO } from '../compartidos/modelos/PaginacionDTO';
import { environment } from '../../environments/environment.development';
import { construirQueryParams } from '../compartidos/funciones/construirQueryParams';

@Injectable({
  providedIn: 'root'
})
export class CinesService implements IServicioCRUD<CineDto,CineCreacionDto>{

  constructor() { }

  private http= inject(HttpClient);
  private urlBase = environment.apiUrl='/cines';



  obtenerPaginado(paginacion: PaginacionDTO): Observable<HttpResponse<CineDto[]>> {
    let queryParams = construirQueryParams(paginacion);
    return this.http.get<CineDto[]>(this.urlBase,{params: queryParams,observe:'response'}) ;

  }
  obtenerPorId(id: number): Observable<CineDto> {
    return this.http.get<CineDto>(`$(this.urlBase)/${id}`)
  }
  actualizar(id: number, entidad: CineCreacionDto): Observable<any> {
    return this.http.put(`$(this.urlBase)/${id}`,entidad);
  }
  crear(entidad: CineCreacionDto): Observable<any> {
    return this.http.post(this.urlBase,entidad);
  }
  borrar(id: number): Observable<any> {
    return this.http.delete(`$(this.urlBase)/${id}`);
  }
}
