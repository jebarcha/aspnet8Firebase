using System.ComponentModel.DataAnnotations;

namespace Netfirebase.Api.Models.Domain;

public class User
{
    //[Key]
    //[Required]
    public int Id { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public string? FirebaseId { get; set; }

    public ICollection<Role>? Roles { get; set; }

}
