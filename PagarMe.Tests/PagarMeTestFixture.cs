using PagarMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace PagarMe.Tests
{
    public class PagarMeTestFixture
    {

        static PagarMeTestFixture ()
        {
            PagarMeService.DefaultApiKey = "sk_test_b9ec56a341db45c5a38d8aac24717020";
            PagarMeService.DefaultEncryptionKey = "ek_test_M712AmOsMTWH0NwI3ttKLNpYLwyNzQ";
        }

        protected static string GenerateSomeEmail() => "some@email.com";
        private static string GenerateSomeSiteUrl() => "http://www.some-site.com";

        protected static RegisterInformation GenerateRegisterInformationTypeIndividual(string documentNumber)
        {
            return new RegisterInformation
            {
                Type = "individual",
                DocumentNumber = documentNumber,
                Name = Guid.NewGuid().ToString(),
                SiteUrl = GenerateSomeSiteUrl(),
                Email = GenerateSomeEmail(),
                MotherName = Guid.NewGuid().ToString(),
                Birthdate = "01/01/0001",
                MonthlyIncome = "1000",
                ProfessionalOccupation = Guid.NewGuid().ToString(),
                Address = new RegisterInformationAddress
                {
                    Street = Guid.NewGuid().ToString(),
                    Complementary = Guid.NewGuid().ToString(),
                    StreetNumber = Guid.NewGuid().ToString(),
                    Neighborhood = Guid.NewGuid().ToString(),
                    City = Guid.NewGuid().ToString(),
                    State = Guid.NewGuid().ToString(),
                    Zipcode = Guid.NewGuid().ToString(),
                    ReferencePoint = Guid.NewGuid().ToString(),
                },
                PhoneNumbers = new[]
                {
                    new RegisterInformationPhone
                    {
                        Ddd = "11",
                        Number = "991507252",
                        Type = "mobile"
                    }
                }
            };
        }
        
        protected static RegisterInformation GenerateRegisterInformationTypeCorporation(string documentNumber)
        {
            return new RegisterInformation
            {
                Type = "corporation",
                DocumentNumber = documentNumber,
                CompanyName = Guid.NewGuid().ToString(),
                TradingName = Guid.NewGuid().ToString(),
                AnnualRevenue = Guid.NewGuid().ToString(),
                CorporationType = Guid.NewGuid().ToString(),
                FoundingDate = Guid.NewGuid().ToString(),
                Email = GenerateSomeEmail(),
                SiteUrl = GenerateSomeSiteUrl(),
                PhoneNumbers = new[]
                {
                    new RegisterInformationPhone
                    {
                        Ddd = "11",
                        Number = "995759282",
                        Type = "mobile"
                    }
                },
                MainAddress = new RegisterInformationAddress
                {
                    Street = Guid.NewGuid().ToString(),
                    Complementary = Guid.NewGuid().ToString(),
                    StreetNumber = Guid.NewGuid().ToString(),
                    Neighborhood = Guid.NewGuid().ToString(),
                    City = Guid.NewGuid().ToString(),
                    State = Guid.NewGuid().ToString(),
                    Zipcode = Guid.NewGuid().ToString(),
                    ReferencePoint = Guid.NewGuid().ToString(),
                },
                ManagingPartners = new[]
                {
                    GeneratePartnerTypeIndividual(documentNumber)
                }
            };
        }
        
        protected static Partner GeneratePartnerTypeIndividual(string documentNumber)
        {
            return new Partner {
                Type = "individual",
                Name = Guid.NewGuid().ToString(),
                DocumentNumber = documentNumber,
                MotherName = Guid.NewGuid().ToString(),
                Birthdate = "01/01/0001",
                Email = GenerateSomeEmail(),
                MonthlyIncome = "1000",
                ProfessionalOccupation = Guid.NewGuid().ToString(),
                SelfDeclaredLegalRepresentative = true,
                Address = new RegisterInformationAddress
                {
                    Street = Guid.NewGuid().ToString(),
                    Complementary = Guid.NewGuid().ToString(),
                    StreetNumber = Guid.NewGuid().ToString(),
                    Neighborhood = Guid.NewGuid().ToString(),
                    City = Guid.NewGuid().ToString(),
                    State = Guid.NewGuid().ToString(),
                    Zipcode = Guid.NewGuid().ToString(),
                    ReferencePoint = Guid.NewGuid().ToString(),
                },
                PhoneNumbers = new[]
                {
                    new RegisterInformationPhone
                    {
                        Ddd = "11",
                        Number = "991507252",
                        Type = "mobile"
                    }
                }
            };
        }
        
        public static Recipient CreateRecipientWithAnotherBankAccount()
        {
            BankAccount bank = new BankAccount
            {
                BankCode = "341",
                Agencia = "0609",
                Conta = "03032",
                ContaDv = "5",
                DocumentNumber = "44417398850",
                LegalName = "Fellipe xD"
            };

            bank.Save();

            return new Recipient()
            {
                TransferInterval = TransferInterval.Monthly,
                TransferDay = 5,
                TransferEnabled = true,
                BankAccount = bank
            };
        }

        public static Recipient CreateRecipient(BankAccount bank)
        {
            return new Recipient()
            {
                TransferInterval = TransferInterval.Monthly,
                TransferDay = 5,
                TransferEnabled = true,
                BankAccount = bank
            };
        }

        public static Recipient CreateRecipient()
        {
            BankAccount bank = CreateTestBankAccount();
            bank.Save();
            return new Recipient()
            {
                TransferInterval = TransferInterval.Daily,
                AnticipatableVolumePercentage = 100,
                TransferEnabled = true,
                BankAccount = bank
            };

        }

        public static Transfer CreateTestTransfer(string bank_account_id,string recipient_id)
        {
            return new Transfer
            {
                Amount = 1000,
                RecipientId = recipient_id,
                BankAccountId = bank_account_id
            };
        }

		public static Plan CreateTestPlan()
		{
			return new Plan()
			{
				Name = "Test Plan",
				Days = 30,
				TrialDays = 0,
				Amount = 1099,
				Color = "#787878",
				PaymentMethods = new PaymentMethod[] { PaymentMethod.CreditCard },
                InvoiceReminder = 3
			};
		}

        public static Plan CreateBoletoPlan()
        {
            return new Plan()
            {
                Name = "Test Plan",
                Days = 30,
                TrialDays = 0,
                Amount = 1099,
                Color = "#787878",
                PaymentMethods = new PaymentMethod[] { PaymentMethod.Boleto },
                InvoiceReminder = 3
            };
        }

        public static BankAccount CreateTestBankAccount()
		{
			return new BankAccount()
			{
				BankCode = "184",
				Agencia = "0808",
				AgenciaDv = "8",
				Conta = "08808",
				ContaDv = "8",
				DocumentNumber = "43591017833",
				LegalName = "Teste " + DateTime.Now.ToShortTimeString()
			};
		}
        
        public static BankAccount CreateTestBankAccount(string documentNumber)
        {
            return new BankAccount()
            {
                BankCode = "184",
                Agencia = "0808",
                AgenciaDv = "8",
                Conta = "08808",
                ContaDv = "8",
                DocumentNumber = "43591017833",
                LegalName = "Teste " + DateTime.Now.ToShortTimeString()
            };
        }

        public static BulkAnticipation CreateBulkAnticipation()
        {
            return new BulkAnticipation()
            {
                Timeframe = TimeFrame.Start,
                PaymentDate = GenerateValidAnticipationDate(),
                RequestedAmount = 900000,
            };
        }

        public static Transaction BoletoTransactionPaidWithPostbackURL()
        {
            var transaction = new Transaction
            {
                Amount = 1099,
                PaymentMethod = PaymentMethod.Boleto,
                Customer = new Customer
                {
                    Name = "Aadwark Silva",
                    DocumentNumber = "64302475200"
                },
                PostbackUrl = "https://apitest.me/handlepostback"
            };
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            return transaction;
        }

		public static async Task PayBoletoTransaction(Transaction t)
		{
			t.Status = TransactionStatus.Paid;
			await Task.Delay(2000);
			await t.SaveAsync();
		}

		public static Transaction CreateTestTransaction()
		{
			return new Transaction
			{
				Amount = 1099,
				PaymentMethod = PaymentMethod.CreditCard,
				CardHash = GetCardHash()
			};
		}

        public static Transaction CreateTestBoletoTransaction()
        {
            return new Transaction
            {
                Amount = 100000,
                PaymentMethod = PaymentMethod.Boleto,
                Customer = new Customer
                {
                    Name = "Aadwark Silva",
                    DocumentNumber = "64302475200"
                }
            };

        }

        public static Transaction CreateTestCardTransactionWithInstallments()
        {
            return new Transaction
            {
                Amount = 1099,
                PaymentMethod = PaymentMethod.CreditCard,
                Installments = 5,
                CardHash = GetCardHash()
            };
        }

        public static Transaction CreateBoletoSplitRuleTransaction(Recipient recipient)
        {
            return new Transaction
            {
                Amount = 10000,
                PaymentMethod = PaymentMethod.Boleto,
                Customer = new Customer
                {
                    Name = "Aadwark Silva",
                    DocumentNumber = "64302475200"
                },
                SplitRules = CreateSplitRule(recipient)
            };
        }

        public static Transaction CreateCreditCardSplitRuleTransaction(Recipient recipient)
        {
            return new Transaction
            {
                Amount = 1000000,
                PaymentMethod = PaymentMethod.CreditCard,
                CardHash = GetCardHash(),
                SplitRules = CreateSplitRule(recipient)
            };
        }


        public static SplitRule[] CreateSplitRule(Recipient recipient)
        {
            List<SplitRule> splits = new List<SplitRule>();
            Recipient rec = CreateRecipient();
            rec.Save();

            SplitRule split1 = new SplitRule()
            {
                Recipient = rec,
                Percentage = 10
            };

            SplitRule split2 = new SplitRule()
            {
                Recipient = recipient,
                Percentage = 90
            };

            splits.Add(split1);
            splits.Add(split2);

            return splits.ToArray();
        }

		public static string GetCardHash()
		{
			var creditcard = new CardHash();

			creditcard.CardHolderName = "Jose da Silva";
			creditcard.CardNumber = "5433229077370451";
			creditcard.CardExpirationDate = "1038";
			creditcard.CardCvv = "018";

			return creditcard.Generate();
		}

        public static Payable returnPayable(int id)
        {
            return PagarMeService.GetDefaultService().Payables.Find(id);
        }

        public static DateTime GenerateValidAnticipationDate()
        {
            DateTime Today = new DateTime();
            Today = DateTime.Now;

            DateTime AnticipationDate = new DateTime();
            AnticipationDate = Today.AddDays(5);

            while (!isValidDay(AnticipationDate))
            {
                AnticipationDate = AnticipationDate.AddDays(2);
            }
            return AnticipationDate;
        }

        private static Boolean isValidDay(DateTime AnticipationDate)
        {
            return !(AnticipationDate.DayOfWeek == DayOfWeek.Sunday || AnticipationDate.DayOfWeek == DayOfWeek.Saturday || isHoliday(AnticipationDate));
        }

        private static Boolean isHoliday(DateTime AnticipationDate)
        {
            var Holidays = GetPagarMeOfficialHolidays(AnticipationDate);

            return Holidays.Any(AnticipationDate.ToString("yyyy-MM-dd").Contains);
        }

        private static List<string> GetPagarMeOfficialHolidays(DateTime AnticipationDate)
        {
            string HolidayCalendarPath = "https://raw.githubusercontent.com/pagarme/business-calendar/master/data/brazil/";
            Uri CalendarURL = new Uri(HolidayCalendarPath + AnticipationDate.Year.ToString() + ".json");
            var request = (HttpWebRequest)WebRequest.Create(CalendarURL);
            var response = (HttpWebResponse)request.GetResponse();

            var PagarMeCalendarString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            JObject PagarMeCalendarJson = JsonConvert.DeserializeObject<JObject>(PagarMeCalendarString);

            var PagarMeCalendar = PagarMeCalendarJson["calendar"];

            var Holidays = new List<string>();

            foreach (var CalendarDay in PagarMeCalendar)
            {
                Holidays.Add(CalendarDay["date"].ToString());
            };

            return Holidays;
        }
    }
}
