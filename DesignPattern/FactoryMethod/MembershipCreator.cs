using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.FactoryMethod
{
    abstract class MembershipCreator
    {
        public MemberProduct memberProduct;
        public abstract MemberProduct CreateMember(MembershipType membershipType);
    }
}
