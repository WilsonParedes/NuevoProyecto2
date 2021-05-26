using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace NuevoProyecto2
{
    class Global<T>
    {
        public static Nodos<T> manejoAr = new Nodos<T>();
        public static Nodos<T> nodoArbol = new Nodos<T>();
        public static NodoArbol<T> Arbol = new NodoArbol<T>();
        public static DataSystem.Herramientas MT = new DataSystem.Herramientas();
        public static DataSystem.GestorBDD GB = new DataSystem.GestorBDD();

        public static string codSys = @"C:\";
        public static string _path = @"C:\Users\wilso\OneDrive\Escritorio\Mariano Galvez\Tercer año 2021\Primer Semestre\Programación 3\Proyecto 1\Proyecto1\temp\";
        public static string nuevoPath = "";
        public static string NombreArch = "";
        public static string folderParh = "";//Directorio Ingresado por Usuario
        public static string _pathTexto = "";
        public static char SeparadorRalla = '|';
        public static char SeparadorPorcentaje = '%';
        public static char SeparadorElevacion = '^';
        public static string cadenadevuelvearbol = "";

        public static NpgsqlConnection ConectaBDD = new NpgsqlConnection();
        public static string URL_DB = "jdbc:postgresql://localhost:5432/TercerSemestreProyecto3";
        public static string USER_DB = "postgres";
        public static string PASSWORD_DB = "wilson";

        public static void conectar()
        {
            try
            {
                ConectaBDD.ConnectionString = "Username = postgres; Password = wilson; Host = localhost; Port = 5432; Database = TercerSemestreProyecto3";
                ConectaBDD.Open();
                Console.WriteLine("Estas Conectado");
            }
            catch (Exception ex)
            {
                Console.WriteLine(null, ex);
            }


        }
    }
}
