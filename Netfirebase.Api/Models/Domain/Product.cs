using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Netfirebase.Api.Models.Domain;

public class Product
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 4)")]
    public decimal Price { get; set; }
}
