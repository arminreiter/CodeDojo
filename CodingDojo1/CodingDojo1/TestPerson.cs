using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo1
{
    public class TestPerson
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public TestPerson(int age, string name)
        {
            Age = age;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} - {Age}";
        }
    }
}
