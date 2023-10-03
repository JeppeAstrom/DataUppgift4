console.log("JavaScript code is LOL.");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7237/temperatureHub", {
        skipNegotiation: true, // Important for CORS and WebSockets, especially if using IIS
        transport: signalR.HttpTransportType.WebSockets, // You can choose other transport types if you need
        mode: 'no-cors' // This sets the mode for fetch requests
    })
    .build();

connection.on("ReceiveTemperature", function (deviceId, temperature) {
    const tempDisplay = document.getElementById("temperatureDisplay");
    tempDisplay.innerHTML = `Received temperature from ${deviceId}: ${temperature}°C`;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
