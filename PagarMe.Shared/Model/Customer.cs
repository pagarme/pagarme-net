//
// Customer.cs
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

namespace PagarMe
{
    public class Customer : Base.Model
    {
        protected override string Endpoint { get { return "/customers"; } }

        public string DocumentNumber
        {
            get { return GetAttribute<string>("document_number"); }
            set { SetAttribute("document_number", value); }
        }

        public DocumentType DocumentType
        {
            get { return GetAttribute<DocumentType>("document_type"); }
            set { SetAttribute("document_type", value); }
        }

        public string Name
        {
            get { return GetAttribute<string>("name"); }
            set { SetAttribute("name", value); }
        }

        public string Email
        {
            get { return GetAttribute<string>("email"); }
            set { SetAttribute("email", value); }
        }

        public DateTime? BornAt
        {
            get { return GetAttribute<DateTime?>("born_at"); }
            set { SetAttribute("born_at", value); }
        }

        public Gender Gender
        {
            get { return GetAttribute<Gender>("gender"); }
            set { SetAttribute("gender", value); }
        }

        public Address[] Addresses
        {
            get { return GetAttribute<Address[]>("addresses"); }
            set { SetAttribute("addresses", value); }
        }

        public Address Address
        {
            get { return GetAttribute<Address>("address"); }
            set { SetAttribute("address", value); }
        }

        public Phone[] Phones
        {
            get { return GetAttribute<Phone[]>("phones"); }
            set { SetAttribute("phones", value); }
        }

        public Phone Phone
        {
            get { return GetAttribute<Phone>("phone"); }
            set { SetAttribute("phone", value); }
        }

        public string ExternalId
        {
            get { return GetAttribute<string>("external_id"); }
            set { SetAttribute("external_id", value); }
        }

        public CustomerType Type
        {
            get { return GetAttribute<CustomerType>("type"); }
            set { SetAttribute("type", value); }
        }

        public string Country
        {
            get { return GetAttribute<string>("country"); }
            set { SetAttribute("country", value); }
        }

        public Document[] Documents
        {
            get { return GetAttribute<Document[]>("documents"); }
            set { SetAttribute("documents", value); }
        }

        public string[] PhoneNumbers
        {
            get { return GetAttribute<string[]>("phone_numbers"); }
            set { SetAttribute("phone_numbers", value); }
        }

        public string Birthday
        {
            get { return GetAttribute<string>("birthday"); }
            set { SetAttribute("birthday", value); }
        }

        public Customer()
            : this(null)
        {

        }

        public Customer(PagarMeService service)
            : base(service)
        {
        }
    }
}

