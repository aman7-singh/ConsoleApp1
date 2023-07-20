using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DesignPattern.Observer
{
    class MobileDisplay : IObserver
    {
        IObservable weatherStation;

        public MobileDisplay(IObservable observable)
        {
            weatherStation = observable;
        }
        public void Update()
        {
            var temprature = this.weatherStation.GetTemprature(0.48);
            Console.WriteLine(temprature);
            Console.WriteLine(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
