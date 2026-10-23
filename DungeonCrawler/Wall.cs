using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    // Wall ärver plats, tecken, färg och Draw från LevelElement
    internal class Wall : LevelElement
    {
        // Kom ihåg om spelaren har upptäckt väggen
        public bool IsDiscovered { get; set; } = false;

        // Körs när vi skapar en ny vägg
        public Wall(int x, int y)
        {
            // Spara väggens position
            X = x;
            Y = y;

            // Väggen visas som ett grått #
            Symbol = '#';
            Color = ConsoleColor.Gray;
        }
    }
}
