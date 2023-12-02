using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;             //手動加入參考
using System.Management.Automation.Runspaces;   //手動加入參考
using System.Threading;
using System.Reflection;

namespace TestDll
{
    public class Program
    {
        // 取得目前的工作目錄
        public static string currentDirectory = Directory.GetCurrentDirectory() + '\\';
        public static string log_file = "TestDll.log";
        // **** 創建log file ****
        static void CreateDirectoryAndFile()
        {
            string filePath = currentDirectory+log_file;
            try
            {
                // 檢查檔案是否存在，如果不存在則建立，檔案存在內容就清空
                if (!File.Exists(filePath))
                {
                    File.Create(filePath);
                }
                else
                {
                    // 清空內容
                    using (FileStream fs = new FileStream(log_file, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        fs.SetLength(0);
                    }                    
                }
            }
            catch (Exception ex)
            {
                process_log("Error!!! " + ex.Message);
            }
        }

        // **** Test manager log file ****
        public static void process_log(string content)
        {
            // 指定要建立或寫入的檔案的路徑
            string filePath = currentDirectory+log_file;

            try
            {
                // 使用 StreamWriter 打開檔案並appand內容
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.Write("["+DateTime.Now.ToString()+"] "+content+'\n');
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Write log Error!!! " + ex.Message);
            }
        }      
        
        // ***** Executing a dll *****
        static bool Execute_dll(string dllPath)
        {
            string callingDomainName = AppDomain.CurrentDomain.FriendlyName;//Thread.GetDomain().FriendlyName;
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain ad = AppDomain.CreateDomain("TestDll DLL");
            ProxyObject obj = (ProxyObject)ad.CreateInstanceFromAndUnwrap(basePath+callingDomainName, "TestDll.ProxyObject");
            try
            {
                process_log(".... Loading Common.dll ....");
                obj.LoadAssembly(currentDirectory+"Common.dll");
            }
            catch (System.IO.FileNotFoundException)
            {
                process_log("!!! 找不到 Common.dll");
                return false;
            }

            process_log(".... Loading "+dllPath+" ....");
            Object[] p = new object[]{dllPath, new object[]{}, new object[]{}};
            var result = obj.Invoke("RunTestItem",p);
            // process_log("             Invoke .Setup()");
            // obj.Invoke("Setup", p);
            // process_log("             Invoke .Run()");
            // obj.Invoke("Run", p);
            // process_log("             Invoke .UpdateResults()");
            // obj.Invoke("UpdateResults", p);
            // process_log("             Invoke .TearDown()");
            // obj.Invoke("TearDown", p);
            // process_log("             Unload "+dllPath);
            AppDomain.Unload(ad);
            obj = null;
            if(result.ToString() == "True") return true;
            else return false;
        }


        // ============== MAIN ==============

        static void Main(string[] args)
        {
            DateTime startTime, endTime;
            TimeSpan timeSpan;
            bool result = true;
            startTime = DateTime.Now;
            CreateDirectoryAndFile();
            process_log("<<Step 1>> Run..." + args[0]);

            try
            {
                result = Execute_dll(currentDirectory + args[0]);
            }
            catch (Exception ex)
            {
                process_log("Run test Error!!! " + ex.Message);
            }

            process_log("<<Step 2>> 測試結束");

            endTime = DateTime.Now;
            timeSpan = endTime - startTime;
            Console.ReadKey();
            // 输出时间间隔
            process_log("執行花費時間: " + timeSpan.Minutes + "分鐘 " + timeSpan.Seconds + "秒");
            process_log("=================Completed================");
            Environment.Exit(0);            
        }
    }

    class ProxyObject : MarshalByRefObject
    {
        Assembly assembly = null;
        public void LoadAssembly(string myDllPath)
        {
            assembly = Assembly.LoadFile(myDllPath);
        }
        public bool Invoke(string methodName, params Object[] args)
        {
            if (assembly == null)
                return false;
            var cName=assembly.GetTypes().First(m=>!m.IsAbstract && m.IsClass);
            string fullClassName = cName.FullName;
            Type tp = assembly.GetType(fullClassName);
            if (tp == null)
                return false;
            MethodInfo method = tp.GetMethod(methodName);
            if (method == null)
                return false;
            Object obj = Activator.CreateInstance(tp);
            var r = method.Invoke(obj, args);
            if(r.ToString() == "True") 
                return true;
            else 
                return false;
        }
    }    
}
