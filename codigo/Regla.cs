using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNC
{
    /// <summary>
    /// Clase que representa una regla de una gramatica
    /// </summary>
    public class Regla
    {
        //CONSTANTES --------------------------------------------------------------

        //Representa el caracter nulo de una gramatica.
        public const char LAMBDA = '&';

        //ATRIBUTOS ---------------------------------------------------------------
        public char generador;
        public List<string> producciones;

        //CONSTRUCTOR -----------------------------------------------------------------
        public Regla(char pgenerador, List<string> pproducciones)
        {
            generador = pgenerador;
            producciones = pproducciones;
        }

        //METODOS ---------------------------------------------------------------------

        /// <summary>
        /// Determina si la regla es terminable por la presencia de una produccion lambda o por una produccion 
        /// con solo terminales
        /// </summary>
        /// <returns>
        /// Retorna true si la regla es terminable por tener una produccion lambda o una produccion
        /// con solo terminales. En caso contrario, retorna false
        /// </returns>
        public bool esTerminablePorProduccion()
        {
            bool respuesta = false;

            for(int i = 0; i < producciones.Count && !respuesta; i++)
            {
                string produccion = producciones.ElementAt(i);

                if (produccion.ElementAt(0).Equals(LAMBDA))
                {
                    respuesta = true;
                }
                else
                {
                    var cadena = produccion.Where(c => Char.IsLower(c));
                    
                    if (cadena.Count() == produccion.Count())
                    {
                        respuesta = true;
                    }
                }
            }
           
            return respuesta;
        }

        /// <summary>
        /// Determina si la regla es terminable por el hecho de tener una produccion con una variable terminable
        /// que la lleva a ser terminable.
        /// </summary>
        /// <param name="terminables">
        /// Lista que contiene todas las variables terminables
        /// </param>
        /// <returns>
        /// Retorna true si la regla es terminable por la presencia de una variable terminable
        /// que la lleva a ser terminable. En caso contrario, retorna false.
        /// </returns>
        public bool esTerminablePorVariableTerminable(List<char> terminables)
        {
            bool respuesta = false;
            
            for (int i = 0; i < producciones.Count && !respuesta; i++)
            {
                string produccion = producciones.ElementAt(i);

                var cadena = produccion.Where(c => Char.IsLower(c) || terminables.Contains(c));

                if (cadena.Count() == produccion.Count())
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Determina las variables alcanzables directas, es decir, aquellas variables que estan en las producciones
        /// </summary>
        /// <returns>
        /// Retorna una lista con todas las variables en las producciones.
        /// </returns>
        public List<char> variablesAlcanzables()
        {
            List<char> variablesAlcanzables = new List<char>();
            
            foreach(string produccion in producciones)
            {
                var lista = produccion.Where(c => Char.IsUpper(c)).ToList<char>();
                variablesAlcanzables = variablesAlcanzables.Union(lista).ToList<char>();
            }

            return variablesAlcanzables;
        }

        /// <summary>
        /// Determina si la regla es anulable por tener una produccion lambda.
        /// </summary>
        /// <returns>
        /// Retorna true si la regla contiene una produccion lambda. En caso contrario, retorna false.
        /// </returns>
        public bool esAnuablePorProduccion()
        {
            bool respuesta = false;

            for(int i = 0; i < producciones.Count() && !respuesta; i++)
            {
                string produccion = producciones.ElementAt(i);
                if(produccion.Equals(LAMBDA.ToString()))
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Determina si la regla es anulable por el hecho de tener una produccion con una variable anulable que 
        /// la lleva a ser anulable
        /// </summary>
        /// <param name="anulables">
        /// Lista con todas las variables anulables
        /// </param>
        /// <returns>
        /// Retorna true en caso de que la regla sea anulable por tener una produccion con una variable anulable que la
        /// lleva a ser anulable. En caso contrario, retorna false.
        /// </returns>
        public bool esAnulablePorVariableAnulable(List<char> anulables)
        {
            bool respuesta = false;

            for (int i = 0; i < producciones.Count && !respuesta; i++)
            {
                string produccion = producciones.ElementAt(i);

                var cadena = produccion.Where(c => anulables.Contains(c));
                if (cadena.Count() == produccion.Count())
                {
                    respuesta = true;
                }

            }

            return respuesta;
        }

        /// <summary>
        /// Obtiene todas las producciones unitarias
        /// </summary>
        /// <returns>
        /// Lista con todas las producciones unitarias.
        /// </returns>
        public List<char> produccionesUnitarias()
        {
            List<char> respuesta = new List<char>();
            
            for(int i = 0; i < producciones.Count(); i++)
            {
                string prod = producciones.ElementAt(i);
                if(prod.Count() == 1)
                {
                    char caracter = prod.ElementAt(0);
                    if(Char.IsUpper(caracter) == true)
                    {
                        respuesta.Add(caracter);
                    }
                }
            }
            return respuesta;
        }

        /// <summary>
        /// metodo utilizado para:
        /// 1. eliminar producciones con variables no terminables
        /// 2. eliminar producciones con variables no alcanzables
        /// </summary>
        /// <param name="variables">
        /// Lista con las variables
        /// </param>
        public void eliminarProduccionesConLasVariables(List<char> variables)
        {
            List<string> eliminar = new List<string>();

            foreach(char variable in variables)
            {
                foreach (string produccion in producciones)
                {
                    if (produccion.Contains(variable) == true)
                    {
                        if(eliminar.Contains(produccion) == false)
                        {
                            eliminar.Add(produccion);
                        }    
                    }
                }
            }

            producciones = producciones.Except(eliminar).ToList<string>();

        }

        /// <summary>
        /// Metodo utilizado para simular las producciones lambda
        /// </summary>
        /// <param name="anulables">
        /// Lista con las variables anulables
        /// </param>
        public void simularProduccionesLambda(List<char> anulables)
        {
            List<string> nuevas = new List<string>();

            //se evalua cada produccion
            foreach(string produccion in producciones)
            {
                //saco las anulables de la produccion y sus posiciones en la misma
                List<char> lista = new List<char>();
                List<int> posiciones = new List<int>();
                for(int x = 0; x < produccion.Count(); x++)
                {
                    char caracter = produccion.ElementAt(x);
                    if(anulables.Contains(caracter) == true)
                    {
                        lista.Add(caracter);
                        posiciones.Add(x);
                    }
                }
              
                //verifico que existan anulables
                if(lista.Count() > 0)
                {
                    //se obtienen las opciones en que se pueden anular las variables
                    int numeroAnulables = lista.Count();
                    int limite = (int) Math.Pow(2, numeroAnulables);
                    List<string> opciones = generarListaDeBinarios(numeroAnulables, limite);
                    
                    //se analiza cada opcion
                    for (int i = 0; i < opciones.Count(); i++)
                    {
                        string nuevaProduccion = produccion;
                        string opcion = opciones.ElementAt(i); //es un numero binario

                        List<int> posicionesAEliminar = new List<int>();
                        for (int j = 0; j < opcion.Count(); j++)
                        {
                            char anulable = lista.ElementAt(j);
                            char decision = opcion.ElementAt(j); //'0' mantengo, '1' elimino

                            if (decision.Equals('1') == true)
                            {
                                int posicion = posiciones.ElementAt(j);
                                posicionesAEliminar.Add(posicion);
                            }
                        }

                        if (posicionesAEliminar.Count() > 0)
                        {
                            //modifico la produccion con las posiciones a eliminar
                            int contador = 0;
                            for (int w = 0; w < posicionesAEliminar.Count(); w++)
                            {
                                int pos = posicionesAEliminar.ElementAt(w) - contador;
                                nuevaProduccion = nuevaProduccion.RemoveAt(pos);

                                contador++;
                            }
                        }

                        if (nuevaProduccion.Equals(produccion) == false)
                        {
                            if(nuevaProduccion.Count() == 0 && generador.Equals('S'))
                            {
                                nuevaProduccion = LAMBDA.ToString();
                                nuevas.Add(nuevaProduccion);
                            }
                            else if (nuevaProduccion.Count() > 0)
                            {
                                nuevas.Add(nuevaProduccion);
                            }                       
                        }
                    }
                }     
            }

            producciones.Remove(LAMBDA.ToString());
            producciones = producciones.Union(nuevas).ToList<string>();
        }

        /// <summary>
        /// Genera una lista de numeros binarios en formato string
        /// </summary>
        /// <param name="componentes">
        /// indica cuantos componentes(tamaño) debe tener cada binario generado
        /// </param>
        /// <param name="limite">
        /// indica cuantos binarios generar
        /// </param>
        /// <returns>
        /// Lista de numeros binarios en formato string
        /// </returns>
        public List<string> generarListaDeBinarios(int componentes, int limite)
        {
            List<string> lista = new List<string>();

            for (int i = 0; i < limite; i++)
            {
                string binario = decimalABinario(i, componentes);
                lista.Add(binario);
            }

            return lista;

        }

        /// <summary>
        /// Convierte un numero en formato decimal a su formato en binario
        /// </summary>
        /// <param name="numero">
        /// Numero decimal
        /// </param>
        /// <param name="componentes">
        /// indica cuantos componentes (tamaño) debe tener el binario generado
        /// </param>
        /// <returns>
        /// String que representa el numero binario
        /// </returns> 
        public string decimalABinario(int numero, int componentes)
        {
            string sal = "";

            int dec = numero;
            int bin;

            while (dec > 0)
            {
                bin = dec % 2;
                dec = dec / 2;

                sal = bin + sal;
            }

            int tamanio = sal.Count();
            while (tamanio != componentes)
            {
                sal = "0" + sal;
                tamanio = sal.Count();
            }

            return sal;
        }

        /// <summary>
        /// Determina cuales son las producciones no unitarias
        /// </summary>
        /// <returns>
        /// Lista con las producciones no unitarias
        /// </returns>
        public List<string> darProduccionesNoUnitarias()
        {
            List<string> respuesta = new List<string>();

            for (int i = 0; i < producciones.Count(); i++)
            {
                string prod = producciones.ElementAt(i);
                if (prod.Count() != 1)
                {
                    respuesta.Add(prod);
                }
                else if(Char.IsLower(prod.ElementAt(0)) == true)
                {
                    respuesta.Add(prod);
                }
                else if (prod.ElementAt(0).Equals(LAMBDA))
                {
                    respuesta.Add(prod);
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Modifica el atributo producciones añadiendo nuevas producciones
        /// </summary>
        /// <param name="nuevasProducciones">
        /// Lista de string con las nuevas producciones a añadir.
        /// </param>
        public void modificarProducciones(List<string> nuevasProducciones)
        {
            producciones = producciones.Union(nuevasProducciones).ToList<string>();
        }

        /// <summary>
        /// Elimina las producciones unitarias
        /// </summary>
        public void eliminarProduccionesUnitarias()
        {
            producciones = darProduccionesNoUnitarias();
        }

        /// <summary>
        /// Convierte todas las producciones en producciones binarias
        /// </summary>
        /// <param name="g">
        /// Referencia al Objeto Gramatica al cual pertenece este objeto Regla
        /// </param>
        public void obtenerProduccionesBinarias(Gramatica g)
        {
            List<string> nuevas = new List<string>();
            for(int i = 0; i < producciones.Count(); i++)
            {
                string produccion = producciones.ElementAt(i);
                int tamanio = produccion.Count();
                if (tamanio > 2) //produccion no binaria
                {
                    char caracter1 = produccion.ElementAt(tamanio - 1);
                    char caracter2 = produccion.ElementAt(tamanio - 2);

                    string nuevaProduccion = caracter2.ToString() + caracter1.ToString();

                    Regla regla = g.reglaUnitariaConProduccion(nuevaProduccion);

                    if (regla != null)
                    {
                        //Reemplazo esos caracteres por la variable que los genera

                        produccion = produccion.RemoveAt(produccion.Count() - 1);
                        produccion = produccion.RemoveAt(produccion.Count() - 1);
                        produccion = produccion + regla.generador.ToString();
                        nuevas.Add(produccion);
                    }
                    else
                    {
                        //creo la produccion nueva

                        char variable = g.variablesPosibles.ElementAt(0);
                        g.variablesPosibles.RemoveAt(0); //menos variables
                        g.variables.Add(variable);

                        //agrego regla
                        Regla nueva = new Regla(variable, new List<string> { nuevaProduccion });
                        g.nuevasReglas.Add(nueva);

                        //modifico produccion
                        produccion = produccion.RemoveAt(produccion.Count() - 1);
                        produccion = produccion.RemoveAt(produccion.Count() - 1);
                        produccion = produccion + variable.ToString();
                        nuevas.Add(produccion);
                    }
                }
                else
                {
                    nuevas.Add(produccion);
                }
               
            }

            producciones = nuevas;
            
        }

        /// <summary>
        /// Determina si la regla es binaria.
        /// Una regla es binaria si todas sus producciones constan de dos variables o 1 terminal
        /// </summary>
        /// <returns>
        /// Retorna true si la regla es binaria. En caso contrario, retorna false
        /// </returns>    
        public bool esBinaria()
        {
            bool respuesta = true;

            for(int i = 0; i < producciones.Count() && respuesta; i++)
            {
                string produccion = producciones.ElementAt(i);

                if(produccion.Count() == 1)
                {
                    char caracter = produccion.ElementAt(0);
                    if(Char.IsLower(caracter) == false && caracter.Equals(LAMBDA) == false) //si es una letra minuscula o lambda
                    {
                        respuesta = false;
                    }
                }
                else if(produccion.Count() == 2)
                {
                    char caracter1 = produccion.ElementAt(0);
                    char caracter2 = produccion.ElementAt(1);
                    if(Char.IsUpper(caracter1) == false || Char.IsUpper(caracter2) == false) //si ambas son mayusculas (variables)
                    {
                        respuesta = false;
                    }
                }
                else
                {
                    respuesta = false;
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Determina si la regla contiene una produccion pasada como parametro
        /// </summary>
        /// <param name="prod">
        /// String que representa la produccion
        /// </param>
        /// <returns>
        /// Retorna true si la regla contiene la produccion. En caso contrario, retorna false.
        /// </returns>
        public bool contieneProduccion(string prod)
        {
            return producciones.Contains(prod);
        }

        /// <summary>
        /// Genera una cadena de texto que representa este objeto Regla
        /// </summary>
        /// <returns>
        /// String que representa este objeto Regla.
        /// </returns>
        public override string ToString()
        {
            string cadena = generador + " -> ";

            int tamanio = producciones.Count();

            for (int i = 0; i < tamanio; i++)
            {
                string prod = producciones.ElementAt(i);

                if (i == tamanio - 1)
                {
                    cadena = cadena + prod;
                }
                else
                {
                    cadena = cadena + prod + " | ";
                }
            }
            
            return cadena;
        }
    }

    /// <summary>
    /// Extensiones
    /// </summary>
    public static class MisExtensiones
    {
        /// <summary>
        /// Metodo que permite eliminar un caracter de una cadena dando su posicion
        /// </summary>
        /// <param name="str">
        /// cadena de texto
        /// </param>
        /// <param name="pos">
        /// entero que representa la posicion en la cual se encuentra el caracter a eliminar
        /// </param>
        /// <returns>
        /// cadena de texto sin el caracter.
        /// </returns>
        public static string RemoveAt(this String str, int pos)
        {
            string cadena = "";

            for(int i = 0; i < str.Count(); i++)
            {
                if(i != pos)
                {
                    cadena = cadena + str.ElementAt(i);
                }
            }

            return cadena;
        }
    }
}
