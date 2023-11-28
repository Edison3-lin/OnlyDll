using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class Runnner
    {
        public static bool RunTestItem(string dllPath)
        {

            Testflow.General.WriteLog("RunTestItem", dllPath);
            object myResult = null;
            Assembly myDll = Assembly.LoadFile(dllPath);
            var myTest=myDll.GetTypes().First(m=>!m.IsAbstract && m.IsClass);
            object myObj = myDll.CreateInstance(myTest.FullName);
            try
            { myTest.GetMethod("Setup").Invoke(myObj, new object[]{}); }
            catch (Exception ex)
            { 
                Console.WriteLine("Setup() Error!!! "+ex.Message); 
                return false;
            }   

            try            
            { myResult = myTest.GetMethod("Run").Invoke(myObj, new object[]{}); }            
            catch (Exception ex)
            {
                Console.WriteLine("Run() Error!!! "+ex.Message); 
                return false;
            }   

            try            
            { myTest.GetMethod("UpdateResults").Invoke(myObj, new object[]{}); }           
            catch (Exception ex)
            {
                Console.WriteLine("UpdateResults() Error!!! "+ex.Message); 
                return false;
            }   

            try            
            { myTest.GetMethod("TearDown").Invoke(myObj, new object[]{}); }
            catch (Exception ex)
            {
                Console.WriteLine("TearDown() Error!!! "+ex.Message); 
                return false;
            }   

            if(myResult.ToString() == "True") return true;
            else return false;
        }
    }
    public class Testflow
    {
        public static int Setup(string DllName)
        {
            General.WriteLog(DllName, "Testflow::Setup");
            return 90;
        }
        public static int Run(string DllName)
        {
            General.WriteLog(DllName, "Testflow::Run");
            return 90;
        }

        public static int UpdateResults(string DllName, bool passFail)
        {
            General.WriteLog(DllName, "Testflow::UpdateResults");
            return 90;
        }

        public static int TearDown(string DllName)
        {
            General.WriteLog(DllName, "Testflow::TearDown");
            return 90;
        }

        public class General
        {
            public static string currentDirectory = Directory.GetCurrentDirectory() + '\\';
            public static int WriteLog(string DllName, string content)
            {
                string log_path = currentDirectory + DllName + ".log";

                try
                {
                    // 使用 StreamWriter 打開檔案並appand內容
                    using (StreamWriter writer = new StreamWriter(log_path, true))
                    {
                        writer.Write("["+DateTime.Now.ToString()+"] "+content+'\n');
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("WriteLog Error!!! " + ex.Message);
                }
            
                return 0;
            }
        }
    }
}
