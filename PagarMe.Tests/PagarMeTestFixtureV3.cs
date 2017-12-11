using PagarMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;

namespace PagarMe.Tests
{
    public class PagarMeTestFixtureV3
    {
        private static PagarMeService CompanyV3;

        static PagarMeTestFixtureV3 ()
        {
            CompanyTemporary company = new CompanyTemporary();
            company.ApiVersion["test"] = "2018-08-28";
            company.ApiVersion["live"] = "2018-08-28";

            company.Save();

            CompanyV3 = new PagarMeService(company.ApiKey["test"].ToString(), company.EncryptionKey["test"].ToString());
        }

        public static Customer CreateTestV3Customer()
        {
            return new Customer (CompanyV3)
            {
                
                ExternalId = "213",
                Name = "Customer Name",
                Type = CustomerType.Individual,
                Email = "customer@mail.com",
                Country = "br",
                Documents = new Document[]{
                    new Document()
                    {
                        Type = DocumentType.Cpf,
                        Number = "38494357336"
                    }
                },
                PhoneNumbers = new string[]
                {
                    "+5511982738291",
                    "+5511829378291"
                },
                Birthday = new DateTime(1991, 12, 12).ToString("yyyy-MM-dd")
            };
        }

        public static Billing CreateTestBilling()
        {
            return new Billing(CompanyV3)
            {
                Name = "Customer billing",
                Address = new Address()
                {
                    Country = "br",
                    State = "sp",
                    City = "Cotia",
                    Neighborhood = "Rio Cotia",
                    Street = "Rua Matrix",
                    StreetNumber = "213",
                    Zipcode = "04250000"
                }
            };
        }

        public static Shipping CreateTestShipping()
        {
            var Today = DateTime.Now;
            return new Shipping(CompanyV3)
            {
                Name = "Customer Shipping",
                Fee = 1000,
                DeliveryDate = Today.AddDays(4).ToString("yyyy-MM-dd"),
                Expedited = false,
                Address = new Address()
                {
                    Country = "br",
                    State = "sp",
                    City = "Cotia",
                    Neighborhood = "Rio Cotia",
                    Street = "Rua Matrix",
                    StreetNumber = "213",
                    Zipcode = "04250000"
                }
            };
        }

        public static Item[] createTestItems()
        {
            return new Item[]
            {
                new Item(CompanyV3)
                {
                    Id = "1",
                    Title = "Car",
                    Quantity = 1,
                    Tangible = true,
                    UnitPrice = 3000
                },
                new Item()
                {
                    Id = "2",
                    Title = "Baby Crib",
                    Quantity = 1,
                    Tangible = true,
                    UnitPrice = 45000
                }
            };
        }

        public static Transaction CreateTestV3CardTransaction()
        {
            return new Transaction(CompanyV3)
            {
                Amount = 1099,
                Installments = 2,
                PaymentMethod = PaymentMethod.CreditCard,
                Customer = CreateTestV3Customer(),
                Billing = CreateTestBilling(),
                Shipping = CreateTestShipping(),
                Item = createTestItems(),
                CardHash = GetCardHash(),
                Async = false
            };
        }

        public static Transaction CreateTestV3BoletoTransaction()
        {
            return new Transaction(CompanyV3)
            {
                Amount = 1099,
                PaymentMethod = PaymentMethod.Boleto,
                Customer = CreateTestV3Customer(),
                Async = false
            };
        }

        public static string GetCardHash()
        {
            var creditcard = new CardHash(CompanyV3);

            creditcard.CardHolderName = "Jose da Silva";
            creditcard.CardNumber = "5433229077370451";
            creditcard.CardExpirationDate = "1038";
            creditcard.CardCvv = "018";

            return creditcard.Generate();
        }
    }
}
