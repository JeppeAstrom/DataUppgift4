using System.ComponentModel.DataAnnotations;

namespace ApiDataKommunkationUppgift;

public class TempSchema
{
    [Required]
    public string Device { get; set; }
    [Required]
    
    public string EncryptedTemperature { get; set; }
}
