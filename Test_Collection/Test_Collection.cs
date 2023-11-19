using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Test_Collection
{
    public class Test_Collection
    {
        private const string DllName = "Test_Collection";
        public static string currentDirectory = Directory.GetCurrentDirectory() + '\\';
 
       public int Setup()
        {
            // common.Setup
            Console.WriteLine("collect setup~~~~~~~~~~~~~~~~~~~");

            return 81;
        }

        public int Run()
        {
            // Testflow.Run(DllName);

            try
            {
                Common.Runnner.RunTestItem(currentDirectory+"TestItem1.dll");
                Common.Runnner.RunTestItem(currentDirectory+"Test3.dll");
                Common.Runnner.RunTestItem(currentDirectory+"TestItem2.dll");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test all " + ex.Message);
            }
            return 82;
        }

        public int UpdateResults()
        {

            // Return the test results from 'Run'
            return 83;
        }

        public int TearDown()
        {
            return 84;
        }
    }
}
