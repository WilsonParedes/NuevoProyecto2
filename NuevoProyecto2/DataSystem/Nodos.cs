using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NuevoProyecto2
{
    class Nodos<T>
    {
        //Se crean los Nodos necesarios para el buen funcionamiento de la Lista
        private NodoVersiones<T> actual { get; set; }
        private NodoVersiones<T> primero { get; set; }
        private NodoVersiones<T> anterior { get; set; }
        private NodoVersiones<T> enlace { get; set; }
        private NodoArbol<T> raiz { get; set; }
        public NodoArbol<T> RaizRepositorio { get; set; }

        public Nodos()
        {
            primero = null;
            actual = null;
            anterior = null;
            raiz = null;
            enlace = null;

        }

        public string pathDirectorio()
        {
            return Global<string>._path;
        }

        

        /*Función que valida si la lista se encuentra vacia, con el fin de determinar si se debe o no
         crear como cabeza de la lista*/
        public bool validarNodosVersiones()
        {
            bool NodoVacio;
            if (primero == null)
            {
                return NodoVacio = true;
            }
            else
            {
                return NodoVacio = false;
            }
        }


        //Método encargado de crear Nodos en la cabeza de la Lista
        public void agregarVersion(T version, Nodos<T> ArbolCompleto)
        {
            NodoVersiones<T> nuevaVersion = new NodoVersiones<T>(version);
            nuevaVersion.siguiente = primero;
            primero = nuevaVersion;
            enlace = (NodoVersiones<T>)ArbolCompleto;
            
        }

        internal NodoArbol<T> Insertar(T valor, T repositorio, Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            (raiz, RaizRepositorio) = Insertar(raiz, RaizRepositorio, valor, repositorio, MenorQue, MayorQue);
            return RaizRepositorio;
        }


        public (NodoArbol<T> numero, NodoArbol<T> datoNodo) Insertar(NodoArbol<T> raizSub, NodoArbol<T> reposi, T valor, T repositorio,
            Func<T, T, bool> MenorQue, Func<T, T, bool> MayorQue)
        {
            if (raizSub == null)
            {
                raizSub = new NodoArbol<T>
                { data = valor, izq = null, der = null };

                reposi = new NodoArbol<T>
                { data = repositorio, izq = null, der = null };
            }
            else if (MenorQue(valor, raizSub.data))
            {
                (raizSub.izq, reposi.izq) = Insertar(raizSub.izq, reposi.izq, valor, repositorio, MenorQue, MayorQue);
            }
            else if (MayorQue(valor, raizSub.data))
            {
                (raizSub.der, reposi.der) = Insertar(raizSub.der, reposi.der, valor, repositorio, MenorQue, MayorQue);
            }
            else throw new Exception("Nodo duplicado");


            return (raizSub, reposi);
        }


        //Método que servirá para leer el contendio del txt
        public string LeerArchivo(string nombretxt)
        {
            string line;
            string conten = "";
            try
            {
                StreamReader sr = new StreamReader(Global<string>._path + nombretxt);
                line = sr.ReadLine();
                while (line != null)
                {
                    conten = conten + "\n" + line;
                    line = sr.ReadLine();
                }
                sr.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exeption: " + e.Message);
            }
            return conten;
        }


        //Método encargado de recorrer e imprimir la Lista enlazada, según el formato requerito en el documento
        public void RecorreListaVersiones()
        {
            actual = primero;
            string lista = "";
            Console.WriteLine("\t\t\t\tDATOS ALMACENADOS EN LA BITACORA\n");
            Console.WriteLine("\t\t\tVersion No.\tFecha y Hora\t\tComentario\n");
            string[] nuevoRepositorio;
            Repositorio ultimaVersion;
            while (actual != null)
            {
                lista = actual.dato.ToString();
                nuevoRepositorio = lista.Split('%');
                ultimaVersion = new Repositorio(nuevoRepositorio[0], nuevoRepositorio[1], nuevoRepositorio[2], nuevoRepositorio[3], nuevoRepositorio[4]);
                
                    Console.WriteLine("\t\t\t\t" + ultimaVersion.contadorauxiliar.ToString().Substring(14) + "\t" + ultimaVersion.fechaapoyo.ToString().Substring(7) +
                        "\t" + ultimaVersion.comentario.ToString().Substring(12) + "\n");
                    actual = actual.siguiente;
            }
        }


        //Este método servirá de apoyo para devolver unicamente el primer nodo de la lista
        public string recorredeapoyo()
        {

            actual = primero;
            string nuevaLista = "";


            while (actual != null)
            {
                nuevaLista = actual.dato.ToString();
                actual = null;
            }

            return nuevaLista;

        }


        //Este método servirá para comparar el contenido de la última versión, y el contenido del txt
        //por lo que necesita de dos parametros, contenido txt y contenido version ultima
        public bool CompararContenido(string contenidonuevo, string contenido_anterior)
        {

            var comparar = false;

            if (contenidonuevo.Equals(contenido_anterior))
            {
                comparar = true;
            }

            return comparar;

        }


        //Función que sirve para llevar un control de las versiones que han sido almacenadas, con el fin de llevar
        //un orden cronologico de las Versiones en la Lista enlazada
        public int DevueveCorrelativoVersion()
        {

            actual = primero;
            string lista = "";
            string contenerVersion = "";
            string[] nuevoRepositorio = null;
            int contador = 0;
            while (actual != null)
            {
                int i = 0;
                for (i = 0; i < 1; i++)
                {
                    lista = actual.dato.ToString();
                    nuevoRepositorio = lista.Split('%');
                    Repositorio busquedaVersion = new Repositorio(nuevoRepositorio[0], null, null, null, null);
                    contenerVersion = busquedaVersion.contadorauxiliar.Substring(14);
                    if (contenerVersion.Equals(""))
                    {
                        contador = 0;
                    }
                    else
                    {
                        contador = Int32.Parse(contenerVersion);
                    }
                }
                actual = null;
            }
            return contador;
        }


        //Esta función servirá para buscar una versión, tomar en cuenta que recibe un parametro, ya que no se buscará
        //por index sino que por contenido
        public (string lista, string contenido) BusquedaVersion(string version)
        {
            actual = primero;
            string listNuevoRepositorio,ListRepositorioCompleto, contenerVersion, ListRepositorioArbol = "";
            string[] repositorioCompleto, nuevoRepositorio, RepositorioArbol = null;
            string contenido = "";
            while (actual != null)
            {
                //Se recorre hasta llegar al nodo indico para esto se recorre todo el contenido del nodo, a modo de
                //obtener el numero de la versión y luego de que coincida, retornar en modo de lista, la información
                //Del nodo
                int i, j, k, l = 0;
                for(i=0; i<1; i++)
                {
                    ListRepositorioCompleto = actual.dato.ToString();
                    repositorioCompleto = ListRepositorioCompleto.Split('(');
                    for (j = 0; j < 1; j++)
                    {
                        listNuevoRepositorio = repositorioCompleto[0].ToString();
                        nuevoRepositorio = listNuevoRepositorio.Split('%');
                        Repositorio busquedaVersion = new Repositorio(nuevoRepositorio[0], null, null, null, null);
                        contenerVersion = busquedaVersion.contadorauxiliar.Substring(14);
                        if (contenerVersion.Equals(version))
                        {
                            for(k=1;k< repositorioCompleto.Length; k++)
                            {
                                
                                contenido = contenido + repositorioCompleto[k]+"\n"+"|";
                            }
                            return (listNuevoRepositorio,contenido);
                            break;
                        }
                    }
                    actual = actual.siguiente;
                }
            }
            return (null,null);
        }

        //Se utiliza esta función, para llevar el conteo de un nodo para posteriormete buscarlo por index en la lista 
        //enlazada
        public int ObtenerIndiceVersiones(string version)
        {
            actual = primero;
            string lista = "";
            string contenerVersion = "";
            string[] nuevoRepositorio = null;
            bool encontrado = false;
            int contador = 0;

            while (actual != null && !encontrado)
            {
                //se recorre cada nodo con la finalidad de llegar hasta la versión deseada, y luego con la ayuda
                //de un contador se extrae la posición, por interación, este servirá de pivote para realizar la elmiminación de nodos
                int i = 0;
                lista = actual.dato.ToString();
                for (i = 0; i < 1; i++)
                {
                    nuevoRepositorio = lista.Split('%');
                    Repositorio busquedaVersion = new Repositorio(nuevoRepositorio[0], nuevoRepositorio[1], nuevoRepositorio[2], nuevoRepositorio[3], nuevoRepositorio[4]);
                    contenerVersion = busquedaVersion.contadorauxiliar.Substring(14);
                    if (contenerVersion.Equals(version))
                    {
                        encontrado = true;

                    }

                }
                contador = contador + 1;
                actual = actual.siguiente;
            }

            if (!encontrado)
            {
                return contador = 0;
            }
            else
            {
                return contador;
            }

        }

        //Esta función realiza la elminación fisica del nodo, recibe por parametros el index extraido por la 
        //Función obtenerIndice
        public void EliminaNodoVersiones(int index)
        {
            if (index < 0)
            {
                Console.WriteLine(Global<string>._pathTexto + "\\"+"La versión no existe");
            }
            else
            {
                //Se compara si el nodo es la cabeza, y se quita el enlace para pasarlo al dato siguiente de la lista
                if (index == 0)
                {
                    primero = primero.siguiente;
                    Console.WriteLine(Global<string>._pathTexto + "\\"+"Registro eliminado con éxito");
                }
                else
                {
                    //si el nodo no es la cabeza, se realiza una interación del index recibido y la lista, con el fin
                    //de llegar a un nodo anterior al index recibido
                    int contador = 0;
                    NodoVersiones<T> temporal = primero;
                    while (contador < index - 1)
                    {
                        temporal = temporal.siguiente;
                        contador++;
                    }
                    //luego de asignar a temporal el nodo anterior al index recibido, se realiza el enlace
                    //al nodo siguiente del siguiente, ingnorando de esta forma el nodo que esta en la posición del index recibido
                    temporal.siguiente = temporal.siguiente.siguiente;
                    Console.WriteLine(Global<string>._pathTexto + "\\"+"Registro eliminado con éxito");

                }
            }


        }

        /*Función que realiza la creación de un Arbol*/

        

        public void EliminarElContenidoArbol()
        {
            Global<object>.nodoArbol.raiz = null;
            Global<object>.nodoArbol.RaizRepositorio = null;

        }


        public string convertirX(Object x)
        {
            return x.ToString();
             
        }


        


    }
}
