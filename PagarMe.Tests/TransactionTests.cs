﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    public class TransactionTests : PagarMeTestFixture
    {
        [Test]
        public void Charge()
        {
            var transaction = CreateTestTransaction();

            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }

        [Test]
        public void ChargeWithAsync()
        {
            var transaction = CreateTestTransaction();
            transaction.Async = true;
            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.Processing);
        }

        [Test]
        public void ChargeWithCustomerBornAtNull()
        {
            var transaction = CreateTestTransaction();
            transaction.Customer = new Customer()
            {
                Name = "Aardvark Silva",
                Email = "aardvark.silva@pagar.me",
                BornAt = null,
                DocumentNumber = "00000000000"
            };

            transaction.Save();

            Assert.IsNull(transaction.Customer.BornAt);
        }

        [Test]
        public void ChargeWithCaptureMethodEMV()
        {
            Type t = typeof(Transaction);
            dynamic dTransaction = Activator.CreateInstance(t, null);
            dTransaction.Amount = 10000;
            var futureTime = DateTime.Now.AddMonths(1);
            CardHash card = new CardHash()
            {
                CardNumber = "4242424242424242",
                CardHolderName = "Aardvark Silva",
                CardExpirationDate = futureTime.ToString("MMyy"),
                CardCvv = "176"

            };

            dTransaction.CardHash = card.Generate();
            dTransaction.capture_method = "emv";
            dTransaction.card_track_2 = "thequickbrownfox";
            dTransaction.card_emv_data = "jumpsoverthelazydog";

            dTransaction.Save();

            Assert.IsNotNull(dTransaction.CardEmvResponse);

        }

        [Test]
        public void Authorize()
        {
            var transaction = CreateTestTransaction();

            transaction.ShouldCapture = false;
            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.Authorized);

            transaction.Capture();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }

        [Test]
        public void CaptureWithSplitRules()
        {
            var transaction = CreateAuthorizedTestTransaction();
            transaction.Save();
            Assert.IsTrue(transaction.Status == TransactionStatus.Authorized);
            Assert.IsTrue(transaction.SplitRules == null || transaction.SplitRules.Length == 0);

            var recipient = CreateRecipient();
            recipient.Save();
            var splitRule = new SplitRule
            {
                Recipient = recipient,
                Percentage = 100
            };
            transaction.Capture(transaction.Amount, new SplitRule[] { splitRule });
            var responseSplits = transaction.SplitRules;
            Assert.IsTrue(responseSplits != null && responseSplits.Length == 1);
            Assert.IsTrue(responseSplits[0].Recipient.Id == recipient.Id);
        }

        [Test]
        public void Refund()
        {
            var transaction = CreateTestTransaction();

            transaction.Save();
            transaction.Refund();

            Assert.IsTrue(transaction.Status == TransactionStatus.Refunded);
        }

        [Test]
        public void RefundWithBoleto()
        {
            var transaction = CreateTestBoletoTransaction();
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();
            var bankAccount = CreateTestBankAccount();
            transaction.Refund(bankAccount);
            Assert.IsTrue(transaction.Status == TransactionStatus.PendingRefund);
        }

        [Test]
        public void BoletoRefund()
        {
            var transaction = CreateTestBoletoTransaction();
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            BankAccount bank = CreateTestBankAccount();

            transaction.Refund(bank);

            Assert.IsTrue(transaction.Status == TransactionStatus.PendingRefund);
        }

        [Test]
        public void PartialRefund()
        {
            var transaction = CreateTestTransaction();

            transaction.Save();
            int amountToBeRefunded = 100;
            transaction.Refund(amountToBeRefunded);

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
            Assert.IsTrue(transaction.RefundedAmount == amountToBeRefunded);
        }

        [Test]
        public void SendMetadata()
        {
            var transaction = CreateTestTransaction();

            transaction.Metadata["test"] = "uhuul";

            transaction.Save();

            Assert.IsTrue(transaction.Metadata["test"].ToString() == "uhuul");
        }

        [Test]
        public void FindPayablesTest()
        {
            Transaction transaction = CreateTestBoletoTransaction();
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

           Payable payable = transaction.Payables.FindAll(new Payable()).First();
           Assert.IsTrue(payable.Amount.Equals(transaction.Amount));
           Assert.IsTrue(payable.Status.Equals(PayableStatus.Paid));
        }
    }

    [TestFixture]
    public class TransactionEventTests : PagarMeTestFixture
    {
        [Test]
        public void HasEvents()
        {
            var transaction = CreateTestTransaction();
            transaction.Save();
            var events = transaction.Events.FindAll(new Event());
            Assert.AreEqual(1, events.Count());
        }

        [Test]
        public void ThrowsExceptionIfIdNull()
        {
            try
            {
                var transaction = CreateTestTransaction();
                var events = transaction.Events;
                Assert.Fail();
            }
            catch (InvalidOperationException)
            {
            }
        }

        [Test]
        public void HasProperties()
        {
            var transaction = CreateTestTransaction();
            transaction.Save();

            var transactionEvent = transaction.Events.FindAll(new Event()).First();
            Assert.IsTrue(transactionEvent.DateCreated.HasValue);
            Assert.IsNotEmpty(transactionEvent.Model);
            Assert.IsNotEmpty(transactionEvent.ModelId);
            Assert.IsNotEmpty(transactionEvent.Id);
            Assert.IsNotEmpty(transactionEvent.Name);
            Assert.IsNotEmpty((string) transactionEvent.Payload["current_status"]);
            Assert.IsNotEmpty((string) transactionEvent.Payload["old_status"]);
            Assert.IsNotEmpty((string) transactionEvent.Payload["desired_status"]);
        }
    }
}
