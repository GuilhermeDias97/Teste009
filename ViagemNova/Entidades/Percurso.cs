using System;
using System.Collections.Generic;
using System.Text;

namespace ViagemNova.Entidades {
    public class Percurso {
        public int CodigoViagem { get; set; } = new Random().Next(1000, 9999);
        public DateTime Data { get; set; }
        public double Distancia { get; set; }
        public string Clima { get; set; }
        public string VariandoClima { get; set; }
        public double DistanciaAlterada { get; set; }
             
        //public Percurso() { // ja tenho esse construtor oculto
      
      //}


    }
}
