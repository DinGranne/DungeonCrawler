using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    // Råttan ärver fiendens egenskaper
    internal class Rat : Enemy
    {
        // Körs när vi skapar en råtta
        public Rat(int x, int y)
        {
            // Spara råttans position
            X = x;
            Y = y;

            // Bestäm hur råttan visas
            Symbol = 'r';
            Color = ConsoleColor.Red;

            // Ge råttan namn och liv
            Name = "Rat";
            HP = 10;

            // Råttans tärningar enligt uppgiften
            AttackDice = new Dice(1, 6, 3);
            DefenceDice = new Dice(1, 6, 1);
        }

        // Gör råttans drag med tillgång till kartan och spelaren
        public override void Update(LevelData level, Player player)
        {
            // Börja med råttans nuvarande plats
            int nextX = X;
            int nextY = Y;

            // Slumpa ett av talen 0, 1, 2 eller 3
            int direction = Random.Shared.Next(0, 4);

            // 0 betyder höger
            if (direction == 0)
            {
                nextX = nextX + 1;
            }

            // 1 betyder vänster
            if (direction == 1)
            {
                nextX = nextX - 1;
            }

            // 2 betyder uppåt
            if (direction == 2)
            {
                nextY = nextY - 1;
            }

            // 3 betyder nedåt
            if (direction == 3)
            {
                nextY = nextY + 1;
            }

            // Stanna om platsen är utanför konsolens skrivyta
            if (nextX < 0 || nextY < 0 ||
                nextX >= Console.BufferWidth || nextY >= Console.BufferHeight)
            {
                return;
            }

            // Stanna om det finns en vägg där
            if (level.IsWall(nextX, nextY))
            {
                return;
            }

            // Stanna om det finns en annan fiende där
            if (level.IsEnemy(nextX, nextY))
            {
                return;
            }

            // Om råttan försöker gå till spelarens plats
            if (nextX == player.X && nextY == player.Y)
            {
                // Attackera spelaren, som får slå tillbaka om den överlever
                Attack(player);

                // Avsluta råttans tur utan att flytta den
                return;
            }

            // Platsen är ledig, så råttan flyttar dit
            X = nextX;
            Y = nextY;
        }
    }
}
