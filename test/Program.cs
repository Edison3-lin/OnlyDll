using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 传递不同数量的参数
            DisplayValues(1, "Hello", 3.14);
            DisplayValues("C#", 42, DateTime.Now);
        }

        static void DisplayValues(params object[] values)
        {
            Console.WriteLine("Number of arguments: " + values.Length);
            
            foreach (var value in values)
            {
                Console.WriteLine(value);
            }
        }
    }
}    
