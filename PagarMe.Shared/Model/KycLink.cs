using System;

namespace PagarMe.Model
{
    public class KycLink : Base.AbstractModel
    {
        public string Base64
        {
            get { return GetAttribute<string>("base64"); }
            protected set { SetAttribute("base64", value); }
        }

        public string Url
        {
            get { return GetAttribute<string>("url"); }
            protected set { SetAttribute("url", value); }
        }

        public DateTime ExpirationDate
        {
            get { return GetAttribute<DateTime>("expiration_date"); }
            protected set { SetAttribute("expiration_date", value); }
        }
        
        public KycLink() : this(null)
        {
        }
        
        public KycLink(PagarMeService service) : base(service)
        {
        }
    }
}