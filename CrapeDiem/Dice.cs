using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrapeDiem
{
    class Die
    {
        public ADieRoll[] MyDieRolls;
        public Die()
        {
            MyDieRolls = new ADieRoll[2];
            for (int i = 0; i < MyDieRolls.Length; ++i)
            {
                MyDieRolls[i] = new ADieRoll();
            }
        }

    }

    class ADieRoll
    {   // req: 
        public int pips { get; set; }
        public ADieRoll()
        {
            pips = GamePlay.rnd.Next(1, 7);
        }
    }
}
