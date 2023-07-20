using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    public interface IFunitureFactory
    {
        Chair CreateChair();
        Sofa CreateSofa();
    }
}
