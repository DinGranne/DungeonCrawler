// Gör att vi kan använda klasserna i DungeonCrawler
using DungeonCrawler;



Console.WriteLine("Välkommen till Dungeon Crawler!");

// Det betyder: skriv texten mellan citattecken i textfönstret och gå sedan till nästa rad.

Console.WriteLine("Du är spelaren: @");
Console.WriteLine("Tryck på valfri tangent för att starta spelet...");

// Skapa ett objekt som håller reda på kartan
LevelData level = new LevelData();

// Hitta kartfilen i samma mapp som det körbara programmet
string levelPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Level1.txt");

// Läs kartan från den platsen
level.Load(levelPath);

// Skapa spelaren med liv och tärningar från klassen Player
Player player = new Player();

// Placera spelaren där @ hittades i kartfilen
player.X = level.PlayerStartX;
player.Y = level.PlayerStartY;

Console.WriteLine($"Spelarens position: ({player.X}, {player.Y})");
// Det betyder: skriv ut spelarens position i textfönstret med hjälp av de två variablerna player.X och player.Y. $-tecknet används för att infoga variabler i strängen.

Console.ReadKey(true);
// console.ReadKey(true) betyder att programmet väntar på att användaren trycker på en tangent innan det fortsätter. True betyder att tangenten inte ska skrivas ut i fönstret.

Console.Clear();
// console.Clear() betyder att textfönstret rensas från all text som tidigare har skrivits ut.

// Rita kartan utifrån spelarens synfält
level.Draw(player.X, player.Y);

Console.SetCursorPosition(player.X, player.Y);
// Välj positionen för spelaren i textfönstret baserat på variablerna player.X och player.Y.

Console.Write("@");
// console.Write("@") betyder att tecknet "@" skrivs ut i textfönstret på den position som valts med Console.SetCursorPosition().

// Välj en rad under kartan där livspoängen ska visas
Console.SetCursorPosition(0, 20);

// Visa spelarens återstående liv
Console.Write($"Liv: {player.HP}");

// Visa stridsmeddelandet på raden under livspoängen
Console.SetCursorPosition(0, 21);
Console.Write(player.CombatMessage);

// Vänta på att användaren trycker på en tangent och lagra informationen om tangenten i variabeln key. True betyder att tangenten inte ska skrivas ut i fönstret.
// Den första readykey väntar vid välkomsttexten, den andra väntar efter att spelaren har placerats på skärmen.

while (true) // Starta en oändlig loop som kommer att fortsätta tills vi bryter ut ur den med break.
{
    // Vänta på en tangent och spara vilken det var
    ConsoleKeyInfo key = Console.ReadKey(true);

    // Om vi trycker på Escape ska loopen avslutas
    if (key.Key == ConsoleKey.Escape)
    {
        break; // Lämna loopen
    }

    // Om vi trycker på högerpilen
    if (key.Key == ConsoleKey.RightArrow)
    {
        // Kontrollera att det finns plats åt höger
        if (player.X < Console.BufferWidth - 1)
        {
            // Räkna ut vilken kolumn spelaren vill gå till
            int nextX = player.X + 1;

            // Fråga kartan om det finns en vägg på platsen
            if (level.IsWall(nextX, player.Y))
            {
                // Här står väggen, så spelaren står kvar
            }

            else
            {
                // Hitta eventuell fiende på platsen till höger
                Enemy? enemy = level.GetEnemy(nextX, player.Y);

                // Om vi hittade en fiende
                if (enemy != null)
                {
                    // Attackera fienden i stället för att gå dit
                    player.Attack(enemy);

                    // Ta bort fienden från kartan om dess liv är slut
                    if (enemy.HP <= 0)
                    {
                        level.Elements.Remove(enemy);
                    }
                }
                else
                {
                    // Ingen vägg eller fiende här, så spelaren får flytta
                    player.X = nextX;
                }
            }
        }
    }

    // Om vi trycker på vänsterpilen
    if (key.Key == ConsoleKey.LeftArrow)
    {
        // Flytta bara om vi inte redan är längst till vänster
        if (player.X > 0)
        {
            // Räkna ut platsen ett steg åt vänster
            int nextX = player.X - 1;

            // Fråga kartan om det finns en vägg på platsen
            if (level.IsWall(nextX, player.Y))
            {
                // Väggen är i vägen, så spelaren står kvar
            }
            else
            {
                // Hitta eventuell fiende på platsen till vänster
                Enemy? enemy = level.GetEnemy(nextX, player.Y);

                // Om vi hittade en fiende
                if (enemy != null)
                {
                    // Attackera i stället för att gå dit
                    player.Attack(enemy);

                    // Ta bort fienden om dess liv är slut
                    if (enemy.HP <= 0)
                    {
                        level.Elements.Remove(enemy);
                    }
                }
                else
                {
                    // Platsen är ledig, så spelaren får flytta
                    player.X = nextX;
                }
            }
        }
    }

    // Om vi trycker på uppåtpilen
    if (key.Key == ConsoleKey.UpArrow)
    {
        // Flytta bara om vi inte redan är högst upp
        if (player.Y > 0)
        {
            // Räkna ut raden ett steg uppåt
            int nextY = player.Y - 1;

            // Fråga kartan om det finns en vägg ett steg uppåt
            if (level.IsWall(player.X, nextY))
            {
                // Väggen är i vägen, så spelaren står kvar
            }
            else
            {
                // Hitta eventuell fiende på platsen ovanför spelaren
                Enemy? enemy = level.GetEnemy(player.X, nextY);

                // Om vi hittade en fiende
                if (enemy != null)
                {
                    // Attackera i stället för att gå dit
                    player.Attack(enemy);

                    // Ta bort fienden om dess liv är slut
                    if (enemy.HP <= 0)
                    {
                        level.Elements.Remove(enemy);
                    }
                }
                else
                {
                    // Platsen är ledig, så spelaren får flytta
                    player.Y = nextY;
                }
            }
        }
    }

    // Om vi trycker på nedåtpilen
    if (key.Key == ConsoleKey.DownArrow)
    {
        // Flytta bara om det finns en rad kvar i konsolens skrivyta. Betyder typ: "Om vi inte redan är längst ner"
        if (player.Y < Console.BufferHeight - 1)
        {
            // Räkna ut raden ett steg nedåt
            int nextY = player.Y + 1;

            // Fråga kartan om det finns en vägg ett steg nedåt
            if (level.IsWall(player.X, nextY))
            {
                // Väggen är i vägen, så spelaren står kvar
            }
            else
            {
                // Hitta eventuell fiende på platsen nedanför spelaren
                Enemy? enemy = level.GetEnemy(player.X, nextY);

                // Om vi hittade en fiende
                if (enemy != null)
                {
                    // Attackera i stället för att gå dit
                    player.Attack(enemy);

                    // Ta bort fienden om dess liv är slut
                    if (enemy.HP <= 0)
                    {
                        level.Elements.Remove(enemy);
                    }
                }
                else
                {
                    // Platsen är ledig, så spelaren får flytta
                    player.Y = nextY;
                }
            }
        }
    }
    
    // Kopiera listan så att vi kan ta bort fiender ur originalet
    List<LevelElement> turnElements = new List<LevelElement>(level.Elements);

    // Gå igenom objekten i kopian
    foreach (LevelElement element in turnElements)
    {
        // Inga fler fiender får göra drag om spelaren har dött
        if (player.HP <= 0)
        {
            break;
        }

        // Om objektet är en fiende
        if (element is Enemy enemy)
        {
            // Låt fienden göra sitt drag
            enemy.Update(level, player);

            // Ta bort fienden om den dog av spelarens motattack
            if (enemy.HP <= 0)
            {
                level.Elements.Remove(enemy);
            }
        }
    }

// Kontrollera om spelarens liv är slut
if (player.HP <= 0)
    {
        // Töm fönstret och visa att spelet är över
        Console.Clear();
        Console.WriteLine("Game over! Ditt liv är slut.");
        Console.WriteLine("Tryck på en tangent för att avsluta.");

        // Vänta så att spelaren hinner läsa
        Console.ReadKey(true);

        // Avsluta spelloopen
        break;
    }

    // Ta bort det gamla tecknet
    Console.Clear();

    // Rita kartan utifrån spelarens synfält
    level.Draw(player.X, player.Y);

    // Rita spelaren på sin nuvarande position
    Console.SetCursorPosition(player.X, player.Y);
    Console.Write("@");

    // Välj en rad under kartan där livspoängen ska visas
    Console.SetCursorPosition(0, 20);

    // Visa spelarens återstående liv
    Console.Write($"Liv: {player.HP}");
    // Visa stridsmeddelandet på raden under livspoängen

    Console.SetCursorPosition(0, 21);
    Console.Write(player.CombatMessage);
}
