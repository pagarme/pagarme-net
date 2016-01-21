using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    public class SubscriptionTests : PagarMeTestFixture
    {
        [Test]
        public void CreateWithPlan()
        {
            var plan = CreateTestPlan();

            plan.Save();

            Assert.AreNotEqual(plan.Id, 0);

            var subscription = new Subscription
            {
                CardHash = GetCardHash(),
                Customer = new Customer
                {
                    Email = "josedasilva@pagar.me"
                },
                Plan = plan
            };

            subscription.Save();

            Assert.AreEqual(subscription.Status, SubscriptionStatus.Paid);
        }

        [Test]
        public void CancelSubscription()
        {
            var plan = CreateTestPlan();
            plan.Save();

            Assert.AreNotEqual(plan.Id, 0);

            var subscription = new Subscription
            {
                CardHash = GetCardHash(),
                Customer = new Customer
                {
                    Email = "josedasilva@pagar.me"
                },
                Plan = plan
            };

            subscription.Save();
            subscription.Cancel();

            Assert.AreEqual(subscription.Status, SubscriptionStatus.Canceled);
        }
    }
}
