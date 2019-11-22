using System;
using System.Collections.Generic;
using System.Text;

namespace ViagemNova.Entidades {
    public class Veiculo {
        public string Proprietario { get; set; }//todo ver o que da para  mudar para private 
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public double CapacidadeTanque { get; set; }
        public double CombustivelGasolina { get; set; }
        public double CombustivelAlcool { get; set; }
        public double VelocidadeMaxima { get; set; }
        public string TipoCarro { get; set; }   //  todo ver se realmente vai ser necessario
        public double AutonomiaGasolina { get; set; }
        public double AutonomiaAlcool { get; set; }
        public int StatusPneu { get; set; }
        public double AutonomiaAlcoolVariando { get; set; }
        public double AutonomiaGasolinaVariando { get; set; }
        

    }
}
