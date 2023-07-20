using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.AbstractFactory
{
    public abstract class Chair
    {
        public abstract bool Has4Legs();
        public abstract void SitOn();
    }
}
