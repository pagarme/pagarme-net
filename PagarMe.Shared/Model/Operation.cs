using System;
namespace PagarMe
{
    public class Operation : Base.Model
    {

        protected override string Endpoint { get { return "/operations"; } }

        public Operation()
            : this(null)
        {
        }

        public Operation(PagarMeService service, string endpointPrefix = "") : base(service, endpointPrefix)
        {
            Metadata = new Base.AbstractModel(Service);
        }

        public string Status
        {
            get { return GetAttribute<string>("status"); }
        }

        public string FailReason
        {
            get { return GetAttribute<string>("fail_reason"); }
        }

        public string Type
        {
            get { return GetAttribute<string>("type"); }
        }

        public bool Rollbacked
        {
            get { return GetAttribute<bool>("rollbacked"); }
        }

        public string Model
        {
            get { return GetAttribute<string>("model"); }

        }

        public string ModelId
        {
            get { return GetAttribute<string>("model_id"); }
        }

        public string GroupId
        {
            get { return GetAttribute<string>("group_id"); }
        }

        public string NextGroupId
        {
            get { return GetAttribute<string>("next_group_id"); }
        }

        public string RequestId
        {
            get { return GetAttribute<string>("request_id"); }
        }

        public long StartedAt
        {
            get { return GetAttribute<long>("started_at"); }
        }

        public long EndedAt
        {
            get { return GetAttribute<long>("ended_at"); }
        }

        public string Processor
        {
            get { return GetAttribute<string>("processor"); }
        }

        public string ProcessorResponseCode
        {
            get { return GetAttribute<string>("processor_response_code"); }
        }

        public Base.AbstractModel Metadata
        {
            get { return GetAttribute<Base.AbstractModel>("metadata"); }
            set { SetAttribute("metadata", value); }
        }
    }
}
