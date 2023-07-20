using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DesignPattern.Observer
{
    class NotepadDisplay : IObserver
    {
        IObservable weatherStation;
        public NotepadDisplay(IObservable observable)
        {
            weatherStation = observable;
        }
        public void Update()
        {
            var temprature = this.weatherStation.GetTemprature(0.28);
            Console.WriteLine(temprature);
            Console.WriteLine(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
