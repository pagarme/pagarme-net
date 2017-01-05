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
        public void FindAllPostbacksTest()
        {
            var transaction = PagarMeTestFixture.CreateTestBoletoTransactionWithPostbackUrl();
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            var postbacks = transaction.Postbacks.FindAll(new Postback());

            foreach (var postback in postbacks)
            {
                Assert.IsTrue(postback.ModelId.Equals(transaction.Id));
            }
        }

        [Test]
        public void FindPostbackTest()
        {
            var transaction = PagarMeTestFixture.CreateTestBoletoTransactionWithPostbackUrl();
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Postback postback = transaction.Postbacks.FindAll(new Postback()).First();
            Postback postbackReturned = transaction.Postbacks.Find(postback.Id);

            Assert.IsTrue(postback.Id.Equals(postbackReturned.Id));
            Assert.IsTrue(postback.Status.Equals(postbackReturned.Status));
            Assert.IsTrue(postback.ModelId.Equals(postbackReturned.ModelId));
        }

        [Test]
        public void RedeliverPostbackTest()
        {
            var transaction = PagarMeTestFixture.CreateTestBoletoTransactionWithPostbackUrl();
            transaction.Save();
            transaction.Status = TransactionStatus.Paid;
            transaction.Save();

            Postback postback = transaction.Postbacks.FindAll(new Postback()).First();
            postback.Redeliver();

            Assert.IsTrue(postback.Status == PostbackStatus.PendingRetry);
        }
    }
}
