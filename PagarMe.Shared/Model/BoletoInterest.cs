﻿namespace PagarMe
{
    public class BoletoInterest : Base.AbstractModel
    {
        public int Days
        {
            get { return GetAttribute<int>("days"); }
            set { SetAttribute("days", value); }
        }

        public int Amount
        {
            get { return GetAttribute<int>("amount"); }
            set { SetAttribute("amount", value); }
        }
        public string Percentage
        {
            get { return GetAttribute<string>("percentage"); }
            set { SetAttribute("percentage", value); }
        }

        public BoletoInterest()
          : this(null)
        {
        }

        public BoletoInterest(PagarMeService service)
            : base(service)
        {
        }
    }
}