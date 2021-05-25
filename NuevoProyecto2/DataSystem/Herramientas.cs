/*Centro de procesamiento del programa, métodos invocables*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuevoProyecto2.DataSystem
{
    class Herramientas
    {
        /*Menú de ayuda para el usuario*/
        /*Método invocado por la opción "dir" en Program*/
        public void Opciones()
        {
            Console.WriteLine("");
            Console.WriteLine("Comando              Parametro                Descripción ayuda");
            Console.WriteLine("search           <Numero Versión>:    Busca una versión del Repositorio");
            Console.WriteLine("create file      <Nombre Archivo>:    Crea archivos en la ruta de acceso");
            Console.WriteLine("create ver       <Nombre Versión>:    Crea un versión de la ruta de acceso");
            Console.WriteLine("binnacle:                             Bitacora de Registros del Repositorio");
            Console.WriteLine("delete           <Número Versión>:    Borra una versión del Repositorio");
            Console.WriteLine("delete rm        <Nombre Archivo>     Borra el archivo especificado de la última versión");
            Console.WriteLine("read:                                 Lee la version actual");
            Console.WriteLine("show tree view   <Número Version>:    Muestra el árbol completo");
        }


        //Método para crear directorio general
        /*Es invocado por la opción "init" en Program*/
        public bool CrearDirectorio(string pathUsuario, string nombreCarpeta, string codSys)
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

        /*Método encargado de crear archivos en el directorio especificado por el usuario*/
        /*Es invocado por CrearArchivosenDirectoriodeUnaVersion y la opción "create file " en Program*/
        public void CrearArchivosEnDirectorio(string op, string codSys, string nombreArchivo,string contenidoArchivo)
        {
            try
            {
                //Crea nuevos elementos dentro del Directorio, el usuario colocará el mismo para Crear el archivo
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                if (op.Contains("create file"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global<string>.NombreArch = op.Substring(12);
                    Global<string>.nuevoPath = Global<string>._pathTexto + Global<string>.NombreArch;
                    StreamWriter sw = new StreamWriter(Global<string>.nuevoPath, true);
                    sw.Close();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(Global<string>._pathTexto + "\\"+ "Archivo Creado");
                    Console.ForegroundColor = ConsoleColor.White;
                }else if(op.Contains("show tree view"))

                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global<string>.NombreArch = nombreArchivo;
                    Global<string>.nuevoPath = Global<string>._pathTexto + Global<string>.NombreArch;
                    StreamWriter sw = new StreamWriter(Global<string>.nuevoPath, true);
                    sw.Close();
                }else if (op.Contains("search"))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global<string>.NombreArch = nombreArchivo;
                    Global<string>.nuevoPath = Global<string>._pathTexto + Global<string>.NombreArch;
                    StreamWriter sw = new StreamWriter(Global<string>.nuevoPath, true);
                    sw.Close();
                    EscribeContenidoEnLosTXT(contenidoArchivo);
                    
                }
                else if (op.Contains("remove rm "))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global<string>.NombreArch = nombreArchivo;
                    Global<string>.nuevoPath = Global<string>._pathTexto + Global<string>.NombreArch;
                    StreamWriter sw = new StreamWriter(Global<string>.nuevoPath, true);
                    sw.Close();
                    EscribeContenidoEnLosTXT(contenidoArchivo);

                }

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(codSys + "El archivo que intenta crear no es correcto");
            }
        }

        /*Método encargado de crear un árbol y posteriormente crear nodos dentro de ese arbol binario de busqueda*/
        /*Es invocado por 2 métodos VisualizacionArbolForm y la opción de "crear ver" en Program*/
        public NodoArbol<object> CrearVersionenArbol(string nombreVresion, string identificador) 
        {
            string[] lista = null;
            int tamañoDirec=0, i= 0;
            string nuevaCadena="", nombreArchivo="", cadena="", peso = "", extensiónArchivo="",contenidoArchivo="";
            ulong nueva = 0;
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
                        extensiónArchivo= pesoArchivo[i].Extension.ToString();
                        nombreArchivo = pesoArchivo[i].ToString();
                        peso = pesoArchivo[i].Length.ToString();
                        cadena = ConvertirCadenaaHexa(nombreArchivo.ToString());
                        double num = Convert.ToDouble(cadena.ToString());
                        contenidoArchivo = Global<object>.manejoAr.LeerArchivo(nombreArchivo);
                        repositorio = new Repositorio(nombreArchivo, contenidoArchivo, peso, cadena);
                        _ = Global<Object>.nodoArbol.Insertar(new Repositorio(num), repositorio, MenorQueEntero, MayorQueEntero);
                        nuevaCadena = nuevaCadena + repositorio.ToString();
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(Global<string>._pathTexto + "\\" + "No se pudo guardar la versión, el siguiente archivo tiene conflicto " + nombreArchivo + " Intente renombrarlo y vuelva a crear la versión");
                        Console.ForegroundColor = ConsoleColor.White;
                        return (null);
                    }
                }

            }
            else
            {
                for (i = 0; i < pesoArchivo.Length; i++)
                {
                    try
                    {
                        nombreArchivo = pesoArchivo[i].ToString();
                        peso = pesoArchivo[i].Length.ToString();
                        cadena = ConvertirCadenaaHexa(nombreArchivo.ToString());
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
                        return (null);

                    }
                }

            }
            return Global<object>.nodoArbol.RaizRepositorio;
        }

        /*Método encarado de convertir el nombre del archivo en formato Hexadecimal*/
        /*Es invocado por un método en Herramientas.CrearVers*/
        private static string ConvertirCadenaaHexa(string cadena)
        {

            StringBuilder sb = new StringBuilder();
            foreach (char caracter in cadena)
            {

                sb.Append(Convert.ToInt64(caracter));

            }

            return sb.ToString();
        }


        /*Método encargado de crear los nodos en la lista enlazada*/
        /*Es invocado por la opción "crear ver " en Program*/
        public void CrearVersionEnListaEnlazada(string contenidoCadena, string nombreVers, Nodos<object> ArbolCompleto)
        {

            /*Se agregó este nuevo bloque de if para validar si se almacenará o no un nodo*/
            if (!Global<object>.manejoAr.validarNodosVersiones()&& contenidoCadena!=null)
            {
                FileInfo[] archivosCarpeta = ArchivosDirectorio();
                int ultimaVersion = Global<object>.manejoAr.DevueveCorrelativoVersion();
                string nombreArchivoContenidVersion, contenidoLista, contenidoVersion;
                (nombreArchivoContenidVersion, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(ultimaVersion.ToString());
                int cantidad = 0;
                string VersConte = "";
                (cantidad, VersConte, contenidoVersion) = DevuelveCantidadArchivosVersion(contenidoLista);
                int coincidencias = ComparaCarpetaconContenidoVersion(contenidoLista, archivosCarpeta);/*compara la cantidad de archivos que coinciden*/
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
                }

            }
            else
            {
                if (contenidoCadena!="")
                {
                    //Si la Lista enlazada se encuentra vacía, se procede a crear un Nodo Cabeza
                    Global<object>.manejoAr.agregarVersion(new Repositorio(nombreVers.Substring(11), contenidoCadena), ArbolCompleto);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(Global<string>._pathTexto + "\\" + "Se almacenó el nodo exitosamente");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine(Global<string>._pathTexto + "\\" + "No se puede crear una versión, debe existir al menos 1 archivo en el directorio");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
                   
            }

        }


        /*Devuelve en número, si todos los archivos que estan en la versión coinciden*/
        /*Método encargado de comparar los contenidos del Directorio con los contenidos en las versiones que el usuario desea buscar*/
        /*Siempre devuelve la cantidad de archivos que coinciden*/
        /*Invodado por el método Herramientas.CrearNodoListaEnlazada*/
        private int ComparaCarpetaconContenidoVersion(string contenidoLista, FileInfo[] archivosCarpeta)
        
        {
            string nombreArchivoCarpeta, ContenidoNombreVersion, contenidodelaVersionanterior, contenidoActualdelArchivo, extensiónArchivo;
            bool coincidencia = false;
            long pesoArchivoCarpeta, contenidoPesoVersion;
            string[] ArrayContenido;
            string[] AuxiliarArrayContenido;
            int contador = 0;
            int i, j, k;
            for (i = 0; i < archivosCarpeta.Length; i++)
            {
                nombreArchivoCarpeta = archivosCarpeta[i].Name.ToString();
                pesoArchivoCarpeta = archivosCarpeta[i].Length;
                extensiónArchivo = archivosCarpeta[i].Extension.ToString();
                contenidoActualdelArchivo = Global<object>.manejoAr.LeerArchivo(nombreArchivoCarpeta);
                ArrayContenido = contenidoLista.Split(Global<char>.SeparadorRalla);
                for (j = 0; j < ArrayContenido.Length - 2; j++)
                {
                    AuxiliarArrayContenido = ArrayContenido[j].Split(Global<char>.SeparadorPorcentaje);
                    for (k = 0; k < 5; k++)
                    {
                        ContenidoNombreVersion = AuxiliarArrayContenido[0].Substring(16).ToString();
                        contenidoPesoVersion =long.Parse(AuxiliarArrayContenido[1].Substring(14));
                        contenidodelaVersionanterior = AuxiliarArrayContenido[4].Substring(19).ToString();
                        coincidencia = Global<object>.manejoAr.CompararContenido(contenidodelaVersionanterior, contenidoActualdelArchivo);
                        if ((nombreArchivoCarpeta.Equals(ContenidoNombreVersion))&& (pesoArchivoCarpeta == contenidoPesoVersion)&&(coincidencia))
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
        /*Devuelve la cantidad de archivos que fueron almacenados en la versión*/
        /*Método invocado por Herramientas.CrearNodoListaEnlazada, Herramientas.VisualizacionArbolForm y Herramientas.OpcionBusqueda*/
        public (int canti, string conVers,string contenidoVersion)  DevuelveCantidadArchivosVersion(string contenidoLista)
        {
            int cantidad = 0;
            int i,j;
            string[] ArrayContenido;
            string[] AuxiliarArrayContenido;
            string nombreVersion = "", contenidoVersion = ""; 
            ArrayContenido = contenidoLista.Split(Global<char>.SeparadorRalla);
            for (i = 0; i < ArrayContenido.Length - 2; i++)
            {
                AuxiliarArrayContenido = ArrayContenido[i].Split(Global<char>.SeparadorPorcentaje);
                for (j = 0; j < 1; j++)
                {
                    cantidad = cantidad +1;
                    nombreVersion += AuxiliarArrayContenido[0].ToString()+ Global<char>.SeparadorPorcentaje;
                    contenidoVersion += AuxiliarArrayContenido[4].Substring(19).ToString() + Global<char>.SeparadorPorcentaje;

                }
            }
            return (cantidad, nombreVersion, contenidoVersion);
        }

        /*Devuelve todos los archivos que se encuentran dentro del Path*/
        /*Devuelve en un array todos los archivos que estan dentro del directorio definido*/
        /*Método invocado por Herramientas.CrearVers y Herramientas.CrearNodoListaEnlazada*/
        private FileInfo[] ArchivosDirectorio()
        {
            DirectoryInfo archivos = new DirectoryInfo(Global<string>._pathTexto);
            FileInfo[] pesoArchivo = archivos.GetFiles();
            return pesoArchivo;
        }

        /*Método encargado de enviar los datos de la versión al árbol, con el fin de ser tratado para su impresíón*/
        /*Método invocado por la opción "show tree view" de Program*/

        public void VisualizacionArbolForm(string numerobuscar, string op)
        {
            int cantidad,j;
            string nombreArchivoContenidVersion, contenidoLista, VersConte, cadenaSinUsar, contenidoVersion;
            (nombreArchivoContenidVersion, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(numerobuscar);
            (cantidad, VersConte, contenidoVersion) = Global<object>.MT.DevuelveCantidadArchivosVersion(contenidoLista);
            EliminarArchivosdelDirectorio();
            Global<bool>.nodoArbol.EliminarElContenidoArbol();
            CrearArchivosenDirectoriodeUnaVersion(VersConte, op,"");
            Nodos<object> ArbolCompleto = new Nodos<object>();
            Global<object>.MT.CrearVersionenArbol(op, "");
        }

        public void OpcionBusqueda(string op)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string nuevaLista, contenidoLista, nuevocontenidoLista, VersConte, contenidoVersion;
            string[] ArrayContenido;
            int cantidad;
            (nuevaLista, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(op.Substring(7));//Llamada al método que realiza la busqueda

            if (contenidoLista != null)
            {
                ArrayContenido = contenidoLista.Split(Global<char>.SeparadorRalla);
            }
            else
            {
                throw new NullReferenceException();
            }
            (cantidad, VersConte, contenidoVersion) = DevuelveCantidadArchivosVersion(contenidoLista);

            /*arregla el formato del contenido almacenado en el árbol*/
            nuevocontenidoLista = ArreglaContenidodelaVersion(ArrayContenido);
            //Método que ayudará a imprimir los datos de la versión obtenida por el método BusquedaVersion
            ImprimelosDatosdeLaVersion(nuevaLista, nuevocontenidoLista);
            EliminarArchivosdelDirectorio();
            CrearArchivosenDirectoriodeUnaVersion(VersConte, op, contenidoVersion);
        }



        /*Método encargado de imprimir por consola, todo los datos de la versión y el contendio de esa versión*/
        /*Método invocado por la opción "search" de Program*/
        private void ImprimelosDatosdeLaVersion(string nuevaLista, string nuevocontenidoLista)
        {
            int i;
            if (nuevaLista != null)
            {
                string[] nuevoArreglo = nuevaLista.Split(Global<char>.SeparadorPorcentaje);
                for (i = 0; i < 1; i++)
                {
                    Repositorio ultimaVersion = new Repositorio(nuevoArreglo[0], nuevoArreglo[1], nuevoArreglo[2], nuevoArreglo[3], '/');
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\t\n            DATOS ALMACENADOS DE LA VERSIÓN");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\t" + ultimaVersion.contadorauxiliar.ToString());
                    Console.WriteLine("\t" + ultimaVersion.fechaapoyo.ToString());
                    Console.WriteLine("\t" + ultimaVersion.comentario.ToString());
                    Console.WriteLine("\t" + "Contenido de la capeta: \n");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("\t\t" + nuevocontenidoLista);
                    Console.ForegroundColor = ConsoleColor.White;

                }
            }
            else
            {
                //Si la versión no existe, envia un mensaje de información
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(Global<string>._pathTexto + "\\" + "LA VERSIÓN NO EXISTE");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
       

        /*Método encargado de arreglar el contenido para su impresión, con el fin de darle un formato mas estetico*/
        /*Método invocado por la opción "search" de Program*/
        private string ArreglaContenidodelaVersion(string [] ArrayContenido)
        {
            int i,j;
            string[] AuxiliarArrayContenido;
            string nuevocontenidoLista = "";
            string contenidVersion = "";
            for (i = 0; i < ArrayContenido.Length - 2; i++)
            {

                AuxiliarArrayContenido = ArrayContenido[i].Split(Global<char>.SeparadorPorcentaje);
                for (j = 0; j < 5; j++)
                {
                    nuevocontenidoLista = nuevocontenidoLista + AuxiliarArrayContenido[j] + "\n\t\t";
                  
                }
                nuevocontenidoLista = nuevocontenidoLista + "\n\t\t";
            }
            return nuevocontenidoLista;
        }

        /*Método encargado de elminar todos los archivos que se encuentran dentro del directorio*/
        /*Método invocado por Herramientas.VisualizacionArbolForm*/
        private void EliminarArchivosdelDirectorio()
        {
            int i;
            string[] eliminar;
            eliminar = Directory.GetFiles(Global<string>._pathTexto);
            for (i = 0; i < eliminar.Length; i++)
            {
                File.Delete(eliminar[i].ToString());
            }
        }


        /*Método encargado de crear todos los archivos que se encuentran en el contenido de la versión dentro del directorio establecido*/
        /*Método invocado por Herramientas.VisualizacionArbolForm*/
        private void CrearArchivosenDirectoriodeUnaVersion(string VersConte, string op, string contenidoVersion) {
            string[] AuxiliarArrayNombre, AuxiliarArrayContenido;
            int i;
            if (op.Contains("show tree view "))
            {
                AuxiliarArrayNombre = VersConte.Split(Global<char>.SeparadorPorcentaje);
                for (i = 0; i < AuxiliarArrayNombre.Length - 1; i++)
                {

                    Global<object>.MT.CrearArchivosEnDirectorio(op, Global<string>.codSys, AuxiliarArrayNombre[i].Substring(16), "");
                }
            }
            else
            {
                AuxiliarArrayNombre = VersConte.Split(Global<char>.SeparadorPorcentaje);
                AuxiliarArrayContenido = contenidoVersion.Split(Global<char>.SeparadorPorcentaje);
                for (i = 0; i < AuxiliarArrayNombre.Length - 1; i++)
                {

                    Global<object>.MT.CrearArchivosEnDirectorio(op, Global<string>.codSys, AuxiliarArrayNombre[i].Substring(16), AuxiliarArrayContenido[i]);
                }
            }
            
        }




        private void EscribeContenidoEnLosTXT(string contendioArchivo)
        {
            StreamWriter escribirTXT = new StreamWriter(Global<string>.nuevoPath);
            escribirTXT.Write(contendioArchivo);
            escribirTXT.Close();
        }


        public void RemoverHojadelArbol(string numerobuscar, string op)
        {
            int cantidad, j;
            string nombreArchivoContenidVersion, contenidoLista, VersConte, cadenaSinUsar, contenidoVersion;
            (nombreArchivoContenidVersion, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(numerobuscar);
            (cantidad, VersConte, contenidoVersion) = Global<object>.MT.DevuelveCantidadArchivosVersion(contenidoLista);
            EliminarArchivosdelDirectorio();
            Global<bool>.nodoArbol.EliminarElContenidoArbol();
            CrearArchivosenDirectoriodeUnaVersion(VersConte, op, contenidoVersion);
            Nodos<object> ArbolCompleto = new Nodos<object>();
            Global<object>.MT.CrearVersionenArbol(op, "crear");
            Func<Object, Object, bool> MenorQueEntero = (x, y) => Convert.ToDouble(x.ToString()) < Convert.ToDouble(y.ToString());
            Func<Object, Object, bool> MayorQueEntero = (x, y) => Convert.ToDouble(x.ToString()) > Convert.ToDouble(y.ToString());
            string cadena = ConvertirCadenaaHexa(op.Substring(12).ToString());
            double num = Convert.ToDouble(cadena.ToString());
            Global<object>.manejoAr.Eliminar(num, MenorQueEntero, MayorQueEntero);

        }


        
        public string  DevuelveCadenadelArbolInOrden(NodoArbol<object> raiz)
        {
            
            if (raiz == null)
            {
                /*Console.Write(" ");*/
            }
            else
            {
                Global<string>.cadenadevuelvearbol += raiz.data;
                DevuelveCadenadelArbolInOrden(raiz.izq);
                DevuelveCadenadelArbolInOrden(raiz.der);

            }
            return Global<string>.cadenadevuelvearbol;
        }


    }
}
