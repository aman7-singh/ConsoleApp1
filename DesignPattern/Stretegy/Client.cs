using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Stretegy
{
    public class Client
    {
        
        public void Execute()
        {
            IFlyStrategy jetFly = new FlyJet();
            IDisplayStrategy funckyDisplay = new DisplayFunky();
            IFlyStrategy naturalFly = new FlyNatural();
            IDisplayStrategy naturalDisplay = new DisplayNatural();

            Console.WriteLine("Cool Duck");
            Duck CoolDuck = new Duck(jetFly, funckyDisplay);
            CoolDuck.Fly();
            CoolDuck.Display();

            Console.WriteLine("Natural Duck");
            Duck NaturalDuck = new Duck(naturalFly, naturalDisplay);
            NaturalDuck.Fly();
            NaturalDuck.Display();

            Console.WriteLine("skilled Duck");
            Duck skilledDuck = new Duck(jetFly, naturalDisplay);
            skilledDuck.Fly();
            skilledDuck.Display();
        }

    }
}
