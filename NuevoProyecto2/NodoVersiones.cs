using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2
{
    class NodoVersiones<T>
    {
        private Nodos<T> v;
        public T dato { get; set; }
        public NodoVersiones<T> siguiente { get; set; }

        public NodoVersiones<T> enlace { get; set; }



        public NodoVersiones(T dato)
        {
            this.dato = dato;
            this.siguiente = null;
            this.enlace = null;
        }


        public NodoVersiones(Nodos<T> v)
        {
            this.v = v;
        }

        public static explicit operator NodoVersiones<T>(Nodos<T> Version)
        {
            return new NodoVersiones<T>(Version);
            throw new NotImplementedException();
        }

    }
}
