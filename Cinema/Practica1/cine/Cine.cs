using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cine
{
    public class Cine
    {
        private Sala[] salas;
        private string nombre;

        public Cine (string nombre, Sala[] salas)
        {
            this.nombre = nombre;
            this.salas = salas;
        }

        public void comprarEntrada (int sala, int sesion, int fila, int columna)
        {
            salas[sala - 1].comprarEntrada(sesion, fila, columna);
        }

        public void comprarEntradasRecomendadas (int sala, int sesion, ButacasContiguas butacas)
        {
            salas[sala - 1].comprarEntradasRecomendadas(sesion , butacas);
        }

        public int getButacasDisponiblesSesion (int sala, int sesion)
        {
            return salas[sala - 1].getButacasDisponiblesSesion(sesion);
        }

        public char[,] getEstadoSesion (int sala, int sesion)
        {
            return salas[sala - 1].getEstadoSesion(sesion);
        }

        public string[] getHorasDeSesionesDeSala (int sala)
        {
            return salas[sala - 1].getHorasDeSesionesDeSala();
        }

        public int getIdEntrada (int sala, int sesion, int fila, int columna)
        {
            return salas[sala - 1].getIdEntrada(sesion, fila, columna);
        }

        public string[] getPeliculas ()
        {
            string[] peliculas = new string[salas.Length];

            for (int i = 0; i < peliculas.Length; i++)
            {
                peliculas[i] = salas[i].getPelicula();
            }

            return peliculas;
        }

        public string recogerEntradas (int id, int sala, int sesion)
        {
            string recojerEntradas = null;

            if (salas[sala - 1].recogerEntradas(id, sesion) != null)
            {
                recojerEntradas = nombre + "#" + salas[sala - 1].recogerEntradas(id, sesion);
            }
            return recojerEntradas;
        } 

        public ButacasContiguas recomendarButacasContiguas (int noButacas, int sala, int sesion)
        {
            return salas[sala - 1].recomendarButacasContiguas(noButacas, sesion);
        }
    }
}
