using ApiDataKommunkationUppgift;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TemperatureAPI.Helpers;

/// <summary>
/// Här börjar jag med att dependency injecta min temperature hub så jag kan använda mig utav temperaturehubmetoden: SendTemperature.
/// Den kollar om objeketet är skapat rätt genom dess attributer. Har endast [Required] på så den kollar bara det. Om det går bra så
/// dekrypterar jag strängen och använder mig sedan av temperature hub metoden som jag nämnde innan.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TemperatureController : ControllerBase
{
    private readonly IHubContext<TemperatureHub> _hubContext;

    public TemperatureController(IHubContext<TemperatureHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendTemperature(TempSchema tempSchema)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

     
        tempSchema.EncryptedTemperature = DecryptData.Decrypt(tempSchema.EncryptedTemperature);

        
        await _hubContext.Clients.All.SendAsync("ReceiveTemperature", tempSchema.Device, tempSchema.EncryptedTemperature);

        return Ok(); 
    }
}
