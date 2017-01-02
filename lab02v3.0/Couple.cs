using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02v3._0
{
    [Serializable]
    class Couple
    {
        public string name { get; set; }
        public string name2 { get; set; }

        public override string ToString()
        {
            return name + " and " + name2;
        }

        public string ToString2()
        {
            return "------ " + name + " and " + name2;
        }

        public Couple()
        {

        }
    }
}
