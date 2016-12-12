using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe
{
    public enum TransferStatus
    {
        [Base.EnumValue("pending_transfer")] PendingTransfer,
        [Base.EnumValue("transferred")] Transferred,
        [Base.EnumValue("failed")] Failed,
        [Base.EnumValue("processing")] Processing,
        [Base.EnumValue("canceled")] Canceled
    }
}
