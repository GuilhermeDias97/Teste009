using System;
using System.Collections.Generic;
using System.Text;
using ViagemNova.Core;
using ViagemNova.Core.entidades;
using ViagemNova.Utilitarios;

namespace ViagemNova.Entidades.core {
    public class Listas {

        public List<Veiculo> Veiculos { get; private set; } = new List<Veiculo>();
        public List<Percurso> Viagens { get; private set; } = new List<Percurso>();
        public List<CarroPercurso> VeiculosEViagens { get; set; } = new List<CarroPercurso>();
        public List<Relatorio> Relatorios { get; set; } = new List<Relatorio>();
        public List<string> MudancasPneu { get; set; } = new List<string>();
        public List<string> AlteracoesClimaticas { get; set; } = new List<string>();

    }

}
