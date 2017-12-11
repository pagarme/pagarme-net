using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    public class TransactionTestsV3 : PagarMeTestFixtureV3
    {
        [Test]
        public void CreateV3CardTransaction()
        {
            var transaction = CreateTestV3CardTransaction();

            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.Paid);
        }


        [Test]
        public void CreateV3BoletoTransaction()
        {
            var transaction = CreateTestV3BoletoTransaction();

            transaction.Save();

            Assert.IsTrue(transaction.Status == TransactionStatus.WaitingPayment);
        }
    }

}
