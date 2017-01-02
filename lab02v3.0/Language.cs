// The Class adds country to the a language
// And has a name of language and returnes it

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab02v3._0
{
    [Serializable]
    class Language : IEnumerable
    {
        string language { get; set; }

        public List<Country> country = new List<Country>();

        public override string ToString()
        {
            return language;
        }

        public string ToString2()
        {
            return "Language ------------------------------- " + language;
        }

        public void Add_Country(Country s)
        {
            country.Add(s);
        }

        public void Remove_Country(string number)
        {
            for (int i = 0; i < country.Count; i++)
                if (country[i].ToString() == number)
                    country.RemoveAt(i);
        }

        public int Length
        {
            get { return country.Count; }
        }

        public Country this[int index]
        {
            get
            {
                return country[index];
            }
            set
            {
                country[index] = value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return country.GetEnumerator();
        }

        public Language(string name)
        {
            this.language = name;
        }

        public Language()
        {
            
        }
    }
}
