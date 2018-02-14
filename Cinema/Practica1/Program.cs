using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cine;
using interfazUsuario;

namespace Practica1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string COMPRAR = "1";
            const string RECOGER = "2";
            const string CONSULTAR_SESION = "3";
            const string COMPRAR_RECOMENDACION = "4";
            const string SALIR = "5";

            bool seguir = true;

            // creamos un cine con 2 salas (con 2 sesiones) para probar las operaciones
            string[] horasSesiones = { "18:00", "22:00" };
            Sala[] salas = {new Sala("Tiburon", horasSesiones, 9, 5, new int[]{3}),
                    new Sala("Tron", horasSesiones, 2, 2)};
            Cine cine = new Cine("CinemaVintage", salas);

            // este programa puede utilizar solo las operaciones obligatorias o todas
            LecturaTeclado teclado = new LecturaTeclado();
            char key = teclado.leerCaracter("Deseas probar solo las operaciones obligatorias? (s/n)");

            VentanillaVirtualUsuario ventanilla;
            if (key == 's' || key == 'S')
                // creamos una ventanilla solo para las operaciones obligatorias
                ventanilla = new VentanillaVirtualUsuario(cine, false);
            else
                // creamos una ventanilla para todas las operaciones
                ventanilla = new VentanillaVirtualUsuario(cine, true);

            do
            {
                // Mostramos el menu principal y pedimos una operacion al usuario
                ventanilla.PedirOperacion();
                string opcion = ventanilla.getOperacionSeleccionada();

                switch (opcion)
                {
                    case COMPRAR:
                        // pedimos al usuario la sala, sesion y la butaca
                        // solo se admiten butacas libres
                        // estos datos quedan almacenados en el objeto ventanilla
                        ventanilla.pedirDatosCompra();
                        // comprobamos si la sesion de la sala seleccionada esta llena
                        if (!ventanilla.getSesionLlena())
                        {
                            cine.comprarEntrada(ventanilla.getSala(),
                                    ventanilla.getSesion(),
                                    ventanilla.getFila(),
                                    ventanilla.getColumna());
                            Console.WriteLine("El id de la venta es " +
                                    cine.getIdEntrada(ventanilla.getSala(),
                                            ventanilla.getSesion(),
                                            ventanilla.getFila(),
                                            ventanilla.getColumna()));
                        }
                        else
                            Console.WriteLine("La sesion seleccionada está llena");
                        break;

                    case RECOGER:
                        // pedimos al usuario el id de la compra, la sala y la sesion
                        // estos datos quedan almacenados en el objeto ventanilla
                        ventanilla.pedirDatosRecogida();
                        string entradas = cine.recogerEntradas(ventanilla.getId(),
                                ventanilla.getSala(),
                                ventanilla.getSesion());
                        // comprobamos si existe el id de compra dado en la sala y
                        // la sesion dadas
                        if (entradas == null)
                            Console.WriteLine("Los datos de la venta son incorrectos");
                        else
                            Console.WriteLine("Tus entradas son: " + entradas);
                        break;

                    case CONSULTAR_SESION:
                        // pedimos al usuario la sala y la sesion
                        // estos datos quedan almacenados en el objeto ventanilla
                        ventanilla.pedirDatosConsultaSesion();
                        char[,] estadoSesion = cine.getEstadoSesion(
                                ventanilla.getSala(),
                                ventanilla.getSesion());
                        ventanilla.mostrarEstadoSesion(estadoSesion);
                        break;

                    case COMPRAR_RECOMENDACION:
                        // pedimos al usuario el no. de butacas contiguas, la sala y la sesion
                        // estos datos quedan almacenados en el objeto ventanilla
                        ventanilla.pedirDatosRecomendacion();
                        ButacasContiguas butacas = cine.recomendarButacasContiguas(
                                ventanilla.getNoButacas(),
                                ventanilla.getSala(),
                                ventanilla.getSesion());
                        // comprobamos si existen suficientes butacas contiguas
                        // en la sala y sesion dadas
                        if (butacas != null)
                        {
                            ventanilla.pedirConfirmacionCompraRecomendacion(butacas);
                            // pedimos al usuario que confirme si quiere las butacas recomendadas
                            // la respuesta queda guardada en el objeto ventanilla
                            if (ventanilla.getRespuesta() == 's' || ventanilla.getRespuesta() == 'S')
                            {
                                cine.comprarEntradasRecomendadas(ventanilla.getSala(),
                                        ventanilla.getSesion(), butacas);
                                Console.WriteLine("El id de la venta es " +
                                        cine.getIdEntrada(ventanilla.getSala(),
                                                ventanilla.getSesion(),
                                                butacas.getFila(),
                                                butacas.getColumna()));
                            }
                            else
                            {
                                Console.WriteLine("Has descartado la recomendación");
                            }

                        }
                        else
                        {
                            Console.WriteLine("No hay tantas butacas disponibles contiguas");
                        }
                        break;

                    case SALIR: seguir = false; break;
                }
            } while (seguir);

        }
    }
}
