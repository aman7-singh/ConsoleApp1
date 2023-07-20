using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics.CodeAnalysis;

namespace TplChannels
{
    public static class MultiStageChannels
    {
        public static async void PrintReverseWords()
        {
            string url = @"http://www.gutenberg.org/files/6130/6130-0.txt";
             string text = Task.Run(async () => await new HttpClient().GetStringAsync(url)).Result;
            var text1 = @"gerb koobe dailI lol taat baabaab berg eBook of The Iliad, by Homer             
This eBook is for the use of anyone anywhere in the United States and
most other parts of the world at no cost and with almost no restrictions
whatsoever.You may copy it, give it away or re - use it under the terms
of the Project Gutenberg License included with this eBook or online at
www.gutenberg.org.If you are not located in the United States, you
will have to check the laws of the country where you are located before
using this eBook.";
            var text2 = @"gerb koobe dailI berg eBook of The Iliad, by Homer             
This eBook is for the use of anyone anywhere in the United States and
most other parts of the world at no cost and with almost no restrictions
whatsoever.You may copy it, give it away or re - use it under the terms
of the Project Gutenberg License included with this eBook or online at
www.gutenberg.org.If you are not located in the United States, you
will have to check the laws of the country where you are located before
using this eBook.";
            var text3 = @"gerb koobe dailI berg eBook of The Iliad, by Homer             
This eBook is for the use of anyone anywhere in the United States and
most other parts of the world at no cost and with almost no restrictions
whatsoever.You may copy it, give it away or re - use it under the terms
of the Project Gutenberg License included with this eBook or online at
www.gutenberg.org.If you are not located in the United States, you
will have to check the laws of the country where you are located before
using this eBook.";

            var msg = new List<string>()
            {
                text//1, text2.ToUpper(), text3.ToLower()
            };

            var generatorChannel = Generator(msg);
            var wordsChannel = GetWords(generatorChannel);
            var largeWords = FilterLargeWords(wordsChannel);
            var reverseWords = FindReversedWords(largeWords);
            PrintReversedWords(reverseWords);

        }

        #region The Generator - Enumerate the Files
        static ChannelReader<string> Generator(List<string> msg)
        {
            var output = Channel.CreateUnbounded<string>();
            Task.Run(async () =>
            {
                foreach (var text in msg)
                {
                   // Console.WriteLine($"################ Added text: '{text}' to channel");
                    await output.Writer.WriteAsync(text);
                    await Task.Delay(200);
                }
                output.Writer.Complete();
            });
            return output;
        }
        #endregion

        #region Stage 1
        static ChannelReader<string[]> GetWords(ChannelReader<string> theReader)
        {
            var output = Channel.CreateUnbounded<string[]>();
            Task.Run(async () =>
            {
                //while when channel is not complete 
                while (await theReader.WaitToReadAsync())
                {
                    while (theReader.TryRead(out var text))
                    {
                        //Console.WriteLine($"###########    Creating word list...{string.Join(",\n", text)}");
                        await output.Writer.WriteAsync(splitTextToWords(text));
                        await Task.Delay(500);
                    }
                }
                output.Writer.Complete();
            });
            return output;
        }

        private static string[] splitTextToWords(string text)
        {
            // Remove common punctuation by replacing all non-letter characters 
            // with a space character.
            char[] tokens = text.Select(c => char.IsLetter(c) ? c : ' ').ToArray();
            text = new string(tokens);
            // Separate the text into an array of words.
            return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion

        #region Stage 2
        static ChannelReader<string[]> FilterLargeWords(ChannelReader<string[]> theReader)
        {
            var output = Channel.CreateUnbounded<string[]>();
            Task.Run(async () =>
            {
                //while when channel is not complete 
                while (await theReader.WaitToReadAsync())
                {
                    while (theReader.TryRead(out var words))
                    {
                        //Console.WriteLine($"############  Filtering word list...{string.Join(",\n", words)}");
                        await output.Writer.WriteAsync(getLargeWords(words));
                    }
                }
                output.Writer.Complete();
            });
            return output;
        }

        private static string[] getLargeWords(string[] words)
        {
            return words.Where(word => word.Length > 3)
                    .Distinct()
                    .ToArray();
        }
        #endregion

        #region Stage 3
        static ChannelReader<ParallelQuery<string>> FindReversedWords(ChannelReader<string[]> theReader)
        {
            var output = Channel.CreateUnbounded<ParallelQuery<string>>();
            Task.Run(async () =>
            {
                //while when channel is not complete 
                while (await theReader.WaitToReadAsync())
                {
                    while (theReader.TryRead(out var words))
                    {
                        //Console.WriteLine($"########### Finding reversed words...{string.Join(",\n", words)}");
                        await output.Writer.WriteAsync(getReversedWords(words));
                    }
                }
                output.Writer.Complete();
            });
            return output;
        }
        private static ParallelQuery<string> getReversedWords(string[] words)
        {
            var wordsSet = new HashSet<string>(words);
            return from word in words.AsParallel()
                   let reverse = new string(word.Reverse().ToArray())
                   where word != reverse && wordsSet.Contains(reverse)
                   select word;
        }
        #endregion

        #region Sink Stage

        static void PrintReversedWords(ChannelReader<ParallelQuery<string>> theReader)
        {
            Task.Run(async () =>
            {
                //while when channel is not complete 
                while (await theReader.WaitToReadAsync())
                {
                    while (theReader.TryRead(out var words))
                    {
                        words.ToList().ForEach(word =>
                        {
                            Console.WriteLine("Found reversed words {0}/{1}",
                      word, new string(word.Reverse().ToArray()));
                        });
                       // Console.WriteLine($"########### Reversed words...{string.Join(",\n", words.ToList())}");
                    }
                }
            });
        }
        #endregion

    }

}
