using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagarMe.Base;

namespace PagarMe
{
    public class Transfer : Base.Model
    {
        protected override string Endpoint { get { return "/transfers"; } }

        public Transfer() : this(null) { }

        public Transfer(PagarMeService service) : base(service) { }

        public int Amount
        {
            get { return GetAttribute <int>("amount");}
            set { SetAttribute("amount", value);}
        }

        public TransferType Type
        {
            get { return GetAttribute<TransferType>("transfer_type");}
        }
        
        public TransferStatus Status
        {
            get { return GetAttribute <TransferStatus>("status");}
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
        }

        public string BankAccountId
        {
            set { SetAttribute("bank_account_id", value); }
        }

        public BankAccount Bank
        {
            get { return GetAttribute<BankAccount>("bank_account"); }
        }

        public string RecipientId
        {
            get { return GetAttribute<string>("recipient_id"); }
            set { SetAttribute("recipient_id", value); }
        }

        public string FundingEstimatedDate
        {
            get { return GetAttribute<string>("funding_estimated_date"); }
        }

        public string FundingDate
        {
            get { return GetAttribute<string>("funding_date"); }
        }

    }
}
