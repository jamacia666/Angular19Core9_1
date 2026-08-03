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
export function extraerErroresIdentity(obj: any): string[]{
    let mensajesDeError: string[] = [];

    for (let i = 0; i < obj.error.length; i++) {
        const elemento = obj.error[i];
        mensajesDeError.push(elemento.description);

    }

    return mensajesDeError;
}
