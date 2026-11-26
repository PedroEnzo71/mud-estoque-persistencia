using MudEstoquePersistenciaDados.Models;

namespace MudEstoquePersistenciaDados.Services.Interfaces
{
    // Contrato do cadastro de categorias: o que a tela pode pedir
    public interface IFornecedorService
    {
        // Lista todas os fornecedoes em ordem alfabética
        Task<IEnumerable<Fornecedor>> ListarTodosAsync();

        // Busca um fornecedor pelo Id; devolve null quando não existe
        Task<Fornecedor?> ObterPorIdAsync(int id);

        // Inclui o fornecedor e devolve o Id gerado pelo banco
        Task<int> AdicionarAsync(Fornecedor fornecedor);

        // Altera o fornecedor; devolve false quando o Id não existe
        Task<bool> AlterarAsync(Fornecedor fornecedor);

        // Exclui o fornecedor; devolve false quando o Id não existe
        Task<bool> ExcluirAsync(int id);
    }
}
