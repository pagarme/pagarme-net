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
    public class PagarMeTestFixture
    {

        static PagarMeTestFixture ()
        {
            PagarMeService.DefaultApiKey = "ak_test_RBORKsHflgcrO7gISMyhatMx8UyiJY";
            PagarMeService.DefaultEncryptionKey = "ek_test_Ajej5CakM8QXGnA2lWX3AarwLWqspL";
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
				PaymentMethods = new PaymentMethod[] { PaymentMethod.CreditCard }
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

        public static BulkAnticipation CreateBulkAnticipation()
        {
            return new BulkAnticipation()
            {
                Timeframe = TimeFrame.Start,
                PaymentDate = DateTime.Now.AddDays(5),
                RequestedAmount = 900000,
                Build = false
            };
        }
        
        public static BulkAnticipation CreateBulkAnticipationWithBuildTrue()
        {
            return new BulkAnticipation()
            {
                Timeframe = TimeFrame.Start,
                PaymentDate = DateTime.Now.AddDays(5),
                Build = true,
                RequestedAmount = 900000
            };
        }

        public static Transaction CreateTestBoletoTransactionWithPostbackUrl()
        {
            return new Transaction
            {
                Amount = 1099,
                PaymentMethod = PaymentMethod.Boleto,
                PostbackUrl = "https://apitest.me/handlepostback"
            };
        }

		public static async Task PayBoletoTransaction(Transaction t)
		{
			t.Status = TransactionStatus.Paid;
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
                PaymentMethod = PaymentMethod.Boleto
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
	}
}
