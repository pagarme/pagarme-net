using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe
{
    public enum TransferType
    {
        [Base.EnumValue("ted")] Ted,
        [Base.EnumValue("doc")] Doc,
        [Base.EnumValue("credito em conta")] Credito_em_conta
    }
}
