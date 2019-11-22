using System;
using System.Collections.Generic;
using System.Text;
using ViagemNova.Core.entidades;

namespace ViagemNova.Entidades {
    public class CarroPercurso {


        // public string StatusPneu { get; set; }
        public int CodigoId { get; set; } = new Random().Next(1000, 9999);
        public Veiculo Veiculo { get; set; }
        public Percurso Percurso { get; set; }
        public CarroPercurso(Veiculo veiculo, Percurso percurso) { // TODO VER SE REALMENTE É NECESSARIO.
            Veiculo = veiculo;
            Percurso = percurso;
        }

        public CarroPercurso() {
        }
    }
}
