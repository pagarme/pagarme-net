using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe
{
    public enum PayableStatus
    {
        [Base.EnumValue("waiting_funds")]
        WaitingFunds,
        [Base.EnumValue("paid")] Paid,
        [Base.EnumValue("suspended")] Suspended
    }
}
