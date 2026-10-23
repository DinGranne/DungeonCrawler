using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    internal class Player
    {
        // Spelarens position från vänster till höger
        public int X { get; set; }

        // Spelarens position uppifrån och ned
        public int Y { get; set; }

        // Spelaren börjar med 100 livspoäng
        public int HP { get; set; } = 100;
       
        // Text som beskriver den senaste striden
        public string CombatMessage { get; set; } = "";

        // Tärningar för spelarens attack: 2d6+2
        public Dice AttackDice { get; set; } = new Dice(2, 6, 2);

        // Tärningar för spelarens försvar: 2d6+0
        public Dice DefenceDice { get; set; } = new Dice(2, 6, 0);

        // Spelaren attackerar en fiende
        public void Attack(Enemy enemy)
        {
            // Slå spelarens attacktärningar och fiendens försvarstärningar
            int attack = AttackDice.Throw();
            int defence = enemy.DefenceDice.Throw();

            // Räkna ut skadan
            int damage = attack - defence;

            // Skadan får inte vara negativ
            if (damage < 0)
            {
                damage = 0;
            }

            // Spara resultatet av spelarens attack som text
            CombatMessage = $"Din attack: {attack}. {enemy.Name}s försvar: {defence}. Skada: {damage}.";

            // Fienden förlorar bara liv om skadan är större än noll
            if (damage > 0)
            {
                enemy.HP = enemy.HP - damage;
            }

            // En död fiende kan inte slå tillbaka
            if (enemy.HP <= 0)
            {
                return;
            }

            // Fienden slår tillbaka med nya tärningskast
            int counterAttack = enemy.AttackDice.Throw();
            int playerDefence = DefenceDice.Throw();

            // Räkna ut skadan på spelaren
            int counterDamage = counterAttack - playerDefence;

            // Motattackens skada får inte vara negativ
            if (counterDamage < 0)
            {
                counterDamage = 0;
            }

            // Lägg motattackens resultat på en ny rad i samma meddelande
            CombatMessage = CombatMessage + "\n" +
                $"Motattack: {counterAttack}. Ditt försvar: {playerDefence}. Skada: {counterDamage}.";

            // Spelaren förlorar bara liv om skadan är större än noll
            if (counterDamage > 0)
            {
                HP = HP - counterDamage;
            }
        }
    }
}
