using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1
{
    public class Class1
    {
        public static int Setup(int a, string c)
        {
            Console.WriteLine(a.ToString() + "   "+ c);
            Console.ReadKey();
            return 1;
        }

        public static int Run(int a, int b, string c)
        {
            Console.WriteLine((a+b).ToString() +"歲的"+ c);
            Console.ReadKey();
            return 0;
        }
        public static int UpdateResults(string a, int b)
        {
            Console.WriteLine("TTTTTTTTTTTTTTTTT    "+a+"   "+b.ToString());
            return 1;
        }

        public static int TearDown(char a, string b)
        {
            Console.WriteLine("SSSSSSSSSSSSSSS "+a+"   "+b);
            return 1;
        }

    }
}
