console.log("JavaScript code is LOL.");

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7237/temperatureHub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
        mode: 'no-cors'
    })
    .build();

connection.on("ReceiveTemperature", function (deviceId, temperature) {
    const tempDisplay = document.getElementById("temperatureDisplay");
    tempDisplay.innerHTML = `Received temperature from ${deviceId}: ${temperature}°C`;
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

let temperature = parseInt(document.getElementById("temperatureDisplay").textContent);

if (temperature <= 15) {

    document.getElementById("temperatureIcon").firstChild.setAttribute("class", "w-20 h-20 fill-stroke text-blue-400");
}
const temperatureValue = parseInt(document.querySelector('.temp-value').textContent);
const sunIcon = document.getElementById('sun-icon');
const snowflakeIcon = document.getElementById('snowflake-icon');

if (temperatureValue >= 10) {
    sunIcon.classList.remove('hidden');
    snowflakeIcon.classList.add('hidden');
} else {
    sunIcon.classList.add('hidden');
    snowflakeIcon.classList.remove('hidden');
}
