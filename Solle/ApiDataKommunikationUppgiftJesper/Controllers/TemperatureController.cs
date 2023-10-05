using ApiDataKommunkationUppgift;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TemperatureAPI.Helpers;

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
