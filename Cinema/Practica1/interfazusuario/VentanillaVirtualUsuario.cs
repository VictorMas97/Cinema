using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cine;
using interfazUsuario.menu;

namespace interfazUsuario
{
    public class VentanillaVirtualUsuario
    {
        private int sala, sesion, fila, columna;
        private int id;
        private Cine cine;
        private bool sesionLlena;
        private int noButacas;
        private char respuesta;
        private Menu menuPrincipal;

        private readonly string[] opcionesMenuPrincipal = {"Comprar Entrada", "Recoger Entrada",
                "Consultar Estado Sesión", "Comprar con recomendación de butacas", "Salir"};
        private readonly string[] opcionesObligatoriasMenuPrincipal = {"Comprar Entrada",
                "Consultar Estado Sesión", "Salir"};
        private string operacion;
        // indica si trabajamos con un menu completo 
        // o solo con las operaciones obligatorias
        private bool versionCompleta;


        public VentanillaVirtualUsuario(Cine cine, bool versionCompleta)
        {
            this.versionCompleta = versionCompleta;
            this.cine = cine;
            if (versionCompleta)
                this.menuPrincipal = new Menu(opcionesMenuPrincipal);
            else
                this.menuPrincipal = new Menu(opcionesObligatoriasMenuPrincipal);
        }

        /** pedimos una sala y una sesion validas al usuario
	     se guardan en los atributos sala y sesion
	    */
        private void pedirSalaSesion()
        {
            Menu menu = new Menu(cine.getPeliculas());
            sala = Int16.Parse(menu.activar());
            string[] sesionesDisponibles = cine.getHorasDeSesionesDeSala(sala);
            menu = new Menu(sesionesDisponibles);
            sesion = Int16.Parse(menu.activar());
        }

        /** mostramos en la consola el estado dado de una sesion
	     * 
	     */
        public void mostrarEstadoSesion(char[,] estadoSesion)
        {
            Console.Write("\n    ");
            int asiento = 1;
            for (int i = 0; i < estadoSesion.GetLength(1); i++)
            {
                if (estadoSesion[0, i] != ' ')
                    Console.Write((asiento++) + " ");
                else
                    Console.Write(" " + " ");
            }
            Console.Write("\n\n");
            for (int i = 0; i < estadoSesion.GetLength(0); i++)
            {
                Console.Write((i + 1) + "   ");
                for (int j = 0; j < estadoSesion.GetLength(1); j++)
                    Console.Write(estadoSesion[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private int calcularNumeroPasillosHastaColumna(int fila, int columna, char[,] estadoSesion)
        {
            int numPasillos = 0;
            int i = 0;
            while (i <= columna + numPasillos)
            {
                if (estadoSesion[fila < 0 ? 0 : fila - 1, i > estadoSesion.GetLength(1) - 1 ? estadoSesion.GetLength(1) - 1 : i] == ' ')
                    numPasillos++;
                i++;
            }
            return numPasillos;
        }


        /**
	     * pedimos una sala, una sesion y una butaca validas al usuario
	     * se guardan en los atributos sala, sesion y (fila, columna)
	     */
        public void pedirDatosCompra()
        {
            this.pedirSalaSesion();
            if (cine.getButacasDisponiblesSesion(sala, sesion) == 0)
                sesionLlena = true;
            else
            {
                sesionLlena = false;
                char[,] estadoSesion = cine.getEstadoSesion(sala, sesion);
                this.mostrarEstadoSesion(estadoSesion);


                LecturaTeclado teclado = new LecturaTeclado();

                bool sigue;
                do
                {
                    sigue = false;
                    fila = teclado.leerNatural("Elige una fila:");
                    columna = teclado.leerNatural("Elige una columna:");
                    // comprobamos si la fila y la columna existen en la sala
                    if (fila < 0 || fila > estadoSesion.GetLength(0) ||
                            columna < 0 || columna > estadoSesion.GetLength(1) - calcularNumeroPasillosHastaColumna(fila, columna, estadoSesion))
                    {
                        Console.WriteLine("La butaca seleccionada "
                                + "no existe, elige otra");
                        sigue = true;
                    }
                    else
                        // comprobamos si la butaca esta ya vendida
                        if (estadoSesion[fila - 1, columna - 1] == 'X')
                    {
                        Console.WriteLine("La butaca seleccionada "
                                + "está ocupada, elige otra");
                        sigue = true;
                    }

                } while (sigue);
            }
        }

        /** pedimos una sala y una sesion validas al usuario
	     se guardan en los atributos sala y sesion
	    */
        public void pedirDatosConsultaSesion()
        {
            this.pedirSalaSesion();
        }

        public int getSala()
        {
            return sala;
        }

        public int getSesion()
        {
            return sesion;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public int getId()
        {
            return id;
        }

        public int getNoButacas()
        {
            return noButacas;
        }

        /** pedimos una sala y una sesion validas al usuario, y un id de compra
	     se guardan en los atributos id, sala y sesion
	    */
        public void pedirDatosRecogida()
        {
            this.pedirSalaSesion();

            LecturaTeclado teclado = new LecturaTeclado();

            id = teclado.leerNatural("Introduce un id de venta correcto:");

        }

        public bool getSesionLlena()
        {
            // TODO Auto-generated method stub
            return sesionLlena;
        }

        /** pedimos una sala y una sesion validas al usuario, y un no. de butacas contiguas
	     se guardan en los atributos noButacas, sala y sesion
	    */
        public void pedirDatosRecomendacion()
        {
            this.pedirSalaSesion();

            LecturaTeclado teclado = new LecturaTeclado();
            do
            {
                noButacas = teclado.leerNatural("Introduce el no. de butacas contiguas:");
            } while (noButacas <= 0);
        }

        /** 
	     * pedimos al usuario que confirme la compra de varias butacas contiguas	
	    */
        public void pedirConfirmacionCompraRecomendacion(ButacasContiguas butacas)
        {
            char[,] estadoSesion = cine.getEstadoSesion(
                    sala,
                    sesion);
            // marcamos las butacas seleccionadas por el usuario en el estado actual
            // de la sesion
            int huecos = 0;
            for (int i = 0; i < butacas.getNoButacas(); i++)
            {
                huecos = calcularNumeroPasillosHastaColumna(butacas.getFila(), butacas.getColumna() + i - 1, estadoSesion);
                estadoSesion[butacas.getFila() - 1, butacas.getColumna() + i + huecos - 1] = 'R';
            }

            // mostramos la seleccion de butacas en consola
            mostrarEstadoSesion(estadoSesion);
            Console.WriteLine("Se recomiendan las butacas marcadas con R");

            LecturaTeclado teclado = new LecturaTeclado();
            respuesta = teclado.leerCaracter("Aceptas la compra? (s/n)");
        }

        public char getRespuesta()
        {
            // TODO Auto-generated method stub
            return respuesta;
        }

        /**
	     *  pedimos al usuario que introduzca un numero de operacion
	     */
        public void PedirOperacion()
        {
            operacion = this.menuPrincipal.activar();
            if (!this.versionCompleta)
                if (operacion == "3")
                    operacion = "5";
                else if (operacion == "2")
                    operacion = "3";
        }

        public string getOperacionSeleccionada()
        {
            return operacion;
        }
    }
}
