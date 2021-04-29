using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2
{
    class NodoVersiones<T>
    {
        public T dato { get; set; }
        public NodoVersiones<T> siguiente { get; set; }



        public NodoVersiones(T dato)
        {
            this.dato = dato;
            this.siguiente = null;
        }
    }
}
