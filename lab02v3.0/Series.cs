using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02v3._0
{
    [Serializable]
    internal class Series : IEnumerable // IEnumerable<T>
    {
        public string series { get; set; }
        public List<Couple> couples = new List<Couple>();

        public Series()
        {

        }

        public string ToString2()
        {
            return "--- " + series;
        }

        public override string ToString()
        {
            return series;
        }

        public Series(string name)
        {
            this.series = name;
        }

        public void Add_Couple(string name1Add, string name2Add)
        {
            couples.Add(new Couple { name = name1Add, name2 = name2Add });
        }

        public void Remove_Couple(String nameRemove)
        {
            for (int i = 0; i < couples.Count; i++)
                if (couples[i].ToString() == nameRemove)
                    couples.RemoveAt(i);
        }

        public int Length
        {
            get { return couples.Count; }
        }

       

        IEnumerator IEnumerable.GetEnumerator()
        {
            return couples.GetEnumerator();
        }

    }
}
