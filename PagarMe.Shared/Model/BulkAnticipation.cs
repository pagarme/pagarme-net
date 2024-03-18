using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Enumeration;

namespace PagarMe.Model
{
    public class BulkAnticipation : Base.Model
    {
        protected override string Endpoint { get { return "/bulk_anticipations"; } }

        public BulkAnticipation() : this(null) { }

        public BulkAnticipation(PagarMeService service) : base(service) { }

        public BulkAnticipation(PagarMeService service, string endpointPrefix = "") :base(service, endpointPrefix) { }

        public BulkAnticipationStatus Status
        {
            get { return GetAttribute<BulkAnticipationStatus>("status"); }
        }

        public TimeFrame Timeframe
        {
            get { return GetAttribute<TimeFrame>("timeframe"); }
            set { SetAttribute("timeframe", value); }
        }

        public DateTime PaymentDate
        {
            get { return GetAttribute<DateTime>("payment_date"); }
            set { SetAttribute("payment_date", Utils.ConvertToUnixTimeStamp(value)); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
        }

        public int RequestedAmount
        {
            set { SetAttribute("requested_amount", value); }
            internal get { return GetAttribute<int>("requested_amount"); }
        }

        public int Fee
        {
            get { return GetAttribute<int>("fee"); }
        }

        public int AnticipationFee
        {
            get { return GetAttribute<int>("anticipation_fee"); }
        }
    }
}
