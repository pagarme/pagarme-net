using System;
namespace PagarMe
{
    public class Zipcode : Base.Model
    {
        protected override string Endpoint { get { return "/zipcodes"; } }
       
        public Zipcode()
            : this(null)
        {
        }

        public Zipcode(PagarMeService service)
            : base(service)
        {
        }

        public string Street
        {
            get { return GetAttribute<string>("street"); }
        }

        public string Neighborhood
        {
            get { return GetAttribute<string>("neighborhood"); }
        }

        public string City
        {
            get { return GetAttribute<string>("city"); }
        }

        public string State
        {
            get { return GetAttribute<string>("state"); }
        }

        public string zipcode
        {
            get { return GetAttribute<string>("zipcode"); }
            set { SetAttribute("zipcode", value); }
        }

        public void CheckZipcode(String zipcode){
            var request = CreateRequest("GET", zipcode);
            ExecuteSelfRequest(request);
        }

    }
}
