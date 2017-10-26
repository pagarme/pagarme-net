//
// BankAccount.cs
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
	public class BankAccount : Base.Model
	{
		protected override string Endpoint { get { return "/bank_accounts"; } }
		
		public string BankCode
		{
			get { return GetAttribute<string>("bank_code"); }
			set { SetAttribute("bank_code", value); }
		}

		public string Agencia
		{
			get { return GetAttribute<string>("agencia"); }
			set { SetAttribute("agencia", value); }
		}

		public string AgenciaDv
		{
			get { return GetAttribute<string>("agencia_dv"); }
			set { SetAttribute("agencia_dv", value); }
		}

		public string Conta
		{
			get { return GetAttribute<string>("conta"); }
			set { SetAttribute("conta", value); }
		}

		public string ContaDv
		{
			get { return GetAttribute<string>("conta_dv"); }
			set { SetAttribute("conta_dv", value); }
		}

		public AccountType Type
		{
			get { return GetAttribute<AccountType>("type"); }
			set { SetAttribute("type", value); }
		}

		public DocumentType DocumentType
		{
			get { return GetAttribute<DocumentType>("document_type"); }
			set { SetAttribute("document_type", value); }
		}

		public string DocumentNumber
		{
			get { return GetAttribute<string>("document_number"); }
			set { SetAttribute("document_number", value); }
		}

		public string LegalName
		{
			get { return GetAttribute<string>("legal_name"); }
			set { SetAttribute("legal_name", value); }
		}

		public bool ChargeTransferFees
		{
			get { return GetAttribute<bool>("charge_transfer_fees"); }
			set { SetAttribute("charge_transfer_fees", value); }
		}

		public BankAccount()
			: this(null)
		{

		}

		public BankAccount(PagarMeService service)
			: base(service)
		{
		}
	}
}
