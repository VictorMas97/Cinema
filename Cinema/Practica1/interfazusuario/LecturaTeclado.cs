using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace interfazUsuario

{
    public class LecturaTeclado
    {
        public int leerNatural(string msg){
		    string entrada = null;
            int nValor = 0;
		    do {
			    Console.Write(msg);
                entrada = Console.ReadLine();
            } while (!int.TryParse(entrada, out nValor) || nValor == 0);	
		    return nValor;
	    }

        public char leerCaracter(string msg){
		    string entrada = null;
		    do {
			    Console.Write(msg);
			    entrada = Console.ReadLine();
		    } while ((entrada.Length > 1));	
		    return entrada[0];
	    }
    }
}
