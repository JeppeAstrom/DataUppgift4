console.log("JavaScript code is LOL.");

//Här ansluter jag till min signalR hub
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7237/temperatureHub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        mode: 'no-cors'
    })
    .build();

//Här tar jag emot temperaturdatan och visar den på hemsidan.
connection.on("ReceiveTemperature", function (deviceId, temperature) {
    const tempDisplay = document.getElementById("temperatureDisplay");
    tempDisplay.innerHTML = `Received temperature from ${deviceId}: ${temperature}°C`;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});


