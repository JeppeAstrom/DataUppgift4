"# WebAppDataKommunikationUppgift" 
"# WebAppDataKommunikationUppgift" 
@startuml
title Container Diagram for Weather System

a. Övergripande beskrivning av systemet:

Systemet är designat för att hämta och visa väderinformation genom en serie av samverkande applikationer.
Konsolapplikationen genererar automatiskt ett randomiserat objekt. 
Innan detta objekt skickas till webb-API:et krypteras det för att säkerställa datans integritet och säkerhet.
Efter mottagning dekrypterar webb-API:et objektet och behandlar det för att hämta relevant väderinformation.
Slutligen hämtar en frontend-applikation i ASP.NET Core denna väderinformation från API:et
och presenterar den på en webbplats för användaren att se.


B:
Här är uml kod för diagrammet:
package "Weather System" {

    [User] as user
    [Konsolapplikation] as consoleApp
    [Webb-API] as api
    [Frontend-applikation i ASP.NET Core] as frontend

    user --> consoleApp: Använder
    consoleApp --> api: Skickar krypterat objekt\nvia HTTPS
    api --> frontend: Svarar med väderinformation\nvia HTTPS

    note right of api
        Dekrypterar datan vid mottagning
    end note

    note bottom of consoleApp
        Krypterar datan innan\nskicka till API
    end note

}

@enduml
