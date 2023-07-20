using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.FactoryMethod
{
    class MembershipCreatorNY : MembershipCreator
    {
        public override MemberProduct CreateMember(MembershipType membershipType)
        {
            MemberProduct memberProduct;
            switch (membershipType)
            {
                case MembershipType.Lifetime:
                    memberProduct = new MemberNYLifetime();
                    break;
                case MembershipType.Annual:
                    memberProduct = new MemberNYAnnual();
                    break;
                default: throw new NotImplementedException();
            }
            return memberProduct;
        }
    }
}
