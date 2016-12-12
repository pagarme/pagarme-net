//
// Card.cs
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
using Newtonsoft.Json;

namespace PagarMe
{
    public class Card : Base.Model
    {
        protected override string Endpoint { get { return "/cards"; } }

        public string CardHash
        {
            get { return GetAttribute<string>("card_hash"); }
            set { SetAttribute("card_hash", value); }
        }

        public string Number
        {
            get { return GetAttribute<string>("number"); }
            set { SetAttribute("number", value); }
        }

        public string ExpirationDate
        {
            get { return GetAttribute<string>("expiration_date"); }
            set { SetAttribute("expiration_date", value); }
        }

        public string Cvv
        {
            get { return GetAttribute<string>("cvv"); }
            set { SetAttribute("cvv", value); }
        }

        public CardBrand Brand
        {
            get { return GetAttribute<CardBrand>("brand"); }
            set { SetAttribute("brand", value); }
        }

        public string HolderName
        {
            get { return GetAttribute<string>("holder_name"); }
            set { SetAttribute("holder_name", value); }
        }

        public string FirstDigits
        {
            get { return GetAttribute<string>("first_digits"); }
            set { SetAttribute("first_digits", value); }
        }

        public string LastDigits
        {
            get { return GetAttribute<string>("last_digits"); }
            set { SetAttribute("last_digits", value); }
        }

        public string Fingerprint
        {
            get { return GetAttribute<string>("fingerprint"); }
            set { SetAttribute("fingerprint", value); }
        }

        public Customer Customer
        {
            get { return GetAttribute<Customer>("customer"); }
            set { SetAttribute("customer", value); }
        }

        public bool Valid
        {
            get { return GetAttribute<bool>("valid"); }
            set { SetAttribute("valid", value); }
        }

        public Card()
            : this(null)
        {

        }

        public Card(PagarMeService service)
            : base(service)
        {
        }
    }
}

