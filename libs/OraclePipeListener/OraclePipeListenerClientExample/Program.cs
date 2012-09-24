using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OraclePipeListenerClientExample
{
    class Program
    {
        Tools.Db.OraclePipeListener pipeListener;

        public Program()
        {
            pipeListener = new Tools.Db.OraclePipeListener("User Id=SYSTEM;Password=1111;Data Source=localhost", "SYSTEM", "TEST_TABLE");
            pipeListener.OnChanged += new Tools.Db.OnChanged(pipeListener_OnChanged);
        }
        static void Main(string[] args)
        {

            Program PipeListenerClient = new Program();
            Console.WriteLine("Waiting for change notification...");
            Console.ReadLine();

        }

        void pipeListener_OnChanged()
        {
            Console.WriteLine("Data changed");
        }
    }
}
