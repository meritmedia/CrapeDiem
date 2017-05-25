using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapeDiem
{
    public class RandomCard
    {
        int[] SuitNames = { 0x003, 0x004, 0x005, 0x006 };
        // default constructor
        public RandomCard() { }
        //Overloaded constructor
        public RandomCard(int c, string yc)
        {
            cardPoint = c;
            yourCard = yc;
        }
        const int SUITS = 4;
        const int NUMS = 14;
        static int[,] deck = new int[NUMS, SUITS];

        // req: private member #1 with get/set properties
        public int cardPoint { get; set; }

        // req: private member #2 with get/set properties
        private string yourCard;
        public string YourCard
        {
            get { return yourCard; }
            set { yourCard = value; }
        }

        //method
        public void CreateDeck()
        {
            int cardCount = 1;
            for (int s = 0; s < SUITS; ++s)
            {
                for (int n = 1; n < NUMS; ++n)
                {
                    deck[n, s] = cardCount;
                    ++cardCount;
                }
            }
        }

        //method
        public void GetCard()
        {
            string cardName;
            int randomCard = GamePlay.rnd.Next(1, 53);
            for (int s = 0; s < SUITS; ++s)
            {
                for (int n = 1; n < NUMS; ++n)
                {
                    if (deck[n, s] == randomCard)
                    {
                        int newN = n;
                        switch (newN)
                        {
                            case 1:
                                cardName = "Ace";
                                break;
                            case 11:
                                cardName = "Jack";
                                break;
                            case 12:
                                cardName = "Queen";
                                break;
                            case 13:
                                cardName = "King";
                                break;
                            default:
                                cardName = newN.ToString();
                                break;
                        }
                        PostCardPoint(cardName, s, newN);
                    }
                }
            }
        }

        public void PostCardPoint(string cardName, int s, int newN)
        {

            yourCard = cardName + " of " + ((char)SuitNames[s]);
            cardPoint = newN;
        }
        public void ShowCards()
        {
            string cardName = "";
            GamePlay.ClearScr();
            Console.WriteLine("\nHere's a deck of cards....");
           for (int c = 1; c < NUMS; ++c)
            {
                for (int s = 0; s < SUITS; ++s) 
                {
                    cardName = deck[c, s].ToString().PadLeft(2)+":";
                    switch (c)
                    {
                        case 1:
                            cardName +=  "A" + (char)SuitNames[s];
                            break;
                        case 11:
                            cardName += "J" + (char)SuitNames[s];
                            break;
                        case 12:
                            cardName += "Q" + (char)SuitNames[s];
                            break;
                        case 13:
                            cardName += "K" + (char)SuitNames[s];
                            break;
                        default:
                            cardName +=  c.ToString() + (char)SuitNames[s];
                            break;
                    }
                    Console.Write(cardName + "\t");
                }
                Console.WriteLine();
            }
        }
    }

}
