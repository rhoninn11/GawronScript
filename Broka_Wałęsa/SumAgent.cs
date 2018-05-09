using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class SumAgent : Agent
    {
        public int Ile { get; set; }
        public int Counter { get; set; }
        public int Sum { get; set; }
        public List<int> Liczby { get; set; }

        

        public SumAgent(int id, List<int> liczby) : base(id)
        {
            Ile = liczby.Count;
            Console.WriteLine("Agent o ID: " + Id + " ma " + Ile + " liczb do dodania");
            Counter = 0;
            Sum = 0;
            Liczby = liczby;
        }

        public override void Update()
        {
            if(Counter<Ile)
            {
                Sum += Liczby[Counter];
                Counter++;
            }
            else
            {
                HasFinished = true;
                Console.WriteLine("Agent o id: " + this.Id + " zakończył sumowanie z sumą: " + this.Sum);
            }
        }
    }
}
