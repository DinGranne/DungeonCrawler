using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    // Gemensam mall för fiender, med plats, tecken, färg och Draw. LevelElement beskriver saker på kartan. Enemy bygger vidare på den mallen och ska lägga till sådant som fiender behöver, exempelvis livspoäng.
    internal abstract class Enemy : LevelElement
    {
        // Fiendens namn, som vi anger när vi skapar en råtta eller orm
        public string Name { get; set; } = "";

        // Hur många livspoäng fienden har kvar
        public int HP { get; set; }
       
        // Tärningar som används när fienden attackerar
        public Dice AttackDice { get; set; } = new Dice(1, 6, 0);

        // Tärningar som används när fienden försvarar sig
        public Dice DefenceDice { get; set; } = new Dice(1, 6, 0);

        // Varje sorts fiende måste ha en Update-metod som tar emot kartan och spelaren
        public abstract void Update(LevelData level, Player player);

        // Fienden attackerar spelaren
        public void Attack(Player player)
        {
            // Slå fiendens attack och spelarens försvar
            int attack = AttackDice.Throw();
            int defence = player.DefenceDice.Throw();

            // Räkna ut skadan
            int damage = attack - defence;

            // Skadan får inte vara negativ
            if (damage < 0)
            {
                damage = 0;
            }

            // Spara fiendens attack i spelarens stridsmeddelande
            player.CombatMessage = $"{Name}s attack: {attack}. Ditt försvar: {defence}. Skada: {damage}.";

            // Dra bort liv om attacken gjorde skada
            if (damage > 0)
            {
                player.HP = player.HP - damage;
            }

            // Om spelaren dog blir det ingen motattack
            if (player.HP <= 0)
            {
                return;
            }

            // Spelaren slår tillbaka med nya tärningskast
            int counterAttack = player.AttackDice.Throw();
            int enemyDefence = DefenceDice.Throw();

            // Räkna ut skadan på fienden
            int counterDamage = counterAttack - enemyDefence;

            // Motattackens skada får inte vara negativ
            if (counterDamage < 0)
            {
                counterDamage = 0;
            }

            // Lägg till resultatet av spelarens motattack på en ny rad
            player.CombatMessage = player.CombatMessage + "\n" +
                $"Din motattack: {counterAttack}. {Name}s försvar: {enemyDefence}. Skada: {counterDamage}.";

            // Dra bort liv om motattacken gjorde skada
            if (counterDamage > 0)
            {
                HP = HP - counterDamage;
            }
        }
    }
}
