using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    public class VictorianSofa : Sofa
    {
        public override bool HasSpring()
        {
            Console.WriteLine("Victorian sofa has no spring");
            return false;
        }

        public override void ThreePeopleSit()
        {
            Console.WriteLine("Victorian sofa 3 people can sit");
        }
    }
}
