import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { PeliculasCreacionDto, PeliculasDto, PeliculasPostGetDTO } from './peliculas';

@Injectable({
  providedIn: 'root'
})
export class PeliculasService {

  constructor() { }

  private http= inject(HttpClient);
  private urlBase = `${environment.apiUrl}/peliculas`;

  public CrearGet(): Observable<PeliculasPostGetDTO> {
    return  this.http.get<PeliculasPostGetDTO>(`${this.urlBase}/postget`);
  }

  public crear(pelicula: PeliculasCreacionDto):Observable<PeliculasDto> {
   const formData = this.construirFormData(pelicula);
   return this.http.post<PeliculasDto>(this.urlBase,formData);
  }

  private construirFormData(pelicula:PeliculasCreacionDto): FormData {
    const formData = new FormData();
    formData.append('titulo',pelicula.titulo);
    formData.append('fechaLanzamiento',pelicula.fechaLanzamiento.toISOString().split('T')[0]);
    if (pelicula.poster) {
      formData.append('poster',pelicula.poster);
    }
     if (pelicula.trailer) {
      formData.append('trailer',pelicula.trailer);
    }
    formData.append('generosIds', JSON.stringify(pelicula.generosIds));
    formData.append('cinesIds', JSON.stringify(pelicula.cinesIds));
    formData.append('actores', JSON.stringify(pelicula.actores));

    return formData;

  }

}
