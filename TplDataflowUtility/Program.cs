using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TplDataflowUtility
{
    class Program
    {
        static void Main()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            //TransformBlockExample();
            // Downloads the requested resource as a string.
            var downloadString = DownloadString();

            // Separates the specified text into an array of words.
            var createWordList = CreateWordList();

            // Removes short words and duplicates.
            var filterWordList = FilterWordList();

            // Finds all words in the specified collection whose reverse also 
            // exists in the collection.
            var findReversedWords = FindReversedWords();

            // Prints the provided reversed words to the console.    
            var printReversedWords = PrintReversedWords();

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            downloadString.LinkTo(createWordList, linkOptions);
            createWordList.LinkTo(filterWordList, linkOptions);
            filterWordList.LinkTo(findReversedWords, linkOptions);
            findReversedWords.LinkTo(printReversedWords, linkOptions);

            downloadString.Post("http://www.gutenberg.org/files/6130/6130-0.txt");
            //downloadString.Complete();

            printReversedWords.Completion.Wait();

            Console.WriteLine("Main thread ");

            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
        }


        private static bool Counter(string input)
        {
            return input.Length < 5;
        }

        private static ActionBlock<string> PrintReversedWords()
        {
            return new ActionBlock<string>(reversedWord =>
            {
                Console.WriteLine("Found reversed words {0}/{1}",
                    reversedWord, new string(reversedWord.Reverse().ToArray()));
            });
        }

        private static TransformManyBlock<string[], string> FindReversedWords()
        {
            return new TransformManyBlock<string[], string>(words =>
            {
                Console.WriteLine("Finding reversed words...");
                var wordsSet = new HashSet<string>(words);
                return from word in words.AsParallel()
                       let reverse = new string(word.Reverse().ToArray())
                       where word != reverse && wordsSet.Contains(reverse)
                       select word;
            });
        }

        private static TransformBlock<string[], string[]> FilterWordList()
        {
            return new TransformBlock<string[], string[]>(words =>
            {
                Console.WriteLine("Filtering word list...");
                return words
                    .Where(word => word.Length > 3)
                    .Distinct()
                    .ToArray();
            });
        }

        private static TransformBlock<string, string[]> CreateWordList()
        {
            return new TransformBlock<string, string[]>(text =>
            {
                Console.WriteLine("Creating word list...");
                // Remove common punctuation by replacing all non-letter characters 
                // with a space character.
                char[] tokens = text.Select(c => char.IsLetter(c) ? c : ' ').ToArray();
                text = new string(tokens);
                // Separate the text into an array of words.
                return text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            });
        }

        private static TransformBlock<string, string> DownloadString()
        {
            return new TransformBlock<string, string>(async uri =>
            {
                Console.WriteLine("Downloading '{0}'...", uri);
                return await new HttpClient().GetStringAsync(uri);
            });
        }

        private static void TransformBlockExample()
        {
            var func = new Func<int, int>(n =>
            {
                Thread.Sleep(1000);
                return n * n;
            });

            var trfBlock = new TransformBlock<int, int>(func);
            Action<Task<int>> action = task =>
            {
                int n = task.Result;
                Console.WriteLine(n);
            };

            for (int i = 0; i < 10; i++)
            {
                trfBlock.Post(i);
            }

            for (int i = 0; i < 10; i++)
            {
                var result = trfBlock.ReceiveAsync();
                result.ContinueWith(action);
            }


            Console.WriteLine("Done");
            Console.Read();
            var buffer = new BufferBlock<int>();
            var consumer = ConsumeData(buffer);
            for (int i = 0; i < 10; i++)
            {
                buffer.Post(i);
            }

            buffer.Complete();

            consumer.Wait();

            Console.WriteLine("Time taken: {0} ms.", consumer.Result);
        }

        static async Task<long> ConsumeData(ISourceBlock<int> source)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            while (await source.OutputAvailableAsync())

            {
                int data = source.Receive();

                Console.WriteLine(data);
            }

            watch.Stop();

            return watch.ElapsedMilliseconds;
        }
    }
}
