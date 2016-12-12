﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PagarMe.Tests
{
	public class PagarMeTestFixture
	{
<<<<<<< HEAD
        static PagarMeTestFixture ()
=======
		static PagarMeTestFixture()
>>>>>>> remotes/pagarme-remote/master
		{
			PagarMeService.DefaultApiKey = "ak_test_AAAfFBJDvGNMA6YMEoxRyIrK0PlhLI";
			PagarMeService.DefaultEncryptionKey = "ek_test_D8fnTNOqaPBQx46QBiDprUzeophI7q";
		}

<<<<<<< HEAD
        public static Recipient CreateRecipientWithAnotherBankAccount()
        {
            BankAccount bank = new BankAccount
            {
                BankCode = "341",
                Agencia = "0609",
                Conta = "03032",
                ContaDv = "5",
                DocumentNumber = "44417398850",
                LegalName = "Fellipe"
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
                TransferInterval = TransferInterval.Monthly,
                TransferDay = 5,
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

		public static Plan CreateTestPlan ()
=======
		public static Plan CreateTestPlan()
>>>>>>> remotes/pagarme-remote/master
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
				LegalName = "TesteTestadoTestando"
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

<<<<<<< HEAD
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

		public static string GetCardHash ()
=======
		public static Transaction CreateTestBoletoTransaction()
>>>>>>> remotes/pagarme-remote/master
		{
			return new Transaction
			{
				Amount = 1000,
				PaymentMethod = PaymentMethod.Boleto
			};
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
