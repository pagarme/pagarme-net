using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    class TransferTest : PagarMeTestFixture
    {
        [Test]
        [ExpectedException(typeof(PagarMeException))]
        public void CreateTestTransferWithDifferentBankAccount()
        {
            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipientWithAnotherBankAccount();
            recipient.Save();
            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();
        }

        [Test]
        public void CreateTransferTest()
        {

            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipient(bank);
            recipient.Save();

            Transaction transaction = PagarMeTestFixture.CreateBoletoSplitRuleTransaction(recipient);
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();
            Assert.IsTrue(transfer.Status == TransferStatus.PendingTransfer);

        }

        [Test]
        public void FindTransferTest()
        {
            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipient(bank);
            recipient.Save();

            Transaction transaction = PagarMeTestFixture.CreateBoletoSplitRuleTransaction(recipient);
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();

            Transfer transferReturned = PagarMeService.GetDefaultService().Transfers.Find(transfer.Id);

            Assert.IsTrue(transferReturned.Id.Equals(transfer.Id));
            Assert.IsTrue(transferReturned.Amount.Equals(transfer.Amount));
            Assert.IsTrue(transferReturned.DateCreated.Equals(transfer.DateCreated));
            Assert.IsTrue(transferReturned.Fee.Equals(transfer.Fee));
            Assert.IsTrue(transferReturned.Status.Equals(transfer.Status));
            Assert.IsTrue(transferReturned.Type.Equals(transfer.Type));
        }

        [Test]
        public void FindAllTransferTest()
        {
            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipient(bank);
            recipient.Save();

            Transaction transaction = PagarMeTestFixture.CreateBoletoSplitRuleTransaction(recipient);
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();

            var transfers = PagarMeService.GetDefaultService().Transfers.FindAll(new Transfer());

            Assert.GreaterOrEqual(transfers.Count(), 1);

        }

        [Test]
        public void CancelTransfer()
        {
            BankAccount bank = PagarMeTestFixture.CreateTestBankAccount();
            bank.Save();
            Recipient recipient = PagarMeTestFixture.CreateRecipient(bank);
            recipient.Save();

            Transaction transaction = PagarMeTestFixture.CreateBoletoSplitRuleTransaction(recipient);
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Transfer transfer = PagarMeTestFixture.CreateTestTransfer(bank.Id, recipient.Id);
            transfer.Save();

            transfer.CancelTransfer();

            Assert.IsTrue(transfer.Status == TransferStatus.Canceled);

        }

    }
}
