using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PagarMe.Model;

namespace PagarMe.Tests
{
	[TestFixture]
	public class RecipientTests : PagarMeTestFixture
	{
		[Test]
		public void CreateWithOldFields ()
		{
			var bank = CreateTestBankAccount ();

			bank.Save ();

			Assert.IsNotNull (bank.Id);

			var recipient = new Recipient () {
				TransferDay = 5,
				TransferEnabled = true,
				TransferInterval = TransferInterval.Weekly,
				BankAccount = bank
			};

			recipient.Save ();

			Assert.IsNotNull (recipient.Id);
		}

        [Test]
        public void ReturnBalance()
        {
            Recipient recipient = CreateRecipient();
            recipient.Save();
            Balance balance = recipient.Balance;

            Assert.IsTrue(balance.Available == 0);
            Assert.IsTrue(balance.Transferred == 0);
            Assert.IsTrue(balance.WaitingFunds == 0);
        }

        [Test]
        public void returnBalaceOperationsPayable()
        {
            Recipient recipient = CreateRecipient();
            recipient.Save();

            Transaction transaction = CreateBoletoSplitRuleTransaction(recipient);
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            System.Threading.Thread.Sleep(10000);

            BalanceOperation[] operation = recipient.Balance.Operations.FindAll(new BalanceOperation()).ToArray();

            Assert.IsNotNull(operation.First().MovementPayable);
            Assert.IsNull(operation.First().MovementBulkAnticipation);
        }

        [Test]
        public void ReturnAnticipationMaxValue()
        {
            Recipient recipient = CreateRecipient();
            recipient.Save();

            DateTime date = DateTime.Now;
            date = GenerateValidAnticipationDate();

            var limit = recipient.MaxAnticipationValue(TimeFrame.Start, date);
            Assert.IsTrue(limit.Amount == 0);
        }

        [Test]
        public void ReturnAnticipationMinValue()
        {
            Recipient recipient = CreateRecipient();
            recipient.Save();

            DateTime date = DateTime.Now;
            date = GenerateValidAnticipationDate();

            var limit = recipient.MinAnticipationValue(TimeFrame.Start, date);
            Assert.IsTrue(limit.Amount == 0);
        }
        
        [Test]
        public void CreateAnticipation()
        {

            BulkAnticipation anticipation = CreateBulkAnticipation();

            Recipient recipient = CreateRecipient();
            recipient.Save();

            Transaction transaction = CreateCreditCardSplitRuleTransaction(recipient);
            transaction.Save();

            recipient.CreateAnticipation(anticipation);

            Assert.IsTrue(anticipation.Status == Enumeration.BulkAnticipationStatus.Pending);               
        }

        [Test]
        public void CancelAnticipation()
        {
            BulkAnticipation anticipation = CreateBulkAnticipation();

            Recipient recipient = CreateRecipient();
            recipient.Save();

            Transaction transaction = CreateCreditCardSplitRuleTransaction(recipient);
            transaction.Save();

            recipient.CreateAnticipation(anticipation);
            Assert.IsTrue(anticipation.Status == Enumeration.BulkAnticipationStatus.Pending);

            recipient.CancelAnticipation(anticipation);
            Assert.IsTrue(anticipation.Status == Enumeration.BulkAnticipationStatus.Canceled);
        }

        [Test]
        public void ReturnAllAnticipations()
        {
            BulkAnticipation anticipation = CreateBulkAnticipation();

            Recipient recipient = CreateRecipient();
            recipient.Save();

            Transaction transaction = CreateCreditCardSplitRuleTransaction(recipient);
            transaction.Save();

            recipient.CreateAnticipation(anticipation);

            Assert.IsTrue(recipient.Anticipations.FindAll(new BulkAnticipation()).Count() == 1);
        }

        [Test]
		public void CreateWithNewFields()
		{
			var bank = CreateTestBankAccount ();

			bank.Save ();

			Assert.IsNotNull (bank.Id);

			var recipient = new Recipient () {
				AnticipatableVolumePercentage = 88,
				AutomaticAnticipationEnabled = true,
				TransferDay = 5,
				TransferEnabled = true,
				TransferInterval = TransferInterval.Weekly,
				BankAccount = bank
			};

			recipient.Save ();

			Assert.IsNotNull (recipient.Id);
			Assert.AreEqual (recipient.AnticipatableVolumePercentage, 88);
			Assert.AreEqual (recipient.AutomaticAnticipationEnabled, true);
		}
		
		[Test]
		public void CreateKycLink()
		{
			var recipient = CreateRecipient();
			recipient.Save();

			var link = recipient.CreateKycLink();

			Assert.IsNotNull(link);
		}

		[Test]
		public void CreateTypeIndividual()
		{
			const string documentNumber = "43591017833";

			var bank = CreateTestBankAccount(documentNumber);
			bank.Save();

			Assert.IsNotNull(bank.Id);

			var recipient = new Recipient
			{
				TransferInterval = TransferInterval.Daily,
				AnticipatableVolumePercentage = 100,
				TransferEnabled = true,
				BankAccount = bank,
				RegisterInformation = GenerateRegisterInformationTypeIndividual(documentNumber)
			};
			recipient.Save();

			Assert.IsNotNull(recipient.Id);

			var expected = PagarMeService.GetDefaultService().Recipients.Find(recipient.Id);

			Assert.IsNotNull(expected);
			Assert.AreEqual(recipient.Id, expected.Id);

			Assert.IsNotNull(expected.RegisterInformation);
			Assert.IsNotNull(expected.RegisterInformation.Address);
			Assert.IsNotNull(recipient.RegisterInformation.PhoneNumbers);

			Assert.AreEqual(recipient.RegisterInformation.Address.Street, expected.RegisterInformation.Address.Street);
			Assert.AreEqual(recipient.RegisterInformation.PhoneNumbers[0].Number,
				expected.RegisterInformation.PhoneNumbers[0].Number);
		}

		[Test]
		public void CreateTypeCorporation()
		{
			const string documentNumber = "43591017833";

			var bank = CreateTestBankAccount(documentNumber);
			bank.Save();

			Assert.IsNotNull(bank.Id);

			var recipient = new Recipient
			{
				TransferInterval = TransferInterval.Daily,
				AnticipatableVolumePercentage = 100,
				TransferEnabled = true,
				BankAccount = bank,
				RegisterInformation = GenerateRegisterInformationTypeCorporation(documentNumber)
			};
			recipient.Save();

			Assert.IsNotNull(recipient.Id);

			var expected = PagarMeService.GetDefaultService().Recipients.Find(recipient.Id);

			Assert.IsNotNull(expected);
			Assert.AreEqual(recipient.Id, expected.Id);

			Assert.IsNotNull(expected.RegisterInformation);
			Assert.IsNotNull(expected.RegisterInformation.MainAddress);
			Assert.IsNotNull(expected.RegisterInformation.ManagingPartners);
			Assert.IsNotNull(recipient.RegisterInformation.PhoneNumbers);

			Assert.AreEqual(recipient.RegisterInformation.MainAddress.Street, expected.RegisterInformation.MainAddress.Street);
			Assert.AreEqual(recipient.RegisterInformation.ManagingPartners[0].Name, expected.RegisterInformation.ManagingPartners[0].Name);
			Assert.AreEqual(recipient.RegisterInformation.PhoneNumbers[0].Number,
				expected.RegisterInformation.PhoneNumbers[0].Number);
		}
	}
}