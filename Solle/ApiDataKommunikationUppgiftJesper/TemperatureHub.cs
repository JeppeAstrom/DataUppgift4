using Microsoft.AspNetCore.SignalR;
using TemperatureAPI.Helpers;


namespace ApiDataKommunkationUppgift;

/// <summary>
/// Mer beskrivet inuti min TemperatureController då jag dependency injectar där.
/// </summary>
public class TemperatureHub : Hub
{
    public async Task SendTemperature(TempSchema tempSchema)
    {
     
        tempSchema.EncryptedTemperature = DecryptData.Decrypt(tempSchema.EncryptedTemperature);

        
        await Clients.All.SendAsync("ReceiveTemperature", tempSchema.Device, tempSchema.EncryptedTemperature);

       
    }
}
