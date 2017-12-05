using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FNC
{
    public partial class VentanaCYK : Form
    {
        VentanaPrincipal principal;
        public VentanaCYK(VentanaPrincipal v)
        {
            InitializeComponent();
            principal = v;
        }

        private void butAplicar_Click(object sender, EventArgs e)
        {
            string cadena = txtCadena.Text.Trim();

            dGVMatriz.Columns.Clear();

            try
            {

                bool respuesta = principal.darGramatica().algoritmoCYK(cadena);
                
                List<string>[,] matriz = principal.darGramatica().matriz;

                //HACER VISIBLE LA MATRIZ
                dGVMatriz.ColumnCount = cadena.Count();

                for(int i = 0; i < cadena.Count(); i++)
                {
                    dGVMatriz.Columns[i].Name = "j = " + i;
                }

               for(int fila = 0; fila < cadena.Count(); fila++)
                {
                    dGVMatriz.Rows.Add();
                    for (int columna = 0; columna < cadena.Count(); columna++)
                    {
                        List<string> valor = matriz[fila, columna];
                        if(valor != null)
                        {
                            string s = "";
                            if(valor.Count() == 0)
                            {
                                s = "{ }";
                            }
                            else
                            {
                                foreach (var elemento in valor)
                                {
                                    s = s + elemento + " ";
                                }
                            }      
                            dGVMatriz.Rows[fila].Cells[columna].Value = s;
                        }    
                    }
                }

                //RESPUESTA
                string resultado = respuesta ? "SÍ" : "NO"; 
                string mensaje = "La gramatica " + resultado + " genera la cadena: " + cadena;
                MessageBox.Show(mensaje);
                
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
