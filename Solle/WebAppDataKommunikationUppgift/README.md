"# WebAppDataKommunikationUppgift" 
"# WebAppDataKommunikationUppgift" 
@startuml
title Container Diagram for Weather System

a. �vergripande beskrivning av systemet:

Systemet �r designat f�r att h�mta och visa v�derinformation genom en serie av samverkande applikationer.
Konsolapplikationen genererar automatiskt ett randomiserat objekt. 
Innan detta objekt skickas till webb-API:et krypteras det f�r att s�kerst�lla datans integritet och s�kerhet.
Efter mottagning dekrypterar webb-API:et objektet och behandlar det f�r att h�mta relevant v�derinformation.
Slutligen h�mtar en frontend-applikation i ASP.NET Core denna v�derinformation fr�n API:et
och presenterar den p� en webbplats f�r anv�ndaren att se.


B:
H�r �r uml kod f�r diagrammet:
package "Weather System" {

    [User] as user
    [Konsolapplikation] as consoleApp
    [Webb-API] as api
    [Frontend-applikation i ASP.NET Core] as frontend

    user --> consoleApp: Anv�nder
    consoleApp --> api: Skickar krypterat objekt\nvia HTTPS
    api --> frontend: Svarar med v�derinformation\nvia HTTPS

    note right of api
        Dekrypterar datan vid mottagning
    end note

    note bottom of consoleApp
        Krypterar datan innan\nskicka till API
    end note

}

@enduml
