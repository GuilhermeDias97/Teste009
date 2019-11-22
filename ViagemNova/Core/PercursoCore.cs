using System;
using ViagemNova.Entidades;
using ViagemNova.Utilitarios;
using ViagemNova.Entidades.core;
using System.Linq;
using System.Globalization;


namespace ViagemNova.Core.entidades {
    public class PercursoCore {
        public Percurso RegistrarViagem() {

            Percurso percurso = new Percurso();
            Console.Write("\nINFORME A DISTANCIA TOTAL DA VIAGEM:");
            percurso.Distancia = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            percurso.DistanciaAlterada = percurso.Distancia;
            Console.Write("\nDIGITE A DATA DESTA VIAGEM (DIA/MÊS/ANO): ");
            percurso.Data = Validacoes.ValidarData(Console.ReadLine());
            Console.Write("\nQUAL A PREVISÃO DO CLIMA PARA ESTA VIAGEM?\nSOL\nCHOVENDO\nNEVANDO\n ");
            percurso.Clima = Validacoes.ValidarClima(Console.ReadLine().ToUpper());
            Console.WriteLine("VIAGEM REGISTRADA");
            return percurso;
        }
        public void MostrarViagens(Listas listas) { // IRA EXIBIR SOMENTE VIAGENS CADASTRADAS NA LISTA DE VIAGENS QUE ESTA EM SISTEMA
            if (listas.Viagens.Count == 0)
                Console.WriteLine("VOCÊ NÃO CADASTROU VIAGENS");
            else if (listas.Viagens.Count >= 1) {
                Console.Write($"\nExiste {listas.Viagens.Count} VIAGENS CADASTRADAS!\n\n");
                foreach (Percurso exibindo in listas.Viagens)
                    Console.WriteLine($"\nCODIGO VIAGEM: {exibindo.CodigoViagem}   DATA: {exibindo.Data.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}    DISTANCIA:{exibindo.DistanciaAlterada}    CLIMA: {exibindo.Clima}");
            }
        }
        public void VariacaoDoClima(Percurso percurso) {
            int random = new Random().Next(1, 4);// TODO TESTAR RANDOM DE 1,4
            percurso.VariandoClima = random == 1 ? "SOL" : random == 2 ? "CHOVENDO" : "NEVANDO";
        }
        //todo status pneu variado
        public void Descalibrar(Veiculo veiculo) {
            int possobilidade = new Random().Next(0, 2); // TODO TESTAR RANDOM DE 0,2 
            if (possobilidade == 0)
                veiculo.StatusPneu--;
        }
        public void RegistrarDistanciaPercorrida(double percorreu, Relatorio relatorio) => relatorio.DistanciaPercorrida = percorreu;// => significa retorno
        public void RegistraQuantidadeDeAbastecimento(int quantidade, Relatorio relatorio) => relatorio.ParadasAbastecimento = quantidade;
        public void RegistroDeCombustivelUtilizado(double totalCombustivelUtiizado, Relatorio relatorio) => relatorio.LitrosConsumido = totalCombustivelUtiizado;
        public void RegistroQuantidadeDeCalibracoes(int qtdcalibracoes, Relatorio relatorio) => relatorio.ParadasCalibragem = qtdcalibracoes;
        public void RegistrandoAlteracoesClimaticas(double percorreu, string clima, Listas listas) {
            string ocorrencia = $"ALTERAÇÃO CLIMÁTICA NO QUILÔMETRO {percorreu}, E CLIMA: {clima}";
            listas.AlteracoesClimaticas.Add(ocorrencia);
        }
        public void AdicionarMudancasPneus(double km, int statusPneu,Listas listas) {
            string pneu = statusPneu == 1 ? "MURCHO" : statusPneu == 2 ? "MODERADO" : "CHEIO";
            string ocorrencia = $"PNEU DESCALIBRADO NO KM {km}, FOI CALIBRADO PARA ({pneu})";
            listas.MudancasPneu.Add(ocorrencia);

        }

        public void CalculandoClimaEPneu(Percurso percurso, Veiculo veiculo) {

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "SOL" && veiculo.StatusPneu == 3) // SOL PNEU BOM
                veiculo.AutonomiaGasolinaVariando = veiculo.AutonomiaGasolina;

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "SOL" && veiculo.StatusPneu == 3)  // SOL PNEU BOM
                veiculo.AutonomiaAlcoolVariando = veiculo.AutonomiaAlcool;

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "SOL" && veiculo.StatusPneu == 2) // SOL PNEU MEDIO
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - (veiculo.AutonomiaGasolina * 0.0725)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "SOL" && veiculo.StatusPneu == 2) // SOL PNEU MEDIO
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaAlcool - (veiculo.AutonomiaAlcool * 0.0725)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "SOL" && veiculo.StatusPneu == 1)// SOL PNEU RUIM
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - (veiculo.AutonomiaGasolina * 0.0915)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "SOL" && veiculo.StatusPneu == 1)// SOL PNEU RUIM
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaAlcool - (veiculo.AutonomiaAlcool * 0.0915)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "CHOVENDO" && veiculo.StatusPneu == 3)// CHOVENDO PNEU BOM
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - (veiculo.AutonomiaGasolina * 0.12)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "CHOVENDO" && veiculo.StatusPneu == 3) // CHOVENDO PNEU BOM
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaGasolinaVariando - (veiculo.AutonomiaGasolinaVariando * 0.30)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "CHOVENDO" && veiculo.StatusPneu == 2) // chovendo pneu medio
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - veiculo.AutonomiaGasolina * (0.12 + 0.0725)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "CHOVENDO" && veiculo.StatusPneu == 2)
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaGasolinaVariando - veiculo.AutonomiaGasolinaVariando * (0.30 + 0.0725)), 2); // choveno pneu medio

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "CHOVENDO" && veiculo.StatusPneu == 1) // chovendo pneu ruim
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - veiculo.AutonomiaGasolina * (0.12 + 0.0915)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "CHOVENDO" && veiculo.StatusPneu == 1) // chovendo pneu ruim
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaGasolinaVariando - veiculo.AutonomiaGasolinaVariando * (0.30 + 0.0915)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "NEVANDO" && veiculo.StatusPneu == 3) // nevando pneu bom
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - (veiculo.AutonomiaGasolina * 0.19)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "NEVANDO" && veiculo.StatusPneu == 3) // nevando pneu bom
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaGasolinaVariando - (veiculo.AutonomiaGasolinaVariando * 0.30)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "NEVANDO" && veiculo.StatusPneu == 2) // nevando pneu medio
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - veiculo.AutonomiaGasolina * (0.19 + 0.0725)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "NEVANDO" && veiculo.StatusPneu == 2) // nevando pneu medio
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaGasolinaVariando - veiculo.AutonomiaGasolinaVariando * (0.30 + 0.0725)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && percurso.Clima == "NEVANDO" && veiculo.StatusPneu == 1) // nevando pneu ruim
                veiculo.AutonomiaGasolinaVariando = Math.Round((veiculo.AutonomiaGasolina - veiculo.AutonomiaGasolina * (0.19 + 0.0915)), 2);

            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && percurso.Clima == "NEVANDO" && veiculo.StatusPneu == 1) // nevando pneu ruim
                veiculo.AutonomiaAlcoolVariando = Math.Round((veiculo.AutonomiaGasolinaVariando - veiculo.AutonomiaGasolinaVariando * (0.30 + 0.0725)), 2); //todo verificar se esta correto


        }

        //estarei colocando dentro da lista 
        public void EscolhendoCarroEViagem(Listas listas, VeiculoCore veiculoCore) {
            if (listas.Veiculos.Count > 0) {

                Veiculo veiculo = veiculoCore.EscolhendoCarroParaDirigir(listas);
                CarroPercurso carroPercurso = new CarroPercurso();

                if (listas.Viagens.Count > 0) {
                    Console.WriteLine("INFORME O CODIGO DA VIAGEM QUE DESEJA DIRIGIR:");
                    MostrarViagens(listas);
                    int codigo = Validacoes.ValidarInteiros(Console.ReadLine());

                    Percurso percurso = listas.Viagens.Find(x => x.CodigoViagem == codigo);

                    carroPercurso.Veiculo = veiculo;
                    carroPercurso.Percurso = percurso;

                    listas.VeiculosEViagens.Add(carroPercurso); // esperado dois parametros conforme construtor criado na classe carro percurso.

                    listas.Veiculos.Remove(veiculo);
                    listas.Viagens.Remove(percurso);

                    Console.WriteLine("\nVIAGEM REGISTRADA COM SUCESSO! \nAGUARDANDO PRÓXIMOS PASSOS...");
                    Console.ReadKey();
                }
                else {
                    Console.WriteLine("VOCÊ PRECISA INSERIR UMA VIAGEM PARA DIRIGIR");
                    Console.ReadKey();
                }
            }
            else {
                Console.WriteLine("CADASTRE UM VEICULO PARA DIRIGIR.\nVOCÊ NÃO CADASTROU NENHUM.");
                Console.ReadKey();
            }
        }
        public void ExibirVeiculosEViagens(Listas listas) { // estarei acessando a lista de viagens que esta em sistema  ela estarão as viagens ja inseridas( viagens programadas)
            if (listas.VeiculosEViagens.Count == 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" VOCÊ NÃO REGISTROU NEM UMA VIAGEM NA LISTA VEICULOS E VIAGENS");
                Console.ResetColor();
            }
            else if (listas.VeiculosEViagens.Count >= 1) {
                Console.WriteLine($"EXISTE {listas.VeiculosEViagens.Count}  REGISTRADA (S)");
                //listas.VeiculosEViagens.ForEach(c => Console.WriteLine(c));

                foreach (CarroPercurso mostrar in listas.VeiculosEViagens) {
                    Console.WriteLine($"\nCODIGO VIAGEM: {mostrar.CodigoId}"); // MOSTRAR APENAS CODIGOS DENTRO DA LIS
                }
            }
        }
        public CarroPercurso EscolherCarroPercurso(Listas listas) { // ACESSAR LISTA DE VEICULOS E VIAGENS como passar parametro somente dentro do metodo partindo automatico

            CarroPercurso carroPercurso = null;

            if (listas.VeiculosEViagens.Count > 0) {
                Console.WriteLine("ESCOLHA UM CODIGO PARA ESTAR INICIANDO: ");
                ExibirVeiculosEViagens(listas);
                Console.WriteLine("INFORME O CÓDIGO DA VIAGEM");
                int codigo = Validacoes.ValidarInteiros(Console.ReadLine());

                carroPercurso = listas.VeiculosEViagens.Find(p => p.CodigoId == codigo); // todo perguntar pq não foi necessario utilizar o apelido "p " neste caso
            }
            return carroPercurso;
        }

        public void Decidindo(Percurso percurso) {
            if (percurso.DistanciaAlterada > 0) {
                Console.ForegroundColor = ConsoleColor.Blue; // todo chamar metodos

                Console.ResetColor();
                int escolher = Validacoes.ValidarInteiros(Console.ReadLine());
                if (escolher == 1) {

                }

            }
        }



    }
}





