using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Enumeration
{
    public enum PostbackStatus
    {
        [Base.EnumValue("processing")] Processing,
        [Base.EnumValue("waiting_retry")] WaitingRetry,
        [Base.EnumValue("pending_retry")] PendingRetry,
        [Base.EnumValue("failed")] Failed,
        [Base.EnumValue("success")] Success
    }
}
