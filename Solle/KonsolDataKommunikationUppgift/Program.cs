using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TemperatureData;

class Program
{
    static async Task Main(string[] args)
    {
        string connectionID = Dns.GetHostName();
        Console.WriteLine("Initializing the Hub connection...");
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7237/temperatureHub")
            .WithAutomaticReconnect() 
            .Build();

        hubConnection.Reconnecting += error =>
        {
            Console.WriteLine($"Connection lost due to an error: {error}. Reconnecting...");
            return Task.CompletedTask;
        };

        hubConnection.Reconnected += connectionId =>
        {
            Console.WriteLine($"Reconnected with connection Id: {connectionId}");
            return Task.CompletedTask;
        };

        hubConnection.Closed += async (error) =>
        {
            Console.WriteLine($"Connection closed due to an error: {error}");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await hubConnection.StartAsync();
        };

        Console.WriteLine("Starting connection...");
        await hubConnection.StartAsync();
        Console.WriteLine("Connection established.");

        while (true)
        {
            double temp = GenerateRandomTemperature();
            string encryptedTemp = EncryptTemperature(temp);
            Console.WriteLine($"Generated Temperature: {temp}°C, Sending Encrypted: {encryptedTemp}...");

            try
            {
                await hubConnection.InvokeAsync("SendTemperature", connectionID, encryptedTemp);
                Console.WriteLine("Temperature data sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending temperature: {ex.Message}");
            }

            await Task.Delay(5000);
        }
    }

    private static int GenerateRandomTemperature()
    {
        Random random = new Random();
        return random.Next(-10, 31); 
    }

    private static string EncryptTemperature(double temperature)
    {
        string temperatureText = temperature.ToString();
        string encryptedTemperature = EncryptData.Encrypt(temperatureText);
        return encryptedTemperature;
    }
}
