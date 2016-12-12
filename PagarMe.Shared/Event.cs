using System;
namespace PagarMe
{
    public class Event : Base.Model
    {
        public Event() : this(null)
        {
        }

        public Event(PagarMeService service, string endpointPrefix = "") : base(service, endpointPrefix)
        {
        }

        protected override string Endpoint
        {
            get { return "/events"; }
        }

        public string Model
        {
            get { return GetAttribute<string>("model"); }
        }

        public string ModelId
        {
            get { return GetAttribute<string>("model_id"); }
        }

        public string Name
        {
            get { return GetAttribute<string>("name"); }
        }

        public Base.AbstractModel Payload
        {
            get { return GetAttribute<Base.AbstractModel>("payload"); }
        }
    }
}