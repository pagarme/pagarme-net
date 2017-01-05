using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Enumeration
{
    public enum PostbackModel
    {
        [Base.EnumValue("transaction")] Transaction,
        [Base.EnumValue("subscription")] Subscription
    }
}
