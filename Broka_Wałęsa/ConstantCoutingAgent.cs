using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class ConstantCoutingAgent : Agent
    {
        public ConstantCoutingAgent(int id) : base(id)
        {
        }

        private int liczba;

        public override void Update()
        {
            if (liczba < 10)
            {
                liczba++;
            //    Console.WriteLine(Id);
            }
            else
            {
                HasFinished = true;
            }
        }
    }
}
