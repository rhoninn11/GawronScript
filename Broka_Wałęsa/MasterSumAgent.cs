using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class MasterSumAgent : Agent
    {
        public List<SumAgent> ListaAgentow { get; set; }
        public int Counter { get; set; }
        public int Sum { get; set; }

        public MasterSumAgent(int id, List<SumAgent> listaAgentow) : base(id)
        {
            ListaAgentow = listaAgentow;
            Counter = 0;
            Sum = 0;
        }

        public override void Update()
        {
            if(Counter<ListaAgentow.Count)
            {
                Sum += ListaAgentow[Counter].Sum;
                Console.WriteLine("cygan: " + Counter);
                Counter++;
            }
            else
            {
                HasFinished = true;
            }
        }
    }
}
