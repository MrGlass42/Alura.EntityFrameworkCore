using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var contexto = new LojaContext())
            {
                var cliente = contexto
                    .Clientes
                    .Include(x => x.EnderecoEntrega)
                    .FirstOrDefault();

                Console.WriteLine($"{cliente.EnderecoEntrega.Logradouro}");

                var produto = contexto
                    .Produtos
                    .Include(x => x.Compras)
                    .Where(x => x.Id == 2002)
                    .FirstOrDefault();

                contexto.Entry(produto)
                    .Collection(x => x.Compras)
                    .Query()
                    .Where(x => x.Preco < 0)
                    .Load();

                Console.WriteLine("\n\nCompras: \n");
                foreach (var item in produto.Compras)
                    Console.WriteLine(item.Preco);
            }
        }

        private static void ExibeProdutosDaPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = contexto
                    .Promocoes
                    .Include("Produtos.Produto")
                    .FirstOrDefault();

                foreach (var item in promocao.Produtos)
                    Console.WriteLine(item.Produto);
            }
        }

        private static void IncluirPromocao()
        {
            using (var contexto = new LojaContext())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima Total !!!";
                promocao.DataInicio = new DateTime(2017, 1, 1);
                promocao.DataInicio = new DateTime(2017, 1, 31);

                var produtos = contexto
                    .Produtos
                    .Where(x => x.Categoria == "Bebidas")
                    .ToList();

                foreach (var item in produtos)
                    promocao.IncluiProduto(item);

                contexto.Promocoes.Add(promocao);
                contexto.SaveChanges();
            }
        }

        private static void UmParaUm()
        {
            var fulano = new Cliente();
            fulano.Nome = "Malvo";
            fulano.EnderecoEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua dos 13 memo",
                Complemento = "Vizyyy",
                Bairro = "Uiiii",
                Cidade = "Santa Isabel"
            };

            using (var contexto = new LojaContext())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }

        private static void MuitosParaMuitos()
        {
            var p1 = new Produto() { Nome = "Suco de Laranja", PrecoUnitario = 8.99, Categoria = "Bebidas", Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", PrecoUnitario = 10.99, Categoria = "Bebidas", Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", PrecoUnitario = 38.99, Categoria = "Comidas", Unidade = "Gramas" };


            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscola Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);
            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContext())
            {
                //contexto.Promocoes.Add(promocaoDePascoa);

                var promocao = contexto.Promocoes.Find(1);
                contexto.Remove(promocao);
                contexto.SaveChanges();
            }
        }

        private static void ExibeEntries(IEnumerable<EntityEntry> Entries)
        {
            foreach (var item in Entries)
                Console.WriteLine($"Produto: {item.Entity}, State: {item.State}");
        }
    }
}
