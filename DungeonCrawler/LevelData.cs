using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    internal class LevelData
    {
        // En lista som ska hålla reda på kartans väggar och fiender
        private List<LevelElement> elements = new List<LevelElement>();
        // Gör att annan kod kan hämta listan
        public List<LevelElement> Elements
        {
            get
            {
                // Lämna tillbaka listan som finns i den här klassen
                return elements;
            }
        }
        // Spelarens startplats från vänster till höger
        public int PlayerStartX { get; set; }

        // Spelarens startplats uppifrån och ned
        public int PlayerStartY { get; set; }

        // Hitta fienden på en viss plats
        public Enemy? GetEnemy(int x, int y)
        {
            // Titta på ett objekt i taget
            foreach (LevelElement element in elements)
            {
                // Kontrollera om objektet är en fiende
                if (element is Enemy enemy)
                {
                    // Kontrollera om fienden står på platsen vi söker
                    if (enemy.X == x && enemy.Y == y)
                    {
                        // Lämna tillbaka just den fienden
                        return enemy;
                    }
                }
            }

            // Ingen fiende finns på platsen
            return null;
        }

        // Kontrollera om det finns en fiende på platsen
        public bool IsEnemy(int x, int y)
        {
            // Titta på ett objekt i taget
            foreach (LevelElement element in elements)
            {
                // Är objektet en fiende som står på den här platsen?
                if (element is Enemy && element.X == x && element.Y == y)
                {
                    // Ja, här finns en fiende
                    return true;
                }
            }
            // Ingen fiende hittades på platsen
            return false;
        }

        // Kontrollera om det finns en vägg på platsen
        public bool IsWall(int x, int y)
        {
            // Titta på ett objekt i taget
            foreach (LevelElement element in elements)
            {
                // Kontrollera att objektet är en vägg och står på rätt plats
                if (element is Wall && element.X == x && element.Y == y)
                {
                    // Ja, här finns en vägg
                    return true;
                }
            }

            // Vi har tittat på alla objekt och hittade ingen vägg här
            return false;
        }

        // Rita det spelaren kan se och väggar som redan upptäckts
        public void Draw(int playerX, int playerY)
        {
            // Kontrollera ett objekt i taget
            foreach (LevelElement element in elements)
            {
                // Räkna ut avståndet från spelaren
                int dx = element.X - playerX;
                int dy = element.Y - playerY;
                int distanceSquared = dx * dx + dy * dy;

                // Fem rutors avstånd i kvadrat är 5 gånger 5 = 25
                bool isVisible = distanceSquared <= 25;

                // Väggar ska komma ihåg om de har upptäckts
                if (element is Wall wall)
                {
                    // Om väggen är nära har spelaren upptäckt den
                    if (isVisible)
                    {
                        wall.IsDiscovered = true;
                    }

                    // Visa alla väggar som spelaren har upptäckt
                    if (wall.IsDiscovered)
                    {
                        wall.Draw();
                    }
                }
                else
                {
                    // Andra objekt, som fiender, syns bara när de är nära
                    if (isVisible)
                    {
                        element.Draw();
                    }
                }
            }
        }

        // Läs in kartan från filen vi anger. Betyder: ”Här finns en metod som heter Load. Den behöver få ett filnamn.”
        public void Load(string filename)
        {
            // Töm listan så att gamla objekt inte blir kvar
            elements.Clear();

            // Läs filens rader och spara dem i ordning i "lines"
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Gå igenom kartans rader uppifrån och ned
            for (int y = 0; y < lines.Length; y++)
            {
                // Gå igenom tecknen på raden från vänster till höger
                for (int x = 0; x < lines[y].Length; x++)
                {
                    // Hämta tecknet på den här platsen
                    char symbol = lines[y][x];

                    // Om tecknet är # skapar vi en vägg
                    if (symbol == '#')
                    {
                        // Lägg den nya väggen i listan
                        elements.Add(new Wall(x, y));
                    }

                    // Om tecknet är r skapar vi en råtta
                    if (symbol == 'r')
                    {
                        // Skapa råttan på den här platsen och lägg den i listan
                        elements.Add(new Rat(x, y));
                    }

                    // Om tecknet är s skapar vi en orm
                    if (symbol == 's')
                    {
                        // Skapa ormen på den här platsen och lägg den i listan
                        elements.Add(new Snake(x, y));
                    }

                    // Om tecknet är @ sparar vi spelarens startplats
                    if (symbol == '@')
                    {
                        PlayerStartX = x;
                        PlayerStartY = y;
                    }
                }
            }
        }
    }
}
