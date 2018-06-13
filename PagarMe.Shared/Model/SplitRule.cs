//
// SplitRule.cs
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
	public class SplitRule : Base.AbstractModel
	{
		public string Id {
			get {
				var result = GetAttribute<object> ("id");
				if (result == null)
					return null;

				return result.ToString ();
			}
			set { SetAttribute ("id", value); }
		}

		[Obsolete ("Recipient is deprecated, use acessors to RecipientId instead.")]
		public Recipient Recipient {
			get { return GetAttribute<Recipient> ("recipient"); }
			set { SetAttribute ("recipient", value); }
		}

		public string RecipientId {
			get { return GetAttribute<string> ("recipient_id"); }
			set { SetAttribute ("recipient_id", value); }
		}

		public bool ChargeProcessingFee {
			get { return GetAttribute<bool> ("charge_processing_fee"); }
			set { SetAttribute ("charge_processing_fee", value); }
		}

		public bool Liable {
			get { return GetAttribute<bool> ("liable"); }
			set { SetAttribute ("liable", value); }
		}

		public int Percentage {
			get { return GetAttribute<int> ("percentage"); }
			set { SetAttribute ("percentage", value); }
		}

		public int Amount {
			get { return GetAttribute<int> ("amount"); }
			set { SetAttribute ("amount", value); }
		}

		public SplitRule ()
			: this (null)
		{
		}

		public SplitRule (PagarMeService service)
			: base (service)
		{
		}
	}
}
