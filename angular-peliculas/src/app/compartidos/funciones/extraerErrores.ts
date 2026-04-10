import { M } from "@angular/cdk/keycodes";

export function extraerErrores(obj: any): string[] {

   const err = obj.error.errors;

   let mensajesDeError: string[] = [];
   for(let llave in err) {
     const mensajesConCampos = err[llave].map((mensaje: string) => `${llave}: ${mensaje}`);

     mensajesDeError= mensajesDeError.concat(mensajesConCampos);
    }


   return mensajesDeError;

}
