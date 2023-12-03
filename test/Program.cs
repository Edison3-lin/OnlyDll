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
            DisplayValues("Edison", new object[] { 22, "Grace" }, new object[] { 20, 30, "Edison" }, new object[]{"林淑芳", 77}, new object[]{'a', "林宏斌"});
            // DisplayValues("C#", 42, DateTime.Now);
            // object[] p = new object[] { 1, 2, new object[] { 11, 22 } };
            // // 讀取第三個元素（一個陣列）
            // object[] innerArray = (object[])p[2];

            // // 讀取第一個元素（11）
            // int number = (int)innerArray[0];

            // Console.WriteLine(number);  // 輸出：11
        }

        static void DisplayValues(params object[] values)
        {
            Console.WriteLine("Number of arguments: " + values.Length);
            object[] innerArray = (object[])values[1];
            string number = (string)innerArray[1];
            Console.WriteLine(number);  // 輸出：11
        }
    }
}    
