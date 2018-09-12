//
// Transaction.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2014 Pagar.me
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using PagarMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PagarMe.Base;

namespace PagarMe
{
    public class Transaction : Base.Model
    {
        private Base.ModelCollection<Event> _events;
        private Base.ModelCollection<Payable> _payables;
        private Base.ModelCollection<AntifraudAnalysis> _antifraud;
        private Base.ModelCollection<Postback> _postbacks;

        protected override string Endpoint { get { return "/transactions"; } }

        [Obsolete("Subscriptions is no longer returned, use SubscriptionId instead.")]
        public Subscription Subscription
        {
            get { return GetAttribute<Subscription>("subscription"); }
            set { SetAttribute("subscription", value); }
        }

        public int SubscriptionId
        {
            get { return GetAttribute<int>("subscription_id") ; }
        }

        public Card Card
        {
            get { return GetAttribute<Card>("card"); }
            set { SetAttribute("card", value); }
        }

        public Customer Customer
        {
            get { return GetAttribute<Customer>("customer"); }
            set { SetAttribute("customer", value); }
        }

        public Address Address
        {
            get { return GetAttribute<Address>("address"); }
            set { SetAttribute("address", value); }
        }

        public Phone Phone
        {
            get { return GetAttribute<Phone>("phone"); }
            set { SetAttribute("phone", value); }
        }

        public TransactionStatus Status
        {
            get { return GetAttribute<TransactionStatus>("status"); }
            set { SetAttribute("status", value); }
        }

        public int AuthorizationAmount
        {
            get { return GetAttribute<int>("authorized_amount"); }
            set { SetAttribute("authorized_amount", value); }
        }

        public int PaidAmount
        {
            get { return GetAttribute<int>("paid_amount"); }
            set { SetAttribute("paid_amount", value); }
        }

        public int RefundedAmount
        {
            get { return GetAttribute<int>("refunded_amount"); }
            set { SetAttribute("refunded_amount", value); }
        }

        public string CardHash
        {
            get { return GetAttribute<string>("card_hash"); }
            set { SetAttribute("card_hash", value); }
        }

        public string CardNumber
        {
            get { return GetAttribute<string>("card_number"); }
            set { SetAttribute("card_number", value); }
        }

        public string CardExpirationDate
        {
            get { return GetAttribute<string>("card_expiration_date"); }
            set { SetAttribute("card_expiration_date", value); }
        }

        public string StatusReason
        {
            get { return GetAttribute<string>("status_reason"); }
            set { SetAttribute("status_reason", value); }
        }

        public string AcquirerResponseCode
        {
            get { return GetAttribute<string>("acquirer_response_code"); }
            set { SetAttribute("acquirer_response_code", value); }
        }

        public string AcquirerName
        {
            get { return GetAttribute<string>("acquirer_name"); }
            set { SetAttribute("acquirer_name", value); }
        }

        public string AuthorizationCode
        {
            get { return GetAttribute<string>("authorization_code"); }
            set { SetAttribute("authorization_code", value); }
        }

        public string SoftDescriptor
        {
            get { return GetAttribute<string>("soft_descriptor"); }
            set { SetAttribute("soft_descriptor", value); }
        }

        public string RefuseReason
        {
            get { return GetAttribute<string>("refuse_reason"); }
        }

        private object tid
        {
            get { return GetAttribute<object>("tid"); }
        }

        public string Tid
        {
            get { return tid.ToString(); }
        }

        private object nsu
        {
            get { return GetAttribute<object>("nsu"); }
        }

        public string Nsu
        {
            get { return nsu.ToString(); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
            set { SetAttribute("amount", value); }
        }

        public int? Installments
        {
            get { return GetAttribute<int?>("installments"); }
            set { SetAttribute("installments", value); }
        }

        public int Cost
        {
            get { return GetAttribute<int>("cost"); }
            set { SetAttribute("cost", value); }
        }

        public string CardHolderName
        {
            get { return GetAttribute<string>("card_holder_name"); }
            set { SetAttribute("card_holder_name", value); }
        }

        public string CardCvv
        {
            get { return GetAttribute<string>("card_cvv"); }
            set { SetAttribute("card_cvv", value); }
        }

        public string CardLastDigits
        {
            get { return GetAttribute<string>("card_last_digits"); }
            set { SetAttribute("card_last_digits", value); }
        }

        public string CardFirstDigits
        {
            get { return GetAttribute<string>("card_first_digits"); }
            set { SetAttribute("card_first_digits", value); }
        }

        public CardBrand CardBrand
        {
            get { return GetAttribute<CardBrand>("card_brand"); }
            set { SetAttribute("card_brand", value); }
        }

        public string CardEmvResponse
        {
            get { return GetAttribute<string>("card_emv_response"); }
            set { SetAttribute("card_emv_response", value); }
        }

        public string PostbackUrl
        {
            get { return GetAttribute<string>("postback_url"); }
            set { SetAttribute("postback_url", value); }
        }

        public PaymentMethod PaymentMethod
        {
            get { return GetAttribute<PaymentMethod>("payment_method"); }
            set { SetAttribute("payment_method", value); }
        }

        public float? AntifraudScore
        {
            get { return GetAttribute<float?>("antifraud_score"); }
            set { SetAttribute("antifraud_score", value); }
        }

        public string BoletoUrl
        {
            get { return GetAttribute<string>("boleto_url"); }
            set { SetAttribute("boleto_url", value); }
        }

        public string BoletoInstructions
        {
            set { SetAttribute("boleto_instructions", value); }
        }

        public DateTime? BoletoExpirationDate
        {
            get { return GetAttribute<DateTime?>("boleto_expiration_date"); }
            set { SetAttribute("boleto_expiration_date", value); }
        }

        public string BoletoBarcode
        {
            get { return GetAttribute<string>("boleto_barcode"); }
            set { SetAttribute("boleto_barcode", value); }
        }

        public string Referer
        {
            get { return GetAttribute<string>("referer"); }
            set { SetAttribute("referer", value); }
        }

        public string IP
        {
            get { return GetAttribute<string>("ip"); }
            set { SetAttribute("ip", value); }
        }

        public bool? ShouldCapture
        {
            get { return GetAttribute<bool>("capture"); }
            set { SetAttribute("capture", value); }
        }

        public bool? Async
        {
            get { return GetAttribute<bool>("async"); }
            set { SetAttribute("async", value); }
        }

        public String LocalTime
        {
          get { return GetAttribute<String>("local_time"); }
          set { SetAttribute("local_time", value); }
        }

        public Base.AbstractModel Metadata
        {
            get { return GetAttribute<Base.AbstractModel>("metadata"); }
            set { SetAttribute("metadata", value); }
        }

        public SplitRule[] SplitRules
        {
            get { return GetAttribute<SplitRule[]>("split_rules"); }
            set { SetAttribute("split_rules", value); }
        }

        public Billing Billing
        {
            get { return GetAttribute<Billing>("billing"); }
            set { SetAttribute("billing", value); }
        }

        public Shipping Shipping
        {
            get { return GetAttribute<Shipping>("shipping"); }
            set { SetAttribute("shipping", value); }
        }

        public Item[] Item
        {
            get { return GetAttribute<Item[]>("items"); }
            set { SetAttribute("items", value); }
        }

        public Base.ModelCollection<Event> Events
        {
            get
            {
                if (Id == null)
                {
                    throw new InvalidOperationException("Transaction must have an Id in order to fetch events");
                }

                return _events ?? (_events = new Base.ModelCollection<Event>(Service, "/events", Endpoint + "/" + Id));
            }
        }

        public Base.ModelCollection<Payable> Payables
        {
            get
            {
                if (Id == null)
                {
                    throw new InvalidOperationException("Transaction must have an Id in order to fetch events");
                }

                return _payables ?? (_payables = new Base.ModelCollection<Payable>(Service, "/payables", Endpoint + "/" + Id));
            }
        }

        public Base.ModelCollection<AntifraudAnalysis> AntifraudAnalysis
        {
            get
            {
                if (Id == null)
                {
                    throw new InvalidOperationException("Transaction must have an Id in order to fetch events");
                }

                return _antifraud ?? (_antifraud = new Base.ModelCollection<AntifraudAnalysis>(Service, "/antifraud_analyses ", Endpoint + "/" + Id));
            }
        }

        public Base.ModelCollection<Postback> Postbacks
        {
            get
            {
                if (Id == null)
                {
                throw new InvalidOperationException("Transaction must have an Id in order to fetch events");
            }

                return _postbacks ?? (_postbacks = new Base.ModelCollection<Postback>(Service, "/postbacks", Endpoint + "/" + Id));

            }
        }

        public Transaction() : this(null) {}

        public Transaction(PagarMeService service)
            : base(service)
        {
            Metadata = new Base.AbstractModel(Service);
        }

        public void Capture(int? amount = null)
        {
            var request = CreateRequest("POST", "/capture");

            if (amount.HasValue)
                request.Query.Add(new Tuple<string, string>("amount", amount.Value.ToString()));

            ExecuteSelfRequest(request);
        }

        public async void CaptureAsync(int? amount = null)
        {
            var request = CreateRequest("POST", "/capture");

            if (amount.HasValue)
                request.Query.Add(new Tuple<string, string>("amount", amount.Value.ToString()));

            await ExecuteSelfRequestAsync(request);
        }

        public void Refund(BankAccount bank, int? amount = null, bool asyncRefund = true)
        {
            var request = CreateRequest("POST", "/refund");

            request.Query =  BuildQueryForKeys("bank_account", bank.ToDictionary(Base.SerializationType.Plain));

            if (amount.HasValue)
                request.Query.Add(new Tuple<string, string>("amount", amount.Value.ToString()));

            if (!asyncRefund)
                request.Query.Add(new Tuple<string, string>("async", asyncRefund.ToString().ToLower()));

            ExecuteSelfRequest(request);
        }

		public void Refund(int? amount = null, bool asyncRefund = true)
        {
            var request = CreateRequest("POST", "/refund");

            if (amount.HasValue)
                request.Query.Add(new Tuple<string, string>("amount", amount.Value.ToString()));

            if(!asyncRefund)
                request.Query.Add(new Tuple<string, string>("async", asyncRefund.ToString().ToLower()));

            ExecuteSelfRequest(request);
        }

        public void RefundWithSplit(int? amount, SplitRule[] split_rules, BankAccount bank = null){

            var request = CreateRequest("POST", "/refund");

            if(bank != null){
                request.Query = BuildQueryForKeys("bank_account", bank.ToDictionary(Base.SerializationType.Plain));
            }

            request.Body = JsonConvert.SerializeObject(split_rules.Select(s => s.ToDictionary(SerializationType.Plain)).ToList());

            request.Query.Add(new Tuple<string, string>("amount", amount.Value.ToString()));

            ExecuteSelfRequest(request);

        }

		public async void RefundAsync(int? amount = null, bool asyncRefund = true)
        {
            var request = CreateRequest("POST", "/refund");

            if (amount.HasValue)
                request.Query.Add(new Tuple<string, string>("amount", amount.Value.ToString()));

            if(!asyncRefund)
                request.Query.Add(new Tuple<string, string>("async", asyncRefund.ToString().ToLower()));

            await ExecuteSelfRequestAsync(request);
        }

        protected override void CoerceTypes()
        {
            base.CoerceTypes();
            CoerceAttribute("card", typeof(Card));
            CoerceAttribute("customer", typeof(Customer));
            CoerceAttribute("address", typeof(Address));
            CoerceAttribute("phone", typeof(Phone));

            var subscriptionId = GetAttribute<object>("subscription_id");

            if (subscriptionId != null)
                SetAttribute("subscription", Service.Subscriptions.Find(subscriptionId.ToString(), false));
        }

        protected override Base.NestedModelSerializationRule SerializationRuleForField(string field, Base.SerializationType type)
        {
            if (field == "customer" && type != Base.SerializationType.Shallow)
                return PagarMe.Base.NestedModelSerializationRule.Full;

            return base.SerializationRuleForField(field, type);
        }
    }
}
