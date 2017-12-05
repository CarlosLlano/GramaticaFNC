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
    public partial class VentanaPrincipal : Form
    {
        private Gramatica g;
        private VentanaCYK ventanaCYK;
        public VentanaPrincipal()
        {
            InitializeComponent();

            txtSalida.ReadOnly = true;

            butPaso2.Enabled = false;
            butPaso3.Enabled = false;
            butPaso4.Enabled = false;
            butPaso5.Enabled = false;
            butPaso6.Enabled = false;
            butCYK.Enabled = false;

            txtNoTerminables.ReadOnly = true;
            txtNoAlcanzables.ReadOnly = true;
            txtAnulables.ReadOnly = true;
            txtConjUnitario.ReadOnly = true;

        }

        //Al presionar este boton se carga la gramatica
        private void butPaso1_Click(object sender, EventArgs e)
        {
            if(txtEntrada.Text.Trim().Count() > 0)
            {
                try
                {
                    g = new Gramatica(txtEntrada.Text);
                    List<char> noTerminables = g.darNoTerminables();

                    string cadena = "ninguna";
                    if (noTerminables.Count() > 0)
                    {
                        cadena = "";
                        for (int i = 0; i < noTerminables.Count(); i++)
                        {
                            char noTerminable = noTerminables.ElementAt(i);

                            cadena = cadena + noTerminable + " ";
                        }
                    }

                    txtNoTerminables.Text = cadena;
                    g.eliminarNoTerminables();
                    txtSalida.Text = g.ToString();

                    butPaso1.Enabled = false;
                    butPaso2.Enabled = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Debe insertar una gramatica");
            }
            
        }

        private void butPaso2_Click(object sender, EventArgs e)
        {
            //Si llego a este paso es que la gramatica ya esta creada
            List<char> noAlcanzables = g.darNoAlcanzables();

            string cadena = "ninguna";
            if (noAlcanzables.Count() > 0)
            {
                cadena = "";
                for (int i = 0; i < noAlcanzables.Count(); i++)
                {
                    char noAlcanzable = noAlcanzables.ElementAt(i);

                    cadena = cadena + noAlcanzable + " ";
                }
            }

            txtNoAlcanzables.Text = cadena;
            g.eliminarNoAlcanzables();
            txtSalida.Text = g.ToString();

            butPaso2.Enabled = false;
            butPaso3.Enabled = true;
        }

        private void butPaso3_Click(object sender, EventArgs e)
        {
            List<char> anulables = g.darAnulables();

            string cadena = "ninguna";
            if (anulables.Count() > 0)
            {
                cadena = "";
                for (int i = 0; i < anulables.Count(); i++)
                {
                    char anulable = anulables.ElementAt(i);

                    cadena = cadena + anulable + " ";
                }
            }

            txtAnulables.Text = cadena;
            g.eliminarProduccionesLambda();
            txtSalida.Text = g.ToString();

            butPaso3.Enabled = false;
            butPaso4.Enabled = true;

            //agrego elementos al comboBox
            foreach(char variable in g.darGeneradores())
            {
                comboBoxConjunto.Items.Add(variable);
            }
            
        }

        private void comboBoxConjunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            char generador = (Char)comboBoxConjunto.SelectedItem;

            string cadena4 = "";
            List<char> conjunto = g.darConjuntoUnitario(generador);

            for (int i = 0; i < conjunto.Count(); i++)
            {
                cadena4 = cadena4 + conjunto.ElementAt(i) + " ";
            }

            txtConjUnitario.Text = cadena4;
        }

        private void butPaso4_Click(object sender, EventArgs e)
        {
            g.eliminarProduccionesUnitarias();

            txtSalida.Text = g.ToString();

            comboBoxConjunto.Enabled = false;
            butPaso4.Enabled = false;
            butPaso5.Enabled = true;
            
        }

        private void butPaso5_Click(object sender, EventArgs e)
        {
            try
            {
                g.generarVariablesPorCadaTerminal();
                txtSalida.Text = g.ToString();

                butPaso5.Enabled = false;
                butPaso6.Enabled = true;
            }
            catch(Exception)
            {
                //El unico error que se puede producir en esta fase, es que no hayan 
                //suficientes variables para obtener producciones binarias
                MessageBox.Show("No hay suficientes variables en el abecedario para generar " +
                    "producciones binarias con esta gramatica");
                limpiar();      
            }
            
        }

        private void butPaso6_Click(object sender, EventArgs e)
        {
            try
            {
                g.generarProduccionesBinarias();
                txtSalida.Text = g.ToString();

                butPaso6.Enabled = false;
                MessageBox.Show("La gramatica ha sido convertida a su Forma Normal de Chomsky" + Environment.NewLine + 
                                "Ahora puede comprobar si la gramatica genera alguna cadena especifica con el boton Algoritmo CYK");

                butCYK.Enabled = true;
            }
            catch(Exception)
            {
                //El unico error que se puede producir en esta fase, es que no hayan 
                //suficientes variables para obtener producciones binarias
                MessageBox.Show("No hay suficientes variables en el abecedario para generar " +
                    "producciones binarias con esta gramatica");
                limpiar();
            }
           
        }

        private void butLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();

        }

        public void limpiar()
        {
            g = null;
            txtSalida.Clear();
            txtEntrada.Clear();

            butPaso1.Enabled = true;
            butPaso2.Enabled = false;
            butPaso3.Enabled = false;
            butPaso4.Enabled = false;
            butPaso5.Enabled = false;
            butPaso6.Enabled = false;

            txtNoTerminables.Clear();
            txtNoAlcanzables.Clear();
            txtAnulables.Clear();
            txtConjUnitario.Clear();
            comboBoxConjunto.Enabled = true;
            comboBoxConjunto.Items.Clear();

            butCYK.Enabled = false;
        }

        public Gramatica darGramatica()
        {
            return g;
        }

        private void butCYK_Click(object sender, EventArgs e)
        {
            if(ventanaCYK != null)
            {
                ventanaCYK.Close();
            }
            ventanaCYK = new VentanaCYK(this);
            ventanaCYK.Show();
        }
    }
}
