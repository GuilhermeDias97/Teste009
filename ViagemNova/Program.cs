using System;
using ViagemNova.Entidades;
using ViagemNova.Utilitarios;
using ViagemNova.Core.entidades;
using ViagemNova.Entidades.core;
using ViagemNova.Core;

namespace ViagemNova {
    public class Program {
        static void Main(string[] args) {


            

            Listas listas = new Listas();
            VeiculoCore veiculocore = new VeiculoCore();
            PercursoCore percursocore = new PercursoCore();

           Menu.MenuPrincipal(veiculocore, listas, percursocore);



        }
    }
}
