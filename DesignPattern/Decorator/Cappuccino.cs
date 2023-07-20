using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Decorator
{
    public class Cappuccino:Beverage
    {
        public override double Cost()
        {
            return 150;
        }
    }
}
