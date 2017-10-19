using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PagarMe.Model;
using PagarMe.Filter;

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
            BalanceOperation[] operation = recipient.Balance.Operations.FindAll(new BalanceOperation()).ToArray();

            Assert.IsNotNull(operation.First().MovementPayable);
            Assert.IsNull(operation.First().MovementBulkAnticipation);
        }

        [Test]
        public void QueryBalaceOperationsAfter ()
        {
            Recipient recipient = CreateRecipient ();
            recipient.Save ();

            Transaction transaction = CreateBoletoSplitRuleTransaction (recipient);
            transaction.Save ();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save ();
            var query = new BalanceOperationQueriableParameters ();
            query.After(DateTime.Now.AddDays(1));
            BalanceOperation [] operation = recipient.Balance.Operations.FindAll (query).ToArray ();

            Assert.AreEqual (0, operation.Length);
        }

        [Test]
        public void QueryBalaceOperationsBefore ()
        {
            Recipient recipient = CreateRecipient ();
            recipient.Save ();

            Transaction transaction = CreateBoletoSplitRuleTransaction (recipient);
            transaction.Save ();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save ();
            var query = new BalanceOperationQueriableParameters ();
            query.BeforeThan (DateTime.Now.AddDays(-1));
            BalanceOperation [] operation = recipient.Balance.Operations.FindAll (query).ToArray ();

            Assert.AreEqual (0, operation.Length);
        }

        [Test]
        public void ReturnAnticipationMaxValue()
        {
            Recipient recipient = CreateRecipient();
            recipient.Save();

            DateTime date = DateTime.Now;
            date = date.AddDays(5);

            var limit = recipient.MaxAnticipationValue(TimeFrame.Start, date);
            Assert.IsTrue(limit.Amount == 0);
        }

        [Test]
        public void ReturnAnticipationMinValue()
        {
            Recipient recipient = CreateRecipient();
            recipient.Save();

            DateTime date = DateTime.Now;
            date = date.AddDays(5);

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
        public void ConfirmAnticipation()
        {
            BulkAnticipation anticipation = CreateBulkAnticipationWithBuildTrue();

            Recipient recipient = CreateRecipient();
            recipient.Save();

            Transaction transaction = CreateCreditCardSplitRuleTransaction(recipient);
            transaction.Save();

            recipient.CreateAnticipation(anticipation);
            Assert.IsTrue(anticipation.Status == Enumeration.BulkAnticipationStatus.Building);

            recipient.ConfirmAnticipation(anticipation);
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
        public void DeleteAnticipation()
        {
            BulkAnticipation anticipation = CreateBulkAnticipationWithBuildTrue();

            Recipient recipient = CreateRecipient();
            recipient.Save();

            Transaction transaction = CreateCreditCardSplitRuleTransaction(recipient);
            transaction.Save();

            recipient.CreateAnticipation(anticipation);
            Assert.IsTrue(anticipation.Status == Enumeration.BulkAnticipationStatus.Building);

            recipient.DeleteAnticipation(anticipation);
            Assert.IsNull(anticipation.Id);
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
	}
}