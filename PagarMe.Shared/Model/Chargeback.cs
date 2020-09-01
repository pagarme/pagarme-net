using System;
using Newtonsoft.Json;
namespace PagarMe
{
    public class Chargeback:  Base.Model
                                  
    {

        protected override string Endpoint { get { return "/chargebacks"; } }


        public string Status
        {
            get { return GetAttribute<string>("status"); }
        }

        public int TransactionId 
        {
            get { return GetAttribute<int>("transaction_id"); }
        }

        public int Installment
        {
            get { return GetAttribute<int>("installment"); }
        }

        public string ReasonCode{
            get { return GetAttribute<string>("reason_code"); }
        }

        public DateTime AccrualDate
        {
            get { return GetAttribute<DateTime>("accrual_date"); }
        }

        public DateTime CreatedAt
        {
            get { return GetAttribute<DateTime>("created_at"); }
        }

        public DateTime UpdatedAt
        {
            get { return GetAttribute<DateTime>("updated_at"); }
        }
        public string CardBrand
        {
            get { return GetAttribute<string>("card_brand"); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
        }

        public int Cycle 
        {
            get { return GetAttribute<int>("cycle"); }
        }

        public Chargeback()
            : this(null)
        {
        }
        public Chargeback(PagarMeService service)
            : base(service)
        {
        }
    }
}
