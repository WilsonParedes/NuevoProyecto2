using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2
{
    class NodoArbol<T>
    {
        public T data { get; set; }
        public NodoArbol<T> izq { get; set; }
        public NodoArbol<T> der { get; set; }
    }
}
