﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DesignPattern.FactoryMethod
{
    class MemberIndAnnual : MemberProduct
    {
        public override void SavePersonalInfo()
        {
            Console.WriteLine(MethodBase.GetCurrentMethod().DeclaringType);
        }
    }
}
