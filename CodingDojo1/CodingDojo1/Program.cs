using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingDojo1
{
    /// <summary>
    /// CodingDojo 1 - Armin Reiter
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Test3();

            Console.ReadLine();
        }

        static void Test1()
        {
            Stack<int> s = new Stack<int>();

            s.Push(1);
            s.Push(2);
            s.Push(3);
            Console.WriteLine(s); // 321
            s.Pop();
            s.Push(4);
            Console.WriteLine(s); // 421

            s.Pop();
            s.Pop();
            Console.WriteLine(s); // 1
            s.Push(5);
            Console.WriteLine(s); // 51
        }

        static void Test2()
        {
            Stack<int> s = new Stack<int>();

            for (int i = 0; i < 26; i++)
                s.Push(i);

            Console.WriteLine(s); // 25 24 23 22 ... 1 0

            for (int i = 0; i < 16; i++)
                s.Pop();

            for (int i = 0; i < 10; i++)
                Console.WriteLine(s.Peek());

            Console.WriteLine(s); // 9 8 7 ... 1 0
        }

        static void Test3()
        {
            Stack<TestPerson> s = new Stack<TestPerson>();

            s.Push(new TestPerson(20, "Stefan"));
            s.Push(new TestPerson(25, "Anna"));
            s.Push(new TestPerson(30, "Stefanie"));
            s.Push(new TestPerson(35, "Armin"));

            Console.WriteLine(s);

            Console.WriteLine(s.Peek());

            Console.WriteLine(s.Pop());
            Console.WriteLine(s.Pop());

            Console.WriteLine(s);
        }
    }
}
