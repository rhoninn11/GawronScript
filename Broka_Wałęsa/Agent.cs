using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Broka_Wałęsa
{
    public abstract class Agent : IRunable
    {
        public bool HasFinished { get; set; }
        public int Id { get; set; }
        public float Frequency { get; set; }
        public bool Synchro { get; set; }

        abstract public void Update();

        public Agent( int id)
        {
            HasFinished = false;
            Frequency = 1000.0F;
            Id = id;
        }

        public IEnumerator<float> CoroutineUpdate()
        {
            
            while(!HasFinished)
            {
                Update();
                Thread.Sleep((int)(1000 / Frequency));
                yield return 0.0F;
            }
            Console.WriteLine("agent o numerze ID: " + Id + " skończył pracę");
            yield break;
        }

        public void run()
        {
            while(!HasFinished)
            {
                Thread.Sleep((int)(1000 / Frequency));
                Update();
            }
            Console.WriteLine("agent o numerze ID: " + Id + " skończył pracę");
        }


    }
}
