using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class SineGenerationgAgent : Agent
    {
        public SineGenerationgAgent(int id) : base(id)
        {
            iterationLimit = (int)((id % 10) * Frequency);
        }

        public override void Update()
        {
            Math.Sin(iterationCount / Frequency);
            if (iterationCount > iterationLimit)
                HasFinished = true;
            iterationCount++;
            //Console.WriteLine(Id);
        }

        private float output;
        private int iterationCount = 0;
        private int iterationLimit;


        public float Output { get => output; set => output = value; }
    }
}
