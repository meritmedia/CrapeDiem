using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapeDiem
{
    class Player
    {
        public Score[] GameHistory;
        // default constructor
        public Player()
        {
            GameHistory = new Score[MAXPLAYERS];
            for (int i = 0; i < MAXPLAYERS; ++i)
            {
                GameHistory[i] = new Score();
            }
        }
        //Overloaded constructor
        public Player(string[] g, int[] pp, string w)
        {
            gamePlayers[0] = "Charlie";
            pp[0] = 0;
            gamePlayers[1] = "Joe";
            pp[1] = 0;
            winner = "";
            for (int i=0;i< MAXPLAYERS; ++i)
            {
                GameHistory[i] = new Score();
            }
        }
        const int MAXPLAYERS = 100;

        // req: private members #1, used in mehods AddPlayer, DisplayScores, Winner
        private string[] gamePlayers = new string[MAXPLAYERS];
        public string[] GamePlayers
        {
            get { return gamePlayers; }
            set { gamePlayers = value; }
        }

        // req: private members #2, used in methods AddPlayer, DisplayScores, ManagePoints, Winner
        private int[] playerPoints = new int[MAXPLAYERS];
        public int[] GetPoints
        {
            get { return playerPoints; }
            set { playerPoints = value; }
        }
        // req: private members #3, used in gameWinner 
        private string winner { get; set;}
        public string Winner
        {
            get { return winner; }
            set { winner = value; }
        }
        //method
        public void CreatePlayer()
        {
            string playerName;
            bool done = false;
            int gamePlayerNDX = 0, minPlayerNDX=1;
            Console.WriteLine("So, who's playing?\n\nType each player's name, followed by [Enter]. Press [Enter] again when done.\n");
            while (done == false)
            {
                playerName = Console.ReadLine();
                if (playerName == "")
                {
                    if (gamePlayerNDX <= minPlayerNDX)
                    {
                        Console.WriteLine("CRAPE' DIEM requires at least two players!!! Please enter a name...\n");
                    }
                    else {
                        done = true;
                    }
                }
                else
                {
                    ++gamePlayerNDX;
                    AddPlayer(gamePlayerNDX, playerName);
                }  
            }
        }

        //method
        public void AddPlayer(int gamePlayerNDX, string playerName)
        {
            int pts = 0;
            gamePlayers[gamePlayerNDX] = playerName;
            playerPoints[gamePlayerNDX] = pts;
            Player GameHistory = new Player();
            Console.WriteLine("Player \"{0}\" added. Enter another player or press [Enter] if done.\n", playerName);
        }
        //method
        public void ResetScore()
        {
            for (int i=0;i< MAXPLAYERS; ++i)
            {
                playerPoints[i] = 0;
            }
        }
        //method
        public void DisplayScores()
        {
            if (GamePlay.doIt != GamePlay.choice4) { 
            Console.WriteLine("= Current Score =====================================");
            int s = 1;
            bool done = false;
            while (done == false)
            {
                Console.WriteLine(" " + s + ") {0,-20} {1,2} pts.", GamePlayers[s], playerPoints[s]);
                ++s;
                if (String.IsNullOrEmpty(GamePlayers[s]) || s == MAXPLAYERS) done = true;
            }
            Console.WriteLine("======================================================");
                
            }
        }

        //method
        public void ManagePoints(int p, int pts)
        {
            playerPoints[p] += pts;
            if (pts == 0) { Console.WriteLine("Something's up - no pts!"); }
            GamePlay.ScoreUpdate(p, c:0,t:0,r:0,die1: 0,die2: 0,pts:pts);
        }

        // method
        public int GetPlayerPoints(int a)
        {
            return playerPoints[a];
        }
        public void gameWinner()
        {
            if (GamePlay.doIt != GamePlay.choice4)
            {
                DisplayScores();
                int[] max = new int[2];
                for (int w = 1; w < gamePlayers.Length; ++w)
                {
                    if (playerPoints[w] > max[0])
                    {
                        max[0] = playerPoints[w];
                        max[1] = w;
                    }
                }
                GamePlay.Continue();
                GamePlay.ClearScr();
                winner = "= Game Winners ================================\n";
                int counter = 0;
                for (int w = 1; w < gamePlayers.Length; ++w)
                {
                    if (playerPoints[w] == max[0])
                    {
                         winner += "  " + string.Format("{0,-15}",gamePlayers[w]) +  string.Format("{0,-2}",max[0]) + "pts\n";
                         ++counter;
                    }
                }
                if (counter > 1)
                {
                    winner += "\n..........It's a " + counter + "-way tie!";
                }
                //winner = "The winner is " + gamePlayers[max[1]] + " with " + max[0] + " points!\n";
            }
        }

        //method
        public void awardPoints(int rollNumber, int numPlayers, int a)
        {
            for (int p = 1; p <= numPlayers; ++p)
            {
                if (p != a)
                {
                    ManagePoints(p, rollNumber);
                }
            }
        }
    }
}

