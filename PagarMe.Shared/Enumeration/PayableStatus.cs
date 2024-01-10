namespace PagarMe
{
    public enum PayableStatus
    {
        [Base.EnumValue("waiting_funds")]
        WaitingFunds,
        [Base.EnumValue("paid")]
        Paid,        
        [Base.EnumValue("prepaid")]
        Prepaid
    }
}
