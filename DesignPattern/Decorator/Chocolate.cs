using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Decorator
{
    public class Chocolate :AddonDecorator
    {
        public Chocolate(Beverage beverage):base(beverage)
        {

        }

        public override double Cost()
        {
            return base.Cost() + 5;
        }
    }
}
