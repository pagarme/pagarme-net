using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PagarMe.Model;
using PagarMe.Enumeration;

namespace PagarMe.Tests
{
    [TestFixture]
    class PostbackTest
    {
        [Test]
        public async Task FindAllPostbacksTest()
        {
            var transaction = PagarMeTestFixture.CreateTestBoletoTransactionWithPostbackUrl();
            await transaction.SaveAsync();
            transaction.Status = TransactionStatus.Paid;
            await transaction.SaveAsync();

            var postbacks = transaction.Postbacks.FindAll(new Postback());

            foreach (var postback in postbacks)
            {
                Assert.IsTrue(postback.ModelId.Equals(transaction.Id));
            }
        }

        [Test]
        public async Task FindPostbackTest()
        {
            var transaction = PagarMeTestFixture.CreateTestBoletoTransactionWithPostbackUrl();
            await transaction.SaveAsync();
            transaction.Status = TransactionStatus.Paid;
            await transaction.SaveAsync();

            Postback postback = transaction.Postbacks.FindAll(new Postback()).First();
            Postback postbackReturned = transaction.Postbacks.Find(postback.Id);

            Assert.IsTrue(postback.Id.Equals(postbackReturned.Id));
            Assert.IsTrue(postback.Status.Equals(postbackReturned.Status));
            Assert.IsTrue(postback.ModelId.Equals(postbackReturned.ModelId));
        }

        [Test]
        public async Task RedeliverPostbackTest()
        {
            var transaction = PagarMeTestFixture.CreateTestBoletoTransactionWithPostbackUrl();
            await transaction.SaveAsync();
            transaction.Status = TransactionStatus.Paid;
            await transaction.SaveAsync();

            Postback postback = transaction.Postbacks.FindAll(new Postback()).First();
            postback.Redeliver();

            Assert.IsTrue(postback.Status == PostbackStatus.PendingRetry);
        }
    }
}
