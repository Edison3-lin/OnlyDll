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
using System.Diagnostics;

namespace TestDll
{
    public class Program
    {
        // 取得目前的工作目錄
        public static string currentDirectory = Directory.GetCurrentDirectory() + '\\';
        public static string log_file = "TestDll.log";
        static Stopwatch ItemWatch;

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
            // 启动计时器
ItemWatch = new Stopwatch();
ItemWatch.Start();


            process_log(".... Loading "+dllPath+" ....");
            Object[] p = new object[]{ dllPath, new object[]{}, new object[]{}, new object[]{}, new object[]{} };
            var result = obj.Invoke("RunTestItem",p);

// 停止计时器
ItemWatch.Stop();

            AppDomain.Unload(ad);
            obj = null;
            if(result.ToString() == "True") return true;
            else return false;
        }

        static void MonitorExecutionTime(object param)
        {
            int timeout = (int)param;
            // 模拟监测线程的一些工作
            do
            {
                Thread.Sleep(1000); // 每隔一秒输出一次当前执行时间
                if(ItemWatch.Elapsed.TotalSeconds >= timeout)
                {
                    Console.WriteLine($"Elapsed time: {ItemWatch.Elapsed.TotalSeconds} milliseconds");
                    // // 停止计时器
                    // ItemWatch.Stop();
                    // break;
                }
                // 添加一些退出条件
            } while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Tab));
            
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
// 启动一个新线程来监测主程序的执行时间
object tout = 5;
Thread monitoringThread = new Thread(new ParameterizedThreadStart(MonitorExecutionTime));
monitoringThread.Start(tout);

for(int i=0;i<5;i++)
{
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


            // 输出时间间隔
            process_log("執行花費時間: " + timeSpan.Minutes + "分鐘 " + timeSpan.Seconds + "秒");
            process_log("=================Completed================");
// 等待监测线程结束
// monitoringThread.Join();
// Console.WriteLine($"Main program execution time: {ItemWatch.Elapsed.TotalSeconds} milliseconds");
            // Console.ReadKey();
}
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
