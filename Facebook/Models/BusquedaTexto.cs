using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.Models
{
    public class BusquedaTexto
    {
        private string texto;
        public List<string> resultado;
        public List<Tuple<string,string>> resultadoEtiquetas;

        public BusquedaTexto(string texto)
        {
            this.texto = texto;
        }

        public void buscarHashtag()
        {
            resultado = new List<string>();
            for (int i=0; i<texto.Length; i++)
            {
                string k = "";
                var aux = texto[i];
                if (aux == '#')
                {
                    i++;
                    if (i < texto.Length) aux = texto[i];
                    while (aux!=' ' && i < texto.Length)
                    {
                        k +=aux;
                        i++;
                        if(i<texto.Length) aux = texto[i];
                    }
                    resultado.Add(k);
                }
            }
        }

        public void buscarEtiqueta()
        {
            resultadoEtiquetas = new List<Tuple<string,string>>();
            var nombre = "";
            var apellido = "";
            int bandera = 0;
            for (int i = 0; i < texto.Length; i++)
            {                
                var aux = texto[i];
                if (aux == '@')
                {
                    i++;
                    if (i < texto.Length) aux = texto[i];
                    while (aux != ' ' && i < texto.Length)
                    {
                        if (Char.IsUpper(aux)) bandera++;
                        if (bandera==1)
                        {
                            nombre += aux;
                        }
                        else
                        {
                            apellido += aux;
                        }
                        i++;
                        if (i < texto.Length) aux = texto[i];
                    }
                    resultadoEtiquetas.Add(Tuple.Create(nombre,apellido));
                }
            }
        }
    }
}
