using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C1
{
    public class Class1
    {
        public static int TearDown()
        {
            //Console.WriteLine("SSSSSSSSSSSSSSSSSSSSSSSSSRR");
            return 1;
        }
        public static int UpdateResults()
        {
            //Console.WriteLine("SSSSSSSSSSSSSSSSSSSSSSSSSRR");
            return 1;
        }
        public static int Setup()
        {
            // Console.WriteLine(a.ToString() + "   "+ c);
            // Console.ReadKey();
            return 1;
        }

        public static int Run(int a, int b, string c)
        {
            Console.WriteLine((a+b).ToString() +"歲的"+ c);
            Console.ReadKey();
            return 0;
        }


    }
}
