namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Cliente
    {
        public Cliente()
        {
        }

        public int Id { get; set; }
        public string Nome { get; internal set; }
        public Endereco EnderecoEntrega { get; internal set; }
    }
}