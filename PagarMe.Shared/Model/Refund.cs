using System;
namespace PagarMe
{
    public class Refund : Base.Model

    {

        protected override string Endpoint { get { return "/refunds"; } }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }

        }

        public string Type 
        {
            get { return GetAttribute<string>("type"); }
        }

        public string Status
        {
            get { return GetAttribute<string>("status"); }
        }

        public string ChargeFeeRecipientId
        {
            get { return GetAttribute<string>("charge_fee_recipient_id"); }
        }

        public string BankAccountId
        {
            get { return GetAttribute<string>("bank_account_id"); }
        }

        public int TransactionId
        {
            get { return GetAttribute<int>("transaction_id"); }
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
        }

        public int FraudCoverageFee
        {
            get { return GetAttribute<int>("fraud_coverage_fee"); }
        }
        public Base.AbstractModel Metadata
        {
            get { return GetAttribute<Base.AbstractModel>("metadata"); }
            set { SetAttribute("metadata", value); }
        }
        public Refund()
            : this(null)
        {

        }

        public Refund(PagarMeService service)
            : base(service)
        {
            Metadata = new Base.AbstractModel(Service);
        }


    }
}
