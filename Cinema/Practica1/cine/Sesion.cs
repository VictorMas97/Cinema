using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cine  
{
    public class Sesion
    {        
        private int[,] estadoAsientos;
        private int asientosDisponibles;
        private int sigIdCompra;
        private string hora;

        public Sesion (string hora, int filas, int columnas, int[] columnasSinAsiento = null)
        {
            estadoAsientos = new int[filas, columnas];
            asientosDisponibles = estadoAsientos.Length;
            this.hora = hora;
            sigIdCompra = 1;

            if (columnasSinAsiento != null) // Si "columnasSinAsiento" no es null, donde haya pasillos en la sesion se introduce un -1 en el array
            {
                for (int i = 0; i < filas; i++)
                {
                    for (int k = 0, j = 0; j < columnas && k < columnasSinAsiento.Length; k++)
                    {                        
                        j = columnasSinAsiento[k];
                        estadoAsientos[i, j] = -1;
                        asientosDisponibles--;
                    }
                }
            }
        }

        private ButacasContiguas buscarButacasContiguasEnFila (int fila, int noButacas)
        {
            ButacasContiguas butacasContiguasEnFila = null;
            int butacasContiguas = 0;
            int numeroPasillos = 0;
            int primeraButaca = 0;
            int i = 0;            

            for (  ; i < estadoAsientos.GetLength(1) && butacasContiguas < noButacas; i++) //Se ejecuta el bucle hasta que encuentro las butacas contiguas que le pases por parámetro o hasta que recorra toda la fila
            {
                if (estadoAsientos[fila - 1, i] == 0)
                {
                    butacasContiguas++;                    
                }
                else if(estadoAsientos[fila - 1, i] == -1)
                {
                    numeroPasillos++;
                }
                else 
                {
                    butacasContiguas = 0;
                }
            }

            if (butacasContiguas == noButacas) // Si hay disponibles la butacas contiguas que pasaste por parametro a nobutacas "butacasContiguasEnFila" no es igual a null
            {
                primeraButaca = i + 1 - noButacas - numeroPasillos;
                butacasContiguasEnFila = new ButacasContiguas(fila, primeraButaca, noButacas);
            }
                       
            return butacasContiguasEnFila;
        }

        private int CalcularNumeroPasillosHastaLaColumna (int fila, int columna)
        {                                                                        
            int numeroPasillosHastaLaColumna = 0;

            for (int i = 0; i < columna + numeroPasillosHastaLaColumna; i++) //Se ejecuta hasta que encuentre la posición el asiento en la fila que le has pasado como parámetro
            {
                if (estadoAsientos[fila - 1, i] == -1) // Si existe un pasillo entremedias, osea un -1, suma uno a la variable "numeroPasillosHastaLaColumna"
                {
                    numeroPasillosHastaLaColumna++;
                }
            }

            return numeroPasillosHastaLaColumna;
        }

        public void comprarEntrada (int fila, int columna)
        {
            estadoAsientos[fila - 1, columna - 1 + CalcularNumeroPasillosHastaLaColumna(fila, columna)] = sigIdCompra; //Compra la entrada en el hueco de la matriz correspondiente y lo rellena con el "sigIdCompra"
            sigIdCompra++;
            asientosDisponibles--;
        }   

        public void comprarEntradasRecomendadas(ButacasContiguas butacas)
        {

            for (int i = butacas.getColumna() - 1; i < butacas.getNoButacas() + butacas.getColumna() - 1; i++)
            {
                estadoAsientos[butacas.getFila() - 1, i + CalcularNumeroPasillosHastaLaColumna(butacas.getFila(), i + 1)] = sigIdCompra; //Compra las entrada en el hueco de la matriz correspondiente y lo rellena con el "sigIdCompra"
                asientosDisponibles--;
            }

            sigIdCompra++;
        }

        public int getButacasDisponiblesSesion()
        {
            return asientosDisponibles;
        }

        public char[,] getEstadoSesion ()
        {
            char[,] estadoSesion = new char[estadoAsientos.GetLength(0), estadoAsientos.GetLength(1)];

            for (int i = 0; i < estadoAsientos.GetLength(0); i++)
            {
                for (int j = 0; j < estadoAsientos.GetLength(1); j++)
                {
                    if (estadoAsientos[i, j] == 0)
                    {
                        estadoSesion[i, j] = 'O';
                    }
                    else if(estadoAsientos[i, j] == -1)
                    {
                        estadoSesion[i, j] = ' ';
                    }
                    else
                    {
                        estadoSesion[i, j] = 'X';
                    }
                }                
            }  

            return estadoSesion;
        }

        public string getHora ()
        {
            return hora;
        }

        public int getIdEntrada (int fila, int columna)
        {     
            return estadoAsientos[fila - 1, columna - 1 + CalcularNumeroPasillosHastaLaColumna(fila, columna)];
        }

        public string recogerEntradas (int id)
        {
            string recojerEntradas = null;
            int fila = 0;
            int butaca = 0;
            

            if (0 < id && id < sigIdCompra)
            {
                recojerEntradas = hora;

                for (int i = 0; i < estadoAsientos.GetLength(0); i++)
                {
                    int numeroPasillos = 0;

                    for (int j = 0; j < estadoAsientos.GetLength(1); j++)
                    {                           
                        if (estadoAsientos[i, j] == id)
                        {
                            fila = i + 1;
                            butaca = j + 1 - numeroPasillos;
                            recojerEntradas += "--" + fila + "," + butaca;
                        }
                        else if (estadoAsientos[i, j] == -1)
                        {                       
                            numeroPasillos++;                        
                        }
                    }
                }

                recojerEntradas += "--";
            }      

            return recojerEntradas; //Recoje las entradas a modo de un string y si el id qu le pasas por parámetro no existe te devuelve null
        }

        public ButacasContiguas recomendarButacasContiguas (int noButacas)
        {
            ButacasContiguas butacasContiguasRecomendadas = null;
            int i = (estadoAsientos.GetLength(0) + 1) / 2 + 1;

            for (; i < estadoAsientos.GetLength(0) + 1 && butacasContiguasRecomendadas == null; i++) // Te comprueba desde la fila mitad +1 hasta la última fila de la sesión si hay el numero de butacas contiguas que le has pasado por parámetro
            {
                butacasContiguasRecomendadas = buscarButacasContiguasEnFila(i, noButacas);
            }

            if (butacasContiguasRecomendadas == null) // Si encuentra en el for de arriba el número de butacas contiguas no hace el de abajo
           {
                for (i = (estadoAsientos.GetLength(0) + 1) / 2; i > 0 && butacasContiguasRecomendadas == null; i--)  // Te comprueba desde la fila mitad hasta la primera fila de la sesión si hay el numero de butacas contiguas que le has pasado por parámetro
                {
                    butacasContiguasRecomendadas = buscarButacasContiguasEnFila(i, noButacas);
                }
            }

            return butacasContiguasRecomendadas;
        }   
    }
}
