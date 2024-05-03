namespace PagarMe
{
    public class RegisterInformation : Base.AbstractModel
    {
        public string Type
        {
            get { return GetAttribute<string>("type"); }
            set { SetAttribute("type", value); }
        }

        public string DocumentNumber
        {
            get { return GetAttribute<string>("document_number"); }
            set { SetAttribute("document_number", value); }
        }

        public string Name
        {
            get { return GetAttribute<string>("name"); }
            set { SetAttribute("name", value); }
        }
        
        public string CompanyName
        {
            get { return GetAttribute<string>("company_name"); }
            set { SetAttribute("company_name", value); }
        }

        public string TradingName
        {
            get { return GetAttribute<string>("trading_name"); }
            set { SetAttribute("trading_name", value); }
        }
        
        public string AnnualRevenue
        {
            get { return GetAttribute<string>("annual_revenue"); }
            set { SetAttribute("annual_revenue", value); }
        }
        
        public string CorporationType
        {
            get { return GetAttribute<string>("corporation_type"); }
            set { SetAttribute("corporation_type", value); }
        }
        
        public string FoundingDate
        {
            get { return GetAttribute<string>("founding_date"); }
            set { SetAttribute("founding_date", value); }
        }
        
        public string Email
        {
            get { return GetAttribute<string>("email"); }
            set { SetAttribute("email", value); }
        }
        
        public string SiteUrl
        {
            get { return GetAttribute<string>("site_url"); }
            set { SetAttribute("site_url", value); }
        }
        
        public string ProfessionalOccupation
        {
            get { return GetAttribute<string>("professional_occupation"); }
            set { SetAttribute("professional_occupation", value); }
        }

        public string MotherName
        {
            get { return GetAttribute<string>("mother_name"); }
            set { SetAttribute("mother_name", value); }
        }
        
        public string Birthdate
        {
            get { return GetAttribute<string>("birthdate"); }
            set { SetAttribute("birthdate", value); }
        }
        
        public string MonthlyIncome
        {
            get { return GetAttribute<string>("monthly_income"); }
            set { SetAttribute("monthly_income", value); }
        }
        
        public RegisterInformationPhone[] PhoneNumbers
        {
            get { return GetAttribute<RegisterInformationPhone[]>("phone_numbers"); }
            set { SetAttribute("phone_numbers", value); }
        }

        public RegisterInformationAddress Address
        {
            get { return GetAttribute<RegisterInformationAddress>("address"); }
            set { SetAttribute("address", value); }
        }
        
        public RegisterInformationAddress MainAddress
        {
            get { return GetAttribute<RegisterInformationAddress>("main_address"); }
            set { SetAttribute("main_address", value); }
        }

        public Partner[] ManagingPartners
        {
            get { return GetAttribute<Partner[]>("managing_partners"); }
            set { SetAttribute("managing_partners", value); }
        }
        
        public RegisterInformation()
            : this(null)
        {
        }

        public RegisterInformation(PagarMeService service)
            : base(service)
        {
        }
    }
    
    public class RegisterInformationPhone : Base.AbstractModel
    {
        public string Ddd
        {
            get { return GetAttribute<string>("ddd"); }
            set { SetAttribute("ddd", value); }
        }

        public string Number
        {
            get { return GetAttribute<string>("number"); }
            set { SetAttribute("number", value); }
        }

        public string Type
        {
            get { return GetAttribute<string>("type"); }
            set { SetAttribute("type", value); }
        }

        public RegisterInformationPhone()
            : this(null)
        {
        }

        public RegisterInformationPhone(PagarMeService service)
            : base(service)
        {
        }
    }
    
    public class RegisterInformationAddress : Base.AbstractModel
    {
        public string Street
        {
            get { return GetAttribute<string>("street"); }
            set { SetAttribute("street", value); }
        }

        public string Complementary
        {
            get { return GetAttribute<string>("complementary"); }
            set { SetAttribute("complementary", value); }
        }

        public string StreetNumber
        {
            get { return GetAttribute<string>("street_number"); }
            set { SetAttribute("street_number", value); }
        }

        public string Neighborhood
        {
            get { return GetAttribute<string>("neighborhood"); }
            set { SetAttribute("neighborhood", value); }
        }

        public string City
        {
            get { return GetAttribute<string>("city"); }
            set { SetAttribute("city", value); }
        }

        public string State
        {
            get { return GetAttribute<string>("state"); }
            set { SetAttribute("state", value); }
        }

        public string Zipcode
        {
            get { return GetAttribute<string>("zipcode"); }
            set { SetAttribute("zipcode", value); }
        }
        
        public string ReferencePoint
        {
            get { return GetAttribute<string>("reference_point"); }
            set { SetAttribute("reference_point", value); }
        }

        public RegisterInformationAddress()
            : this(null)
        {
        }

        public RegisterInformationAddress(PagarMeService service)
            : base(service)
        {
        }
    }

    public class Partner : Base.AbstractModel
    {
        public string Type
        {
            get { return GetAttribute<string>("type"); }
            set { SetAttribute("type", value); }
        }
        
        public string Name
        {
            get { return GetAttribute<string>("name"); }
            set { SetAttribute("name", value); }
        }
        
        public string DocumentNumber
        {
            get { return GetAttribute<string>("document_number"); }
            set { SetAttribute("document_number", value); }
        }
        
        public string MotherName
        {
            get { return GetAttribute<string>("mother_name"); }
            set { SetAttribute("mother_name", value); }
        }
        
        public string Birthdate
        {
            get { return GetAttribute<string>("birthdate"); }
            set { SetAttribute("birthdate", value); }
        }
        
        public string Email
        {
            get { return GetAttribute<string>("email"); }
            set { SetAttribute("email", value); }
        }
        
        public string MonthlyIncome
        {
            get { return GetAttribute<string>("monthly_income"); }
            set { SetAttribute("monthly_income", value); }
        }
        
        public string ProfessionalOccupation
        {
            get { return GetAttribute<string>("professional_occupation"); }
            set { SetAttribute("professional_occupation", value); }
        }
        
        public bool SelfDeclaredLegalRepresentative
        {
            get { return GetAttribute<bool>("self_declared_legal_representative"); }
            set { SetAttribute("self_declared_legal_representative", value); }
        }
        
        public RegisterInformationAddress Address
        {
            get { return GetAttribute<RegisterInformationAddress>("address"); }
            set { SetAttribute("address", value); }
        }

        public RegisterInformationPhone[] PhoneNumbers
        {
            get { return GetAttribute<RegisterInformationPhone[]>("phone_numbers"); }
            set { SetAttribute("phone_numbers", value); }
        }

        public Partner()
            : this(null)
        {
        }

        public Partner(PagarMeService service)
            : base(service)
        {
        }
    }
}