using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2
{
    class NodoArbol<T>:Nodos<T>
    {
        private Repositorio v;



        public T data { get; set; }
        public NodoArbol<T> izq { get; set; }
        public NodoArbol<T> der { get; set; }


        public NodoArbol()
        {
            this.der = null;
            this.izq = null;
        }
        public NodoArbol(T dato)
        {
            this.data = dato;
        }

        public NodoArbol(Repositorio v)
        {
            this.v = v;
        }
       
    }
}
