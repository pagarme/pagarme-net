//
// PagarMeService.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2014 jonathanlima
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

namespace PagarMe
{
    public class PagarMeService
    {
        private static PagarMeService _defaultService;

        public static string DefaultApiEndpoint { get; set; }
        public static string DefaultApiKey { get; set; }
        public static string DefaultEncryptionKey { get; set; }

        public Base.ModelCollection<Card> Cards { get; private set; }
        public Base.ModelCollection<Subscription> Subscriptions { get; private set; }
        public Base.ModelCollection<Customer> Customers { get; private set; }
        public Base.ModelCollection<Transaction> Transactions { get; private set; }
        public Base.ModelCollection<Transfer> Transfers { get; private set; }
        public Base.ModelCollection<Plan> Plans { get; private set; }
        public Base.ModelCollection<Recipient> Recipients { get; private set; }
        public Base.ModelCollection<BankAccount> BankAccounts { get; private set; }
        public Base.ModelCollection<Payable> Payables { get; private set; }
        public Base.ModelCollection<Refund> Refunds { get; private set; }

        static PagarMeService()
        {
            DefaultApiEndpoint = "https://api.pagar.me/1";
        }

        public static PagarMeService GetDefaultService()
        {
            if (_defaultService == null)
                _defaultService = new PagarMeService(DefaultApiKey, DefaultEncryptionKey, DefaultApiEndpoint);

            return _defaultService;
        }

        private readonly string _apiKey, _encryptionKey, _apiEndpoint;

        public string ApiKey
        {
            get { return _apiKey; }
        }

        public string EncryptionKey
        {
            get { return _encryptionKey; }
        }

        public string ApiEndpoint

        {
            get { return _apiEndpoint; }
        }

        public PagarMeService(string apiKey, string encryptionKey)
            : this(apiKey, encryptionKey, DefaultApiEndpoint)
        {


        }

        public PagarMeService(string apiKey, string encryptionKey, string apiEndpoint)
        {
            _apiKey = apiKey;
            _encryptionKey = encryptionKey;
            _apiEndpoint = apiEndpoint;

            Cards = new Base.ModelCollection<Card>(this, "/cards");
            Subscriptions = new Base.ModelCollection<Subscription>(this, "/subscriptions");
            Transactions = new Base.ModelCollection<Transaction>(this, "/transactions");
            Plans = new Base.ModelCollection<Plan>(this, "/plans");
            Customers = new Base.ModelCollection<Customer>(this, "/customers");
            Recipients = new Base.ModelCollection<Recipient>(this, "/recipients");
            BankAccounts = new Base.ModelCollection<BankAccount>(this, "/bank_accounts");
            Payables = new Base.ModelCollection<Payable>(this, "/payables");
            Transfers = new Base.ModelCollection<Transfer>(this, "/transfers");
            Refunds = new Base.ModelCollection<Refund>(this, "/refunds");
        }
    }
}

