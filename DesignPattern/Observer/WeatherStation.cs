using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Observer
{
    class WeatherStation : IObservable
    {
        List<IObserver> subscribers = new List<IObserver>();
        public void Add(IObserver observer)
        {
            subscribers.Add(observer);
        }
        public void Remove(IObserver observer)
        {
            if (subscribers != null)
            {
                subscribers.Remove(observer);
            }
        }
        public double GetTemprature(double buffer)
        {
            return 100.32 + buffer;
        }

        public void Notify()
        {
            foreach(var subscriber in subscribers)
            {
                Console.WriteLine($" --------- {subscriber} --------- ");
                subscriber.Update();
            }
        }

        
    }
}
