using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Decorator
{
    public class Caramel:AddonDecorator
    {
        public Caramel(Beverage beverage) : base(beverage)
        {

        }
        public override double Cost()
        {
           return base.Cost() +3;
        }

    }
}
