using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks.Dataflow;

namespace TplDataflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Action Block!");

            var block = new ActionBlock<int>((timeout) =>
            {
                Thread.Sleep(timeout);
                Console.WriteLine("inside Action block!");
            });

            for(int i=0; i<10; i++)
            {
                if(block.Post(1000))
                {
                    Console.WriteLine("Success");
                }
                else
                {
                    Console.WriteLine("Failure");
                }
            }
            Console.ReadKey();
        }
    }
}
