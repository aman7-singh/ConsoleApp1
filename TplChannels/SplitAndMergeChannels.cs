using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TplChannels
{
    public class SplitAndMergeChannels
    {
        public static async Task RunPipeline()
        {
            var unboundedChannel = Channel.CreateUnbounded<string>();
            // var unboundedChannel = Channel.CreateBounded<string>(1);

            var producer = new Producer(unboundedChannel.Writer, 0);
            var consumer = new Consumer(unboundedChannel.Reader, 0);
            var writing = Task.Run(async () =>
            {
                await producer.ProduceWorkAsync();
            });
            //await consumer.ConsumeWorkAsync();

            var splitter = await Split(unboundedChannel.Reader, 5);
            Console.WriteLine($"Number of channels after split {splitter.Length}");
            await MergeChannel(splitter);                       
        }

        async static Task<ChannelReader<T>> MergeChannel<T>(params ChannelReader<T>[] channelReaders)
        {
            var output = Channel.CreateUnbounded<T>();
            await Task.Run(async () =>
            {
                async Task Redirect(ChannelReader<T> channelReader)
                {
                    while (await channelReader.WaitToReadAsync())
                    {
                        while (channelReader.TryRead(out var msg))
                        {
                            Console.WriteLine($"Merging to one channel: {msg}");
                            await output.Writer.WriteAsync(msg);
                        }
                    }
                }
                await Task.WhenAll(channelReaders.Select(channelReader => Redirect(channelReader)).ToArray());
                output.Writer.Complete();
            });
            return output;
        }

        async static Task<ChannelReader<string>[]> Split<T>(ChannelReader<T> channelReader, int n)
        {
            var outputs = new Channel<string>[n];
            for (var i = 0; i < n; i++)
                outputs[i] = Channel.CreateUnbounded<string>();

            await Task.Run(async () =>
            {
                int index = 0;
                while (await channelReader.WaitToReadAsync())
                {
                    while (channelReader.TryRead(out var msg))
                    {
                        Console.WriteLine($"splitter {index} : Completing todo: {msg}");
                        await outputs[0].Writer.WriteAsync($"msg from channels {index} - {msg}");
                        await outputs[1].Writer.WriteAsync($"msg from channels {index} - {msg}");
                        await outputs[2].Writer.WriteAsync($"msg from channels {index} - {msg}");

                        await Task.Delay(500);
                        index = (index + 1) % n;
                    }
                }
                foreach (var ch in outputs)
                    ch.Writer.Complete();
            });
            return outputs.Select(ch => ch.Reader).ToArray();
        }
    }

    public class Producer
    {
        private static readonly List<string> _todoItems = new List<string>()
        {
            "Make a coffee",
            "Read CodeMaze articles",
            "Go for a run",
            "Eat lunch",
            "1 Make a coffee",
            "1 Read CodeMaze articles",
            "1 Go for a run",
            "1 Eat lunch",
            "2 Make a coffee",
            "2 Read CodeMaze articles",
            "2 Go for a run",
            "2 Eat lunch",
            "3 Make a coffee",
            "3 Read CodeMaze articles",
            "3 Go for a run",
            "3 Eat lunch",
        };

        private readonly ChannelWriter<string> _channelWriter;
        private int _channelId;
        public Producer(ChannelWriter<string> channelWriter, int channelId)
        {
            _channelWriter = channelWriter;
            _channelId = channelId;
        }

        public async Task ProduceWorkAsync()
        {
            foreach (var todo in _todoItems)
            {
                Console.WriteLine($"## ProduceWorkAsync - ChannelId : {_channelId}, Added todo: '{todo}' to channel");
                await _channelWriter.WriteAsync(todo);
                await Task.Delay(200);
            }
            _channelWriter.Complete();
        }
    }
    public class Consumer
    {
        private readonly ChannelReader<string> _channelReader;
        private int _channelId;
        public Consumer(ChannelReader<string> channelReader, int channelId)
        {
            _channelReader = channelReader;
            _channelId = channelId;
        }

        public async Task ConsumeWorkAsync()
        {
            try
            {
                while (await _channelReader.WaitToReadAsync())
                {
                    while (_channelReader.TryRead(out var todo))
                    {
                        Console.WriteLine($"ConsumeWorkAsync - ChannelId : {_channelId}, Completing todo: {todo}");
                        await Task.Delay(500);
                    }
                }
                await _channelReader.Completion;
            }
            catch (ChannelClosedException)
            {
                Console.WriteLine("Channel was closed");
            }
        }
    }
}
