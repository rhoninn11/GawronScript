using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broka_Wałęsa
{
    interface IRunable
    {
        void run();

        IEnumerator<float> CoroutineUpdate();

        bool HasFinished { get; set; }
    }
}
