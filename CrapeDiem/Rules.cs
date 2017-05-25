using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapeDiem
{
    class Rules
    {
      
        public Rules() { }

        public Rules(string[] Line) { }
        public string[] Line = {

             "The player with the most drinks wins!*",
             "Rules:*",
             "The dealer starts the game by pulling the top card from a shuffled deck.",
             "This card represents your \"point\".*",
             "Your goal is to roll some combination of the two dice to match the point",
             "of the card pulled by the dealer. You'll get three turns, each consisting",
             "of a different card and up to three roll chances to make your point.",
             "|",
             "Turn Play:*",
             "The dealer pulls a card and this becomes your point to make.",
             "The player must then choose between conceding and buying everyone a round, ",
             "or calling \"CRAPE' DIEM\" and rolling the dice.*",
             "The player then rolls the dice. If either the pips of an individual die or",
             "the combined total equals your point then you've made your point on your",
             "first roll and you get 1 point for every player (everyone buys you a drink).*",
             "However, if you do not make your point, you have two options:*",
             "1. buy each player TWO drinks (every player except the thrower gets 2 points",
             "each) - choose \"Buy 2 drink(s)!\"",
             "or",
             "2. call \"Crape' Diem!\" and roll the dice a second time.",
             "|",
             "If you make your point on your second roll you get two drinks from every",
             "player (points equal to two times the number of players less the roller).",
             "If you do not, your option is to either:*",
             "1. buy THREE drinks for each player (each player besides the thrower gets",
             "three points), or",
             "2. call \"Crape' Diem\" and try a third roll.",
             "|",
             "If you do not make your point on your third roll you buy each player four",
             "drinks (each player gets four points). On the other hand, if you make your",
             "point then you get the number of drinks (points) equal to four times the",
             "number of the other players. If there's two other players that's 4 times 2,",
             "or 8 points awarded.*",
             "Jacks and Queens are worth 11 and 12, respectively. A's are worth 1.",
             "If a Q was pulled by the host, a matching roll could be 2 6's.*",
             "Obviously, if the dealer pulls a K, then your best strategy would be to buy",
             "everyone a drink BEFORE your first roll!*",
             "Unless, of course, you're just feeling generous.",
             "|"
        };

        public void DisplayRules()
        {
            GamePlay.ClearScr();
            foreach (string l in Line)
            {
                
                if (l=="|")
                {
                    GamePlay.Continue();
                    GamePlay.ClearScr();
                }
                else { 
                
                Console.WriteLine(" " + l.Replace('*','\n'));
                     }
            }
        }
    }
}