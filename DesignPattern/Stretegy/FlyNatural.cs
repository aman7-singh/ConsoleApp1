using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DesignPattern.Stretegy
{
    public class FlyNatural : IFlyStrategy
    {
        public void Fly()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
