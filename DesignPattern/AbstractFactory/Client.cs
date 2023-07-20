using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    public class Client
    {
        public void Execute()
        {
            Console.WriteLine("\n ---------------- Modern-------------------");           
            ClientMethod(new ModernFactory());

            Console.WriteLine("\n ---------------- Victorian-------------------");
            ClientMethod(new VictorianFactory());
        }
        public void ClientMethod(IFunitureFactory factory)
        {
            var chair = factory.CreateChair();
            chair.Has4Legs();
            chair.SitOn();

            var sofa = factory.CreateSofa();  
            sofa.HasSpring();
            sofa.ThreePeopleSit();
        }
    }
}
