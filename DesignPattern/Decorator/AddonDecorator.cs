using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Decorator
{
    public abstract class AddonDecorator : Beverage
    {
        public Beverage beverage;
        public AddonDecorator(Beverage beverage)
        {
            this.beverage = beverage;
        }
        public override double Cost()
        {
            return this.beverage.Cost() ;
        }
    }
}
