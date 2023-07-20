using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    class ModernFactory : IFunitureFactory
    {
        public Chair CreateChair()
        {
            return new ModernChair();
        }

        public Sofa CreateSofa()
        {
            return new ModernSofa();
        }
    }
}
