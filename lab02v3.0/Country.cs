using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02v3._0
{
    [Serializable]
    class Country : IEnumerable
    {
        public string country { get; set; }
        public List<Series> series = new List<Series>();

        public Country()
        {

        }

        public override string ToString()
        {
            return country;
        }

        public string ToString2()
        {
            return country + " ------------------------------------------------------------";
        }

        public Country(string name)
        {
            this.country = name;
        }

        public void Add_Series(Series s)
        {
            series.Add(s);
        }

        public void Remove_Series(string number)
        {
            for (int i = 0; i < series.Count; i++)
                if (series[i].ToString() == number)
                    series.RemoveAt(i);
        }

        public int Length
        {
            get { return series.Count; }
        }

        public Series this[int index]
        {
            get
            {
                return series[index];
            }
            set
            {
                series[index] = value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return series.GetEnumerator();
        }

    }
}
