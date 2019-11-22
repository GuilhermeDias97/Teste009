using System;
using ViagemNova.Entidades;
using ViagemNova.Utilitarios;
using System.Globalization;
using ViagemNova.Entidades.core;
using System.Linq;

namespace ViagemNova.Core.entidades {
    public class VeiculoCore { // para alterar
        public Veiculo CadastrarCarro() {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Veiculo veiculo = new Veiculo();

            Console.Write("\nDIGITE O NOME DO PROPRIETARIO:");
            veiculo.Proprietario = Validacoes.ValidarLetras(Console.ReadLine());
            Console.Write("INFORME O MODELO DO CARRO:");
            veiculo.Modelo = Console.ReadLine();
            Console.Write("INFORME A MARCA: ");
            veiculo.Marca = Validacoes.ValidarLetras(Console.ReadLine());
            Console.Write("INFORME O ANO DO VEICULO ENTRE 1900 E 2020: ");
            veiculo.Ano = Validacoes.ValidarAnoDoCarro(Console.ReadLine()); // TODO deve estar em tipo int saber pq não é aceito
            Console.Write("INFORME A PLACA: ");
            veiculo.Placa = Validacoes.ValidarPlaca(Console.ReadLine().ToUpper());
            Console.Write("INFORME A CAPACIDADE MÁXIMA DO TANQUE DE COMBUSTIVEL EM LITROS:");
            veiculo.CapacidadeTanque = Validacoes.ValidarDouble(Console.ReadLine());
            Console.Write("INFORME A VELOCIDADE MAXIMA DO SEU CARRO:");
            veiculo.VelocidadeMaxima = Validacoes.ValidarDouble(Console.ReadLine());
            Console.Clear();
            Console.Write("SEU CARRO É :\n\tFLEX\n\tGASOLINA\n\tALCOOL\n\n\t");
            Console.ResetColor();
            veiculo.TipoCarro = Validacoes.ValidarTipoCombustivel(Console.ReadLine().ToUpper());

            if (veiculo.TipoCarro == "FLEX") {
                Console.WriteLine("QUANTOS QUILÔMETROS SEU CARRO FAZ COM 1 LITRO DE GASOLINA?");
                veiculo.AutonomiaGasolina = Validacoes.ValidarInteiros(Console.ReadLine());
                Console.WriteLine("QUANTOS QUILÔMETROS SEU CARRO FAZ COM 1 LITRO DE ALCOOL");
                veiculo.AutonomiaAlcool = Validacoes.ValidarInteiros(Console.ReadLine());
            }
            else if (veiculo.TipoCarro == "GASOLINA") {
                Console.WriteLine("QUANTOS QUILÔMETROS SEU CARRO FAZ COM 1 LITRO DE GASOLINA?");
                veiculo.AutonomiaGasolina = Validacoes.ValidarInteiros(Console.ReadLine());
            }
            else {
                Console.WriteLine("QUANTOS QUILÔMETROS SEU CARRO FAZ COM 1 LITRO DE ALCOOL");
                veiculo.AutonomiaAlcool = Validacoes.ValidarInteiros(Console.ReadLine());
            }
            StatusDoPneu(veiculo);


            Console.WriteLine("CARRO CADASTRADO  E ADICIONADO NA LISTA COM SUCESSO");
            return veiculo;
        }

        // define status do pneu
        public void StatusDoPneu(Veiculo veiculo) { // prestar atenção com tabela verdade para dar certo

            Console.WriteLine("IFORME A SITUAÇÃO DOS PNEUS DO SEU CARRO: \n[3]- ORIGINAL E CHEIO \n[2]- RÉPLICA E MUCHO\n[1]- PNEUS VULCANIZADOS E VAZANDO");
            veiculo.StatusPneu = Validacoes.ValidarPneu(Console.ReadLine());
            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && veiculo.StatusPneu == 3)
                Console.WriteLine("PIRATARIA É CRIME, SEMPRE OPTE POR PNEUS ORIGINAIS PARA A SUA SEGURANÇA!");

            else if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && veiculo.StatusPneu == 2) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("VOCÊ ESTA COLOCANDO EM RISCO SUA VIDA E DE MAIS MOTORISTAS\n\n SUA AUTONOMIA SERÁ REDUZIDA EM 7,25%");
                veiculo.AutonomiaGasolinaVariando = veiculo.AutonomiaGasolinaVariando - veiculo.AutonomiaGasolina * 0.0725;
            }
            else if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA" && veiculo.StatusPneu == 1) {
                Console.WriteLine("PNEUS COM GRANDE RISCO DE ESTOURAR ENTRETANDO DA PARA SEGUIR VIAGEM\n CUIDADO!\n\n SUA AUTONOMIA SERÁ REDUZIDA EM 9,15%");
                veiculo.AutonomiaGasolinaVariando = veiculo.AutonomiaGasolinaVariando - veiculo.AutonomiaGasolina * 0.0915;
                Console.ResetColor();
            }
            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && veiculo.StatusPneu == 3)
                Console.WriteLine("PIRATARIA É CRIME, SEMPRE OPTE POR PNEUS ORIGINAIS PARA A SUA SEGURANÇA!");
            else if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL" && veiculo.StatusPneu == 2) {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("VOCÊ ESTA COLOCANDO EM RISCO SUA VIDA E DE MAIS MOTORISTAS\n\n SUA AUTONOMIA SERÁ REDUZIDA EM 7,25%");
                veiculo.AutonomiaAlcoolVariando = veiculo.AutonomiaAlcoolVariando - veiculo.AutonomiaAlcool * 0.0725;
            }
            Console.ReadKey();
        }
        public void Abastecendo(Listas listas) {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nBEM VINDO AO POSTO DEV4JOBS\n");
            Console.ResetColor();
            ExibirCarrosCadastrados(listas);
            Console.WriteLine("INFORME APLACA DO CARRO QUE DESEJA ABASTECER:");

            string placa = Validacoes.ValidarPlaca(Console.ReadLine().ToUpper());// localizar carro
            Veiculo veiculo = listas.Veiculos.Where(p => p.Placa == placa).FirstOrDefault();

            if (veiculo.TipoCarro == "FLEX") {// TODO definir se é flex, alcool ou gasolina.
                Console.WriteLine($"\tO QUE  VOCÊ PRETENDE ABASTECER?\n\tA CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque} LITROS\n\t" +
                                  $"VOCÊ QUER ABASTECER:\n\tGASOLINA\n\tALCOOL\n");
                string escolhercombustivel = Validacoes.ValidarTipoCombustivel(Console.ReadLine().ToUpper());

                if (escolhercombustivel == "GASOLINA") {

                    Console.WriteLine($"A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque } LITROS \n ");

                    if (veiculo.CombustivelAlcool + veiculo.CombustivelGasolina > veiculo.CapacidadeTanque) {
                        Console.WriteLine("VOCÊ ATINGIU O LIMITE DO SEU TANQUE E NÃO PODE ABASTECER MAIS \n TENTE NOVAMENTE!");
                        Console.ReadLine();
                        return; //  sair
                    }
                    else
                        AbastecerOuCompletaGasolina(veiculo);
                }
                if (escolhercombustivel == "ALCOOL") { // não posso colocar else if pois else não pode iniciar 
                    AbastecerOuCompletarAlcool(veiculo);
                }
                if (veiculo.CombustivelAlcool + veiculo.CombustivelGasolina > veiculo.CapacidadeTanque) {

                    Console.WriteLine("VOCÊ ATINGIU O LIMITE DO SEU TANQUE E NÃO PODE ABASTECER MAIS\n TENTE NOVAMENTE.");
                    Console.ReadLine();
                    return; // quebrar e sair
                }
            }
            if (veiculo.TipoCarro == "GASOLINA") // carro tipo gasolina irei perguntar se quer completar ou não
                AbastecerOuCompletaGasolina(veiculo);
            if (veiculo.TipoCarro == "ALCOOL") {
                Console.WriteLine($"CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque} LITROS\n\tVOCÊ QUER COMPLETAR O TANQUE (S OU N)"); // tipo Alcool // caso não queira completar combustivel do carro a alcool

                AbastecerOuCompletarAlcool(veiculo);
            }
        }
        public void AbastecerOuCompletaGasolina(Veiculo veiculo) {
            double qtd = 0.0;
            do {
                Console.WriteLine("DESEJA COMPLETAR O TANQUE, (S/N)");
                string simounao = Validacoes.SimOuNao(Console.ReadLine().ToUpper());

                if (simounao == "S") {// todo validar 
                    double completandogasolina = (veiculo.CombustivelAlcool - veiculo.CombustivelGasolina);
                    completandogasolina = veiculo.CapacidadeTanque - completandogasolina;
                    Console.WriteLine($"A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque.ToString("F2", CultureInfo.InvariantCulture)} LITROS\n");
                    veiculo.CombustivelGasolina = Math.Abs(completandogasolina);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"SEU TANQUE ESTA COMPLETO COM {veiculo.CombustivelGasolina.ToString("F2", CultureInfo.InvariantCulture)} LITROS DE GASOLINA ");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else {

                    Console.WriteLine($"A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque} LITROS\n" +
                                      $" QUANTOS LITROS VOCÊ DESEJA ABASTECER DE GASOLINA?\n");

                    qtd = Validacoes.ValidarInteiros(Console.ReadLine());

                    if (veiculo.CombustivelAlcool + veiculo.CombustivelGasolina + qtd > veiculo.CapacidadeTanque) {
                        Console.WriteLine("VOCÊ ATINGIU O LIMITE DO SEU TANQUE E NÃO PODE ABASTECER MAIS");
                        return; // quebrar
                    }
                    if (veiculo.CombustivelGasolina + qtd <= veiculo.CapacidadeTanque) {
                        veiculo.CombustivelGasolina += qtd;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"VOCÊ ABASTECEU {veiculo.CombustivelGasolina.ToString("F2", CultureInfo.InvariantCulture)} LITROS DE GASOLINA");
                        Console.ResetColor();
                        Console.ReadLine();
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"SEU TANQUE NÃO SUPORTA ESSA QUANTIDADE\n A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque - veiculo.CombustivelAlcool - veiculo.CombustivelGasolina} LITROS");
                        Console.WriteLine($"VOCÊ TEM EM SEU TANQUE {veiculo.CombustivelAlcool - veiculo.CombustivelGasolina} LITROS DISPONIVEIS OU SEJA:\n ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nVOCÊ PODE ABASTECER {veiculo.CapacidadeTanque - veiculo.CombustivelAlcool - veiculo.CombustivelGasolina} LITROS  PARA COMPLETAR O TANQUE\n");
                        Console.ResetColor();
                        qtd = 2000;
                    }
                }
            }
            while (qtd == 2000); // enquanto
            return;
        }
        public void AbastecerOuCompletarAlcool(Veiculo veiculo) {
            Console.WriteLine("DESEJA COMPLETAR SEU COMBUSTIVEL DE AlCOOL? \n (S / N)");
            double qtd = 0.0;
            do {
                string simounao = Validacoes.SimOuNao(Console.ReadLine().ToUpper());

                if (simounao == "S") {// todo validar 
                    double completandoalcool = (veiculo.CombustivelAlcool - veiculo.CombustivelGasolina);
                    completandoalcool = veiculo.CapacidadeTanque - completandoalcool;
                    Console.WriteLine($"A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque.ToString("F2", CultureInfo.InvariantCulture)} LITROS\n");
                    veiculo.CombustivelAlcool = Math.Abs(completandoalcool);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"SEU TANQUE ESTA COMPLETO COM {veiculo.CombustivelAlcool.ToString("F2", CultureInfo.InvariantCulture)} LITROS DE ALCOOL ");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                else {
                    Console.WriteLine($"A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque} LITROS\n" +
                  $" QUANTOS LITROS VOCÊ DESEJA ABASTECER DE ALCOOL?\n");

                    qtd = Validacoes.ValidarInteiros(Console.ReadLine());

                    if (veiculo.CombustivelAlcool + veiculo.CombustivelGasolina + qtd > veiculo.CapacidadeTanque) {
                        Console.WriteLine("VOCÊ ATINGIU O LIMITE DO SEU TANQUE E NÃO PODE ABASTECER MAIS");
                        return; // quebrar
                    }
                    if (veiculo.CombustivelAlcool + qtd <= veiculo.CapacidadeTanque) {
                        veiculo.CombustivelAlcool += qtd;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"VOCÊ ABASTECEU {veiculo.CombustivelAlcool.ToString("F2", CultureInfo.InvariantCulture)} LITROS DE ALCOOL");
                        Console.ResetColor();

                        Console.ReadLine();
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"SEU TANQUE NÃO SUPORTA ESSA QUANTIDADE\n A CAPACIDADE DO SEU TANQUE É DE {veiculo.CapacidadeTanque - veiculo.CombustivelAlcool - veiculo.CombustivelGasolina} LITROS");
                        Console.WriteLine($"VOCÊ TEM EM SEU TANQUE {veiculo.CombustivelAlcool - veiculo.CombustivelGasolina} LITROS DISPONIVEIS OU SEJA:\n ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nVOCÊ PODE ABASTECER {veiculo.CapacidadeTanque - veiculo.CombustivelAlcool - veiculo.CombustivelGasolina} LITROS  PARA COMPLETAR O TANQUE\n");
                        Console.ResetColor();
                        qtd = 2000;
                    }
                }
            }
            while (qtd == 2000); // enquanto
            return;
        }
        public void ExibirCarrosCadastrados(Listas listas) {
            Console.ForegroundColor = ConsoleColor.Yellow;

            if (listas.Veiculos.Count == 0)
                Console.WriteLine("VOCÊ NÃO CADASTROU VEICULOS");
            if (listas.Veiculos.Count >= 1) {
                Console.WriteLine($"VOCÊ CADASTROU {listas.Veiculos.Count} VEICULOS\n\n");
                foreach (Veiculo exibindo in listas.Veiculos)
                    Console.WriteLine($"\nPROPRIETÁRIO: {exibindo.Proprietario} , MODELO:{exibindo.Modelo} , MARCA: {exibindo.Marca} CARRO:{exibindo.TipoCarro}, ANO: {exibindo.Ano}, PLACA: {exibindo.Placa}, VELOCIDADE MAXIMA: {exibindo.VelocidadeMaxima.ToString("F2", CultureInfo.InvariantCulture)} Km\nCAPACIDADE TANQUE: {exibindo.CapacidadeTanque.ToString("F2", CultureInfo.InvariantCulture)} LITROS\n\n" +
                                      $" AUTONOMIA GASOLINA:{exibindo.AutonomiaGasolina.ToString("F2", CultureInfo.InvariantCulture)}  Km/L \n AUTONOMIA ALCOOL: {exibindo.AutonomiaAlcool.ToString("F1", CultureInfo.InvariantCulture)}  Km/L  \n COMBUSTIVEL: {exibindo.CombustivelAlcool} LITROS DE ALCOOL \n\nCOMBUSTIVEL:{exibindo.CombustivelGasolina} LITROS DE GASOLINA ");
            }
            Console.ResetColor();
            Console.ReadKey();
        }
        public void CalibrarPneu(Veiculo veiculo) {
            Console.WriteLine("\tBEM VINDO AO POSTO DEV4JOBS: \n\n");
            Console.Write("\n SEU PNEU ESTÁ MURCHO!\n COMO DESEJA CALIBRAR OS PNEUS?\n[1]-MODERADO\n[2]-CHEIO\n[3]-TROCAR POR NOVOS: ");
            veiculo.StatusPneu = Validacoes.ValidarPneu(Console.ReadLine());
        }
        public void AutonomiaClima(Percurso percurso, Veiculo veiculo) {
            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "GASOLINA") {
                if (percurso.Clima.Equals("ENSOLARADO"))
                    veiculo.AutonomiaGasolinaVariando = veiculo.AutonomiaGasolina;
                else if (percurso.Clima.Equals("CHOVENDO"))
                    veiculo.AutonomiaGasolinaVariando = veiculo.AutonomiaGasolina - veiculo.AutonomiaGasolina * 0.12;
                else
                    veiculo.AutonomiaGasolinaVariando = veiculo.AutonomiaGasolina - veiculo.AutonomiaGasolina * 0.19;
            }
            if (veiculo.TipoCarro == "FLEX" || veiculo.TipoCarro == "ALCOOL") {
                if (percurso.Clima.Equals("ENSOLARADO"))
                    veiculo.AutonomiaAlcoolVariando = veiculo.AutonomiaAlcool;
                else if (percurso.Clima.Equals("CHOVENDO"))
                    veiculo.AutonomiaAlcoolVariando = veiculo.AutonomiaAlcool - veiculo.AutonomiaAlcool * 0.42;
                else
                    veiculo.AutonomiaAlcoolVariando = veiculo.AutonomiaAlcool - veiculo.AutonomiaAlcool * 0.49;
            }
        }

        //TODO TENTAR OVERRIDE TO STRING
        public Veiculo EscolhendoCarroParaDirigir(Listas listas) {

            ExibirCarrosCadastrados(listas);

            Console.WriteLine("DIGITE A PLACA DO CARRO: ");
            string placa = Validacoes.ValidarPlaca(Console.ReadLine().ToUpper());// localizar carro
            Veiculo veiculo = listas.Veiculos.Where(p => p.Placa == placa).FirstOrDefault();
            Console.WriteLine($"O bCARRO ESCOLHIDO FOI\n MODELO: {veiculo.Modelo}\nMARCA: {veiculo.Marca}\nPLACA: {veiculo.Placa}\nTIPO DE COMBUSTIVEL: {veiculo.TipoCarro} VELOCIDADE: {veiculo.VelocidadeMaxima}\nCOMBUSTIVEL ALCOOL:{veiculo.CombustivelAlcool} Litros\nCOMBUSTIVEL GASOLINA: {veiculo.CombustivelGasolina} Litros"); // PODERIA DAR UM OVER RIDE TBEM
            Console.ReadKey();

            return veiculo;
        }
        public void PartindoManual(PercursoCore percursocore, Listas listas, CarroPercurso carroPercurso) {

            double percorreu = 0.0;
            int QtdAbastecimento = 0;
            int QtdCalibragem = 0;
            double combustivelgasto = 0.0;
            Relatorio relatorio = new Relatorio();

            void CarregarRelatorio() {
                percursocore.RegistraQuantidadeDeAbastecimento(QtdAbastecimento, relatorio);
                percursocore.RegistrarDistanciaPercorrida(percorreu, relatorio);
                percursocore.RegistroQuantidadeDeCalibracoes(QtdCalibragem, relatorio);
                percursocore.RegistroDeCombustivelUtilizado(combustivelgasto, relatorio);
                percursocore.RegistrandoAlteracoesClimaticas(percorreu, carroPercurso.Percurso.Clima, listas);
            }
            while (carroPercurso.Percurso.DistanciaAlterada > 0.0) {

                Console.WriteLine("QUANTOS QUILÔMETROS VOCÊ DESEJA PERCORRER?");
                double dirigindomanual = Validacoes.ValidarDouble(Console.ReadLine());

                for (int i = 1; i <= dirigindomanual; i++) {

                    percursocore.CalculandoClimaEPneu(carroPercurso.Percurso, carroPercurso.Veiculo);

                    if (carroPercurso.Veiculo.TipoCarro == "FLEX" && carroPercurso.Veiculo.CombustivelAlcool == 0.0 && carroPercurso.Veiculo.CombustivelGasolina == 0.0) {
                        Console.WriteLine("O VEICULO ESTA SEM COMBUSTIVEL DESEJA ABASTECER(S/N)");
                        string queroabastecer = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                        if (queroabastecer == "S") {
                            AbastecerOuCompletarAlcool(carroPercurso.Veiculo);
                            AbastecerOuCompletaGasolina(carroPercurso.Veiculo);
                            QtdAbastecimento++;
                        }
                        else if (queroabastecer == "N") {
                            Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                            CarregarRelatorio();
                            break;
                        }
                        if (carroPercurso.Veiculo.StatusPneu == 0) {
                            Console.WriteLine("VOCÊ PRECISA CALIBRAR O PNEU PARA PROSSEGUIR VIAGEM\n DESEJA CALIBRAR (S/N) ");
                            string decidindo = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                            if (decidindo == "S") {
                                CalibrarPneu(carroPercurso.Veiculo);
                                QtdCalibragem++;
                            }
                            else {
                                Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                                CarregarRelatorio();
                                break;
                            }
                        }
                    }
                    else if (carroPercurso.Veiculo.TipoCarro == "GASOLINA" && carroPercurso.Veiculo.CombustivelGasolina == 0.0) { // tabela verdade
                        Console.WriteLine("O VEICULO ESTA SEM COMBUSTIVEL DESEJA ABASTECER(S/N)");
                        string decidindo = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                        if (decidindo == "S") {
                            AbastecerOuCompletaGasolina(carroPercurso.Veiculo);
                            QtdAbastecimento++;
                        }
                        else if (decidindo == "N") {
                            Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                            CarregarRelatorio();
                            break;
                        }
                    }
                    if (carroPercurso.Veiculo.StatusPneu == 0) {
                        Console.WriteLine("VOCÊ PRECISA CALIBRAR O PNEU PARA PROSSEGUIR VIAGEM\n DESEJA CALIBRAR (S/N) ");
                        string quero = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                        if (quero == "S") {
                            CalibrarPneu(carroPercurso.Veiculo);
                            QtdCalibragem++;
                        }
                        else {
                            Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                            CarregarRelatorio();
                            break;
                        }
                    }

                    if (carroPercurso.Veiculo.TipoCarro == "ALCOOL" && carroPercurso.Veiculo.CombustivelAlcool == 0.0) { // tabela verdade
                        Console.WriteLine("O VEICULO ESTA SEM COMBUSTIVEL DESEJA ABASTECER(S/N)");
                        string decidindo = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                        if (decidindo == "S") {
                            AbastecerOuCompletarAlcool(carroPercurso.Veiculo);
                            QtdAbastecimento++;
                        }
                        else if (decidindo == "N") {
                            Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                            CarregarRelatorio();
                            break;
                        }
                    }
                    if (carroPercurso.Veiculo.StatusPneu == 0) {
                        Console.WriteLine("VOCÊ PRECISA CALIBRAR O PNEU PARA PROSSEGUIR VIAGEM\n DESEJA CALIBRAR (S/N) ");
                        string x = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                        if (x == "S") {
                            CalibrarPneu(carroPercurso.Veiculo);
                            QtdAbastecimento++;
                        }
                        else {
                            Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                            CarregarRelatorio();
                            break;
                        }
                    }
                    if (carroPercurso.Percurso.DistanciaAlterada <= 0.0)
                        break;

                    carroPercurso.Percurso.DistanciaAlterada -= 1.0;
                    percorreu++;

                    if (carroPercurso.Veiculo.TipoCarro == "FLEX" || carroPercurso.Veiculo.TipoCarro == "ALCOOL") {
                        carroPercurso.Veiculo.CombustivelAlcool -= 1.0 / carroPercurso.Veiculo.AutonomiaAlcoolVariando;
                        combustivelgasto += 1.0 / carroPercurso.Veiculo.AutonomiaAlcoolVariando;
                    }
                    if (carroPercurso.Veiculo.TipoCarro == "FLEX" || carroPercurso.Veiculo.TipoCarro == "GASOLINA") {
                        carroPercurso.Veiculo.CombustivelGasolina -= 1.0 / carroPercurso.Veiculo.AutonomiaGasolinaVariando;
                        combustivelgasto += 1.0 / carroPercurso.Veiculo.AutonomiaGasolinaVariando;
                    }
                    if (percorreu % 100.0 == 0 && percorreu > 0) { // todo saber pq esta contando o zero
                        Console.WriteLine("VOCÊ PERCORREU 100 KM");
                        percursocore.CalculandoClimaEPneu(carroPercurso.Percurso, carroPercurso.Veiculo);
                        percursocore.RegistrandoAlteracoesClimaticas(percorreu, carroPercurso.Percurso.Clima, listas);
                        percursocore.Descalibrar(carroPercurso.Veiculo);
                    }
                }

                if (carroPercurso.Percurso.DistanciaAlterada <= 0.0) break;
                Console.Write($" ATÉ AGORA VOCÊ PERCORREU {percorreu.ToString("F2", CultureInfo.InvariantCulture)} KM !\nDESEJA CONTINUAR A VIAGEM? (S/N): ");
                string opcao = Validacoes.SimOuNao(Console.ReadLine());
                if (opcao != "S") return;
            }
            CarregarRelatorio();
            listas.Relatorios.Add(relatorio);
            Console.WriteLine("VIAGEM CONCLUIDA COM SUCESSO!\nOBRIGADO!");
        }
        public void PartindoAutomatico(CarroPercurso carroPercurso, Listas listas, PercursoCore percursoCore) { // TODO ESTUDAR FUNÇÕES LOCAIS E IMPLEMENTAR. DIMINUIR METODO.
            double percorreu = 0.0;
            double cont = 0.;
            int QtdAbastecimento = 0;
            int QtdCalibragem = 0;
            double combustivelgasto = 0.0;
            Relatorio relatorio = new Relatorio(); // instanciar um relatório para usalo para adicionar alterações durante o percurso
            relatorio.CarroPercurso = carroPercurso;

            void CarregarRelatorio() {
                percursoCore.RegistraQuantidadeDeAbastecimento(QtdAbastecimento, relatorio);
                percursoCore.RegistrarDistanciaPercorrida(percorreu, relatorio);
                percursoCore.RegistroQuantidadeDeCalibracoes(QtdCalibragem, relatorio);
                percursoCore.RegistroDeCombustivelUtilizado(combustivelgasto, relatorio);
                percursoCore.RegistrandoAlteracoesClimaticas(percorreu, carroPercurso.Percurso.Clima, listas);
            }
            while (carroPercurso.Percurso.DistanciaAlterada > 0.0) {

                percursoCore.CalculandoClimaEPneu(carroPercurso.Percurso, carroPercurso.Veiculo);

                if (carroPercurso.Veiculo.TipoCarro == "FLEX" && carroPercurso.Veiculo.CombustivelAlcool == 0.0 && carroPercurso.Veiculo.CombustivelGasolina == 0.0) { // tabela verdade
                   
                    
                        Console.WriteLine("O VEICULO ESTA SEM COMBUSTIVEL DESEJA ABASTECER(S/N)");
                        string queroabastecer = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                        if (queroabastecer == "S") {
                            AbastecerOuCompletarAlcool(carroPercurso.Veiculo);
                            AbastecerOuCompletaGasolina(carroPercurso.Veiculo);
                            QtdAbastecimento++;
                        }
                        else if (queroabastecer == "N") {
                            Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                            CarregarRelatorio();
                            return;
                        }
                        if (carroPercurso.Veiculo.StatusPneu == 0) {
                            Console.WriteLine("VOCÊ PRECISA CALIBRAR O PNEU PARA PROSSEGUIR VIAGEM\n DESEJA CALIBRAR (S/N) ");
                            string decidindo = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                            if (decidindo == "S") {
                                CalibrarPneu(carroPercurso.Veiculo);
                                QtdCalibragem++;
                            }
                            else {
                                Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                                CarregarRelatorio();
                            return;
                            }
                        }

                    
                }
                else if (carroPercurso.Veiculo.TipoCarro == "GASOLINA" && carroPercurso.Veiculo.CombustivelGasolina == 0.0) { // tabela verdade
                    Console.WriteLine("O VEICULO ESTA SEM COMBUSTIVEL DESEJA ABASTECER(S/N)");
                    string queroabastecer = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                    if (queroabastecer == "S") {
                        AbastecerOuCompletaGasolina(carroPercurso.Veiculo);
                        QtdAbastecimento++;
                    }
                    else if (queroabastecer == "N") {
                        Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                        CarregarRelatorio();
                        break;
                    }
                }
                if (carroPercurso.Veiculo.StatusPneu == 0) {
                    Console.WriteLine("VOCÊ PRECISA CALIBRAR O PNEU PARA PROSSEGUIR VIAGEM\n DESEJA CALIBRAR (S/N) ");
                    string decidindo = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                    if (decidindo == "S") {
                        CalibrarPneu(carroPercurso.Veiculo);
                        QtdCalibragem++;
                    }
                    else {
                        Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                        CarregarRelatorio();
                        break;
                    }
                }

                if (carroPercurso.Veiculo.TipoCarro == "ALCOOL" && carroPercurso.Veiculo.CombustivelAlcool == 0.0) { // tabela verdade
                    Console.WriteLine("O VEICULO ESTA SEM COMBUSTIVEL DESEJA ABASTECER(S/N)");
                    string queroabastecer = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                    if (queroabastecer == "S") {
                        AbastecerOuCompletarAlcool(carroPercurso.Veiculo);
                        QtdAbastecimento++;
                    }
                    else if (queroabastecer == "N") {
                        Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                        CarregarRelatorio();
                        return;
                    }
                }
                if (carroPercurso.Veiculo.StatusPneu == 0) {
                    Console.WriteLine("VOCÊ PRECISA CALIBRAR O PNEU PARA PROSSEGUIR VIAGEM\n DESEJA CALIBRAR (S/N) ");
                    string decidindo = Validacoes.SimOuNao(Console.ReadLine().ToUpper());
                    if (decidindo == "S") {
                        CalibrarPneu(carroPercurso.Veiculo);
                        QtdAbastecimento++;
                    }
                    else {
                        Console.WriteLine("VEICULO INCAPAZ DE PROSSEGUIR VIAGEM");
                        CarregarRelatorio();
                        return;
                    }
                }
                carroPercurso.Percurso.DistanciaAlterada -= 0.1;

                if (carroPercurso.Veiculo.TipoCarro == "FLEX" || carroPercurso.Veiculo.TipoCarro == "ALCOOL") {
                    carroPercurso.Veiculo.CombustivelAlcool -= 0.1 / carroPercurso.Veiculo.AutonomiaAlcoolVariando;
                    combustivelgasto += 0.1 / carroPercurso.Veiculo.AutonomiaAlcoolVariando;
                }
                if (carroPercurso.Veiculo.TipoCarro == "FLEX" || carroPercurso.Veiculo.TipoCarro == "GASOLINA") {
                    carroPercurso.Veiculo.CombustivelGasolina -= 0.1 / carroPercurso.Veiculo.AutonomiaGasolinaVariando;
                    combustivelgasto += 0.1 / carroPercurso.Veiculo.AutonomiaGasolinaVariando;
                }
                percorreu += 0.1;
                cont += 0.1;
                // todo aumentar cont ==100 valor para 100 depois
                if (cont >= 1.0) {
                    Console.WriteLine("VOCÊ PERCORREU 100 KM");
                    percursoCore.VariacaoDoClima(carroPercurso.Percurso);
                    percursoCore.RegistrandoAlteracoesClimaticas(percorreu, carroPercurso.Percurso.Clima, listas);
                    percursoCore.Descalibrar(carroPercurso.Veiculo);
                    cont = 0.0;
                    Console.ReadKey();
                    CarregarRelatorio(); // TODO CARRO PERCURSO FICA NULO
                    listas.Relatorios.Add(relatorio);
                    Console.WriteLine("VIAGEM CONCLUIDA COM SUCESSO!\nOBRIGADO!");
                    Console.ReadLine();
                    return;
                }
            }




        }


    }
}





