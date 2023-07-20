﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DesignPattern.Stretegy
{
    public class DisplayNatural : IDisplayStrategy
    {
        public void Display()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
