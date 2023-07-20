using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPattern.FactoryMethod
{
    class MembershipCreatorInd : MembershipCreator
    {
        public override MemberProduct CreateMember(MembershipType membershipType)
        {
            switch(membershipType)
            {
                case MembershipType.Lifetime:
                    memberProduct = new MemberIndLifetime();
                    break;
                case MembershipType.Annual:
                    memberProduct = new MemberIndAnnual();
                    break;
                default: throw new NotImplementedException();
            }
            return memberProduct;
        }
    }
}
