using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cine
{
    public class ButacasContiguas
    {
        private int fila, columna, noButacas;

        public ButacasContiguas(int fila, int columna, int noButacas)
        {
            this.fila = fila;
            this.columna = columna;
            this.noButacas = noButacas;
        }

        public int getFila()
        {
            return fila;
        }

        public int getColumna()
        {
            return columna;
        }

        public int getNoButacas()
        {
            return noButacas;
        }
    }
}
