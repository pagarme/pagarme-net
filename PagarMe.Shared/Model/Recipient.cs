//
// Recipient.cs
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
	public class Recipient : Base.Model
	{
		protected override string Endpoint { get { return "/recipients"; } }

		public BankAccount BankAccount
		{
			get { return GetAttribute<BankAccount>("bank_account"); }
			set { SetAttribute("bank_account", value); }
		}

		public bool TransferEnabled
		{
			get { return GetAttribute<bool>("transfer_enabled"); }
			set { SetAttribute("transfer_enabled", value); }
		}

		public DateTime LastTransfer
		{
			get { return GetAttribute<DateTime>("last_transfer"); }
			set { SetAttribute("last_transfer", value); }
		}

		public TransferInterval TransferInterval
		{
			get { return GetAttribute<TransferInterval>("transfer_interval"); }
			set { SetAttribute("transfer_interval", value); }
		}

		public int TransferDay
		{
			get { return GetAttribute<int>("transfer_day"); }
			set { SetAttribute("transfer_day", value); }
		}

		public bool AutomaticAnticipationEnabled
		{
			get { return GetAttribute<bool>("automatic_anticipation_enabled"); }
			set { SetAttribute("automatic_anticipation_enabled", value); }
		}

		public double AnticipatableVolumePercentage
		{
			get { return GetAttribute<double>("anticipatable_volume_percentage"); }
			set { SetAttribute("anticipatable_volume_percentage", value); }
		}


		public Recipient()
			: this(null)
		{

		}

		public Recipient(PagarMeService service)
			: base(service)
		{
		}
	}
}
