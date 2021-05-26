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
                        repetir = Global<bool>.MT.CrearDirectorio(inicializar.Substring(5), nombreCarpeta, Global<string>.codSys);
                        Global<object>.MT.CrearArchivosEnDirectorio("create file bitacora.dat", Global<string>.codSys, "", "");
                        Global<object>.conectar();
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

                        if (op.Contains("create file "))
                        {
                            Global<object>.MT.CrearArchivosEnDirectorio(op, Global<string>.codSys,"","");

                        } else if (op.Contains("create ver "))
                        {

                            string cadenaquitar = "";
                            Global<bool>.nodoArbol.EliminarElContenidoArbol();
                            NodoArbol<object> ArbolCompleto = new NodoArbol<object>();
                            ArbolCompleto = Global<object>.MT.CrearVersionenArbol(op,"crear");
                            Global<string>.cadenadevuelvearbol = "";
                            Global<string>.cadenadevuelvearbol = Global<object>.MT.DevuelveCadenadelArbolInOrden(ArbolCompleto);
                            Global<object>.MT.CrearVersionEnListaEnlazada(Global<string>.cadenadevuelvearbol, op, ArbolCompleto);

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
                        else if (op.Contains("search "))
                        {
                            //Caso para realizar una busqueda de Versiones, el usuario tendrá la oportunidad de buscar al versión que desee y 
                            //recibir por consola la información completa de la versión
                            try
                            {
                                Global<object>.MT.OpcionBusqueda(op);
                                
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
                            Global<object>.GB.ExtraerTabla();
                        }
                        else if (op.Contains("delete "))
                        {
                            //Caso para Eliminar una versión, el usuario tendrá la libertad de eliminar todas las versiones que desee
                            string eliminar;
                            Console.ForegroundColor = ConsoleColor.White;
                            Global<object>.manejoAr.EliminaNodoVersiones(Global<object>.manejoAr.ObtenerIndiceVersiones(op.Substring(7)) - 1);//Llamada al método ElminarNodo

                        }
                        else if (op.Contains("show tree view "))
                        {
                            try
                            {
                                string numerobuscar = op.Substring(15);
                                Global<object>.MT.VisualizacionArbolForm(numerobuscar, op);
                                Form1 formulario = new Form1();
                                formulario.DevuelveVersion(numerobuscar);
                                Application.EnableVisualStyles();
                                Application.Run(new Form1());
                            }
                            catch
                            {
                                Console.WriteLine("No existe la versión para visualizar el árbol");
                            }
                            
                            
                            
                        }else if (op.Contains("remove rm "))
                        {
                            Global<object>.MT.RemoverHojadelArbol(Global<object>.manejoAr.DevueveCorrelativoVersion().ToString(), op);

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
