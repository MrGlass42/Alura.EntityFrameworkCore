
namespace Alura.Loja.Testes.ConsoleApp
{
    public class PromocaoProduto
    {
        public int PromocaoId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public Promocao Promocao { get; set; }
    }
}
