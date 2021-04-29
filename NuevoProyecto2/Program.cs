﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            string comentario;
            string contenido;
            string nombreAr;
            string inicializar;
            bool repetir = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(codSys);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                inicializar = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if ((inicializar.Contains("init")) == true)
                {
                    //Método para crear el Directorio
                    CreacionDirectorio(inicializar.Substring(5), codSys);
                    Console.Write(codSys);
                    op = Console.ReadLine();
                    while (op != "exit")
                    {

                        switch (op)

                        {
                            case "create":
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                contenido = Convert.ToString(Global.manejoAr.LeerArchivo(Global.NombreArch));//Se extrae el contenido del TXT modificado o no
                                Console.ForegroundColor = ConsoleColor.White;

                                /*Se agregó este nuevo bloque de if para validar si se almacenará o no un nodo*/
                                if (!Global.manejoAr.validarNodos())
                                {
                                    //Si la Lista esta Llena, se crea un nuevo nodo para preparar la comparación de ambos contenidos, TXT y Versión anterio
                                    string lista = Global.manejoAr.recorredeapoyo();
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
                                    if (!Global.manejoAr.CompararContenido(contenido, contenidoanterior.Substring(11)))
                                    {
                                        //Si los contenidos son distintos, se procede a crear una Nueva versión
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        Console.WriteLine(Global.nuevoPath + "\\" + "En el archivo txt existe una modificación, se crea una nueva versión, por favor presione Enter");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.ReadLine();
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.Write(Global.nuevoPath + "\\" + "Ingrese un comentario para el repositorio\\");
                                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                        comentario = Console.ReadLine();
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Global.manejoAr.agregarVersion(new Repositorio(comentario, contenido));
                                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                                        Console.WriteLine(Global.nuevoPath + "\\" + "Se actualiza el nodo exitosamente");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    else
                                    {
                                        //Si los contenidos son iguales, no es necesario crear una versión nueva
                                        Console.ForegroundColor = ConsoleColor.DarkRed;
                                        Console.WriteLine("El txt no sufrio ninguna modificación");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }

                                }
                                else
                                {
                                    //Si la Lista enlazada se encuentra vacía, se procede a crear un Nodo Cabeza
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.Write(Global.nuevoPath + "\\" + "Ingrese un comentario para el repositorio\\");
                                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                                    comentario = Console.ReadLine();
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Global.manejoAr.agregarVersion(new Repositorio(comentario, contenido));
                                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                                    Console.WriteLine(Global.nuevoPath + "\\" + "Se almacenó el nodo exitosamente");
                                    Console.ForegroundColor = ConsoleColor.White;

                                }
                                op = Console.ReadLine();
                                break;

                            case "read":
                                //Método encargado de leer el contenido del archivo
                                Console.Write(Global.nuevoPath + "\\");
                                nombreAr = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                Console.WriteLine(Global.manejoAr.LeerArchivo(nombreAr));//Método que lee el contenido del archivo
                                Console.ForegroundColor = ConsoleColor.White;
                                op = Console.ReadLine();
                                break;
                            case "dir":
                                //Caso para ingresar al directorio del sistema, es un menú de ayuda
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                Opciones();
                                Console.Write(codSys);
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                op = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            case "search":
                                //Caso para realizar una busqueda de Versiones, el usuario tendrá la oportunidad de buscar al versión que desee y 
                                //recibir por consola la información completa de la versión
                                string version;
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(Global.nuevoPath + "\\" + "Ingrese la versión que le interesa buscar\\");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                version = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                string nuevaLista = Global.manejoAr.BusquedaVersion(version);//Llamada al método que realiza la busqueda
                                int j = 0;
                                //El if y for ayudarán a imprimir los datos de la versión obtenida por el método BusquedaVersion
                                if (nuevaLista != null)
                                {
                                    string[] nuevoArreglo = nuevaLista.Split('%');
                                    for (j = 0; j < 1; j++)
                                    {
                                        Repositorio ultimaVersion = new Repositorio(nuevoArreglo[0], nuevoArreglo[1], nuevoArreglo[2], nuevoArreglo[3], nuevoArreglo[4]);
                                        if (nuevoArreglo[4].Substring(8).Equals("1"))
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                                            Console.WriteLine("\t" + ultimaVersion.contadorauxiliar.ToString());
                                            Console.WriteLine("\t" + ultimaVersion.fechaapoyo.ToString());
                                            Console.WriteLine("\t" + ultimaVersion.comentario.ToString());
                                            Console.WriteLine("\t" + ultimaVersion.contenido.ToString());
                                            Console.ForegroundColor = ConsoleColor.White;
                                            StreamWriter escribirTXT = new StreamWriter(Global.nuevoPath);
                                            escribirTXT.Write(ultimaVersion.contenido.ToString().Substring(12));
                                            escribirTXT.Close();

                                        }
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
                                break;
                            case "binnacle":
                                //Con este caso se imprime por consola la información de las Versiones, siguiendo las especificación del
                                //requerimiento
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Global.manejoAr.recorre(); //Llamada al método Recorrer, este recorre la lista enlazada
                                Console.ForegroundColor = ConsoleColor.White;
                                op = Console.ReadLine();
                                break;
                            case "delete":
                                //Caso para Eliminar una versión, el usuario tendrá la libertad de eliminar todas las versiones que desee
                                string eliminar;
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.Write(Global.nuevoPath + "\\" + "Ingrese la versión que desea eliminar\\");
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                version = Console.ReadLine();
                                Console.ForegroundColor = ConsoleColor.White;
                                Global.manejoAr.eliminarNodo(Global.manejoAr.obtenerIndice(version) - 1);//Llamada al método ElminarNodo
                                op = Console.ReadLine();
                                break;

                            default:
                                //Cuando no ingrese ninguna opcion validad el usuario se repetirá el menú
                                Console.ForegroundColor = ConsoleColor.White;
                                /*Console.Write("error de comando");*/
                                Console.Write(codSys);
                                op = Console.ReadLine();

                                break;

                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(codSys);
                    Console.Write("Incialice el programa 'init'\n");
                    repetir = true;
                    Console.ForegroundColor = ConsoleColor.White;
                }
            } while (repetir);
        }

        //Método que almacena el menú
        public static void Opciones()
        {
            Console.WriteLine("");
            Console.WriteLine("search:      Buscar un Repositorio");
            Console.WriteLine("create:      Crea un Repositorio");
            Console.WriteLine("binnacle:    Bitacora de Registros del Repositorio");
            Console.WriteLine("delete:      Borra un Registro");
            Console.WriteLine("read:        Lee la version actual");
        }

        //Método para crear directorio
        public static void CreacionDirectorio(string pathUsuario, string codSys)
        {


            Global.folderParh = pathUsuario;
            if (Directory.Exists(pathUsuario))
            {
                try
                {
                    //Si la ruta de acceso existe, se da la opción para eliminar datos o utilizar los mismos
                    Console.WriteLine(codSys + "Se accedio a la siguiente ruta de acceso \n");
                    Console.WriteLine(Global.folderParh + "\\");
                    /*//se almacena el contenido del directorio en un Array para posteriormente recorrer
                    string[] lita = new string[10];
                    lita = Directory.GetFiles(Global.folderParh);
                    int i = 0;
                    for (i = 0; i < lita.Length; i++)
                    {
                        Console.WriteLine(lita[i].ToString() + "\n");
                    }*/

                    Console.Write(codSys + "Ingrese nombre de la carpeta\\");
                    string carpeta = Console.ReadLine();
                    Console.WriteLine(codSys + "Se creó la siguiente ruta de acceso");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(Directory.CreateDirectory(Global.folderParh + "\\" + carpeta + "\\"));
                    Global._pathTexto = (Global.folderParh + "\\" + carpeta + "\\");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch
                {
                    Console.WriteLine(codSys + "La ruta de acceso no es valida");
                }
                try
                {
                    //Crea nuevos elementos dentro del Directorio, el usuario colocará el mismo para Crear el archivo
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(codSys + "Ingrese el nombre del txt que desea crear\\");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Global.NombreArch = Console.ReadLine() + ".txt";
                    Global.nuevoPath = Global._pathTexto + Global.NombreArch;
                    StreamWriter sw = new StreamWriter(Global.nuevoPath, true);
                    sw.Close();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(codSys + "Archivo Creado");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine(codSys + "No se ha encontrado el archivo");
                }

            }
            else
            {
                Console.WriteLine(codSys + "La ruta de acceso no es valida");
            }



            /*
            //se evalua si existe el directorio
            if (!Directory.Exists(Global.folderParh))
            {
                //Sino existe, se crea
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Directory.CreateDirectory(Global.folderParh);
                Console.WriteLine("Se accedio a la siguiente ruta de acceso");
                Console.WriteLine(Global.folderParh + "\\");
                Console.WriteLine("Ingrese nombre de la carpeta");
                string carpeta = Console.ReadLine();
                Console.WriteLine("Se creó la siguiente ruta de acceso");
                Console.WriteLine(Global.folderParh + "\\" + carpeta + "\\");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else
            {
                //Si la ruta de acceso existe, se da la opción para eliminar datos o utilizar los mismos
                Console.WriteLine("Se accedio a la siguiente ruta de acceso y contiene los siguiente datos\n");
                Console.WriteLine(Global.folderParh + "\\");
                //se almacena el contenido del directorio en un Array para posteriormente recorrer
                string[] lita = new string[10];
                lita = Directory.GetFiles(Global.folderParh);
                int i = 0;
                for (i = 0; i < lita.Length; i++)
                {
                    Console.WriteLine(lita[i].ToString() + "\n");
                }

                Console.WriteLine("Ingrese nombre de la carpeta");
                string carpeta = Console.ReadLine();
                Console.WriteLine("Se creó la siguiente ruta de acceso");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(Directory.CreateDirectory(Global.folderParh + "\\" + carpeta + "\\"));
                Console.ForegroundColor = ConsoleColor.White;

                /*bool repetir;
                //Pregunta para eliminación
                do
                {
                    repetir = false;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Se recomienda eliminar la ruta anterior para crear una nueva y evitar problemas de compatibilidad, desea elminar? (si/no)");
                    Console.ForegroundColor = ConsoleColor.White;
                    string eliminar = Console.ReadLine();
                    string patheliminar;

                    switch (eliminar)
                    {
                        case "si":
                            //Si él usuario indica que si, se elmina el contenido del Directorio
                            for (i = 0; i < lita.Length; i++)
                            {
                                File.Delete(lita[i].ToString());
                            }
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            //Se elimina el Directorio completo
                            Directory.Delete(Global.folderParh);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("La ruta fue elminada con exito, presione enter para crear una nueva");
                            string espera = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            //Se crea nuevo Directorio
                            Directory.CreateDirectory(Global.folderParh);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Se creó la siguiente ruta de acceso");
                            Console.WriteLine(Global.folderParh + "\\");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "no":
                            //Si el usuario no quiere eliminar el Directorio, se utilizará el mismo
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("La ruta de acceso que se utilizará es");
                            Console.WriteLine(Global.folderParh + "\\");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        default:
                            Console.WriteLine("La opción ingresada no es valida");
                            repetir = true;
                            break;
                    }
                } while (repetir);
            }*/

        }
    }

}