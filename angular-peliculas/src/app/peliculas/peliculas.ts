import { ActorAutoCompleteDto } from "../actores/actores";

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
