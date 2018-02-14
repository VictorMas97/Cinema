using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cine;
using interfazUsuario;

namespace Practica1
{
    class TestVentaEntradas
    {
        static void Main1(string[] args)
        {
            //creamos un cine con 2 salas(con 2 sesiones) para probar las operaciones
            string[] horasSesiones = { "18:00", "22:00" };
            Sala[] salas = {new Sala("Tiburon", horasSesiones, 9, 5, new int[]{3}),
                            new Sala("Tron", horasSesiones, 2, 2)};
            Cine cine = new Cine("CinemaVintage", salas);

            //necesitamos la ventanilla para mostrar el estado de la sesion
            VentanillaVirtualUsuario ventanilla = new VentanillaVirtualUsuario(cine, true);

            foreach (String pelicula in cine.getPeliculas())
            {
                Console.WriteLine(pelicula);
            }

            cine.comprarEntrada(1, 1, 2, 1);
            cine.comprarEntrada(1, 1, 9, 3);

            int idVenta = cine.getIdEntrada(1, 1, 9, 3);

            Console.WriteLine("Id de venta es:" + idVenta);

            ButacasContiguas butacas = cine.recomendarButacasContiguas(4, 1, 1);

            cine.comprarEntradasRecomendadas(1, 1, butacas);

            int idVenta1 = cine.getIdEntrada(1, 1, butacas.getFila(), butacas.getColumna());

            cine.recogerEntradas(idVenta1, 1, 1);

            ventanilla.mostrarEstadoSesion(cine.getEstadoSesion(1, 1));

            Console.WriteLine("No. de butacas disponibles: " + cine.getButacasDisponiblesSesion(1, 1));

            Console.WriteLine("Tickets :" + cine.recogerEntradas(idVenta, 1, 1));

            Console.WriteLine("Tickets recomendados:" + cine.recogerEntradas(idVenta1, 1, 1));
        }
    }
}
