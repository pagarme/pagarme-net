using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Enumeration;

namespace PagarMe.Model
{
    public class PostbackDelivery : Base.AbstractModel
    {
        public PostbackDelivery(PagarMeService service) : base(service) {}

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

        public string ResponseBody
        {
            get { return GetAttribute<string>("response_body"); }
        }

        public string ResponseHeaders
        {
            get { return GetAttribute<string>("response_headers"); }
        }

        public int ResponseTime
        {
            get { return GetAttribute<int>("response_time"); }
        }

        public PostbackDeliveryStatus status
        {
            get { return GetAttribute<PostbackDeliveryStatus>("status"); }
        }

        public int StatusCode
        {
            get { return GetAttribute<int>("status_code"); }
        }

        public string StatusReason
        {
            get { return GetAttribute<string>("status_reason"); }
        }

    }
}
