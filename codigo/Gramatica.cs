using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FNC
{
    /// <summary>
    /// Clase que representa una gramatica
    /// </summary>
    public class Gramatica
    {
        //ATRIBUTOS --------------------------------------------------------------------------------

        //Reglas de la gramatica
        public List<Regla> reglas;
        
        //Representa todas las variables utilizadas.
        public List<char> variables;

        //terminales en la gramatica
        public List<char> terminales;

        //variables posibles
        public List<char> variablesPosibles = new List<char>(){ 'A','B','C', 'D', 'E','F','G','H','I','J','K','L','M',
             'N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};

        //nuevas reglas que se generan en el proceso de obtener producciones binarias
        public List<Regla> nuevasReglas = new List<Regla>();

        //matriz de resultado al aplicar el algoritmo CYK sobre una cadena
        public List<string>[,] matriz;

        //CONSTRUCTOR --------------------------------------------------------------------------------
        /// <summary>
        /// Genera un objeto Gramatica a partir de una cadena de texto que contiene la gramatica expresada en 
        /// un formato como el siguiente:
        /// S : aXbX
        /// X : aY | bY | &
        /// Y : X | c
        /// </summary>
        /// <param name="texto">
        /// Cadena de texto con la gramatica
        /// </param>
        /// <exception cref="System.Exception">
        /// Lanza excepciones en los casos siguientes:
        /// CASO 1: Regla no respeta el formato de separacion por ":"
        /// CASO 2: La parte izquierda de una regla es mas de un caracter (tamaño mayor a 1)
        /// CASO 3: La parte izquierda de una regla no es una letra mayuscula
        /// CASO 4: Una produccion contiene un caracter que no es un terminal, variable o labmda
        /// CASO 5: Una produccion contiene labmda y mas caracteres.
        /// </exception>
        public Gramatica(string texto)
        {
            reglas = new List<Regla>();
            variables = new List<char>();
            terminales = new List<char>();

            string[] lineas = texto.Split('\n');
            for (int i = 0; i < lineas.Count(); i++)
            {
                string linea = lineas.ElementAt(i);

                if (linea != null && linea.Trim().Count() != 0)
                {
                    linea = linea.Replace(" ", "");

                    //VERIFICACION 1
                    String[] partes = linea.Split(':');
                    if (partes.Length != 2)
                    {
                        throw new Exception("Regla sin el formato : " + linea);
                    }

                    if (partes[0].Length != 1)
                    {
                        throw new Exception("La parte izquierda de una regla debe ser un caracter");
                    }

                    //VERIFICACION 2
                    char generador = partes[0].ElementAt(0);
                    if(generador < 'A' || generador > 'Z' || generador.Equals(Regla.LAMBDA))
                    {
                        throw new Exception("El generador debe ser una letra mayuscula");
                    }

                    //VERIFICACION 3
                    string[] producciones = partes[1].Split('|');  
                    for (int j = 0; j < producciones.Count(); j++)
                    {
                        string prod = producciones.ElementAt(j).Trim(); //quito espacios en blanco que pueden quedar
                 
                        //examino cada caracter de la produccion 
                        for(int z = 0; z < prod.Count(); z++)
                        {
                            char c = prod.ElementAt(z);
                            if (c.Equals(Regla.LAMBDA) == false)
                            {
                                if(Char.IsLetter(c) == false)
                                {
                                    throw new Exception("el caracter " + c + " no es un terminal, variable o lambda");
                                }
                                //---------------------------
                                else if(Char.IsLower(c))
                                {
                                    if(terminales.Contains(c) == false)
                                    {
                                        terminales.Add(c);
                                    }
                                }
                                else if(Char.IsUpper(c))
                                {
                                    if (variables.Contains(c) == false)
                                    {
                                        variables.Add(c);
                                        variablesPosibles.Remove(c);
                                    }
                                }
                                //-----------------------------
                            }
                            else
                            {
                                if (prod.Count() > 1)
                                {
                                    throw new Exception("Lambda debe aparecer sola y una unica vez en una produccion");
                                }
                            }
                        }

                        producciones[j] = prod;
                    }

                    //si llego hasta aqui es que no hubo ningun problema con el formato
                    Regla nueva = new Regla(generador, producciones.ToList<string>());
                    reglas.Add(nueva);

                    //-----------------------
                    if(variables.Contains(generador) == false)
                    {
                        variables.Add(generador);
                    }
                    //-------------------------

                }
            }
        }

       //METODOS ------------------------------------------------------------------------------------------

       /// <summary>
       /// Obtiene las variables terminables de la gramatica
       /// </summary>
       /// <returns>
       /// Retorna una lista con las variables terminables
       /// </returns>
        public List<char> darTerminables()
        {
            List<char> terminables = new List<char>();

            //inicializacion
            foreach(Regla regla in reglas)
            {
                if(regla.esTerminablePorProduccion() == true)
                {
                    terminables.Add(regla.generador);
                }
            }

            //repeticion hasta que no haya cambios
            bool cambio = true;
            while(cambio)
            {
                List<char> terminablesIniciales = new List<char>(terminables);
                
                foreach (Regla regla in reglas)
                {
                    char generador = regla.generador;
                    if (terminables.Contains(generador) == false)
                    {
                        if (regla.esTerminablePorVariableTerminable(terminables) == true)
                        {
                            terminables.Add(generador);
                        }
                    }
                }

                if(terminablesIniciales.Count() == terminables.Count())
                {
                    cambio = false;
                }  
            }

            return terminables;
        }

        /// <summary>
        /// Obtiene las variables no terminables de la gramatica
        /// </summary>
        /// <returns>
        /// Retorna una lista con las variables no terminables
        /// </returns>
        public List<char> darNoTerminables()
        {
            List<char> generadores = darGeneradores();

            return generadores.Except(darTerminables()).ToList<char>();
        }

        /// <summary>
        /// Obtiene las variables alcanzables de la gramatica
        /// </summary>
        /// <returns>
        /// Retorna una lista con las variables alcanzables
        /// </returns>
        public List<char> darAlcanzables()
        {
            //inicializacion
            List<char> alcanzables = new List<char>() { 'S' };
            List<char> yaAnalizadas = new List<char>();

            //repeticion hasta que no haya cambios
            bool cambio = true;
            while(cambio)
            {
                List<char> alcanzablesIniciales = new List<char>(alcanzables);
                foreach(char c in alcanzables)
                {
                    if(yaAnalizadas.Contains(c) == false)
                    {
                        Regla regla = darRegla(c);                      
                        if(regla != null)
                        {
                            List<char> variablesAlcanzables = regla.variablesAlcanzables();
                            alcanzables = alcanzables.Union(variablesAlcanzables).ToList<char>();       
                        }
                        yaAnalizadas.Add(c);
                    }
                }
                if(alcanzablesIniciales.Count() == alcanzables.Count())
                {
                    cambio = false;
                }
            }

            return alcanzables;
        }

        /// <summary>
        /// Obtiene las variables no alcanzables de la gramatica
        /// </summary>
        /// <returns>
        /// Retorna una lista con las variables no alcanzables
        /// </returns>
        public List<char> darNoAlcanzables()
        {
            List<char> generadores = darGeneradores();

            return generadores.Except(darAlcanzables()).ToList<char>();
        }

        /// <summary>
        /// Obtiene las variables anulables de la gramatica
        /// </summary>
        /// <returns>
        /// Retorna una lista con las variables anulables
        /// </returns>
        public List<char> darAnulables()
        {
            List<char> anulables = new List<char>();

            //inicializacion
            foreach(Regla regla in reglas)
            {
                if(regla.esAnuablePorProduccion() == true)
                {
                    anulables.Add(regla.generador);
                }
            }

            //repeticion hasta que no haya cambios
            bool cambio = true;
            while (cambio)
            {
                List<char> anulablesIniciales = new List<char>(anulables);

                foreach (Regla regla in reglas)
                {
                    if (anulables.Contains(regla.generador) == false)
                    {
                        if (regla.esAnulablePorVariableAnulable(anulables) == true)
                        {
                            anulables.Add(regla.generador);    
                        }
                    }
                }

                if (anulablesIniciales.Count() == anulables.Count())
                {
                    cambio = false;
                }
            }
            return anulables;
        }

        /// <summary>
        /// Obtiene el conjunto unitario de una variable
        /// </summary>
        /// <param name="generador">
        /// Representa la variable a la cual se calcula el conjunto unitario
        /// </param>
        /// <returns>
        /// Una lista con las variables pertenecientes al conjunto unitario de la variable pasada como parametro
        /// </returns>
        public List<char> darConjuntoUnitario(char generador)
        { 
            //inicializacion
            List<char> conjunto = new List<char>() { generador };
            List<char> yaEstudiado = new List<char>();

            //repeticion hasta que no haya cambios
            bool cambio = true;
            while(cambio)
            {
                List<char> conjuntoInicial = new List<char>(conjunto);

                foreach(char caracter in conjunto)
                {
                    if(yaEstudiado.Contains(caracter) == false)
                    {
                        Regla regla = darRegla(caracter);
                        if(regla != null)
                        {
                            conjunto = conjunto.Union(regla.produccionesUnitarias()).ToList<char>();
                        }
                        yaEstudiado.Add(caracter);
                    }   
                }

                if(conjuntoInicial.Count() == conjunto.Count())
                {
                    cambio = false;
                }
            }

            return conjunto;
        }

        /// <summary>
        /// Obtiene todas las variables que actuan como generadores. Una variable actua como generador si tiene
        /// una regla asociada.
        /// </summary>
        /// <returns>
        /// Una lista con todas las variables de la gramatica que tienen una regla asociada (actuan como generadores)
        /// </returns>
        public List<char> darGeneradores()
        {
            List<char> respuesta = new List<char>();

            foreach (Regla regla in reglas)
            {
                respuesta.Add(regla.generador);
            }

            return respuesta;
        }

        /// <summary>
        /// Obtiene la regla asociada a una variable
        /// </summary>
        /// <param name="generador">
        /// Representa la variable de la cual se quiere obtener su regla
        /// </param>
        /// <returns>
        /// Un objeto Regla que representa la regla asociada a la variable pasada como parametro
        /// </returns>
        public Regla darRegla(char generador)
        {
            var busqueda = reglas.Where(r => r.generador.Equals(generador));
          
            if(busqueda.Count() == 0)
            {
                return null;
            }
            else
            {
                var regla  = (Regla) busqueda.ElementAt(0);
                return  regla;
            }
        }

        /// <summary>
        /// Metodo que elimina todas las variables no terminables de la gramatica.
        /// Al eliminar una variable no terminable se pierde su regla asociada y todas las producciones que la contenian
        /// </summary>
        public void eliminarNoTerminables()
        {
            List<char> terminables = darTerminables();
            List<char> generadores = darGeneradores();

            List<char> noTerminables = generadores.Except(terminables).ToList<char>();
 
            foreach(char noTerminable in noTerminables)
            {
                //eliminar la regla
                Regla regla = darRegla(noTerminable);
                if(regla != null)
                {
                    reglas.Remove(regla);
                }
            }

            //En cada regla eliminar producciones que contengan variables NO terminables
            foreach (Regla rule in reglas)
            {
                rule.eliminarProduccionesConLasVariables(noTerminables);
                if (rule.producciones.Count() == 0)
                {
                    reglas.Remove(rule);
                }
            }
        }

        /// <summary>
        /// Metodo que elimina todas las variables no alcanzables de la gramatica.
        /// Al eliminar una variable no alcanzable se pierde su regla asociada y todas las producciones que la contenian
        /// </summary>
        public void eliminarNoAlcanzables()
        {
            List<char> alcanzables = darAlcanzables();
            List<char> generadores = darGeneradores();

            List<char> noAlcanzables = generadores.Except(alcanzables).ToList<char>();

            foreach (char noAlc in noAlcanzables)
            {
                //eliminar la regla
                Regla regla = darRegla(noAlc);
                if (regla != null)
                {
                    reglas.Remove(regla);
                }
            }

            //En cada regla eliminar producciones que contengan variables NO Alcanzables
            foreach (Regla rule in reglas)
            {
                rule.eliminarProduccionesConLasVariables(noAlcanzables);
                if (rule.producciones.Count() == 0)
                {
                    reglas.Remove(rule);
                }
            }



        }

        /// <summary>
        /// Metodo que elimina y simula, añadiendo nuevas producciones, todas las producciones labmda.
        /// </summary>
        public void eliminarProduccionesLambda()
        {
            List<char> anulables = darAnulables();
           
            foreach(Regla regla in reglas)
            {
                regla.simularProduccionesLambda(anulables);
                
            }
        }

        /// <summary>
        /// Metodo que elimina y simula, añadiendo nuevas producciones, todas las producciones unitarias
        /// </summary>
        public void eliminarProduccionesUnitarias()
        {
            List<char> generadores = darGeneradores();

            foreach(char gen in generadores)
            {
                List<char> conjuntoUnitario = darConjuntoUnitario(gen);
                Regla rule = darRegla(gen);
                rule.eliminarProduccionesUnitarias();

                foreach (char variable in conjuntoUnitario)
                {
                    if(variable.Equals(gen) == false)
                    {
                        Regla regla = darRegla(variable);
                        if(regla != null)
                        {
                            List<string> produccionesNuevas = regla.darProduccionesNoUnitarias();
                            rule.modificarProducciones(produccionesNuevas);
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Toda produccion con tamaño mayor a 1 y que contenga un terminal, debe reemplazar dicho terminal por
        /// una variable. Este metodo realiza esta tarea teniendo como recurso las variables posibles y las variables
        /// hasta el momento utilizadas.
        /// Se añaden nuevas reglas con el formato (variable nueva -> terminal reemplazado)
        /// </summary>
        public void generarVariablesPorCadaTerminal()
        {
            List<char> variablesPermitidas = variablesPosibles.Except(variables).ToList<char>(); //para asegurar
            int numeroDeReglas = reglas.Count();

            //ASIGNACION
            Dictionary<char, char> asignaciones = new Dictionary<char, char>();       
            for(int i = 0; i < terminales.Count(); i++)
            {
                char terminal = terminales.ElementAt(i);
                char variableAsignada = variablesPermitidas.ElementAt(i);

                //asignacion
                asignaciones.Add(terminal, variableAsignada);
                //aumento variables
                variables.Add(variableAsignada);
                variablesPosibles.Remove(variableAsignada);
               
                //creo la regla nueva
                //Regla regla = new Regla(variableAsignada, new List<string>() { terminal.ToString() });
                //reglas.Add(regla);
                //nuevasReglas.Add(regla);
            }
            
            //MODIFICO PRODUCCIONES DE CADA REGLA
            for(int y = 0; y < numeroDeReglas; y++)
            {
                Regla reg = reglas.ElementAt(y);
                List<string> producciones = new List<string>(reg.producciones);

                for (int x = 0; x < producciones.Count(); x++)
                {
                    string produccion = producciones.ElementAt(x);

                    if(produccion.Count() > 1)
                    {
                        for (int i = 0; i < produccion.Count(); i++)
                        {
                            char caracter = produccion.ElementAt(i);
                            if (terminales.Contains(caracter))
                            {
                                char variableAsignada = asignaciones[caracter];
                                produccion = produccion.Replace(caracter, variableAsignada);
                  
                                if(darGeneradores().Contains(variableAsignada) == false)
                                {
                                    //creo la regla nueva
                                    Regla regla = new Regla(variableAsignada, new List<string>() { caracter.ToString() });
                                    reglas.Add(regla);
                                    nuevasReglas.Add(regla);
                                }
                            }
                        }

                        producciones.Insert(x, produccion);
                        producciones.RemoveAt(x + 1);
                    }   
                }

                reg.producciones = producciones;
            }   
        }

        /// <summary>
        /// Convierte cada produccion de una regla en una produccion binaria
        /// </summary>
        public void generarProduccionesBinarias()
        {
            variablesPosibles = variablesPosibles.Except(variables).ToList<char>(); //para asegurar

            bool reglasBinarias = todasLasReglasSonBinarias();
            while(reglasBinarias == false)
            {
                foreach (Regla regla in reglas)
                {
                    regla.obtenerProduccionesBinarias(this);
                }
                reglas = reglas.Union(nuevasReglas).ToList<Regla>();

                reglasBinarias = todasLasReglasSonBinarias();
            }
            
        }

        /// <summary>
        /// Determina si todas las reglas de la gramatica tienen sus producciones en forma binaria.
        /// </summary>
        /// <returns>
        /// Retorna true si todas las reglas tienen sus producciones en forma binaria. En caso contrario, retorna false
        /// </returns>
        public bool todasLasReglasSonBinarias()
        {
            bool respuesta = true;
            
            for(int i = 0; i < reglas.Count() && respuesta; i++)
            {
                Regla r = reglas.ElementAt(i);
                respuesta = r.esBinaria();
            }

            return respuesta;
        }


        /// <summary>
        /// Busca sobre las nuevas reglas generadas (que son unitarias, en el sentido que solo tienen una produccion), 
        /// una regla que tenga al parametro como produccion.
        /// Las nuevas reglas generadas estan representadas por el atributo nuevasReglas.
        /// </summary>
        /// <param name="prod">
        /// String que representa una produccion
        /// </param>
        /// <returns>
        /// Retorna la regla que tiene como produccion al parametro. En caso de que no encuentre ninguna regla,
        /// retorna null.
        /// </returns>
        public Regla reglaUnitariaConProduccion(string prod)
        {
            Regla buscada = null;

            for(int i = 0; i < nuevasReglas.Count() && buscada == null; i++)
            {
                Regla r = nuevasReglas.ElementAt(i);
                List<string> producciones = r.producciones; //una lista de una unica produccion

                if(producciones.ElementAt(0).Equals(prod))
                {
                    buscada = r;
                }
            }

            return buscada;
        }

        /// <summary>
        /// Metodo que implementa el algoritmo CYK para determinar si la gramatica genera una determinada cadena 
        /// pasada como parametro. Se asume que la gramatica ya ha sido convertida a su FNC.
        /// Se tiene el atributo "matriz" para comprobar como el algoritmo se desarrollo.
        /// </summary>
        /// <param name="cadena">
        /// String que representa la cadena
        /// </param>
        /// <returns>
        /// Retorna true si la gramatica genera la cadena pasada como parametro. En caso contrario, retorna false
        /// </returns>
        public bool algoritmoCYK(string cadena)
        {
            cadena = cadena.Trim();
            //comprobacion del formato de la cadena -----------------
            if(cadena.Count() == 0)
            {
                throw new Exception("Debe especificar una cadena no vacia");
            }
            foreach(var c in cadena)
            {
                if(terminales.Contains(c) == false)
                {
                    throw new Exception("La cadena contiene caracteres que no existen en el alfabeto de la gramatica");
                }
            }
            //-------------------------------------------------------

            bool respuesta = false;

            int n = cadena.Count();
            matriz = new List<string>[n, n];
           

            for(int j = 1; j <= n; j++)
            {
                if(j == 1)
                {
                    //Recorrido por la cadena
                    for(int x = 0; x < cadena.Count(); x++)
                    {
                        char caracter = cadena.ElementAt(x);
                      
                        List<string> list = darGeneradoresDeAlMenosUnaDeLasProducciones
                            (new List<string>() { caracter.ToString() });
                       

                        matriz[x, j-1] = list;
                        
                    }
                  
                }
                else
                {
                    for (int i = 1; i <= n - j + 1; i++)
                    {
                        List<string> generadores = new List<string>();

                        for (int k = 1; k <= j - 1; k++)
                        {
                            List<string> lista1 = matriz[i-1, k-1];
                            List<string> lista2 = matriz[i + k - 1, j - k - 1];

                            List<string> posibles = generarPosiblesProducciones(lista1, lista2);
                            List<string> gen = darGeneradoresDeAlMenosUnaDeLasProducciones(posibles);

                            generadores = generadores.Union(gen).ToList<string>();
                        }
                        matriz[i - 1, j - 1] = generadores;
                    }
                }
            }

            List<string> lista = matriz[0, n - 1];
            if(lista.Contains("S"))
            {
                respuesta = true;
            }

            return respuesta;
        }

        /// <summary>
        /// Obtiene las variables que en su regla asociada producen alguna de las producciones pasadas como parametro
        /// </summary>
        /// <param name="producciones">
        /// Lista de string que representa las producciones
        /// </param>
        /// <returns>
        /// Una lista de string con las variables
        /// </returns>
        public List<string> darGeneradoresDeAlMenosUnaDeLasProducciones(List<string> producciones)
        {
            List<string> respuesta = new List<string>();

            foreach(string prod in producciones)
            {
                foreach(Regla reg in reglas)
                {
                    if(reg.contieneProduccion(prod))
                    {
                        string generador = reg.generador.ToString();
                        if(respuesta.Contains(generador) == false)
                        {
                            respuesta.Add(generador);
                        }
                    }
                }
            }

            return respuesta;
        }

        /// <summary>
        /// Genera producciones a partir de dos listas cuyo contenido son variables. Metodo utilizado en el metodo
        /// que implementa el algorito CYK.
        /// </summary>
        /// <param name="lista1">
        /// Lista de string
        /// </param>
        /// <param name="lista2">
        /// Lista de string
        /// </param>
        /// <returns>
        /// Retorna una lista de string con las nuevas producciones
        /// </returns>
        public List<string> generarPosiblesProducciones(List<string> lista1, List<string> lista2)
        {
            List<string> respuesta = new List<string>();

            if(lista1.Count() == 0 && lista2.Count() > 0)
            {
                respuesta = lista2;
            }
            else if(lista1.Count() > 0 && lista2.Count() == 0)
            {
                respuesta = lista1;
            }
            else if(lista1.Count() > 0 && lista2.Count() > 0)
            {
                for (int i = 0; i < lista1.Count(); i++)
                {
                    string cad1 = lista1.ElementAt(i);

                    for (int j = 0; j < lista2.Count(); j++)
                    {
                        string cad2 = lista2.ElementAt(j);
                        string nuevo = cad1 + cad2;
                        respuesta.Add(nuevo);
                    }
                }
            }
            
            return respuesta;
        }


        /// <summary>
        /// Genera una cadena de texto que representa este objeto Gramatica
        /// </summary>
        /// <returns>
        /// Un string que representa este objeto Gramatica
        /// </returns>
        public override string ToString()
        { 
            string cadena = "";

            foreach(Regla regla in reglas)
            {
                cadena = cadena + regla.ToString() + Environment.NewLine;
            }

            return cadena;
        }

    }
}
