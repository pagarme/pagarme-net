using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Tests
{
    public class PagarMeTestFixture
    {
        static PagarMeTestFixture()
        {
            PagarMeService.DefaultApiKey = "ak_test_RBORKsHflgcrO7gISMyhatMx8UyiJY";
            PagarMeService.DefaultEncryptionKey = "ek_test_Ajej5CakM8QXGnA2lWX3AarwLWqspL";
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
                PaymentMethods = new PaymentMethod[] { PaymentMethod.CreditCard }
            };
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

        public static string GetCardHash()
        {
            var creditcard = new CardHash();

            creditcard.CardHolderName = "Jose da Silva";
            creditcard.CardNumber = "5433229077370451";
            creditcard.CardExpirationDate = "1016";
            creditcard.CardCvv = "018";

            return creditcard.Generate();
        }
    }
}
