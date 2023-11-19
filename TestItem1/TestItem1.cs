using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Diagnostics;

namespace TestItem1
{
    public class TestItem1
    {
        private const string DllName = "TestItem1";

        public int Setup()
        {
            // common.Setup
            return 11;
        }

        public int Run()
        {
            // common.Setup
            Console.WriteLine("I'm TestItem1 !!!!!!!!!!");
            return 12;
        }

        public int UpdateResults()
        {
            return 13;
        }

        public int TearDown()
        {
            return 14;
        }
    }
}
