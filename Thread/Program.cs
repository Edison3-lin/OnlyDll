using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TT
{
    internal class Program
    {
        static Stopwatch ss = new Stopwatch();
        static void Main()
        {
            // 启动一个新线程来监测主程序的执行时间
            Thread monitoringThread = new Thread(MonitorExecutionTime);
            monitoringThread.Start();

            // 主程序开始执行
            Console.WriteLine("Main program started.");

            // 模拟主程序的一些工作
            for (int i = 0; i < 100; i++)
            {
                // 执行一些操作
                 Thread.Sleep(100);
            }

            // 主程序执行完毕
            Console.WriteLine("Main program finished.");

            // 停止计时器
            ss.Stop();

            // 等待监测线程结束
            monitoringThread.Join();

            Console.WriteLine($"Main program execution time: {ss.Elapsed.TotalSeconds} seconds");
        }

        static void MonitorExecutionTime()
        {
            // 启动计时器
            ss.Start();

            // 模拟监测线程的一些工作
            do
            {
                Thread.Sleep(2000); // 每隔一秒输出一次当前执行时间

                Console.WriteLine($"Elapsed time: {ss.Elapsed.TotalSeconds} seconds");

                // 在实际应用中，你可能需要在此处添加一些退出条件
            } while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Tab));
            
        }

    }
}
