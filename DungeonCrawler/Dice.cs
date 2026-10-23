using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonCrawler
{
    internal class Dice
    {
        // Hur många tärningar vi ska slå
        private int numberOfDice;

        // Hur många sidor varje tärning har
        private int sidesPerDice;

        // Ett tal som läggs till efter kastet
        private int modifier;

        // Ta emot och spara inställningarna för tärningarna
        public Dice(int numberOfDice, int sidesPerDice, int modifier)
        {
            // Spara hur många tärningar som ska slås
            this.numberOfDice = numberOfDice;

            // Spara hur många sidor varje tärning har
            this.sidesPerDice = sidesPerDice;

            // Spara talet som ska läggas till efter kastet
            this.modifier = modifier;
        }
        // Slå tärningarna och lämna tillbaka den totala poängen
        public int Throw()
        {
            // Börja med de extra poängen
            int total = modifier;

            // Slå en tärning i taget
            for (int i = 0; i < numberOfDice; i++)
            {
                // Slumpa ett tal från 1 till antalet sidor
                int roll = Random.Shared.Next(1, sidesPerDice + 1);

                // Lägg tärningens resultat till summan
                total = total + roll;
            }

            // Lämna tillbaka den färdiga summan
            return total;
        }
        // Beskriv tärningarnas inställningar som text
        public override string ToString()
        {
            // Ett negativt tal har redan ett minustecken
            if (modifier < 0)
            {
                return $"{numberOfDice}d{sidesPerDice}{modifier}";
            }

            // För noll och positiva tal skriver vi ett plustecken
            return $"{numberOfDice}d{sidesPerDice}+{modifier}";
        }
    }
}
