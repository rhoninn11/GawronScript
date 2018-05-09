using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class WordSumAgent : Agent
    {
        public int Ile { get; set; }
        public int Counter { get; set; }
        public List<string> Source { get; set; }
        public Dictionary<string, int> ZliczoneSlowa { get; set; }

        public WordSumAgent(int id, List<string> slowa) : base(id)
        {
            ZliczoneSlowa = new Dictionary<string, int>();
            Source = slowa;
            Ile = Source.Count;
            Counter = 0;

            Console.WriteLine("Agent o id: " + this.Id + " ma " + Ile + "słów do przeliczenia");
        }


        public override void Update()
        {
            if(Counter < Ile)
            {
                var slowo = Source[Counter].ToLower();
                try
                {
                    var i = ZliczoneSlowa[slowo];
                    i++;
                    ZliczoneSlowa[slowo] = i;
                }
                catch(KeyNotFoundException)
                {
                    ZliczoneSlowa.Add(slowo, 1);
                }
                Counter++;
            }
            else
            {
                HasFinished = true;
            }
        }
    }
}
