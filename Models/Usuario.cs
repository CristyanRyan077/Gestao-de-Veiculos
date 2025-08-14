using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_de_combustivel.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Senha { get; set; } = string.Empty;

        public Perfil Perfil { get; set; }

        public ICollection<VeiculoUsuarios>? Veiculos { get; set; }
    }
    public enum Perfil
    {
        Administrador,
        Usuario
    }
}
