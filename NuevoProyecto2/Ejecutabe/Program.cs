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
            string contenido;
            string nombreAr;
            string inicializar;
            bool repetir = false;
            string nombreCarpeta = "";

            do
            {
                repetir = false;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(Global<string>.codSys);
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                inicializar = Console.ReadLine();
                Console.Write("\\");
                nombreCarpeta = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                if ((inicializar.Contains("init")) == false)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(Global<string>.codSys);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Incialice el programa 'init'\n");
                    repetir = true;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    try
                    {
                        repetir = Global<bool>.MT.CreacionDirectorio(inicializar.Substring(5), nombreCarpeta, Global<string>.codSys);
                    }
                    catch (ArgumentOutOfRangeException t)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write(Global<string>.codSys + "Coloque una ruta valida\n");
                        Console.ForegroundColor = ConsoleColor.White;
                        repetir = true;
                    }
                    //Método para crear el Directorio
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(Global<string>._pathTexto+"\\");
                        Console.ForegroundColor = ConsoleColor.White;
                        op = Console.ReadLine();

                        if (op.Contains("create file"))
                        {
                            Global<object>.MT.CrearArchivosEnDirectorio(op, Global<string>.codSys,"");

                        } else if (op.Contains("create ver"))
                        {

                            string cadena = "";
                            Global<bool>.nodoArbol.EliminarElContenidoArbol();
                            Nodos<Object> ArbolCompleto = new Nodos<Object>();
                            (cadena, ArbolCompleto) = Global<object>.MT.CrearVers(op,"crear");
                            Global<object>.MT.CrearNodoListaEnlazada(cadena, op, ArbolCompleto);

                        }
                        else if (op.Equals("read"))
                        {
                            //Método encargado de leer el contenido del archivo
                            Console.Write(Global<string>._pathTexto + "\\");
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
                            Global<string>.MT.Opciones();

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
                                if (contenidoLista != null)
                                {
                                    ArrayContenido = contenidoLista.Split('|');
                                }
                                else
                                {
                                    throw new NullReferenceException();
                                }
                                for (k = 0; k < ArrayContenido.Length - 2; k++)
                                {

                                    AuxiliarArrayContenido = ArrayContenido[k].Split('%');
                                    for (l = 0; l < 4; l++)
                                    {
                                        nuevocontenidoLista = nuevocontenidoLista + AuxiliarArrayContenido[l] + "\n\t\t";
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
                                    Console.WriteLine(Global<string>._pathTexto + "\\" + "LA VERSIÓN NO EXISTE");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine(Global<string>._pathTexto + "\\" + "La versión no existe");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }

                        else if (op.Equals("binnacle"))
                        {
                            //Con este caso se imprime por consola la información de las Versiones, siguiendo las especificación del
                            //requerimiento
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Global<object>.manejoAr.RecorreListaVersiones(); //Llamada al método Recorrer, este recorre la lista enlazada
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (op.Contains("delete"))
                        {
                            //Caso para Eliminar una versión, el usuario tendrá la libertad de eliminar todas las versiones que desee
                            string eliminar;
                            Console.ForegroundColor = ConsoleColor.White;
                            Global<object>.manejoAr.EliminaNodoVersiones(Global<object>.manejoAr.ObtenerIndiceVersiones(op.Substring(7)) - 1);//Llamada al método ElminarNodo

                        }
                        else if (op.Contains("show tree view"))
                        {
                            int cantidad, i, j;
                            string numerobuscar = op.Substring(15);
                            string nombreArchivoContenidVersion, contenidoLista, VersConte;
                            string[] AuxiliarArrayContenido;
                            (nombreArchivoContenidVersion, contenidoLista) = Global<object>.manejoAr.BusquedaVersion(numerobuscar);
                            (cantidad, VersConte) = Global<object>.MT.DevuelveCantidadArchivosVersion(contenidoLista);
                            string[] eliminar;
                            eliminar = Directory.GetFiles(Global<string>._pathTexto);
                            for (j = 0; j < eliminar.Length; j++)
                            {
                                File.Delete(eliminar[j].ToString());
                            }

                            AuxiliarArrayContenido = VersConte.Split('%');
                            for (i = 0; i < AuxiliarArrayContenido.Length-1; i++)
                            {
                                Global<object>.MT.CrearArchivosEnDirectorio(op, Global<string>.codSys, AuxiliarArrayContenido[i].Substring(16));
                            }
                            string cadenaSinUsar;
                            Global<bool>.nodoArbol.EliminarElContenidoArbol();
                            Nodos<object> ArbolCompleto = new Nodos<object>();
                            (cadenaSinUsar, ArbolCompleto) = Global<object>.MT.CrearVers(op,"");


                            Form1 formulario = new Form1();
                            formulario.DevuelveVersion(numerobuscar);
                            Application.EnableVisualStyles();
                            /*Application.SetCompatibleTextRenderingDefault(false);*/
                            Application.Run(new Form1());
                        }
                        else
                        {
                            if (op.Equals("exit"))
                            {
                                break;
                            }
                            //Cuando no ingrese ninguna opcion validad el usuario se repetirá el menú
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(Global<string>._pathTexto + "\\" + "\\La opción no existe, consulte --dir-- para ayuda\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            /*Console.Write("error de comando");*/
                        }
                    } while (op != "exit");
                }
            
            } while (repetir);
        }
    } 

}
