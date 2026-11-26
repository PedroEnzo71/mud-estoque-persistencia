using Microsoft.EntityFrameworkCore;
using MudEstoquePersistenciaDados.Data;
using MudEstoquePersistenciaDados.Models;
using MudEstoquePersistenciaDados.Services.Interfaces;

namespace MudEstoquePersistenciaDados.Services.Implementations
{
    public class ProdutoService : IProdutoService
    {

        private readonly IDbContextFactory<EstoqueDbContext> _fabrica;

        // Injeção de dependência (o "D" do SOLID): o serviço declara do que precisa no
        // construtor e o contêiner entrega a fábrica registrada no Program.cs
        public ProdutoService(IDbContextFactory<EstoqueDbContext> fabrica)
        {
            _fabrica = fabrica;
        }

        public async Task<int> AdicionarAsync(Produto produto)
        {
            await using EstoqueDbContext contexto = await _fabrica.CreateDbContextAsync();
            bool nomeEmUso = await contexto.Produtos.AnyAsync(p => p.Nome == produto.Nome);
            if (nomeEmUso)
            {
                throw new InvalidOperationException($"Já existe um produto com esse nome {produto.Nome}");
            }
            contexto.Produtos.Add(produto);
            await contexto.SaveChangesAsync();
            return produto.Id;
        }

        public async Task<bool> AlterarAsync(Produto produto)
        {
            await using EstoqueDbContext contexto = await _fabrica.CreateDbContextAsync();
            Produto? produtoExistente = await contexto.Produtos.FindAsync(produto.Id);
            if (produtoExistente == null)
            {
                return false;
            }
            bool nomeEmUso = await contexto.Produtos.AnyAsync(p => p.Nome == produto.Nome);
            if (nomeEmUso)
            {
                throw new InvalidOperationException($"Já existe um produto com Nome {produto.Nome}");
            }
            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.CodigoBarras = produto.CodigoBarras;
            produtoExistente.PrecoUnitario = produto.PrecoUnitario;
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            await using EstoqueDbContext contexto = await _fabrica.CreateDbContextAsync();
            Produto? produtoExistente = await contexto.Produtos.FindAsync(id);
            if (produtoExistente == null)
            {
                return false;
            }
            bool temProdutos = await contexto.Produtos.AnyAsync(p => p.Id == id);
            if (temProdutos)
            {
                throw new InvalidOperationException($"Este produto não pode ser excluído.");
            }
            contexto.Produtos.Remove(produtoExistente);
            await contexto.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Produto>> ListarTodosAsync()
        {
            await using EstoqueDbContext contexto = await _fabrica.CreateDbContextAsync();
            return await contexto.Produtos.AsNoTracking().OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<Produto?> ObterPorIdAsync(int id)
        {
            await using EstoqueDbContext contexto = await _fabrica.CreateDbContextAsync();
            return await contexto.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
