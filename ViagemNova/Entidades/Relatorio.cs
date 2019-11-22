using System;

using System.Globalization;
using System.Text;

namespace ViagemNova.Entidades {
    public class Relatorio {
        public CarroPercurso CarroPercurso { get; set; }
        public double LitrosConsumido { get; set; }
        public double DistanciaPercorrida { get; set; }
        public int ParadasAbastecimento { get; set; }
        public int ParadasCalibragem { get; set; }
        public int CodigoId { get; set; } = new Random().Next(1000, 4000);


        public override string ToString() {
            StringBuilder relatorio = new StringBuilder();

            relatorio.AppendLine($" LITROS DE COMBUSTÍVEL CONSUMIDO: {LitrosConsumido.ToString("F2", CultureInfo.InvariantCulture)} - DISTÂNCIA PERCORRIDA: {DistanciaPercorrida} KM");
            relatorio.AppendLine($"PARADAS  ABASTECIMENTO: {ParadasAbastecimento} - PARADAS CALIBRAGEM: {ParadasCalibragem}");
            relatorio.AppendLine($"CLIMA PREVISTO / INICIAL: {CarroPercurso.Percurso.Clima}");
            relatorio.AppendLine("\n-- ALTERAÇÕES  CLIMÁTICAS --");
           // MundancasClimaticas.ForEach(x => relatorio.AppendLine(x));
            relatorio.AppendLine("\n-- DESCALIBRAGENS --");
         //  MudancasPneu .ForEach(x => relatorio.AppendLine(x));
            relatorio.AppendLine($"SITUAÇÃO DA VIAGEM: {(CarroPercurso.Percurso.DistanciaAlterada <= 0 ? "CONCLUIDA" : "NÃO CONCLUIDA")}");
            return relatorio.ToString();
        }





    }
}


