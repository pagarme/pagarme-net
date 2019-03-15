//
// Subscription.cs
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
using System;
using System.Collections.Generic;

namespace PagarMe
{
    public class Subscription : Base.Model
    {
        protected override string Endpoint { get { return "/subscriptions"; } }

        public Transaction CurrentTransaction
        {
            get { return GetAttribute<Transaction>("current_transaction"); }
            set { SetAttribute("current_transaction", value); }
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

        public Plan Plan
        {
            get { return GetAttribute<Plan>("plan"); }
            set { SetAttribute("plan", value); }    
        }

        public SubscriptionStatus Status
        {
            get { return GetAttribute<SubscriptionStatus>("status"); }
            set { SetAttribute("status", value); }
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

        public string CardCvv
        {
            get { return GetAttribute<string>("card_cvv"); }
            set { SetAttribute("card_cvv", value); }
        }

        public int Charges
        {
            get { return GetAttribute<int>("charges"); }
            set { SetAttribute("charges", value); }
        }

        public DateTime? CurrentPeriodStart
        {
            get { return GetAttribute<DateTime?>("current_period_start"); }
            set { SetAttribute("current_period_start", value); }
        }

        public DateTime? CurrentPeriodEnd
        {
            get { return GetAttribute<DateTime?>("current_period_end"); }
            set { SetAttribute("current_period_end", value); }
        }

        public string CardHolderName
        {
            get { return GetAttribute<string>("card_holder_name"); }
            set { SetAttribute("card_holder_name", value); }
        }

        public string CardLastDigits
        {
            get { return GetAttribute<string>("card_last_digits"); }
            set { SetAttribute("card_last_digits", value); }
        }

        public CardBrand CardBrand
        {
            get { return GetAttribute<CardBrand>("card_brand"); }
            set { SetAttribute("card_brand", value); }
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

        public int? Amount
        {
            get { return GetAttribute<int>("amount"); }
            set { SetAttribute("amount", value); }
        }

        public Subscription()
            : this(null)
        {

        }

        public Subscription(PagarMeService service)
            : base(service)
        {
            Metadata = new Base.AbstractModel(Service);
        }

        public void Cancel()
        {
            var request = CreateRequest("POST", "/cancel");

            ExecuteSelfRequest(request);
        }

        public async void CancelAsync()
        {
            var request = CreateRequest("POST", "/cancel");

            await ExecuteSelfRequestAsync(request);
        }

        protected override void CoerceTypes()
        {
            base.CoerceTypes();

            CoerceAttribute("card", typeof(Card));
            CoerceAttribute("customer", typeof(Customer));
            CoerceAttribute("current_transaction", typeof(Transaction));
            CoerceAttribute("address", typeof(Address));
            CoerceAttribute("phone", typeof(Phone));
            CoerceAttribute("plan", typeof(Plan));
        }

        protected override PagarMe.Base.NestedModelSerializationRule SerializationRuleForField(string field, Base.SerializationType type)
        {
            if (field == "customer" && type != Base.SerializationType.Shallow)
                return PagarMe.Base.NestedModelSerializationRule.Full;

            return base.SerializationRuleForField(field, type);
        }
    }
}

