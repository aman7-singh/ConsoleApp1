using System;
using System.Reflection;

namespace DesignPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Stretagy pattern
            Console.WriteLine("###################### Stretagy pattern ######################");
            var stretagyClient = new Stretegy.Client();
            stretagyClient.Execute();
            #endregion

            #region Observer Pattern
            Console.WriteLine("\n ###################### Observer pattern ######################");
            var observerClient = new Observer.Client();
            observerClient.Execute();
            observerClient.ExecuteNotify();
            #endregion

            #region Decorator pattern
            Console.WriteLine("\n ###################### Decorator pattern ######################");
            var decoratorClient = new Decorator.Client();
            decoratorClient.Execute();
            #endregion

            #region Factory Method Pattern
            Console.WriteLine("\n ###################### Factory Method pattern ######################");
            var factoryMethodClient = new FactoryMethod.Client();
            factoryMethodClient.Execute();
            #endregion

            #region AbstractFactory pattern
            Console.WriteLine("\n ###################### AbstractFactory pattern ######################");
            var abstractFactoryClient = new AbstractFactory.Client();
            abstractFactoryClient.Execute();
            #endregion
            Console.ReadKey();
        }
    }
}


