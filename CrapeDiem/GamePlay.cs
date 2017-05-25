/*
Name: Charlie Pecot
 * Date: 11/29/2015
 * Project: Final Project
 * Crape' Diem
*/
using CrapeDiem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace CrapeDiem
{
    // The main class
    // if/then: line 47
    // switch construct: line 109
    // for loop: line
    // while loop: line 
    // single dimensional array: line
    // multi-dimensional array: line 
    // 7 methods in the main class: PlayAgain, RollTheDice, Concession, Check3rdRoll, GetNumberOfPlayers, ClearScr
    // overloaded constructor: in file Player.cs, class Player
    // 3 private members with gets/sets in file Player.cs
    // Random class used below, used to generate dice rolls, and card pulls
    // static variable "doIt" gets changed in method PlayAgain

    class GamePlay
    {
        public static char doIt = '1';
        public static char choice1 = '1';
        public static char choice2 = '2';
        public static char choice3 = '3';
        public static char choice4 = '4';
        public static char choiceQ = 'Q';
        public static int me = 1;
        public const int ROLLSPERTURN = 3;
        public const int TURNS = 3;
        public static Random rnd = new Random();
        public static Player GetPlayer = new Player();
        public static RandomCard NewDeck = new RandomCard();
        const int YAY = 2;
        const int BOO = 1;        
        static void Main(string[] args)
        {
            (new System.Media.SoundPlayer(@Environment.CurrentDirectory + "\\background.wav")).Play();

            Rules GameRules = new Rules();
            GameRules.DisplayRules();
            
            while (doIt <= choice3)
            {
                ClearScr();
                if (doIt == choice1)
                {
                    GetPlayer.CreatePlayer();
                } else
                if (doIt== choice3)
                {
                    GetPlayer.ResetScore();
                }

                NewDeck.CreateDeck();
                int numPlayers = GetNumberOfPlayers();
                for (int a = 1; a <= numPlayers; ++a) // start player loop
                {
                    for (int t = 1; t <= TURNS; ++t) // 3 turns
                    {
                        GetPlayer.DisplayScores();
                        NewDeck.GetCard();
                        
                        int r = 0;
                        bool done = false;
                        while (done == false)
                        {
                            ++r;
                            if (r <= ROLLSPERTURN) // 3 rolls possible per turn
                            {
                                ClearScr();
                                Console.WriteLine(" Player {3} \"{2}\" is up, Turn {0} of {1}.\n", t, ROLLSPERTURN, GetPlayer.GamePlayers[a], a);
                                Console.WriteLine("****************************************************\n" +
                                                  " ************** TURN {0} of {1} ************************\n" +
                                                  "****************************************************\n", t, ROLLSPERTURN);
                                Console.WriteLine(" The dealer pulled the card: {0}, Your point to make: {1}\n", NewDeck.YourCard, NewDeck.cardPoint);
                                string choice = "0";
                                string validChoices = "01234Qq";
                                while (choice == "0")
                                {
                                    string whatRoll = "Roll ";
                                    Console.Write(whatRoll);
                                    for (int i = 0; i < Console.WindowWidth-whatRoll.Length; ++i)
                                    {
                                        Console.Write(r);
                                    }
                                    Console.WriteLine("\n Press (1) to concede and buy {0} drink(s),  ", r * (numPlayers - GamePlay.me));
                                    Console.WriteLine("       (2) to call Crape Diem and take roll {0}!", r);
                                    Console.WriteLine("       (3) to review the score. ");
                                    Console.WriteLine("       (4) to review the rules. ");
                                        Console.Write("       (Q) to quit the game.                        => ");
                                    choice = Console.ReadLine();
                                    if (choice != "" )
                                    {
                                        choice = choice.Substring(0, 1);
                                    }
                                    if (validChoices.IndexOf(choice) == -1 || choice == "")
                                    {
                                        choice = "0";
                                    }
                                    choice = choice.ToUpper();
                                    ClearScr();
                                    switch (choice)
                                    {
                                        case "1":
                                            PlaySound(YAY);
                                            done = Concession(r, a);
                                            GetPlayer.DisplayScores();
                                            ScoreUpdate(a, t, NewDeck.cardPoint, r, Pips[0], Pips[1], 0);
                                            // DisplayStatus(a,t,r);
                                            Continue();
                                            break;
                                        case "2":
                                            done = DidYouMakeYourPoint(a, r, RollTheDice());
                                            ScoreUpdate(a, t, NewDeck.cardPoint, r, Pips[0], Pips[1], 0);
                                            if (r < ROLLSPERTURN)
                                            {
                                                Continue();
                                            }
                                            break;
                                        case "3":
                                            GetPlayer.DisplayScores();
                                            --r;  // reset the roll count
                                            Continue();
                                            break;
                                        case "4":
                                            GameRules.DisplayRules();
                                            --r; // reset the roll count
                                            Continue();
                                            break;
                                        case "Q":
                                            done = false;
                                            r += ROLLSPERTURN;
                                            t += TURNS;
                                            a += numPlayers;
                                            doIt = choice4;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                if (GamePlay.doIt != choice4)
                                {
                                    // 3 rolls and you didn't make your point
                                    int maxPts = 4;
                                    Console.WriteLine("\n The round's on you!");
                                    Console.WriteLine(" Everyone gets {0} drink(s).\n", maxPts);
                                    PlaySound(YAY);
                                    GetPlayer.awardPoints(maxPts, numPlayers, a);
                                    GetPlayer.DisplayScores();
                                    // You bought the drinks so end the current turn
                                    Continue();
                                }
                                done = true;
                            }

                        }

                        EndOfTurn(a, t, r);
                    }
                    
                }
                if (GamePlay.doIt != choice4)
                {
                    GetPlayer.gameWinner();
                    Console.WriteLine(GetPlayer.Winner);
                }
                doIt = PlayAgain(); 
            }
            ClearScr();
            DisplayGameHistory();
            Console.Write("\n Thanks for playing. Press [Enter]...");
            Console.ReadLine();
        }
        static char PlayAgain()
        {
            string doIt_ = "";
            char doit = choice1;
            bool done = false;
            while (!done) { 
            Console.WriteLine("\n Would you like to play again?\n");
            Console.WriteLine(" (1) Yes with different players");
            Console.WriteLine(" (2) Yes with same players, keep old scores");
            Console.WriteLine(" (3) Yes with same players, reset scores");
                Console.Write(" (4) No, get me out of here      => ");
            doIt_ = Console.ReadLine();
            if (doIt_!="")
                {
                  doIt = Convert.ToChar(doIt_);
                    if (doIt >= choice1 && doIt <= choice4)
                      { done = true; }
                    }    
                }
            return doIt;
        }
        public static int[] Pips = new int[2];
        static void DisplayStatus(int a, int t, int r)
        {
            int PipsTotal = Pips[0] + Pips[1];
            Console.WriteLine("= Status =================================");
            Console.WriteLine("  Player:      {0}{1}", GetPlayer.GamePlayers[a],a);
            Console.WriteLine("  Turn:        {0}", t);
            Console.WriteLine("  Last Roll:   #{0}: {3}, <{1}>+<{2}>", r, Pips[0], Pips[1], PipsTotal);
            Console.WriteLine("  Your point:  {0}", NewDeck.cardPoint);
            Console.WriteLine("==========================================");
        }
        
        static  int[] RollTheDice()
        {
            Die myDieRoll = new Die();
            Pips[0] = myDieRoll.MyDieRolls[0].pips;
            Pips[1] = myDieRoll.MyDieRolls[1].pips;
            int[] diepips = { myDieRoll.MyDieRolls[0].pips, myDieRoll.MyDieRolls[1].pips};
            return diepips;
        }
        static bool Concession(int r, int a)
        {
            string drinks;
            switch (r)
            {
                case 1:
                    drinks = "1 drink";
                    break;
                default:
                    drinks = r.ToString() + " drinks";
                    break;
            }
            Console.WriteLine(" You have conceeded your point on roll {0}.\n",r);
            Console.WriteLine(" You're buying the round!");
            Console.WriteLine(" {0} drink(s) for each roll, for each player except you!\n",  drinks);
            GetPlayer.awardPoints(r, GetNumberOfPlayers(), a);
            // You bought the drinks so end the current turn
            return true;
        }
        /*****************************************************/
        static bool DidYouMakeYourPoint(int playerNDX, int r, int[] diepips)
        {
            Console.WriteLine("= Results of {1}'s Roll {0} ===========================================\n", r, GetPlayer.GamePlayers[playerNDX]);
            Console.WriteLine(" Your point to make: {3}. You rolled: {0}, <{1}>+<{2}>.\n", (diepips[0]+diepips[1]), diepips[0], diepips[1], NewDeck.cardPoint);
            if (diepips[0] == NewDeck.cardPoint || diepips[1] == NewDeck.cardPoint || (diepips[0]+ diepips[1]) == NewDeck.cardPoint)
            {
                int pts = r * (GetNumberOfPlayers() - me); // the roll number times (the number of players minus the player rolling)

                Console.WriteLine(" {1}, you made your point! Everyone buys you {0} drink(s).\n", pts, GetPlayer.GamePlayers[playerNDX]);
                PlaySound(BOO);
                GetPlayer.ManagePoints(playerNDX, pts);
                GetPlayer.DisplayScores();
                return true;
            }
            else
            {
                Console.WriteLine(" On your roll {1}, {0}, you did not make your point!\n", GetPlayer.GamePlayers[playerNDX],r);

                if (r > GamePlay.ROLLSPERTURN)
                {
                    Check3rdRoll(r, GetNumberOfPlayers(), playerNDX);
                }
                return false;
            }
        }
        static void Check3rdRoll(int r, int numPlayers, int a)
        {
                Console.WriteLine(" That was your third roll. You must buy everyone 4 drinks.");
            PlaySound(2);
            GetPlayer.awardPoints(r, numPlayers, a);
                GetPlayer.DisplayScores();
            // You bought the drinks so end the current turn
        }
        static int  GetNumberOfPlayers()
        {
            int numPlayers = 0;
            for (int a = 1; a < GetPlayer.GamePlayers.Length; ++a)
            {
                if (!String.IsNullOrEmpty(GetPlayer.GamePlayers[a])) numPlayers = a;
            }
            return numPlayers;
        }
        public static void Continue()
        {
            Console.Write("\nPress [Enter] to continue...");
            Console.ReadLine();
        }
        static int[,]  MyGameHistory = new int[100,7];
        static int counter = 0;
        public static void ScoreUpdate(int a, int pts)
        {
            GetPlayer.GameHistory[a].Player = a;
            GetPlayer.GameHistory[a].Points = pts;
            MyGameHistory[counter, 0] = GetPlayer.GameHistory[a].Player;
            MyGameHistory[counter, 6] = GetPlayer.GameHistory[a].Points;
        }
        public static void ScoreUpdate(int a, int t, int c, int r, int die1, int die2, int pts)
        {
            GetPlayer.GameHistory[a].Player = a;
            GetPlayer.GameHistory[a].Turn = t;
            GetPlayer.GameHistory[a].CardPoint = c;
            GetPlayer.GameHistory[a].Roll = r;
            GetPlayer.GameHistory[a].DieOne = die1;
            GetPlayer.GameHistory[a].DieTwo = die2;
            GetPlayer.GameHistory[a].Points = pts;
            MyGameHistory[counter, 0] = GetPlayer.GameHistory[a].Player;
            MyGameHistory[counter, 1] = GetPlayer.GameHistory[a].Turn;
            MyGameHistory[counter, 2] = GetPlayer.GameHistory[a].CardPoint;
            MyGameHistory[counter, 3] = GetPlayer.GameHistory[a].Roll;
            MyGameHistory[counter, 4] = GetPlayer.GameHistory[a].DieOne;
            MyGameHistory[counter, 5] = GetPlayer.GameHistory[a].DieTwo;
            MyGameHistory[counter, 6] = GetPlayer.GameHistory[a].Points;
            ++counter;
        }
        public static  void DisplayGameHistory()
        {
            Console.WriteLine("= Game History ===================================================");
            Console.WriteLine("ID  Player           Turn   CardPt   Roll   DieOne DieTwo  Points ");
            int a = 0;
            while (MyGameHistory[a, 0]> 0 )
            {
                Console.Write("{0,2}) {1,-12}",a,GetPlayer.GamePlayers[MyGameHistory[a, 0]]);
                for (int b=1;b<7;++b)
                {
                    Console.Write("{0,7}\t",MyGameHistory[a, b]);
                }
                ++a;
                Console.WriteLine();
            }
        }
        public static void PlaySound(int s)
        {
            string mySound = "";
            switch (s)
            {
                case 2:
                    mySound = "yay.wav";
                    break;
                default:
                    mySound = "boo.wav";
                    break;
            }
            mySound = Environment.CurrentDirectory + "\\" + mySound;
            if (File.Exists(mySound))
            {
                (new System.Media.SoundPlayer(@mySound)).Play();
            }
           
        }
        public static void EndOfTurn(int a, int t, int r) {
            if (t <= 3) { 
            Console.WriteLine("\n*************************************************" + 
                              "\n************* END of TURN {0} **********************" +
                              "\n*************************************************\n", t);
            DisplayStatus(a, t, r);
            }
            if (t == TURNS)
            {
                Console.WriteLine(" Player {1} \"{0}\", that was your last turn\n", GetPlayer.GamePlayers[a], a);
            }
            if (t <= 3) 
            {
                Continue();
            }
            
        }
        public static void ClearScr()
        {
            Console.Clear();
            int[] SuitNames = { 0x003, 0x004, 0x005, 0x006 };
            string myLine = "";
            while (myLine.Length < Console.WindowWidth)
            {
                for (int s = 0; s < SuitNames.Length; ++s)
                {
                    myLine += (char)SuitNames[s];
                }
            }
            string CDTitle_ = ">>> CRAPE' DIEM <<<";
            string CDTitle = "";
            while (CDTitle.Length < Console.WindowWidth)
            {
                CDTitle += CDTitle_;
            }
            string CDHead = myLine +
                            CDTitle.Substring(0, Console.WindowWidth) +
                            myLine + "\n";
            Console.WriteLine(CDHead);
        }
    }
}
