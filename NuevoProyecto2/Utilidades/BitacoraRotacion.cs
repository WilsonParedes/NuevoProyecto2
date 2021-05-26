using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2.Utilidades
{
    class BitacoraRotacion
    {

        int fe;
        string nodosrotacion;
        string rotacion;

        public BitacoraRotacion(int fe, string rotacion)
        {
            this.fe = fe;
            this.rotacion = rotacion;
        }

        public override string ToString()
        {
            return $"Factor Equilibrio:  {fe}"+ 
            $"Rotación : {rotacion}";
        }
    }
}
