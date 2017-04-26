using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheServer
{
    class Program
    {
        private const string DefaultPipeName = "my_fucking_pipe";
        static void Main(string[] args)
        {
            try
            {
                Process firstProc = new Process();
                firstProc.StartInfo.FileName = "CustomForms.exe";
                firstProc.EnableRaisingEvents = true;

                for(int x = 0; x < 20; x ++)
                firstProc.Start();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Welcome to my Hot Server!");
            new HotServer(DefaultPipeName);
        }
    }
}
