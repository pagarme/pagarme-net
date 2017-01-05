using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Enumeration;

namespace PagarMe.Model
{
    public class Postback : Base.Model
    {
        protected override string Endpoint { get { return "/postbacks"; } }

        public Postback() : this(null) { }

        public Postback(PagarMeService service) : base(service) { }

        public Postback(PagarMeService service, string endpointPrefix = "") : base(service, endpointPrefix) { }

        public PostbackDelivery[] Deliveries
        {
            get { return GetAttribute<PostbackDelivery[]>("deliveries"); }
        }

        public string Headers
        {
            get { return GetAttribute<string>("headers"); }
        }

        public PostbackModel Model
        {
            get { return GetAttribute<PostbackModel>("model"); }
        }

        public string ModelId
        {
            get { return GetAttribute<string>("model_id"); }
        }

        public string Payload
        {
            get { return GetAttribute<string>("payload"); }
        }

        public string RequestUrl
        {
            get { return GetAttribute<string>("request_url"); }
        }

        public int Retries
        {
            get { return GetAttribute<int>("retries"); }
        }

        public string Signature
        {
            get { return GetAttribute<string>("signature"); }
        }

        public PostbackStatus Status
        {
            get { return GetAttribute<PostbackStatus>("status"); }
        }

        public void Redeliver()
        {
            var request = CreateRequest("POST", "/redeliver");

            var response = request.Execute();

            LoadFrom(response.Body);

        }
    }
}
