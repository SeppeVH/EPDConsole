# ChipSoft Technische opdracht
Seppe Van Hoof

## Design keuzes
- **N-layer architecture**: De applicatie is opgebouwd volgens een n-layer architecture, wat zorgt voor een duidelijke scheiding van 
verantwoordelijkheden tussen de verschillende lagen (presentatie (console), business layer, data access layer, domain). Dit maakt de code beter onderhoudbaar en uitbreidbaar.
Elke laag is ook afzonderlijk testbaar, wat de kwaliteit van de code ten goede komt.
- **Dependency Inversion Principle**: Voor de managers en repositories worden interfaces gebruikt, wat zorgt voor een losse koppeling tussen de lagen. Ook zorgt dit er voor dat de implementaties makkelijk vervangbaar zijn.
- **Repository pattern**: De data access layer maakt gebruik van het repository pattern, wat zorgt voor een abstractie van de database interacties. 
Dit maakt het eenvoudiger om de database te vervangen of te wijzigen zonder de business logic te beïnvloeden. 
Het vergemakkelijkt ook het toevoegen van nieuwe repositories voor andere entiteiten.
- **User Superclass**: De `User` klasse is een superclass van `Patient` en `Physician`, wat ervoor zorgt dat er geen dubbele attributen voorkomen.

## Gebruikte technologieën
- **NUnit**: Voor het schrijven en uitvoeren van unit tests.
- **Moq**: Voor het mocken van dependencies in de unit tests.
- **Entity Framework Core**: Voor de interactie met de database.
- **SQLite**: Als database voor het opslaan van gegevens.