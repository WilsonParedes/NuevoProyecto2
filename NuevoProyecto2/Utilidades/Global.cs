using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NuevoProyecto2
{
    class Global <T>
    {
        public static Nodos<T> manejoAr = new Nodos<T>();
        public static Nodos<T> nodoArbol = new Nodos<T>();
        public static NodoArbol<T> Arbol = new NodoArbol<T>();
        public static DataSystem.Herramientas MT = new DataSystem.Herramientas();
        public static string codSys = @"C:\";
        public static string _path = @"C:\Users\wilso\OneDrive\Escritorio\Mariano Galvez\Tercer año 2021\Primer Semestre\Programación 3\Proyecto 1\Proyecto1\temp\";
        public static string nuevoPath = "";
        public static string NombreArch = "";
        public static string folderParh = "";//Directorio Ingresado por Usuario
        public static string _pathTexto = "";

    }
}
