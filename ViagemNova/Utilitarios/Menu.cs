using System;
using ViagemNova.Core;
using ViagemNova.Core.entidades;
using ViagemNova.Entidades;
using ViagemNova.Entidades.core;
using System.Linq;


namespace ViagemNova.Utilitarios {
    public class Menu {

        internal static void MenuPrincipal(VeiculoCore veiculoCore, Listas listas, PercursoCore percursoCore) {
            // todo dividir menu em submenus

            while (true) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\t SEJA BEM VINDO AO NOSSO SISTEMA.\n"); // todo definir um nome para o sistema
                Console.ResetColor();
                Console.WriteLine("====================INFORMAÇÕES CARRO====================\n\n");
                Console.WriteLine("O QUE DESEJA FAZER?\n ");
                Console.WriteLine(" [A] - CADASTRAR CARRO ");
                Console.WriteLine(" [B] - ABASTECER");
                Console.WriteLine(" [C] - ESCOLHER UM CARRO DOS CARROS CADASTRADOS ");
                Console.WriteLine(" [D] - MOSTRAR DADOS DO CARRO CADASTRADO");
                Console.WriteLine(" [E] - INSERIR INFORMAÇÕES DA VIAGEM\n\n");

                Console.WriteLine(" [ESC] - SAIR");

                ConsoleKeyInfo tecla = Console.ReadKey(); // armazena uma tecla
                switch (tecla.Key) {

                    case ConsoleKey.Escape:
                        Console.WriteLine("\n OOBRIGADO! PRESSIONE ENTER PARA SAIR..");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.A:
                        Console.WriteLine("\nQUANTOS CARROS PRETENDE CADASTRAR:\n");
                        int qtd = Validacoes.ValidarInteiros(Console.ReadLine());
                        for (int i = 1; i <= qtd; i++) {
                            Console.WriteLine($"DIGITE DADOS DO {i}º CARRO:\n");
                            listas.Veiculos.Add(veiculoCore.CadastrarCarro());


                        }
                        break;

                    case ConsoleKey.B:

                        veiculoCore.Abastecendo(listas); // ESCOHER CARRO PELA PLACA E ABASTECER 

                        Console.ReadKey();

                        break;

                    case ConsoleKey.C:
                        Veiculo veiculo = veiculoCore.EscolhendoCarroParaDirigir(listas); // ESTAREI ESCOLHENDO UM CARRO DENTRO DA MINHA LISTA PARA DIRIGIR

                        break;

                    case ConsoleKey.D:
                        veiculoCore.ExibirCarrosCadastrados(listas); // EXIBIR MINHA LISTA DE CARROS CADASTRADOS
                        Console.ReadKey();


                        break;

                    case ConsoleKey.E:
                        Console.Clear();

                        SubMenuViagem(listas, percursoCore, veiculoCore);

                        break;

                    default:
                        Console.Write("\nVALOR INVÁLIDO, PRESSIONE QUALQUER TECLA PARA REINICIAR");
                        Console.ReadKey();


                        break;

                }
            }
        }
        internal static void SubMenuViagem(Listas listas, PercursoCore percursoCore, VeiculoCore veiculoCore) {
            Console.WriteLine("====================INFORMAÇÕES VIAGEM====================\\n\n");

            Console.WriteLine(" [A] - CADASTRAR VIAGEM ");// adicionar viagem na lista de viagens
            Console.WriteLine(" [B] - EXIBIR VIAGENS ");
            Console.WriteLine(" [C] - PLANEJAR VIAGEM \n");
            Console.WriteLine(" [D] - VIAJAR");
            Console.WriteLine(" [E] - EXIBIR RELATÓRIOS\n");

            Console.WriteLine(" [ESC] - SAIR");

            while (true) {

                ConsoleKeyInfo tecla = Console.ReadKey(); // armazena uma tecla
                switch (tecla.Key) {

                    case ConsoleKey.Escape:
                        Console.WriteLine("\n OOBRIGADO! PRESSIONE ENTER PARA SAIR..");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.A:
                        // registra e adiciona uma viagem dentro da lista
                        Console.WriteLine("VOCÊ PODE CADASTRAR MAIS DE UMA VIAGEM.\nQUANTAS VIAGENS VOCÊ QUER CADASTRAR?");
                        int x = Validacoes.ValidarInteiros(Console.ReadLine());

                        for (int i = 1; i <= x; i++) {
                            Console.WriteLine($"DIGITE INFORMAÇÕES DA {i}º VIAGEM ");
                            listas.Viagens.Add(percursoCore.RegistrarViagem());
                        }

                        break;

                    case ConsoleKey.B:
                        percursoCore.MostrarViagens(listas); // EXIBIR MINHA LISTA DE VIAGENS CADASTRADAS
                        Console.ReadKey();

                        break;

                    case ConsoleKey.C:
                        percursoCore.EscolhendoCarroEViagem(listas, veiculoCore);// escolher carro pela placa e escolher viagem
                        CarroPercurso carroPercurso = percursoCore.EscolherCarroPercurso(listas);// inicar a viagem automatico ou manual
                        veiculoCore.PartindoAutomatico(carroPercurso, listas, percursoCore);
                        Console.ReadKey();

                        break;

                    case ConsoleKey.D:

                        break;

                    case ConsoleKey.E:

                        listas.VeiculosEViagens.ForEach(s => Console.WriteLine(s));
                        Console.WriteLine("\n\nESCOLHA E DIGITE UMA VIAGEM PARAEXIBIR RELATÓRIO:");
                        int codigo = Validacoes.ValidarInteiros(Console.ReadLine());
                        Relatorio relatorio = listas.Relatorios.Where(s => s.CarroPercurso.CodigoId == codigo).FirstOrDefault();
                        Console.Clear();
                        if (relatorio == null) {
                            Console.WriteLine("NÃO EXISTE RELATÓRIOS!\nAPERTE QUALQUER TECLA PARA VOLTAR");
                            Console.ReadKey();
                        }
                        else if (listas.Relatorios.Count(s => s.CarroPercurso.CodigoId == codigo) > 1) {
                            Console.WriteLine(" EXISTE MAIS DE UM RELATÓRIO\nVIAGEM CANCELADA");
                            foreach (Relatorio s in listas.Relatorios)
                                if (s.CarroPercurso.CodigoId == codigo)
                                    Console.WriteLine(s);
                        }
                        else Console.WriteLine(relatorio);
                                                                        
                        break;

                    default:
                        Console.Write("\nOBRIGADO, PRESSIONE QUALQUER TECLA PARA REINICIAR");
                        Console.ReadKey();
                        Menu.MenuPrincipal(veiculoCore, listas, percursoCore);

                        break;
                }
            }
        }
        internal static void EscolherPartida(VeiculoCore veiculoCore, CarroPercurso carroPercurso) {

            Console.WriteLine("TEMOS DUAS MANEIRAS DE PROSSEGUIR ESSA VIAGEM:\n[A]-AUTOMATICO\n[B]-PROCEDURAL\n\nCOMO DESEJA PROSSEGUIR?\n\n [ESC] - SAIR");
            while (true) {
                Console.Clear();
                ConsoleKeyInfo tecla = Console.ReadKey(); // armazena uma tecla
                switch (tecla.Key) {

                    case ConsoleKey.Escape:
                        Console.WriteLine("\n OOBRIGADO! PRESSIONE ENTER PARA SAIR..");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.A:


                        break;


                }
            };


        }
    }

}
