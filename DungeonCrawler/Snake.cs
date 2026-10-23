using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    // Ormen ärver fiendens egenskaper
    internal class Snake : Enemy
    {
        // Körs när jag skapar en orm
        public Snake(int x, int y)
        {
            // Spara ormens position
            X = x;
            Y = y;

            // Bestäm hur ormen visas
            Symbol = 's';
            Color = ConsoleColor.Green;

            // Ge ormen namn och liv
            Name = "Snake";
            HP = 25;

            // Ormens tärningar enligt uppgiften
            AttackDice = new Dice(3, 4, 2);
            DefenceDice = new Dice(1, 8, 5);
        }

        // Gör fiendens drag med tillgång till kartan och spelaren
        public override void Update(LevelData level, Player player)
        {
            // Skillnaden mellan ormens och spelarens kolumn
            int distanceX = X - player.X;

            // Skillnaden mellan ormens och spelarens rad
            int distanceY = Y - player.Y;

            // Beräkna avståndet i kvadrat
            int distanceSquared =
                distanceX * distanceX + distanceY * distanceY;

            // Om spelaren är mer än två rutor bort står ormen still
            if (distanceSquared > 4)
            {
                return;
            }

            // Stå kvar om vi inte hittar en bättre plats
            int bestX = X;
            int bestY = Y;
            int bestDistance = distanceSquared;

            // Prova de fyra riktningarna
            for (int direction = 0; direction < 4; direction++)
            {
                // Börja varje försök från ormens nuvarande plats
                int nextX = X;
                int nextY = Y;

                // Prova höger, vänster, uppåt eller nedåt
                if (direction == 0)
                {
                    nextX = X + 1;
                }
                else if (direction == 1)
                {
                    nextX = X - 1;
                }
                else if (direction == 2)
                {
                    nextY = Y - 1;
                }
                else
                {
                    nextY = Y + 1;
                }

                // Hoppa över platser utanför skrivytan
                if (nextX < 0 || nextY < 0 ||
                    nextX >= Console.BufferWidth || nextY >= Console.BufferHeight)
                {
                    continue;
                }

                // Hoppa över platser med väggar eller fiender
                if (level.IsWall(nextX, nextY) || level.IsEnemy(nextX, nextY))
                {
                    continue;
                }

                // Räkna ut avståndet från den möjliga platsen till spelaren
                int dx = nextX - player.X;
                int dy = nextY - player.Y;
                int newDistance = dx * dx + dy * dy;

                // Spara platsen om den är längre bort än den bästa hittills
                if (newDistance > bestDistance)
                {
                    bestX = nextX;
                    bestY = nextY;
                    bestDistance = newDistance;
                }
            }

            // Flytta till den bästa platsen, eller stå kvar
            X = bestX;
            Y = bestY;

           
        }
    }
}
