﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2.DataSystem
{
    class Herramientas
    {

        public void Opciones()
        {
            Console.WriteLine("");
            Console.WriteLine("Comando              Parametro                Descripción ayuda");
            Console.WriteLine("search           <Numero Versión>:    Busca una versión del Repositorio");
            Console.WriteLine("create file      <Nombre Archivo>:    Crea archivos en la ruta de acceso");
            Console.WriteLine("create ver       <Nombre Versión>:    Crea un versión de la ruta de acceso");
            Console.WriteLine("binnacle:                             Bitacora de Registros del Repositorio");
            Console.WriteLine("delete           <Número Versión>:    Borra una versión del Repositorio");
            Console.WriteLine("read:                                 Lee la version actual");
            Console.WriteLine("show tree view   <Número Version>:    Muestra el árbol completo");
        }


        //Método para crear directorio
        public bool CreacionDirectorio(string pathUsuario, string nombreCarpeta, string codSys)
        {

            bool repetir = false;
            Global<string>.folderParh = pathUsuario;
            if (Directory.Exists(pathUsuario) && (!pathUsuario.Equals("")))
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(Directory.CreateDirectory(Global<string>.folderParh + "\\" + nombreCarpeta + "\\"));
                    Global<string>._pathTexto = (Global<string>.folderParh + "\\" + nombreCarpeta + "\\");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(codSys + "Se creó ruta de acceso");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.WriteLine(codSys + "La ruta de acceso no es valida");
                    repetir = true;
                }
            }
            else
            {
                Console.WriteLine(codSys + "No se puede crear la ruta, intente de nuevo\n");
                repetir = true;
            }
            return repetir;
        }

        public void CrearArchivosEnDirectorio(string crearArchivo, string codSys, string cadena)
        {
            try
            {
                //Crea nuevos elementos dentro del Directorio, el usuario colocará el mismo para Crear el archivo
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (crearArchivo.Contains("create file"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global<string>.NombreArch = crearArchivo.Substring(12);
                    Global<string>.nuevoPath = Global<string>._pathTexto + Global<string>.NombreArch;
                    StreamWriter sw = new StreamWriter(Global<string>.nuevoPath, true);
                    sw.Close();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(Global<string>._pathTexto + "\\"+ "Archivo Creado");
                    Console.ForegroundColor = ConsoleColor.White;
                }else if(crearArchivo.Contains("show tree view"))

                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global<string>.NombreArch = cadena;
                    Global<string>.nuevoPath = Global<string>._pathTexto + Global<string>.NombreArch;
                    StreamWriter sw = new StreamWriter(Global<string>.nuevoPath, true);
                    sw.Close();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(Global<string>._pathTexto + "\\" + "Archivo Creado");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(codSys + "El archivo que intenta crear no es correcto");
            }
        }


        public (string cadena, Nodos<Object> ArbolCompleto) CrearVers(string nombreVresion, string identificador) 
        {
            string[] lista = null;
            int tamañoDirec = 0;
            int i = 0;
            string nuevaCadena = "";
            string nombreArchivo = "";
            ulong nueva = 0;
            string cadena = "";
            string peso = "";
            /*Lista Enlazada para crear Ramas*/
            Console.ForegroundColor = ConsoleColor.White;
            Func<Object, Object, bool> MenorQueEntero = (x, y) => Convert.ToDouble(x.ToString()) < Convert.ToDouble(y.ToString());
            Func<Object, Object, bool> MayorQueEntero = (x, y) => Convert.ToDouble(x.ToString()) > Convert.ToDouble(y.ToString());
            FileInfo[] pesoArchivo = ArchivosDirectorio();
            tamañoDirec = (Global<string>._pathTexto.Length);
            Repositorio repositorio;

            if (identificador.Equals("crear"))
            {
                for (i = 0; i < pesoArchivo.Length; i++)
                {
                    try
                    {
                        /*nombreArchivo = lista[i].Substring(tamañoDirec).ToString();*/
                        nombreArchivo = pesoArchivo[i].ToString();
                        peso = pesoArchivo[i].Length.ToString();
                        cadena = ConvertirCadena(nombreArchivo.ToString());
                        double num = Convert.ToDouble(cadena.ToString());
                        repositorio = new Repositorio(nombreArchivo, cadena, peso, cadena);
                        _ = Global<Object>.nodoArbol.Insertar(new Repositorio(num), repositorio, MenorQueEntero, MayorQueEntero);
                        nuevaCadena = nuevaCadena + repositorio.ToString();
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Global<string>._pathTexto + "\\" + "No se pudo guardar la versión, el siguiente archivo tiene conflicto " + nombreArchivo + " Intente renombrarlo y vuelva a crear la versión");
                        Console.ForegroundColor = ConsoleColor.White;
                        return (null, null);
                        break;

                    }
                }

            }
            else
            {
                for (i = 0; i < pesoArchivo.Length; i++)
                {
                    try
                    {
                        /*nombreArchivo = lista[i].Substring(tamañoDirec).ToString();*/
                        nombreArchivo = pesoArchivo[i].ToString();
                        peso = pesoArchivo[i].Length.ToString();
                        cadena = ConvertirCadena(nombreArchivo.ToString());
                        double num = Convert.ToDouble(cadena.ToString());
                        repositorio = new Repositorio(nombreArchivo);
                        _ = Global<Object>.nodoArbol.Insertar(new Repositorio(num), repositorio, MenorQueEntero, MayorQueEntero);
                        nuevaCadena = nuevaCadena + repositorio.ToString();
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Global<string>._pathTexto + "\\" + "No se pudo guardar la versión, el siguiente archivo tiene conflicto " + nombreArchivo + " Intente renombrarlo y vuelva a crear la versión");
                        Console.ForegroundColor = ConsoleColor.White;
                        return (null, null);
                        break;

                    }
                }

            }
            return (nuevaCadena, Global<Object>.nodoArbol.RaizRepositorio);
        }

        private static string ConvertirCadena(string cadena)
        {

            StringBuilder sb = new StringBuilder();
            foreach (char caracter in cadena)
            {

                sb.Append(Convert.ToInt64(caracter));

            }

            return sb.ToString();
        }


        public void CrearNodoListaEnlazada(string contenidoCadena, string nombreVers, Nodos<object> ArbolCompleto)
        {

            /*Se agregó este nuevo bloque de if para validar si se almacenará o no un nodo*/
            if (!Global<object>.manejoAr.validarNodosVersiones()&& contenidoCadena!=null)
            {
                FileInfo[] archivosCarpeta = ArchivosDirectorio();
                int ultimaVersion = Global<object>.manejoAr.DevueveCorrelativoVersion();
                string nombreArchivoCarpeta, nombreArchivoContenidVersion, contenidoLista, ContenidoVersion;
                string[] ArrayContenido = null;
                string[] AuxiliarArrayContenido = null;
                (nombreArchivoContenidVersion, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(ultimaVersion.ToString());
                bool sincoincidencia = false;
                int i, j, k;

                int cantidad = 0;
                string VersConte = "";
                (cantidad, VersConte) = DevuelveCantidadArchivosVersion(contenidoLista);
                int coincidencias = ComparaCarpetaconContenidoVersion(contenidoLista, archivosCarpeta);
                if (cantidad == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(Global<string>._pathTexto + "\\" + "Debe crear un archivo en la carpeta de origen, utilice comand --dir-- para ayuda");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                if (cantidad != archivosCarpeta.Length)
                {
                    Global<object>.manejoAr.agregarVersion(new Repositorio(nombreVers.Substring(11), contenidoCadena), ArbolCompleto);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(Global<string>._pathTexto + "\\" + "Se almacenó el nodo exitosamente");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {

                    if (coincidencias == archivosCarpeta.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Global<string>._pathTexto + "\\" + "No existe ninguna modificación en la carpeta de origen");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Global<object>.manejoAr.agregarVersion(new Repositorio(nombreVers.Substring(11), contenidoCadena), ArbolCompleto);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(Global<string>._pathTexto + "\\" + "Se almacenó el nodo exitosamente");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    /*for (i = 0; i < archivosCarpeta.Length; i++)
                    {
                        nombreArchivoCarpeta = archivosCarpeta[i].Name.ToString();
                        ArrayContenido = contenidoLista.Split('|');
                        for (j = 0; j < ArrayContenido.Length - 2; j++)
                        {
                            AuxiliarArrayContenido = ArrayContenido[j].Split('%');
                            for (k = 0; k < 4; k++)
                            {
                                ContenidoVersion = AuxiliarArrayContenido[0].Substring(16).ToString();
                                if (nombreArchivoCarpeta.Equals(ContenidoVersion))
                                {
                                    contador = contador + 1;
                                    break;
                                }
                                break;
                            }
                        }
                    }*/
                }

            }
            else
            {
                if (contenidoCadena!=null)
                {
                    //Si la Lista enlazada se encuentra vacía, se procede a crear un Nodo Cabeza
                    Global<object>.manejoAr.agregarVersion(new Repositorio(nombreVers.Substring(11), contenidoCadena), ArbolCompleto);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(Global<string>._pathTexto + "\\" + "Se almacenó el nodo exitosamente");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }


        /*Devuelve en número, si todos los archivos que estan en la versión coinciden*/

        private int ComparaCarpetaconContenidoVersion(string contenidoLista, FileInfo[] archivosCarpeta)
        {
            string nombreArchivoCarpeta, ContenidoVersion;
            string[] ArrayContenido;
            string[] AuxiliarArrayContenido;
            int contador = 0;
            int i, j, k;
            for (i = 0; i < archivosCarpeta.Length; i++)
            {
                nombreArchivoCarpeta = archivosCarpeta[i].Name.ToString();
                ArrayContenido = contenidoLista.Split('|');
                for (j = 0; j < ArrayContenido.Length - 2; j++)
                {
                    AuxiliarArrayContenido = ArrayContenido[j].Split('%');
                    for (k = 0; k < 4; k++)
                    {
                        ContenidoVersion = AuxiliarArrayContenido[0].Substring(16).ToString();
                        if (nombreArchivoCarpeta.Equals(ContenidoVersion))
                        {
                            contador = contador + 1;
                            break;
                        }
                        break;
                    }
                }
            }
            return contador;
        }
        /*Devuelve en número, la cantidad de arhcivos que fueron almacenados en la versión*/

        public (int canti, string conVers)  DevuelveCantidadArchivosVersion(string contenidoLista)
        {
            int cantidad = 0;
            int i,j;
            string[] ArrayContenido;
            string[] AuxiliarArrayContenido;
            string ContenidoVersion="";
            ArrayContenido = contenidoLista.Split('|');
            for (i = 0; i < ArrayContenido.Length - 2; i++)
            {
                AuxiliarArrayContenido = ArrayContenido[i].Split('%');
                for (j = 0; j < 1; j++)
                {
                    cantidad = cantidad +1;
                    ContenidoVersion += AuxiliarArrayContenido[0].ToString()+"%";

                }
            }
            return (cantidad, ContenidoVersion);
        }

        /*Devuelve todos los archivos que se encuentran dentro del Path*/
        private FileInfo[] ArchivosDirectorio()
        {
            DirectoryInfo archivos = new DirectoryInfo(Global<string>._pathTexto);
            FileInfo[] pesoArchivo = archivos.GetFiles();
            return pesoArchivo;
        }
    }
}