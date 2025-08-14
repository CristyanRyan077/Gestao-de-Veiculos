using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_de_combustivel.Models
{
    [Table("Veiculos")]
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; } = string.Empty;
        [Required]
        public string Modelo { get; set; } = string.Empty;
        [Required]
        public string Placa { get; set; } = string.Empty;
        [Required]
        public int AnoFabricacao { get; set; }
        [Required]
        public int AnoModelo { get; set; }

        public ICollection<Consumo>? Consumos { get; set; }

        public ICollection<VeiculoUsuarios>? Usuarios { get; set; }

    }
}
