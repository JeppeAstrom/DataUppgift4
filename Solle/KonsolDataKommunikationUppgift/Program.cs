using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using KonsolDataKommunikationUppgift;
using TemperatureData;

/// <summary>
/// Jag genererar upp en random temperatur mellan -20 till +40 grader. Jag enkrypterar sedan temperaturen med hjälp av min EncryptData klass.
/// Jag skapar sedan upp min TempDto som innehåller två properties: Device och encryptedtemperature. Där har jag även attributet [Required] så det måste innehålla något.
/// Jag skickar sedan iväg min TempDto till apiet genom en http response och kollar om det gick bra eller inte med en try catch.
/// </summary>

class Program
{
    static async Task Main(string[] args)
    {
        string connectionID = Dns.GetHostName();
        Console.WriteLine("Initializing...");

        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri("https://localhost:7237/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                double temp = GenerateRandomTemperature();
                string encryptedTemp = EncryptTemperature(temp);
                Console.WriteLine($"Generated Temperature: {temp}°C, Sending Encrypted: {encryptedTemp}...");

                TempDto tempData = new TempDto
                {
                    Device = connectionID,
                    EncryptedTemperature = encryptedTemp
                };

                try
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Temperature/send", tempData);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Temperature data sent successfully.");
                    }
                    else
                    {
                        Console.WriteLine($"Error sending temperature: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending temperature: {ex.Message}");
                }

                await Task.Delay(5000);
            }
        }
    }

    private static int GenerateRandomTemperature()
    {
        Random random = new Random();
        return random.Next(-20, 40);
    }

    private static string EncryptTemperature(double temperature)
    {
        string temperatureText = temperature.ToString();
        string encryptedTemperature = EncryptData.Encrypt(temperatureText);
        return encryptedTemperature;
    }
}
