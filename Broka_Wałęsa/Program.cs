using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Broka_Wałęsa
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("załadowano");
            Console.ReadKey();
            List<IRunable> listaAgentow = GenerateRunnables(9,4000);
            RunThreads(listaAgentow);
            //RunFibers(listaAgentow);
            Console.ReadKey();

        }

        static List<IRunable> GenerateRunnables(int n, int m)
        {
            List<IRunable> runablesList = new List<IRunable>();
            List<SumAgent> sumAgentList = new List<SumAgent>();
            List<int> liczby = new List<int>();

            //int mult = 10;
            //for (int i = 0; i < mult; i++)
            //    runablesList.Add(new ConstantCoutingAgent(i));
            //for (int i = 0; i < mult; i++)
            //    runablesList.Add(new SineGenerationgAgent(i + mult));
            //for (int i = 0; i < 10; i++)
            //    runablesList.Add(new CountingAgent(i + 2 * mult));

            var random = new Random();
            for (int i = 0; i < m; i++)
            {
                liczby.Add(random.Next(100, 2000));
            }

            bool podzielne = false;
            int liczebnosc = 0;
            liczebnosc = m / n;

            if(m%n == 0)
            {
                podzielne = true;
            }

            int ilu = n;
            for (int i = 0; i < ilu; i++)
            {
                if (podzielne)
                {
                    sumAgentList.Add(new SumAgent(i, liczby.GetRange(i * liczebnosc, liczebnosc)));
                }
                else
                {
                    if (i == ilu - 1)
                    {
                        sumAgentList.Add(new SumAgent(i, liczby.GetRange(i * liczebnosc, liczby.Count-i*liczebnosc)));
                    }
                    else
                    {
                        sumAgentList.Add(new SumAgent(i, liczby.GetRange(i * liczebnosc, liczebnosc)));
                    }
                }
                runablesList.Add(sumAgentList[i]);
            }
            runablesList.Add(new MasterSumAgent(ilu, sumAgentList.GetRange(0, ilu)));

            return runablesList;
        }
        
        static void RunThreads(List<IRunable> agentList)
        {
            ////List<Thread> wontki = new List<Thread>();
            //
            //foreach(IRunable i in agentList)
            //{
            //    Thread wontek = new Thread(i.run);
            //    
            //   // wontki.Add(wontek);
            //    wontek.Start();
            //}
            //
            //bool anyCondition = true;
            //while (anyCondition)
            //{
            //    anyCondition = !agentList.All(a => a.HasFinished == true);
            //    Thread.Sleep(100);
            //}
            //Console.WriteLine("wszystkie wontki zakończyły swoj proces xD");

            for(int i =0; i<agentList.Count-1; i++)
            {
                Thread wontek = new Thread(agentList[i].run);
                wontek.Start();
            }

            bool anyCondition = true;
            List<IRunable> sumujaceCygany = agentList.GetRange(0, agentList.Count - 1);

            while (anyCondition)
            {
                anyCondition = !sumujaceCygany.All(a => a.HasFinished == true);
                Thread.Sleep(100);
            }

            MasterSumAgent krolCyganow = (MasterSumAgent)agentList[agentList.Count - 1];
            Thread wontekSumujacyZKrolemCyganow = new Thread(krolCyganow.run);
            wontekSumujacyZKrolemCyganow.Start();

            anyCondition = true;
            while (anyCondition)
            {
                anyCondition = !agentList.All(a => a.HasFinished == true);
                Thread.Sleep(100);
            }
            Console.WriteLine("agent sumujący zakończył pracę z wynikiem: " + krolCyganow.Sum);
        }

        static void RunFibers(List<IRunable> agentList)
        {
            //IEnumerable<IEnumerator<float>> fibers = agentList.Select(a => a.CoroutineUpdate());

            List<IEnumerator<float>> fibers = new List<IEnumerator<float>>();

            foreach (IRunable a in agentList)
                fibers.Add(a.CoroutineUpdate());

            bool endCondition = false;

            while(!endCondition)
            {
                foreach(var f in fibers)
                {
                    f.MoveNext();
                }
                endCondition = agentList.All(a => a.HasFinished == true);
                //Console.WriteLine(endCondition);
            }
        }
    }
}
