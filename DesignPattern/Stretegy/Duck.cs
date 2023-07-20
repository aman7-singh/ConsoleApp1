using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.Stretegy
{
    public class Duck
    {
        IFlyStrategy flyStretegy;
        IDisplayStrategy displayStrategy;

        public Duck(IFlyStrategy flyStr, IDisplayStrategy displayStr)
        {
            this.flyStretegy = flyStr;
            this.displayStrategy = displayStr;
        }

        public void Fly()
        {
            flyStretegy.Fly();
        }

        public void Display()
        {
            displayStrategy.Display();
        }
    }
}
