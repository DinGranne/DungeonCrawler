using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    // Gemensam mall som väggar och fiender ska bygga vidare på
    internal abstract class LevelElement
    {
        // Objektets plats från vänster till höger
        public int X { get; set; }

        // Objektets plats uppifrån och ned
        public int Y { get; set; }
        // Tecknet som visar objektet, till exempel # för en vägg
        public char Symbol { get; set; }

        // Färgen som tecknet ska visas med
        public ConsoleColor Color { get; set; }
        // Visa objektet på rätt plats och med rätt färg
        public void Draw()
        {
            // Välj objektets färg
            Console.ForegroundColor = Color;

            // Välj platsen där tecknet ska skrivas
            Console.SetCursorPosition(X, Y);

            // Skriv objektets tecken
            Console.Write(Symbol);

            // Återställ färgen för nästa utskrift
            Console.ResetColor();
        }
    }
}
