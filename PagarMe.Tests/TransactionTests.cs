using System;
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
            CardHash card = new CardHash()
            {
                CardNumber = "4242424242424242",
                CardHolderName = "Aardvark Silva",
                CardExpirationDate = "0140",
                CardCvv = "123"
            };

            dTransaction.CardHash = card.Generate();
            dTransaction.acquirers_configuration_id = "ac_cj7nrwwjb057txv6et3k5fd8c";
            dTransaction.capture_method = "emv";
            dTransaction.card_track_2 = "4242424242424242%3D51046070000091611111";
            dTransaction.card_emv_data = "9A031708119C01009F02060000000001009F10200FA501A030F8000000000000000000000F0000000000000000000000000000009F1A0200769F1E0830303030303030309F2608DF91B6A4D449C9819F3303E0F0E89F360202889F370411859D5F9F2701809F34034203005F2A0209868202580095056280046000";
            dTransaction.local_time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffffZ");
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
        public void Refund()
        {
            var transaction = CreateTestTransaction();

            transaction.Save();
            transaction.Refund();

            Assert.IsTrue(transaction.Status == TransactionStatus.Refunded);
        }
        [Test]
        public async Task RefundWithSynchronousRequest()
        {
            var transaction = CreateTestCardTransactionWithInstallments();
            transaction.PostbackUrl = "https://api.aardvark.me/1/handlepostback";
            transaction.Async = false;
            await transaction.SaveAsync();

            transaction.Refund(asyncRefund: false);

            Assert.IsTrue(transaction.Status == TransactionStatus.Refunded);
        }

        [Test]
        public async Task RefundWithAsynchronousRequest()
        {
            var transaction = CreateTestCardTransactionWithInstallments();
            transaction.PostbackUrl = "https://api.aardvark.me/1/handlepostback";
            transaction.Async = false;
            await transaction.SaveAsync();

            transaction.Refund();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
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

        [Test]
        public void TransactionWithSplitRule()
        {
            var transaction = CreateTestTransaction();
            Recipient r1 = CreateRecipient();
            Recipient r2 = CreateRecipient();
            r1.Save();
            r2.Save();
            transaction.SplitRules = new SplitRule[] {
                 new SplitRule() {
                        Liable = true,
                        Percentage = 10,
                        ChargeProcessingFee = true,
                        RecipientId = r1.Id
                },
                new SplitRule(){
                        RecipientId = r2.Id,
                        ChargeProcessingFee = true,
                        Liable = true,
                        Percentage = 90
                    }
            };
            transaction.Save();
            Assert.True(transaction.Status == TransactionStatus.Paid);
            Assert.AreEqual(transaction.Amount, 1099);
            Assert.AreEqual(transaction.SplitRules[1].RecipientId, r1.Id);
            Assert.AreEqual(transaction.SplitRules[1].Percentage ,10);
            Assert.True(transaction.SplitRules[1].ChargeProcessingFee);
            Assert.True(transaction.SplitRules[1].Liable);
            Assert.AreEqual(transaction.SplitRules[0].RecipientId , r2.Id);
            Assert.AreEqual(transaction.SplitRules[0].Percentage, 90);
            Assert.True(transaction.SplitRules[0].ChargeProcessingFee);
            Assert.True(transaction.SplitRules[0].Liable);

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
            Assert.IsNotEmpty((string)transactionEvent.Payload["current_status"]);
            Assert.IsNotEmpty((string)transactionEvent.Payload["old_status"]);
            Assert.IsNotEmpty((string)transactionEvent.Payload["desired_status"]);
        }

        [Test]
        public void RefuseReasonAcquirer()
        {
            var transaction = new Transaction();
            transaction.Amount = 10000;
            CardHash card = new CardHash()
            {
                CardNumber = "4242424242424242",
                CardHolderName = "Aardvark Silva",
                CardExpirationDate = "0140",
                CardCvv = "614"
            };
            transaction.CardHash = card.Generate();
            transaction.Customer = new Customer()
            {
                Name = "John Appleseed",
                Email = "jhon@pagar.me",
                DocumentNumber = "11111111111"
            };
            transaction.Save();
            Assert.AreEqual(transaction.RefuseReason, "acquirer");
        }

        [Test]
        public void RefuseReasonNull()
        {
            var transaction = new Transaction();
            transaction.Amount = 10000;
            CardHash card = new CardHash()
            {
                CardNumber = "4242424242424242",
                CardHolderName = "Aardvark Silva",
                CardExpirationDate = "0140",
                CardCvv = "123"
            };
            transaction.CardHash = card.Generate();
            transaction.Customer = new Customer()
            {
                Name = "John Appleseed",
                Email = "jhon@pagar.me",
                DocumentNumber = "11111111111"
            };
            transaction.Save();
            Assert.Null(transaction.RefuseReason);
        }

        [Test]
        public void RefundWithSplit()
        {
            var transaction = CreateTestTransaction();
            Recipient r1 = CreateRecipient();
            Recipient r2 = CreateRecipient();
            r1.Save();
            r2.Save();
            transaction.SplitRules = new SplitRule[]{
                new SplitRule(){
                    Liable = true,
                    Percentage = 50,
                    ChargeProcessingFee = true,
                    RecipientId = r1.Id
                },
                new SplitRule(){
                    Liable = true,
                    Percentage = 50,
                    ChargeProcessingFee = true,
                    RecipientId = r2.Id
                }
            };
            transaction.Save();
            var splitRules = new SplitRule[]{
                new SplitRule(){
                    Id = transaction.SplitRules[0].Id,
                    Amount = 200,
                    RecipientId = transaction.SplitRules[0].RecipientId,
                    ChargeProcessingFee = true
                },
                new SplitRule(){
                    Id = transaction.SplitRules[1].Id,
                    Amount = 200,
                    RecipientId = transaction.SplitRules[1].RecipientId,
                    ChargeProcessingFee = true
                }
            };
            transaction.RefundWithSplit(400,splitRules);
            List<Payable> payables = transaction.Payables.FindAll(new Payable()).ToList();
            Assert.AreEqual(payables.ElementAt(0).Amount,-200);
            Assert.AreEqual(payables.ElementAt(1).Amount, -200);
        }

        [Test]
        public void RefundWithSplitBoleto(){
            var transaction = CreateTestBoletoTransaction();
            Recipient r1 = CreateRecipient();
            Recipient r2 = CreateRecipient();
            r1.Save();
            r2.Save();
            transaction.SplitRules = new SplitRule[]{
                new SplitRule(){
                    Liable = true,
                    Percentage = 50,
                    ChargeProcessingFee = true,
                    RecipientId = r1.Id
                },
                new SplitRule(){
                    Liable = true,
                    Percentage = 50,
                    ChargeProcessingFee = true,
                    RecipientId = r2.Id
                }
            };
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();
            var splitRules = new SplitRule[]{
                new SplitRule(){
                    Id = transaction.SplitRules[0].Id,
                    Amount = 200,
                    RecipientId = transaction.SplitRules[0].RecipientId,
                    ChargeProcessingFee = true
                },
                new SplitRule(){
                    Id = transaction.SplitRules[1].Id,
                    Amount = 200,
                    RecipientId = transaction.SplitRules[1].RecipientId,
                    ChargeProcessingFee = true
                }
            };
            BankAccount ba = CreateTestBankAccount();
            ba.Save();
            transaction.RefundWithSplit(400, splitRules,ba);
            List<Payable> payables = transaction.Payables.FindAll(new Payable()).ToList();
            Assert.AreEqual(payables.ElementAt(0).Amount, -200);
            Assert.AreEqual(payables.ElementAt(1).Amount, -200);
            Assert.AreEqual(transaction.Status,TransactionStatus.PendingRefund);
        }
    }
}
