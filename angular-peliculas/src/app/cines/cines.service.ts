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




  private http= inject(HttpClient);
  private urlBase = `${environment.apiUrl}/cines`;
    constructor() { }

  public obtenerPaginado(paginacion: PaginacionDTO): Observable<HttpResponse<CineDto[]>> {
    let queryParams = construirQueryParams(paginacion);
    console.log('URL' + this.urlBase);


    return this.http.get<CineDto[]>(`${this.urlBase}`,{params: queryParams,observe:'response'}) ;
  }

 public obtenerPorId(id: number): Observable<CineDto> {
    return this.http.get<CineDto>(`$(this.urlBase)/${id}`)
  }
  public actualizar(id: number, cine: CineCreacionDto): Observable<any> {
    return this.http.put(`$(this.urlBase)/${id}`,cine);
  }
 public crear(cine: CineCreacionDto): Observable<any> {
    return this.http.post(this.urlBase,cine);
  }
 public borrar(id: number): Observable<any> {
    return this.http.delete(`$(this.urlBase)/${id}`);
  }
}
