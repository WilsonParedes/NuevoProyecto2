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

        public string estado { get; set; }

        public Repositorio(string comentario, string contenido)
        {

            this.contador = Global.manejoAr.DevueveCorrelativoVersion() + 1;
            this.comentario = comentario;
            this.contenido = contenido;
            fecha = DateTime.Now.ToString();
            this.estado = "1";
        }

        public Repositorio(string contadorauxiliar, string fechadeapoyo, string comentario, string contenido, string estado)
        {
            this.contadorauxiliar = contadorauxiliar;
            this.fechaapoyo = fechadeapoyo;
            this.comentario = comentario;
            this.contenido = contenido;
            this.estado = estado;
        }

        public override string ToString()
        {
            return $"Versión No.:  {contador}" + "%" +
                   $"Fecha: {fecha}" + "%" +
                   $"Comentario: {comentario}" + "%" +
                   $"Contenido: {contenido}" + "%" +
                   $"Estado: {estado}";

        }


    }
}
