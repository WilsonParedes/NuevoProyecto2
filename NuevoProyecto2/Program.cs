using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace NuevoProyecto2
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.Run(new Form1());*/

            string op;
            string codSys = @"C:\";
            
            string contenido;
            string nombreAr;
            string inicializar;
            bool repetir = false;
            string nombreCarpeta = "";

            do
            {
                repetir = false;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(codSys);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                inicializar = Console.ReadLine();
                Console.Write("\\");
                nombreCarpeta = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if ((inicializar.Contains("init")) == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(codSys);
                    Console.Write("Incialice el programa 'init'\n");
                    repetir = true;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    try
                    {
                        repetir = CreacionDirectorio(inicializar.Substring(5), nombreCarpeta, codSys);
                    }
                    catch (ArgumentOutOfRangeException t)
                    {
                        Console.Write(codSys + "Coloque una ruta valida\n");
                        repetir = true;
                    }
                    //Método para crear el Directorio
                    do
                    {
                        Console.Write(codSys);
                        op = Console.ReadLine();
                        if (op.Contains("create file"))
                        {
                            CrearArchivosEnDirectorio(op, codSys);
                        }else if(op.Contains("create ver"))
                        {

                            string cadena = "";
                            Global<bool>.nodoArbol.eliminarArboles();
                            Nodos<Object> ArbolCompleto = new Nodos<Object>();
                            (cadena, ArbolCompleto) = CrearVers(op);
                            CrearNodoListaEnlazada(cadena, op, ArbolCompleto);
                            op = Console.ReadLine();
                        }
                        else if (op.Equals("read"))
                        {
                            //Método encargado de leer el contenido del archivo
                            Console.Write(Global<string>.nuevoPath + "\\");
                            nombreAr = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine(Global<string>.manejoAr.LeerArchivo(nombreAr));//Método que lee el contenido del archivo
                            Console.ForegroundColor = ConsoleColor.White;
                            op = Console.ReadLine();
                        }
                        else if (op.Equals("dir"))
                        {
                            //Caso para ingresar al directorio del sistema, es un menú de ayuda
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Opciones();
                            Console.Write(codSys);
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            op = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (op.Contains("search"))
                        {
                            //Caso para realizar una busqueda de Versiones, el usuario tendrá la oportunidad de buscar al versión que desee y 
                            //recibir por consola la información completa de la versión
                            try
                            {
                                string version;
                                Console.ForegroundColor = ConsoleColor.White;
                                string nuevaLista = "";
                                string contenidoLista = "";
                                string[] ArrayContenido = null;
                                string nuevocontenidoLista = "";
                                string[] AuxiliarArrayContenido = null;
                                string AuxiliarnuevocontenidoLista = "";
                                (nuevaLista, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(op.Substring(7));//Llamada al método que realiza la busqueda
                                int j = 0;
                                int k = 0;
                                int l = 0;
                                /*For para arreglar el formato del contenido almacenado en el árbol*/
                                ArrayContenido = contenidoLista.Split('|');
                                for (k = 0; k < ArrayContenido.Length-2; k++)
                                {
                                    AuxiliarArrayContenido = ArrayContenido[k].Split('%');
                                    for (l = 0; l < 4; l++)
                                    {
                                        
                                        nuevocontenidoLista = nuevocontenidoLista + AuxiliarArrayContenido[l]+"\n\t\t";
                                    }
                                }
                                //El if y for ayudarán a imprimir los datos de la versión obtenida por el método BusquedaVersion
                                if (nuevaLista != null)
                                {
                                    string[] nuevoArreglo = nuevaLista.Split('%');
                                    for (j = 0; j < 1; j++)
                                    {
                                        Repositorio ultimaVersion = new Repositorio(nuevoArreglo[0], nuevoArreglo[1], nuevoArreglo[2], nuevoArreglo[3], "/");
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
                                    Console.WriteLine("LA VERSIÓN NO EXISTE");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                op = Console.ReadLine();
                            }
                            catch
                            {
                                Console.WriteLine(codSys + "La ruta de acceso no es valida");
                                op = Console.ReadLine();
                            }
                        }

                        else if (op.Equals("binnacle"))
                        {
                            //Con este caso se imprime por consola la información de las Versiones, siguiendo las especificación del
                            //requerimiento
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Global<object>.manejoAr.recorre(); //Llamada al método Recorrer, este recorre la lista enlazada
                            Console.ForegroundColor = ConsoleColor.White;
                            op = Console.ReadLine();
                        }
                        else if (op.Contains("delete"))
                        {
                            //Caso para Eliminar una versión, el usuario tendrá la libertad de eliminar todas las versiones que desee
                            string eliminar;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(Global<string>.nuevoPath + "\\" + "Ingrese la versión que desea eliminar\\");
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            eliminar = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Global<int>.manejoAr.eliminarNodo(Global<int>.manejoAr.obtenerIndice(eliminar) - 1);//Llamada al método ElminarNodo
                            op = Console.ReadLine();
                            break;
                        }
                        else if (op.Contains("show tree view"))
                        {
                            Application.EnableVisualStyles();
                            /*Application.SetCompatibleTextRenderingDefault(false);*/
                            Application.Run(new Form1());
                            op = Console.ReadLine();
                        }
                        else
                        {
                            if (op.Equals("exit"))
                            {
                                break;
                            }
                            //Cuando no ingrese ninguna opcion validad el usuario se repetirá el menú
                            Console.Write(codSys + "\\La opción no existe, consulte --dir-- para ayuda\n");
                            /*Console.Write("error de comando");*/
                        }
                    } while (op != "exit");
                }
            
            } while (repetir);
        }

        //Método que almacena el menú
        public static void Opciones()
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
        public static bool CreacionDirectorio(string pathUsuario, string nombreCarpeta, string codSys)
        {

            bool repetir = false;
            Global<string>.folderParh = pathUsuario;
            if (Directory.Exists(pathUsuario)&&(!pathUsuario.Equals("")))
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(Directory.CreateDirectory(Global<string>.folderParh + "\\" + nombreCarpeta + "\\"));
                    Global<string>._pathTexto = (Global<string>.folderParh + "\\" + nombreCarpeta + "\\");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(codSys + "Se creó ruta de acceso");
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

        public static void CrearArchivosEnDirectorio(string crearArchivo, string codSys)
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
                    Console.WriteLine(codSys + "Archivo Creado");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(codSys + "El archivo que intenta crear no es correcto");
            }
        }

        public static (string cadena, Nodos<Object> ArbolCompleto) CrearVers(string nombreVresion)
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
            Func<Object, Object, bool> MenorQueEntero = (x, y) => Convert.ToUInt64(x.ToString()) < Convert.ToUInt64(y.ToString());
            Func<Object, Object, bool> MayorQueEntero = (x, y) => Convert.ToUInt64(x.ToString()) > Convert.ToUInt64(y.ToString());
            /*lista = Directory.GetFileSystemEntries(Global<string>._pathTexto);*/
            DirectoryInfo archivos = new DirectoryInfo(Global<string>._pathTexto);
            FileInfo[] pesoArchivo = archivos.GetFiles();
            tamañoDirec = (Global<string>._pathTexto.Length);

            Repositorio repositorio;
            for (i = 0; i < pesoArchivo.Length; i++)
            {
                /*nombreArchivo = lista[i].Substring(tamañoDirec).ToString();*/
                nombreArchivo = pesoArchivo[i].ToString();
                peso = pesoArchivo[i].Length.ToString();
                cadena = ConvertirCadena(nombreArchivo.ToString());
                nueva = ulong.Parse(cadena.Substring((cadena.Length)/3, (cadena.Length) / 3));
                repositorio = new Repositorio(nombreArchivo, cadena, peso, cadena);
                _ = Global<Object>.nodoArbol.Insertar(new Repositorio(nueva), repositorio, MenorQueEntero, MayorQueEntero);
                nuevaCadena = nuevaCadena+repositorio.ToString();
            }

            /*Global.manejoAr.agregarVersion((new Repositorio(nombreVresion.Substring(11), contenido)), Global.arbol.Insertar(45, MenorQueEntero, MayorQueEntero), "ver")*/;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.White;
            return (nuevaCadena, Global<Object>.nodoArbol.RaizRepositorio);
        }

        private static void CrearNodoListaEnlazada(string contenidoCadena,string nombreVers, Nodos<object> ArbolCompleto)
        {
            /*
                Console.ForegroundColor = ConsoleColor.DarkRed;
                contenido = Convert.ToString(Global.manejoAr.LeerArchivo(Global.NombreArch));//Se extrae el contenido del TXT modificado o no
                Console.ForegroundColor = ConsoleColor.White;*/
            /*Se agregó este nuevo bloque de if para validar si se almacenará o no un nodo*/
            if (!Global<bool>.manejoAr.validarNodos())
            {
                /*//Si la Lista esta Llena, se crea un nuevo nodo para preparar la comparación de ambos contenidos, TXT y Versión anterio
                string lista = Global<string>.manejoAr.recorredeapoyo();
                string contenidoanterior = "";
                int i = 0;
                 //Con este for extraemos el contenido que deseamos evaluar de la última versión almacenada en la lista enlazada
                 for (i = 0; i < 1; i++)
                {
                    string[] nuevoRepositorio = lista.Split('%');
                    Repositorio ultimaVersion = new Repositorio(null, null, nuevoRepositorio[2], nuevoRepositorio[3], nuevoRepositorio[4]);
                    contenidoanterior = ultimaVersion.contenido.ToString();
                }
                    //Con él if, logramos comparar el contenido de la versión anterior, con el contenido extraido del TXT
                    if (!Global<bool>.manejoAr.CompararContenido(contenido, contenidoanterior.Substring(11)))
                    {
                        //Si los contenidos son distintos, se procede a crear una Nueva versión
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(Global<string>.nuevoPath + "\\" + "En el archivo txt existe una modificación, se crea una nueva versión, por favor presione Enter");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(Global<string>.nuevoPath + "\\" + "Ingrese un comentario para el repositorio\\");
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        comentario = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.White;
                        Global<object>.manejoAr.agregarVersion(new Repositorio(comentario, contenido));
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(Global<string>.nuevoPath + "\\" + "Se actualiza el nodo exitosamente");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        //Si los contenidos son iguales, no es necesario crear una versión nueva
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("El txt no sufrio ninguna modificación");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                */
            }
            else
            {
                //Si la Lista enlazada se encuentra vacía, se procede a crear un Nodo Cabeza
                Global<object>.manejoAr.agregarVersion(new Repositorio(nombreVers.Substring(11), contenidoCadena), ArbolCompleto);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(Global<string>.nuevoPath + "\\" + "Se almacenó el nodo exitosamente");
                Console.ForegroundColor = ConsoleColor.White;

            }
                           
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

    } 

}
