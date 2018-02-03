//
// Item.cs
//
// Author:
//       Murilo Souza <murilo.souza@pagar.me>
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
    public class Item : Base.AbstractModel
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

        public string Title
        {
            get { return GetAttribute<string>("title"); }
            set { SetAttribute("title", value); }
        }

        public int UnitPrice
        {
            get { return GetAttribute<int>("unit_price"); }
            set { SetAttribute("unit_price", value); }
        }

        public int Quantity
        {
            get { return GetAttribute<int>("quantity"); }
            set { SetAttribute("quantity", value); }
        }

        public Boolean Tangible
        {
            get { return GetAttribute<Boolean>("tangible"); }
            set { SetAttribute("tangible", value); }
        }

        public string Date
        {
            get { return GetAttribute<String>("date"); }
            set { SetAttribute("date", value); }
        }

        public string Venue
        {
            get { return GetAttribute<String>("venue"); }
            set { SetAttribute("venue", value); }
        }


        public Item()
            : this(null)
        {
        }

        public Item(PagarMeService service)
            : base(service)
        {
        }
    }
}

