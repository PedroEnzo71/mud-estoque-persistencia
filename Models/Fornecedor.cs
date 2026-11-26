using System.ComponentModel.DataAnnotations;

namespace MudEstoquePersistenciaDados.Models
{
    public class Fornecedor
    {
        [Key]
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        [Required, MaxLength(200)]
        public string CNPJ { get; set; }
        [Required, MaxLength(200)]

        public string Email { get; set; }
        [MaxLength(20), EmailAddress]
        public string Telefone { get; set; }
        [MaxLength(300)]
        public string Endereco { get; set; }
        public bool Ativo { get; set; } = true;

        // Lado "um" do 1:N: não vira coluna
        public ICollection<Produto> Produtos
        { get; set; } = [];

    }
}
