using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Observer
{
    class Client
    {
        IObservable observable;
        public void Execute()
        {
            observable = new WeatherStation();

            IObserver mobile = new MobileDisplay(observable);
            observable.Add(mobile);

            IObserver notepad = new NotepadDisplay(observable);
            observable.Add(notepad);
        }

        public void ExecuteNotify()
        {
            observable.Notify();
        }
    }
}
