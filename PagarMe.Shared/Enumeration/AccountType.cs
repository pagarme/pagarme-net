using System;
using System.Runtime.Serialization;

namespace PagarMe
{
    public enum AccountType
    {
        [Base.EnumValue("conta_corrente")]
        Corrente,
        [Base.EnumValue("conta_poupanca")]
        Poupanca,
        [Base.EnumValue("conta_corrente_conjunta")]
        CorrenteConjunta,
        [Base.EnumValue("conta_poupanca_conjunta")]
        PoupancaConjunta
    }
}
