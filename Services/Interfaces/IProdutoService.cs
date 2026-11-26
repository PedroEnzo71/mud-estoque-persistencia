using MudEstoquePersistenciaDados.Models;

namespace MudEstoquePersistenciaDados.Services.Interfaces
{
    // Contrato do cadastro de produtos: o que a tela pode pedir
    public interface IProdutoService
    {
        // Lista todas os produtos em ordem alfabética
        Task<IEnumerable<Produto>> ListarTodosAsync();

        // Busca um produto pelo Id; devolve null quando não existe
        Task<Produto?> ObterPorIdAsync(int id);

        // Inclui o produto e devolve o Id gerado pelo banco
        Task<int> AdicionarAsync(Produto produto);

        // Altera o produto; devolve false quando o Id não existe
        Task<bool> AlterarAsync(Produto produto);

        // Exclui o produto; devolve false quando o Id não existe
        Task<bool> ExcluirAsync(int id);
    }
}
