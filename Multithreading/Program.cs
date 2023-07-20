using System;
using System.Threading;
using System.Threading.Tasks;
using static Multithreading.Program;

namespace Multithreading
{
    public  class Program
    {
        private static void Main(string[] args)
        {
            //Console.WriteLine("SumCallBack ...............");
            //CallBackThread();

            //Console.WriteLine("IO_Operation ...............");
            //IO_Operation();

            //Console.WriteLine("IO_Operation_ManualResetEvent ...............");
            //IO_Operation_ManualResetEvent();

            //Console.WriteLine("IO_Operation_AutoResetEvent ...............");
            //IO_Operation_AutoResetEvent();


            //Console.WriteLine("IO_Operation_Mutex ...............");
            //IO_Operation_Mutex();

            //Console.WriteLine("IO_Operation_Semaphore ...............");
            //IO_Operation_Semaphore();

            //Console.WriteLine("ThreadPool_Operation ...............");
            //ThreadPool_Operation.ThreadPoolOperation();

            Console.WriteLine("TaskOperation ...............");
            TaskOperation taskOperation = new TaskOperation();
            taskOperation.ButtonClick();

            Console.ReadLine();
        }

        private static void Display(string str)
        {
            Console.WriteLine(str + "-- from Display");
        }

        public delegate void WriteCallBack(string str);
        private static void CallBackThread()
        {
            WriteCallBack writeCallBack = new WriteCallBack(Display);
            //WriteCallBack writeCallBack2 = Display;
            SumCallBack sumCallBack = new SumCallBack(12, writeCallBack);
            for (int i=0; i<3; i++)
            {
                new Thread(new ThreadStart(sumCallBack.sum)).Start();      
            }
        }
        private static void IO_Operation()
        {
            Thread T1 = new Thread(new ThreadStart(IO_OperationLock.Write));
            Thread T2 = new Thread(new ThreadStart(IO_OperationLock.Read));

            T1.Start();
            T2.Start();
            Console.WriteLine(T1.Name);
            Console.WriteLine(T2.Name);
            T1.Join();
            T2.Join();
        }
        private static void IO_Operation_ManualResetEvent()
        {
            Thread T1 = new Thread(new ThreadStart(IO_OperationManualResetEvent.Write));
            Thread T2 = new Thread(new ThreadStart(IO_OperationManualResetEvent.Read));

            T1.Start();
            T2.Start();
            Console.WriteLine(T1.Name);
            Console.WriteLine(T2.Name);
            T1.Join();
            T2.Join();
        }
        private static void IO_Operation_AutoResetEvent()
        {
            Thread T1 = new Thread(new ThreadStart(IO_OperationAutoResetEvent.Write));
            Thread T2 = new Thread(new ThreadStart(IO_OperationAutoResetEvent.Read));

            T1.Start();
            T2.Start();
            Console.WriteLine(T1.Name);
            Console.WriteLine(T2.Name);
            T1.Join();
            T2.Join();
        }
        private static void IO_Operation_Mutex()
        {
            for(int i=0; i<3; i++)
            {
                Thread T1 = new Thread(new ThreadStart(IO_OperationMutexSemaphore.Write));
                T1.Start();
            }
        }
        private static void IO_Operation_Semaphore()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread T1 = new Thread(new ThreadStart(IO_OperationMutexSemaphore.WriteSem));
                T1.Start();
            }
        }

        
    }

    public class SumCallBack
    {
        int _num =0;
        WriteCallBack _writeCallBack;
        public SumCallBack(int num, WriteCallBack writeCallBack)
        {
            _num = num;
            _writeCallBack = writeCallBack;
        }
        public void sum()
        {
            _writeCallBack?.Invoke(Environment.CurrentManagedThreadId + " thred " + (_num+2));
        }
    }
    public class IO_OperationLock
    {
        private static readonly object _lock = new object();
        public static void Write()
        {
            for (int i = 0; i < 10; i++)
            {
                lock (_lock)
                {
                    Monitor.Pulse(_lock);
                    Console.WriteLine(Environment.CurrentManagedThreadId + "pulse comp");
                    Thread.Sleep(500);
                    Monitor.Wait(_lock);
                    Console.WriteLine(Environment.CurrentManagedThreadId + "wait comp");
                }
            }
        }
        public static void Read()
        {
            for (int i = 0; i < 10; i++)
            {
                bool locktaken = false;
                Monitor.Enter(_lock, ref locktaken);
                try
                {
                    //Monitor.Pulse(_lock);
                    Console.WriteLine(Environment.CurrentManagedThreadId + "pulse comp");
                    Thread.Sleep(500);
                    //Monitor.Wait(_lock);
                    Console.WriteLine(Environment.CurrentManagedThreadId + "wait comp");

                }
                finally
                {
                    if (locktaken)
                    {
                        Monitor.Exit(_lock);
                    }
                }
            }
        }
    }
    public class IO_OperationManualResetEvent
    {
        private static ManualResetEvent _mre = new ManualResetEvent(false);
        public static void Write()
        {
            for (int i = 0; i < 3; i++)
            {
                _mre.Reset();
                Console.WriteLine(Environment.CurrentManagedThreadId + "pulse comp");
                Thread.Sleep(5000);
                _mre.Set();
                Console.WriteLine(Environment.CurrentManagedThreadId + "wait comp");
            }
        }

        public static void Read()
        {
            for (int i = 0; i < 3; i++)
            {

                //Monitor.Pulse(_lock);
                Console.WriteLine(Environment.CurrentManagedThreadId + "pulse comp");
                Thread.Sleep(500);
                _mre.WaitOne();
                Console.WriteLine(Environment.CurrentManagedThreadId + "wait comp");

            }
        }
    }
    public class IO_OperationAutoResetEvent
    {
        private static AutoResetEvent _mre = new AutoResetEvent(false);
        public static void Write()
        {
            for (int i = 0; i < 3; i++)
            {
                _mre.Reset();
                Console.WriteLine(Environment.CurrentManagedThreadId + "pulse comp");
                Thread.Sleep(5000);
                _mre.Set();
                Console.WriteLine(Environment.CurrentManagedThreadId + "wait comp");
            }
        }

        public static void Read()
        {
            for (int i = 0; i < 3; i++)
            {

                //Monitor.Pulse(_lock);
                Console.WriteLine(Environment.CurrentManagedThreadId + "pulse comp");
                Thread.Sleep(500);
                _mre.WaitOne();
                Console.WriteLine(Environment.CurrentManagedThreadId + "wait comp");

            }
        }
    }
    public class IO_OperationMutexSemaphore
    {
        static Mutex mut = new Mutex();
        public static void Write()
        {    
            Console.WriteLine(Environment.CurrentManagedThreadId + " waiting");
            mut.WaitOne();
            Console.WriteLine(Environment.CurrentManagedThreadId + " working");
            Thread.Sleep(5000);
            mut.ReleaseMutex();
            Console.WriteLine(Environment.CurrentManagedThreadId + " Completed");
        }
        static Semaphore sem = new Semaphore(1,2);
        public static void WriteSem()
        {
            Console.WriteLine(Environment.CurrentManagedThreadId + " waiting");
            sem.WaitOne();
            Console.WriteLine(Environment.CurrentManagedThreadId + " working");
            Thread.Sleep(5000);
            sem.Release();
            Console.WriteLine(Environment.CurrentManagedThreadId + " Completed");
        }
    }

    public class ThreadPool_Operation
    {
        public static void ThreadPoolOperation()
        {
            for (int i = 0; i < 3; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadMethod));
            }
        }
        public static void ThreadMethod(object obj)
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine(thread.ManagedThreadId + " - " + thread.IsBackground + " - " + thread.IsThreadPoolThread);
        }
    }

    public class TaskOperation
    {
        public async void ButtonClick()
        {
            Console.WriteLine("main task started");
            await CallProcess();
            Console.WriteLine("main task completed");
        }

        private async Task CallProcess()
        {
            Console.WriteLine("Call Process Started");
            await Processing();
            Console.WriteLine("Call Process Completed");
        }
        private static Task Processing()
        {
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("task1");

            }).ContinueWith((TaskResult) =>
            {
                Thread.Sleep(1000);
                Console.WriteLine(TaskResult);
                Console.WriteLine("task2");
            });
            return Task.Run(() =>
            {
                try
                {
                    Thread.Sleep(5000);
                    throw new Exception("my exception");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            });
        }
    }
}
