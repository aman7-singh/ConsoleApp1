using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Threading;
using BenchmarkDotNet.Attributes;

namespace TplChannels
{
    public class SingleChannel
    {
        public static async Task ChannelRun(int delayMs, int numberOfReaders,
            int howManyMessages = 100, int maxCapacity = 10)
        {
            //use a bounded channel is useful if you have a slow consumer
            //unbounded may lead to OutOfMemoryException
            var channel = Channel.CreateBounded<string>(maxCapacity);

            var reader = channel.Reader;
            var writer = channel.Writer;
            var tasks = new List<Task>();

            

            //consumer
            await Task.Run(async () =>
            {
                for (int i = 0; i < numberOfReaders; i++)
                {
                    tasks.Add(Task.Run(() => Read(reader, i + 1, delayMs)));
                    await Task.Delay(delayMs);
                }
            });

            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(2));
            
            //producer 
            await Write(writer, howManyMessages, cts.Token);

            await Task.WhenAll(tasks);   
        }

        public static async Task Write(ChannelWriter<string> writer, int howManyMessages, CancellationToken token = default)
        {
            //Write message to the channel, but since Read has Delay
            //we will get back pressure applied to the writer, which causes it to block
            //when writing. Unbounded channels do not block ever
            for (int i = 0; i < howManyMessages; i++)
            {
                Console.WriteLine($"Writing at {DateTime.Now.ToLongTimeString()}");
                await writer.WriteAsync($"SomeText message '{i}", token);
                await Task.Delay(200);       // slow writing speed

            }

            //Tell readers we are complete with writing, to stop them awaiting 
            //WaitToReadAsync() forever
            writer.Complete();
        }

        static async Task Read(ChannelReader<string> theReader, int readerNumber, int delayMs)
        {
            //while when channel is not complete 
            while (await theReader.WaitToReadAsync())
            {
                while (theReader.TryRead(out var theMessage))
                {
                    Console.WriteLine($"Reader {readerNumber} read '{theMessage}' at {DateTime.Now.ToLongTimeString()}");
                    //simulate some work
                    await Task.Delay(delayMs);     //slow reading speed
                }
            }
            await theReader.Completion;
        }
    }
}
