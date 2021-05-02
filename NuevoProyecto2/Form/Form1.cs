using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NuevoProyecto2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            MostrarInOrden(Global<object>.nodoArbol.RaizRepositorio, 0, " ");
        }

        int auxX = 0;
        private void MostrarInOrden(NodoArbol<object> raiz, int auxY, string espacio)
        {
            Graphics nodo;
            nodo = CreateGraphics();

            if (raiz == null)
            {
                Console.Write(" ");
            }
            else
            {
                auxX += 125;
                MostrarInOrden(raiz.izq, auxY + 90, espacio);

                nodo.FillRectangle(Brushes.White, 80 + auxX - auxY, 80 + auxY, 100, 50);//Color, Coordenada, Coordenada Y, Ancho, Alto
                nodo.DrawString(raiz.data.ToString(), Font, Brushes.Black, 90 + auxX - auxY, 90 + auxY);//Cadena, fuente, color, coordenadas x y*/
                Pen myNooRaiz = new Pen(Color.Blue, 3);// CAMBIAR EL BORDE DEL CIRCULO *COLOR
                nodo.DrawRectangle(myNooRaiz, 80 + auxX - auxY, 80 + auxY, 100, 50);
                /*Pen linea1 = new Pen(Color.Black, 2);
                nodo.DrawLine(linea1, 30 + auxX - auxY, 40 + auxY, 10 + auxX - auxY, 50+ auxY);//POSICION EN X, POSICION EN Y
                /*
                Console.SetCursorPosition(1 + auxX - auxY, 1 + auxY);*/
                /*Console.Write(raiz.data)*/
                
                MostrarInOrden(raiz.der, 90 + auxY, espacio);

            }

            /*Console.Write("\n");*/
        }

        public void DevuelveVersion(string opcion)
        {

            MostrarInOrden(Global<object>.nodoArbol.RaizRepositorio, 0, " ");

        }
    }
}
