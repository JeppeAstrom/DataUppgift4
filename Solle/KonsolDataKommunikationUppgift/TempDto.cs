using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonsolDataKommunikationUppgift;

public class TempDto
{
    [Required]

    public string Device { get; set; }
    [Required]
  
    public string EncryptedTemperature { get; set; }
}

