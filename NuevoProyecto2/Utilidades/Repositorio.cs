using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2
{
    class Repositorio
    {

        public int contador = 0;

        public string contadorauxiliar;
        public string comentario { get; set; }
        public string contenido { get; set; }

        public string fecha;
        public string fechaapoyo { get; set; }
        public char separador { get; set; }

        public double posicion { get; set; }

        public string nombreArchivo { get; set; }
        public string pesoArchivo { get; set; }
        public string valorHexa { get; set; }

        public int validarNombrearhc = 0;


        public Repositorio(string comentario, string contenido)
        {

            this.contador = Global<object>.manejoAr.DevueveCorrelativoVersion() + 1;
            this.comentario = comentario;
            this.contenido = contenido;
            fecha = DateTime.Now.ToString();
            this.separador = Global<char>.SeparadorRalla;
        }

        public Repositorio(string nombreArchivo, string contenido, string pesoArchivo, string valorHexa)
        {
            this.nombreArchivo = nombreArchivo;
            this.pesoArchivo = pesoArchivo;
            this.fecha = DateTime.Now.ToString();
            this.contenido = contenido;
            this.valorHexa = valorHexa;
        }


        public Repositorio(string contadorauxiliar, string fechadeapoyo, string comentario, string contenido, char separador)
        {
            this.contadorauxiliar = contadorauxiliar;
            this.fechaapoyo = fechadeapoyo;
            this.comentario = comentario;
            this.contenido = contenido;
            this.separador = separador;
        }

        public Repositorio(string nombreArchivo)
        {
            this.nombreArchivo = nombreArchivo;
            this.validarNombrearhc = validarNombrearhc + 1;
            this.contador = 1;
           
        }
        public Repositorio(double posicion)
        {
            this.posicion = posicion;
        }


        public override string ToString()
        {
            if (posicion>0)
            {
                return posicion.ToString();
            }
            /*Datos Arbol*/
            if (contador <= 0)
            {
            return Global<char>.SeparadorElevacion +
                       $"Nombre Archivo: {nombreArchivo}" + Global<char>.SeparadorPorcentaje +
                       $"Peso Archivo: {pesoArchivo}"+ Global<char>.SeparadorPorcentaje +
                       $"Fecha: {fecha}" + Global<char>.SeparadorPorcentaje +
                       $"Valor Hexa: {valorHexa}" + Global<char>.SeparadorPorcentaje +
                       $"Contenido archivo: {contenido}"; 
            }
            if (validarNombrearhc>0)
            {
                return $"{nombreArchivo}";
            }
            return $"Versión No.:  {contador}" + Global<char>.SeparadorPorcentaje +
                   $"Fecha: {fecha}" + "%" +
                   $"Comentario: {comentario}" + Global<char>.SeparadorPorcentaje +
                   $"Contenido: {contenido}" + Global<char>.SeparadorPorcentaje +
                   $"Separador: {separador}";

        }

        


    }
}
