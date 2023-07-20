using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace TplChannels
{
    [MemoryDiagnoser]
    class Program
    {
        [Benchmark(Baseline = true)]
        static async Task Main(string[] args)
        {

            Stopwatch watch = new Stopwatch();
            watch.Start();

            //Console.WriteLine("########## Without Channels #############");
            //var writetask = Write(10, 200);
            //var readtask = Read(1, 500, writetask);
            //await Task.WhenAll(readtask, writetask);

            Console.WriteLine("##########SingleChannel#############");
            await SingleChannel.ChannelRun(500, 1, 10, 5);

            //Console.WriteLine("##########Channels in Sequence#############");
            //MultiStageChannels.PrintReverseWords();

            //Console.WriteLine("##########SplitAndMergeChannels#############");
            //await SplitAndMergeChannels.RunPipeline();



            Console.WriteLine("Main thread ");

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
        }


        private static ConcurrentQueue<string> buffer = new ConcurrentQueue<string>();
        public static async Task Write(int howManyMessages, int delayMs)
        {
            for (int i = 0; i < howManyMessages; i++)
            {
                Console.WriteLine($"Writing - {i} at {DateTime.Now.ToLongTimeString()}");
                buffer.Enqueue($"SomeText message '{i}");
                await Task.Delay(delayMs);       // slow writing speed
            }
        }

        static async Task Read(int readerNumber, int delayMs, Task writetask)
        {
            while (writetask.Status == TaskStatus.Running || writetask.Status == TaskStatus.WaitingForActivation)
            {
                while (buffer.TryDequeue(out string res))
                {
                    Console.WriteLine($"Reader {readerNumber} read '{res}' at {DateTime.Now.ToLongTimeString()}");
                    //simulate some work
                    await Task.Delay(delayMs);     //slow reading speed
                }
            }
        }
    }
}
