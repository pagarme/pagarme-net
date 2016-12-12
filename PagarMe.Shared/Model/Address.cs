//
// Address.cs
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
    public class Address : Base.AbstractModel
    {
        public string Id
        {
            get
            {
                var result = GetAttribute<object>("id");

                if (result == null)
                    return null;

                return result.ToString();
            }
            set { SetAttribute("id", value); }
        }

        public string Street
        {
            get { return GetAttribute<string>("street"); }
            set { SetAttribute("street", value); }
        }

        public string Complementary
        {
            get { return GetAttribute<string>("complementary"); }
            set { SetAttribute("complementary", value); }
        }

        public string StreetNumber
        {
            get { return GetAttribute<string>("street_number"); }
            set { SetAttribute("street_number", value); }
        }

        public string Neighborhood
        {
            get { return GetAttribute<string>("neighborhood"); }
            set { SetAttribute("neighborhood", value); }
        }

        public string City
        {
            get { return GetAttribute<string>("city"); }
            set { SetAttribute("city", value); }
        }

        public string State
        {
            get { return GetAttribute<string>("state"); }
            set { SetAttribute("state", value); }
        }

        public string Zipcode
        {
            get { return GetAttribute<string>("zipcode"); }
            set { SetAttribute("zipcode", value); }
        }

        public string Country
        {
            get { return GetAttribute<string>("country"); }
            set { SetAttribute("country", value); }
        }

        public Address()
            : this(null)
        {
        }

        public Address(PagarMeService service)
            : base(service)
        {
        }
    }
}

