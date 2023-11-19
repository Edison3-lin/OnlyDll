using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;             //手動加入參考
using System.Management.Automation.Runspaces;   //手動加入參考
using Common;

namespace Test3
{
    public class Test3
    {
        private const string DllName = "Test3";

        public int Setup()
        {
            // common.Setup

            return 31;
        }

        public int Run()
        {
            Console.WriteLine("Test3.dll OK!!");
            return 32;
        }

        public int UpdateResults()
        {
            return 33;
        }

        public int TearDown()
        {
            return 34;
        }

    }
}
