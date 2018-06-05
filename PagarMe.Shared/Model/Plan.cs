//
// Plan.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2015 Pagar.me
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
using System.Linq;
using PagarMe.Base;

namespace PagarMe
{
    public class Plan : Base.Model
    {
        protected override string Endpoint { get { return "/plans"; } }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
            set { SetAttribute("amount", value); }
        }

        public int Days
        {
            get { return GetAttribute<int>("days"); }
            set { SetAttribute("days", value); }
        }

        public int TrialDays
        {
            get { return GetAttribute<int>("trial_days"); }
            set { SetAttribute("trial_days", value); }
        }

        public string Name
        {
            get { return GetAttribute<string>("name"); }
            set { SetAttribute("name", value); }
        }

        public PaymentMethod[] PaymentMethods
        {
            get
            {
                return GetAttribute<String[]> ("payment_methods")
                    .Select(s => EnumMagic.ConvertFromString(typeof(PaymentMethod), s))
                    .Cast<PaymentMethod>()
                    .ToArray();
            }

            set
            {
                var strings = value.Select(e => EnumMagic.ConvertToString(e)).ToArray();
                SetAttribute("payment_methods", strings);
            }
        }

        public string Color
        {
            get { return GetAttribute<string>("color"); }
            set { SetAttribute("color", value); }
        }

        public int? Charges
        {
            get { return GetAttribute<int?>("charges"); }
            set { SetAttribute("charges", value); }
        }

        public int? Installments
        {
            get { return GetAttribute<int?>("installments"); }
            set { SetAttribute("installments", value); }
        }

        public int? InvoiceReminder
        {
            get { return GetAttribute<int?>("invoice_reminder"); }
            set { SetAttribute("invoice_reminder", value); }
        }

        public Plan()
            : this(null)
        {

        }

        public Plan(PagarMeService service)
            : base(service)
        {
        }
    }
}

