using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Finger
{
    class Program
    {
        private const string DefaultPipeName = "my_fucking_pipe";
        static void Main(string[] args)
        {
            try
            {
                Process firstProc = new Process();
                firstProc.StartInfo.FileName = "../DisplayWindow/DisplayWindow.exe";
                firstProc.EnableRaisingEvents = true;
                Console.WriteLine("Looking for " + firstProc.StartInfo.FileName);

                for (int x = 0; x < 5; x ++)
                firstProc.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Looking for " + firstProc.StartInfo.FileName);
                Console.WriteLine(ex.Message + " " + firstProc.StartInfo.FileName);
                
            }

            Console.WriteLine("Welcome to my Hot Server!");
            new HotServer(DefaultPipeName);
        }
    }
}
