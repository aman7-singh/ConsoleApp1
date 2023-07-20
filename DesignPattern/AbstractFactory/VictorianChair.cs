using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    public class VictorianChair : Chair
    {
        public override bool Has4Legs()
        {
            Console.WriteLine("Victorian chair has legs");
            return true;
        }

        public override void SitOn()
        {
            Console.WriteLine("Victorian chair - sit on");
        }
    }
}
