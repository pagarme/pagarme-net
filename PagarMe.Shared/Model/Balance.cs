using PagarMe.Base;
using PagarMe.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagarMe.Model
{
    public class Balance : Base.Model
    {
        protected override string Endpoint { get { return "/balance"; } }

        public Balance() : this(null) { }

        public Balance(PagarMeService service) : base(service) { }

        public Balance(PagarMeService serivce, string endpointPrefix = "") : base(serivce, endpointPrefix) { }

        public int WaitingFunds
        {
            get { return GetAttribute<Amount>("waiting_funds").amount; }
        }

        public int Available
        {
            get { return GetAttribute<Amount>("available").amount; }
        }

        public int Transferred
        {
            get { return GetAttribute<Amount>("transferred").amount; }
        }
        
        public QueriableModelCollection<BalanceOperation, BalanceOperationQueriableParameters> Operations
        {
            get { return new BalanceOperation(Service, endpointPrefix).History(Endpoint); }
        }

        private class Amount : Base.Model
        {
            protected override string Endpoint { get { return ""; } }

            public Amount(PagarMeService service) : base(service) { }

            public int amount
            {
                get { return GetAttribute<int>("amount"); }
            }
        }

    }
}
