using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Base;
using PagarMe.Enumeration;

namespace PagarMe
{
    public class Payable : Base.Model
    {
        protected override string Endpoint { get { return "/payables"; } }

        public Payable() : this(null) { }

        public Payable(PagarMeService service) : base(service) {}

        public Payable(PagarMeService service, string endpointPrefix = "") :base(service, endpointPrefix) { }

        public PayableStatus Status
        {
            get { return GetAttribute<PayableStatus>("status"); }
            private set { SetAttribute("status", value); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
            set { SetAttribute("amount", value); }
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
            private set { SetAttribute("fee", value); }
        }

        public int AnticipationFee
        {
            get { return GetAttribute<int>("anticipation_fee"); }
            set { SetAttribute("anticipation_fee", value); }
        }

        public int Installment
        {
            get { return GetAttribute<int>("installment"); }
            set { SetAttribute("installment",value); }
        }

        public int TransactionId
        {
            get { return GetAttribute<int>("transaction_id"); }
            set { SetAttribute("transaction_id", value); }
        }

        public string SplitRuleId
        {
            get { return GetAttribute<string>("split_rule_id"); }
            set { SetAttribute("split_rule_id", value); }
        }

        public string BulkAnticipationId
        {
            get { return GetAttribute<string>("bulk_anticipation_date"); }
            set { SetAttribute("bulk_anticipation_date",value); }
        }

        public string RecipientId
        {
            get { return GetAttribute<string>("recipient_id"); }
            set { SetAttribute("recipient_id", value); }
        }

        public DateTime PaymentDate
        {
            get { return GetAttribute<DateTime>("payment_date"); }
            set { SetAttribute("payment_date", Utils.ConvertToUnixTimeStamp(value)); }
        }

        public DateTime OriginalPaymentDate
        {
            get { return GetAttribute<DateTime>("original_payment_date"); }
            set { SetAttribute("original_payment_date", Utils.ConvertToUnixTimeStamp(value)); }
        }

        public PayableType Type
        {
            get { return GetAttribute<PayableType>("type"); }
            set { SetAttribute("type", value); }
        }

        public PaymentMethod PaymentMethod
        {
            get { return GetAttribute<PaymentMethod>("payment_method"); }
            set { SetAttribute("payment_method", value); }
        }
    }
}
