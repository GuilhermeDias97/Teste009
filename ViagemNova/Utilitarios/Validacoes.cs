using System;
using System.Text.RegularExpressions;
using System.Globalization;
using ViagemNova.Entidades.core;

namespace ViagemNova.Utilitarios {
    class Validacoes {

        public static string ValidarVogais(string a) // VALIDAR vogais
     {
            while (a != "A" && a != "B") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO PARA COMBUSTIVEL, DIGITE (A ou B,): ");
                a = Console.ReadLine().ToUpper();
                Console.ResetColor();
            }
            return a;
        }
        public static string SimOuNao(string s) {
            while (s != "S" && s != "N") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO PARA COMBUSTIVEL, DIGITE (S ou N): ");
                s = Console.ReadLine().ToUpper();
                Console.ResetColor();
            }
            return s;
        }
        public static string CondicaoClimatica(string j) {
            while (j != "ENSOLARADO" && j != "NEVANDO" && j != "CHOVENDO") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO PARA O CLIMA CONFORME INDICADO: ");
                j = Console.ReadLine().ToUpper();
                Console.ResetColor();
            }
            return j;
        }
        public static string ValidarTipoCombustivel(string c) // VALIDAR  tipo de combustivel
{
            while (c != "FLEX" && c != "GASOLINA" && c != "ALCOOL") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO PARA COMBUSTIVEL, DIGITE (FLEX, GASOLINA OU ALCOOL: ");
                c = Console.ReadLine().ToUpper();
                Console.ResetColor();
            }
            return c;
        }
        public static string ValidarPlaca(string p) // VALIDAR Placa não precisa instanciar
{

            while (!Regex.IsMatch(p, @"^[a-zA-Z]{3}-[0-9]{4}$") || string.IsNullOrEmpty(p) || string.IsNullOrWhiteSpace(p)) // @ significa q posso colocar barra invertida e ^ indica inicio da string 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO, DIGITE A PLACA CORRETAMENTE EX:( ABC-0000 ): ");
                p = Console.ReadLine().ToUpper();
                Console.ResetColor();
            }
            return p;
        }
        public static string ValidarLetras(string l) // statico pq não preciso instanciar
{
            while (!Regex.IsMatch(l, @"^[a-zA-Z\u00C0-\u00FF]+$")) // barra  @ "  \  " ^indica q inicio string 
            {
                Console.ForegroundColor = ConsoleColor.Red; // cor
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS LETRAS: ");
                l = Console.ReadLine();
                Console.ResetColor();
            }
            return l;

        }
        public static int ValidarInteiros(string i) // VALIDAR INTEIROS
        {
            int result;
            while (!int.TryParse(i, out result) || result < 0) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES INTEIROS POSITIVOS entre 0 : ");
                i = Console.ReadLine();
                Console.ResetColor();
            }
            return result;
        }
        public static int ValidarPneu(string v) // VALIDAR PNEUS
{
            int result;
            while (!int.TryParse(v, out result) || result <= 0 && result >= 2) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES INTEIROS POSITIVOS DE 0 A 2 : ");
                v = Console.ReadLine();
                Console.ResetColor();
            }
            return result;
        }
        public static int ValidarAnoDoCarro(string a) // VALIDAR  ANO DO CARRO ENTRE 1900 E 2020
{
            int result;
            while (!int.TryParse(a, out result) || result < 1900 || result > 2020) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("\nVALOR INVÁLIDO, DIGITE APENAS VALORES INTEIROS POSITIVOS entre 1900 e 2020 : ");
                a = Console.ReadLine();
                Console.ResetColor();
            }
            return result;
        }
        public static double ValidarDouble(string d) // VALIDAR DOUBLE
{
            double x;
            while ((!double.TryParse(d, out x)) || (x <= 0.0)) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Numero inválido, digite novamente, que não seja negativo ou igual a 0");
                Console.ResetColor();
                d = Console.ReadLine();
            }
            return x;
        }
        public static string ValidarModelos(string m) // VALIDAR MODELO DO CARRO ACEITANDO SOMENTE LETRAS E NUMEROS
{
            while (!Regex.IsMatch(m, @"^[a-z A-Z 0-9]+$") || string.IsNullOrEmpty(m) || string.IsNullOrWhiteSpace(m)) {
                Console.Write("\n INCORETO, DIGITE APENAS LETRAS [A-Z]: ");
                m = Console.ReadLine();
            }
            return m;


        }
        public static DateTime ValidarData(string d) // VALIDAR DATA
       {
            DateTime data;
            while (!DateTime.TryParseExact(d, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out data)) {
                Console.Write("\nVALOR INVÁLIDO, DIGITE UMA DATA VÁLIDA [dd/MM/yyyy]: ");
                d = Console.ReadLine();
            }
            return data;



        }
        public static string ValidarClima(string c) {
            while (c != "SOL" && c != "CHOVENDO" && c != "NEVANDO") {
                Console.Write("VALOR INVÁLIDO! ESCOLHA ENTRE \nSOL  \nCHOVENDO  \nNEVANDO: ");
                Console.WriteLine();
                c = Console.ReadLine().ToUpper();
            }
            return c;
        }
        public static bool ValidandoCodigo(Listas listas, int idgerado) => (listas.Viagens.Exists(cod => cod.CodigoViagem == idgerado) || listas.VeiculosEViagens.Exists(x => x.CodigoId == idgerado) || listas.Relatorios.Exists(x => x.CodigoId == idgerado)) ? true : false;
    }
}