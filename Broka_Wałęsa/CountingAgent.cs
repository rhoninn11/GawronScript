using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class CountingAgent : Agent
    {

        private int liczba = 0;

        public CountingAgent(int id) : base(id)
        {
        }

        public override void Update()
        {
            if (liczba < Id%100)
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
