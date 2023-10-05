using Microsoft.AspNetCore.SignalR;
using TemperatureAPI.Helpers;


namespace ApiDataKommunkationUppgift;


public class TemperatureHub : Hub
{
    public async Task SendTemperature(TempSchema tempSchema)
    {
     
     
        tempSchema.EncryptedTemperature = DecryptData.Decrypt(tempSchema.EncryptedTemperature);

        
        await Clients.All.SendAsync("ReceiveTemperature", tempSchema.Device, tempSchema.EncryptedTemperature);

       
    }
}
