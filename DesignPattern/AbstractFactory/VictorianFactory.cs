using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    class VictorianFactory : IFunitureFactory
    {
        public Chair CreateChair()
        {
            return new VictorianChair();
        }

        public Sofa CreateSofa()
        {
            return new VictorianSofa();
        }
    }
}
