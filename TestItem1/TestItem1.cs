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

            // 取得目前執行檔的路徑
            string filePath = "c:\\Users\\edison\\Downloads\\PXE-master\\TestManager\\TestManager\\bin\\Debug\\TestManager.exe";

            // 取得版本信息
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(filePath);

            // 顯示版本號
            Console.WriteLine($"File Version: {versionInfo.FileVersion}");
            Console.WriteLine($"Product Version: {versionInfo.ProductVersion}");
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
