using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    public class MasterWordSumAgent : Agent
    {
        public List<WordSumAgent> Podwladni { get; set; }
        public Dictionary<string,int> PoliczoneSlowa { get; set; }
        public List<KeyValuePair<string,int>> IndexSlow { get; set; }
        public int IluPodwladnych { get; set; }
        public int PodwladniC { get; set; }
        public int IleSlow { get; set; }
        public int SlowaC { get; set; }


        public MasterWordSumAgent(int id, List<WordSumAgent> liczacySlawa) : base(id)
        {
            Podwladni = liczacySlawa;
            IluPodwladnych = Podwladni.Count;
            PodwladniC = 0;
            SlowaC = 0;
            PoliczoneSlowa = new Dictionary<string, int>();
        }

        public override void Update()
        {
            if(PodwladniC < IluPodwladnych)
            {
                if(SlowaC == 0)
                {
                    IleSlow = Podwladni[PodwladniC].ZliczoneSlowa.Count;
                    IndexSlow = Podwladni[PodwladniC].ZliczoneSlowa.ToList();
                }

                if(SlowaC < IleSlow)
                {
                    var slowo = IndexSlow[SlowaC].Key;

                    try
                    {
                        var i = PoliczoneSlowa[slowo];
                        i += IndexSlow[SlowaC].Value;
                        PoliczoneSlowa[slowo] = i;
                    }
                    catch(KeyNotFoundException)
                    {
                        PoliczoneSlowa.Add(slowo, 1);
                    }

                    SlowaC++;
                }
                else
                {
                    SlowaC = 0;
                    PodwladniC++;
                }
            }
            else
            {

                foreach (var i in PoliczoneSlowa)
                {
                    Console.WriteLine(i.Key + " x " + i.Value);
                }
                hasFinished = true;
            }
        }
    }
}
