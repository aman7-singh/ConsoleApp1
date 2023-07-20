using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.FactoryMethod
{
    class Client
    {
        public void Execute()
        {
            MembershipCreator membership;
            MemberProduct memberProduct;

            membership = new MembershipCreatorInd();
            memberProduct = membership.CreateMember(MembershipType.Lifetime);
            memberProduct.SavePersonalInfo();
            memberProduct = membership.CreateMember(MembershipType.Annual);
            memberProduct.SavePersonalInfo();

            membership = new MembershipCreatorNY();
            memberProduct = membership.CreateMember(MembershipType.Lifetime);
            memberProduct.SavePersonalInfo();
            memberProduct = membership.CreateMember(MembershipType.Annual);
            memberProduct.SavePersonalInfo();
        }
    }
}
