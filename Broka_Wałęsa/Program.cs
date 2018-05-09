using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Broka_Wałęsa
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Console.WriteLine("załadowano");
            Console.ReadKey();
            //List<IRunable> listaAgentow = GenerateRunnables(161,40000);
            List<IRunable> listaAgentów = GenerateDictionaryRunables(@"pliki\wieszcz.txt",5);
            //RunThreads(listaAgentow);
            RunWordThreads(listaAgentów);
            //RunFibers(listaAgentow
            Console.ReadKey();

        }

        static List<IRunable> GenerateRunnables(int n, int m)
        {
            List<IRunable> runablesList = new List<IRunable>();
            List<SumAgent> sumAgentList = new List<SumAgent>();
            List<int> liczby = new List<int>();
            /*
            int mult = 10;
            for (int i = 0; i < mult; i++)
                runablesList.Add(new ConstantCoutingAgent(i));
            for (int i = 0; i < mult; i++)
                runablesList.Add(new SineGenerationgAgent(i + mult));
            for (int i = 0; i < 10; i++)
                runablesList.Add(new CountingAgent(i + 2 * mult));
            */
            
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

        static List<IRunable> GenerateDictionaryRunables(string filePath, int n)
        {
            List<IRunable> runablesList = new List<IRunable>();
            List<WordSumAgent> wordSumAgentList = new List<WordSumAgent>();

            FileStream plik = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(plik);

            string readedText = reader.ReadToEnd();
            readedText = readedText.Replace(".", "");
            readedText = readedText.Replace(",", "");
            readedText = readedText.Replace("?", "");
            readedText = readedText.Replace("!", "");

            reader.Close();
            plik.Close();

            List<string> listaSlow = new List<string>(readedText.Split(' '));
            //foreach(string str in listaSlow)
            //{
            //    Console.WriteLine(str);
            //}

            bool podzielne = false;
            int liczebnosc = 0;
            int m = listaSlow.Count;
            liczebnosc = m / n;

            if (m % n == 0)
            {
                podzielne = true;
            }

            int ilu = n;
            for( int i = 0; i < n; i++)
            {
                if (podzielne)
                {
                    wordSumAgentList.Add(new WordSumAgent(i, listaSlow.GetRange(i * liczebnosc, liczebnosc)));
                }
                else
                {
                    if (i == ilu - 1)
                    {
                        wordSumAgentList.Add(new WordSumAgent(i, listaSlow.GetRange(i * liczebnosc, listaSlow.Count - i * liczebnosc)));
                    }
                    else
                    {
                        wordSumAgentList.Add(new WordSumAgent(i, listaSlow.GetRange(i * liczebnosc, liczebnosc)));
                    }
                }
                runablesList.Add(wordSumAgentList[i]);
            }
            runablesList.Add(new MasterWordSumAgent(ilu, wordSumAgentList.GetRange(0, ilu)));

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

            bool HasAllTreadEnded = false;
            List<IRunable> sumujacyCygani = agentList.GetRange(0, agentList.Count - 1);

            while (!HasAllTreadEnded)
            {
                HasAllTreadEnded = sumujacyCygani.All(a => a.HasFinished == true);
                Thread.Sleep(100);
            }

            MasterSumAgent ichKrol = (MasterSumAgent)agentList[agentList.Count - 1];
            Thread wontekKrola = new Thread(agentList[agentList.Count-1].run);
            wontekKrola.Start();

            HasAllTreadEnded = false;
            while (!HasAllTreadEnded)
            {
                HasAllTreadEnded = agentList.All(a => a.HasFinished == true);
                Thread.Sleep(100);
            }
            Console.WriteLine("agent sumujący zakończył pracę z wynikiem: " + ichKrol.Sum);
        }

        static void RunWordThreads(List<IRunable> agentList)
        {
            for (int i = 0; i < agentList.Count - 1; i++)
            {
                Thread wontek = new Thread(agentList[i].run);
                wontek.Start();
            }

            bool HasAllTreadEnded = false;
            List<IRunable> wieszcze = agentList.GetRange(0, agentList.Count - 1);

            while (!HasAllTreadEnded)
            {
                HasAllTreadEnded = wieszcze.All(a => a.HasFinished == true);
                Thread.Sleep(100);
            }

            MasterWordSumAgent krolWieszczy = (MasterWordSumAgent)agentList[agentList.Count - 1];

            var jegoWontek = new Thread(krolWieszczy.run);
            jegoWontek.Start();
            HasAllTreadEnded = false;

            while (!HasAllTreadEnded)
            {
                HasAllTreadEnded = agentList.All(a => a.HasFinished == true);
                Thread.Sleep(100);
            }

            Console.WriteLine("zakończono");

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
