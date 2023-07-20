using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    class ModernSofa : Sofa
    {
        public override bool HasSpring()
        {
            Console.WriteLine("Modern Sofa has spring");
            return true;
        }

        public override void ThreePeopleSit()
        {
            Console.WriteLine("Modern sofa - 3 people sit");
        }
    }
}
