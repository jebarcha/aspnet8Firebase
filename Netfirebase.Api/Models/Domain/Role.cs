namespace Netfirebase.Api.Models.Domain;

public sealed class Role : Enumeration<Role>
{
    public static readonly Role Client = new(1, "Client");

    public Role(int id, string name) : base(id, name)
    {

    }

    public ICollection<Permission>? Permissions { get; set; }

    //public ICollection<User>? Users { get; set; }
}
