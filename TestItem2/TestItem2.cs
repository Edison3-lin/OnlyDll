using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Management.Automation;             //手動加入參考
using System.Management.Automation.Runspaces;   //手動加入參考

namespace TestItem2
{
    public class TestItem2
    {
        private const string DllName = "TestItem2";

        public int Setup()
        {
            // common.Setup
            return 21;
        }

        public int Run()
        {

            Console.WriteLine("I'm TestItem2 ----");
            // common.Setup
            return 22;
        }

        public int UpdateResults()
        {
            return 23;
        }

        public int TearDown()
        {
            return 24;
        }

    }
}
