using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Enumeration
{
    public enum PostbackDeliveryStatus
    {
        [Base.EnumValue("success")] Success,
        [Base.EnumValue("processing")] Processing,
        [Base.EnumValue("failed")] Failed
    }
}
