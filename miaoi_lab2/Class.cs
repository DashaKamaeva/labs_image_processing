using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace miaoi_lab2
{
    [Serializable]
    public class Class
    {
        public string name;
        public List<Example> examples= new List<Example>();
    
        public Class()
        {
            name = "all";
        }
        public Class(List<Example> examples)
        {
            this.examples = examples;
        }

        public void addExample(Example example)
        {
            example.id = this.examples.Count + 1;
            this.examples.Add(example);
        }

        public List<Example> findExamplesByClass(string className)
        {
            List<Example> ls = new List<Example>();
            ls = examples.FindAll(e=>e.className==className);
            name = className;
            return ls;
        }
        public List<string> findClasses()
        {
            List<string> ls = new List<string>();
            foreach(Example ex in examples)
            {
                ls.Add(ex.className);
            }
            return new List<string>(ls.Distinct());
        }
    }

    [Serializable]
    public class Example
    {
        public int id;
        public string className;
        public int Nuzlov;
        public int Nkonc;
        public string image;
        public int zond1=0;
        public int zond2=0;

        public Example(Class cl,string className, int Nuzlov, int Nkonc, string image)
        {
            this.id = cl.examples.Count+1 ;
            this.className = className;
            this.Nuzlov = Nuzlov;
            this.Nkonc = Nkonc;
            this.image = image;
        }

        public void changeClassName(string name)
        {
            this.className = name;
        }
        public void addZonds(int z1, int z2)
        {
            zond1 = z1;
            zond2 = z2;
        }


    }
}
