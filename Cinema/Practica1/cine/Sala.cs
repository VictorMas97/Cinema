using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cine
{
    public class Sala
    {
        private Sesion[] sesiones;
        private string pelicula;

        public Sala (string pelicula, string[] horasSesiones, int filas, int columnas, int[] columnasSinAsiento = null)
        {
            sesiones = new Sesion[horasSesiones.Length]; //Se genera un array de sesiones de tantos huecos como horas de sesiones hay en una misma sala            
            this.pelicula = pelicula;

            for (int i = 0; i < horasSesiones.Length; i++)
            {
                sesiones[i] = new Sesion (horasSesiones[i], filas, columnas, columnasSinAsiento); // Se reyena el array de las distintas sesiones con los datos recibidos por parametro            
            }
        }

        public void comprarEntrada (int sesion, int fila, int columna)
        {
            sesiones[sesion - 1].comprarEntrada(fila, columna);            
        }

        public void comprarEntradasRecomendadas (int sesion, ButacasContiguas butacas)
        {
            sesiones[sesion - 1].comprarEntradasRecomendadas(butacas);
        }

        public int getButacasDisponiblesSesion (int sesion)
        {
            return sesiones[sesion - 1].getButacasDisponiblesSesion();
        }

        public char[,] getEstadoSesion (int sesion)
        {            
            return sesiones[sesion - 1].getEstadoSesion();
        }

        public string[] getHorasDeSesionesDeSala ()
        {
            string[] horasDeSesionesDeSala = new string[sesiones.Length]; //Se genera un array de horas de sesiones de tantos huecos como sesiones hay en una misma sala

            for (int i = 0; i < horasDeSesionesDeSala.Length; i++)
            {
                horasDeSesionesDeSala[i] = sesiones[i].getHora();  // Se reyena el array de las distintas horas que hay en una sala
            }

            return horasDeSesionesDeSala;
        }

        public int getIdEntrada (int sesion, int fila, int columna)
        {
            return sesiones[sesion - 1].getIdEntrada(fila, columna); 
        }

        public string getPelicula ()
        {
            return pelicula;
        } 

        public string recogerEntradas (int id, int sesion)
        {
            string recojerEntradas = null;

            if (sesiones[sesion - 1].recogerEntradas(id) != null)
            {
                recojerEntradas = pelicula + "#" + sesiones[sesion - 1].recogerEntradas(id);
            }
            return recojerEntradas; 
        }   

        public ButacasContiguas recomendarButacasContiguas (int noButacas, int sesion)
        {
            return sesiones[sesion - 1].recomendarButacasContiguas(noButacas); 
        } 
    }
}