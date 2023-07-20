using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Decorator
{
    public class Espresso : Beverage
    {
        public override double Cost()
        {
            return 100;
        }
    }
}
