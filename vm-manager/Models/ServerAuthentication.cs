using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_manager.Models;

public class ServerAuthentication
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public Server Server { get; set; }
    public Guid ServerId { get; set; }
    public PrivateKey? PrivateKey { get; set; }
    public string? Password { get; set; }
}