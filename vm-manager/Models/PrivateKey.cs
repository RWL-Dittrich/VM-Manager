using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vm_manager.Models;

public class PrivateKey
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string PrivateKeyString { get; set; }
    
    public string PublicKeyString { get; set; }

    public string Passphrase { get; set; }
    
    
}