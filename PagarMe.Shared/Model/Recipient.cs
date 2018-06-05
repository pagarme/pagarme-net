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
using Newtonsoft.Json.Linq;
using PagarMe.Model;
using PagarMe.Base;

namespace PagarMe
{
    public class Recipient : Base.Model
    {
        private Base.ModelCollection<BulkAnticipation> _anticipations;
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

        public string AutomaticAnticipationType
        {
            get { return GetAttribute<String>("automatic_anticipation_type"); }
            set { SetAttribute("automatic_anticipation_type", value); }
        }

        public int AutomaticAnticipation1025Delay
        {
            get { return GetAttribute<int>("automatic_anticipation_1025_delay"); }
            set { SetAttribute("automatic_anticipation_1025_delay", value); }
        }

        public string AutomaticAnticipationDays
        {
            get{ return GetAttribute<String>("automatic_anticipation_days"); }
            set { SetAttribute("automatic_anticipation_days",value);}
        }

        public string Status
        {
            get { return GetAttribute<string>("status"); }
        }

        public string StatusReason
        {
            get { return GetAttribute<string>("status_reason"); }
        }
             
        public Balance Balance
        {
            get { return new ModelCollection<Balance>(Service, "/balance", Endpoint + "/" + Id).FindAllObject(new Balance()); }
        }

        private BulkAnticipationLimit AnticipationLimits(TimeFrame timeframe, DateTime paymentDate)
        {
            BulkAnticipationLimit bulk = new BulkAnticipationLimit();
            bulk.TimeFrame = timeframe;
            bulk.PaymentDate = Utils.ConvertToUnixTimeStamp(paymentDate).ToString();

            return new ModelCollection<BulkAnticipationLimit>(Service, "/bulk_anticipations/limits", Endpoint + "/" + Id).FindAllObject(bulk);
        }

        public Limit MaxAnticipationValue(TimeFrame timeframe, DateTime paymentDate)
        {
            var limit = AnticipationLimits(timeframe, paymentDate);
            return limit.Maximum;
        }

        public Limit MinAnticipationValue(TimeFrame timeframe, DateTime paymentDate)
        {
            var limit = AnticipationLimits(timeframe, paymentDate);
            return limit.Minimum;
        }

        public void CreateAnticipation(BulkAnticipation anticipation)
        {
            var request = CreateRequest("POST", "/bulk_anticipations");

            request.Query = anticipation.BuildQueryForKeys(anticipation.ToDictionary(SerializationType.Plain));
            var response = request.Execute();

            anticipation.LoadFrom(response.Body);
        }

        public void ConfirmAnticipation(BulkAnticipation anticipation)
        {
            var request = CreateRequest("POST", "/bulk_anticipations/" + anticipation.Id + "/" + "confirm");
            var response = request.Execute();

            anticipation.LoadFrom(response.Body);
        }

        public void CancelAnticipation(BulkAnticipation anticipation)
        {
            var request = CreateRequest("POST", "/bulk_anticipations/" + anticipation.Id + "/" + "cancel");
            var response = request.Execute();

            anticipation.LoadFrom(response.Body);
        }

        public void DeleteAnticipation(BulkAnticipation anticipation)
        {
            var request = CreateRequest("DELETE", "/bulk_anticipations/" + anticipation.Id);
            request.Execute();

            anticipation.LoadFrom("{}");
        }

        public Base.ModelCollection<BulkAnticipation> Anticipations
        {
            get
            {
                if (Id == null)
                {
                    throw new InvalidOperationException("Recipient must have an Id in order to fetch events");
                }

                return _anticipations ?? (_anticipations = new Base.ModelCollection<BulkAnticipation>(Service, "/bulk_anticipations", Endpoint + "/" + Id));
            }
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
