using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    public class ModernChair : Chair
    {
        public override bool Has4Legs()
        {
            Console.WriteLine("Modern chair has no legs");
            return false;
        }

        public override void SitOn()
        {
            Console.WriteLine("Modern chair - sit on");
        }
    }
}
