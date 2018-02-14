using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace interfazUsuario.menu
{
    public class Menu
    {
        private string[] opciones = null;
        /**
         * Enumerado que definir las distintas formas de indicar cómo se selecciona una opción del menú.
         *
         */
        public enum Numerar { NUMERO, LETRA_ABC };

        /**
         * Atributo que indica cómo se han de numerar las opciones y que indica cuál es el tipo
         * de valor que se espera.
         */
        private Numerar numerar = Numerar.NUMERO;

        /**
         * Constructor que recibe las opciones y cómo se han de numerar
         * @param opciones es el vector de cadenas de caracteres que se utilizará en el menú
         * @param numerar establece cómo se ha de numerar, y cual es el valor que ha de introducir el usuario para seleccionar una opción
         */
        public Menu(string[] opciones, Numerar numerar)
        {//Constructor
            copiaOpciones(opciones);
            this.numerar = numerar;
        }//Constructor

        /**
         * Constructor que recibe sólo las opciones. Las opciones se numerarán utilizando números
         * @param opciones es el vector de cadenas de caracteres que se utilizará en el menú
         */
        public Menu(string[] opciones)
        {//Menu
            copiaOpciones(opciones);
        }//Menu
        /**
         * Servicio que activa el menú y retorna una cadea de caracteres con la opción seleccionada. 
         * @return string  con la opción seleccionada. Si el modo es númer ver Numerar.NUMERO el valor 
         * irá de 1 a n en cambio si es letra ver Numerar.LETRA_ABC el valor irá de 'a' a 'z'
         */
        public string activar()
	    {//Activar
		    string opcionSeleccionada=null;
		    int nValor = -1;
            
		    mostrarOpciones();
		   
		    
		    while (opcionSeleccionada==null
				    || (this.numerar==Numerar.LETRA_ABC && (opcionSeleccionada[0]<'a'
						    || opcionSeleccionada[0]>=((int)'a'+opciones.Length)))
						    || (this.numerar==Numerar.NUMERO && 
						    (nValor<1 ||
								    nValor>opciones.Length)))
		    {//W
			    Console.Write("\n Seleccione una de las opciones para continuar: ");

			    opcionSeleccionada=Console.ReadLine();
			    if (this.numerar==Numerar.NUMERO && int.TryParse(opcionSeleccionada, out nValor)) 
				    nValor = Int16.Parse(opcionSeleccionada);	

		    }//W

		    return opcionSeleccionada;
	    }//Activar

        /**
         * Servicio privado que sirve para copiar las opciones del menú que se pasan a cualquiera de los constructores dado	
         * @param opciones contiene un vector de strings con las opciones que va a tener el menu
         */
        private void copiaOpciones(string[] opciones)
        {//copiaOpciones
            this.opciones = new string[opciones.Length];
            for (int i = 0; i < opciones.Length; i++)
            {//for
                this.opciones[i] = opciones[i];
            }//for
        }//copia Opciones

        /**
         * Método que se encarga de mostrar las opciones del menú anteponiendo un número o una letra según corresponda
         * 
         */
        private void mostrarOpciones()
	    {//mostrarOpciones
		    Console.WriteLine();
		    for (int i=0;i<opciones.Length;i++)
		    {//for
                string opcion = (this.numerar == Numerar.NUMERO ? Convert.ToString(i + 1) : Convert.ToString((char)(int)'a'+1));
			    Console.Write("{0})\t{1}\n", opcion,opciones[i]);
		    }//for
	    }//mostrarOpciones
    }
}
