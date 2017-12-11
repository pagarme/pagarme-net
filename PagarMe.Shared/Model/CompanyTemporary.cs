using System;
using System.Collections.Generic;
using System.Text;
using PagarMe.Base;
using PagarMe.Enumeration;

namespace PagarMe
{
    public class CompanyTemporary : Base.Model
    {
        protected override string Endpoint { get { return "/companies/temporary"; } }

        public CompanyTemporary() : this(null) { }

        public CompanyTemporary(PagarMeService service) : base(service) {
            ApiVersion = new Base.AbstractModel(Service);
        }

        public CompanyTemporary(PagarMeService service, string endpointPrefix = "") :base(service, endpointPrefix) { }

        public Base.AbstractModel ApiVersion
        {
            get { return GetAttribute<Base.AbstractModel>("api_version"); }
            set { SetAttribute("api_version", value); }
        }

        public Base.AbstractModel ApiKey
        {
            get { return GetAttribute<Base.AbstractModel>("api_key"); }
        }

        public Base.AbstractModel EncryptionKey
        {
            get { return GetAttribute<Base.AbstractModel>("encryption_key"); }
        }
    }
}
