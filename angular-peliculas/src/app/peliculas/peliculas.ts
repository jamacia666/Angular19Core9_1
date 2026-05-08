import { ActorAutoCompleteDto } from "../actores/actores";
import { CineDto } from "../cines/cines";
import { GeneroDTO } from "../generos/generos";

export interface PeliculasDto {
 id: number;
 titulo: string;
 fechaLanzamiento: Date;
 trailer: string;
 poster?: string;
}

export interface PeliculasCreacionDto {
 titulo: string;
 fechaLanzamiento: Date;
 trailer: string;
 poster?: File;
 generosIds?: number[];
 cinesIds?: number[];
 actores?: ActorAutoCompleteDto[];
}

export interface PeliculasPostGetDTO {
  generos: GeneroDTO[];
  cines: CineDto[];
}
