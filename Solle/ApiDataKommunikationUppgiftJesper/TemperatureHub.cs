using Microsoft.AspNetCore.SignalR;
using TemperatureAPI.Helpers;

namespace ApiDataKommunkationUppgift;

public class TemperatureHub:Hub
{
	public async Task SendTemperature(string deviceId, string encryptedTemperature)
	{
		encryptedTemperature = DecryptData.Decrypt(encryptedTemperature);

		await Clients.All.SendAsync("ReceiveTemperature", deviceId, encryptedTemperature);
	}
}
