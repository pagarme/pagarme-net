using System;
using NUnit.Framework;

namespace PagarMe.Tests
{
    [TestFixture]
    class PlanTest
    {
        [Test]
        public void testCreatePlan()
        {
            Plan plan = PagarMeTestFixture.CreateTestPlan();
            plan.Save();
            Assert.NotNull(plan.Id);
            Assert.AreEqual(plan.TrialDays,0);
            Assert.AreEqual(plan.Days,30);
            Assert.AreEqual(plan.Amount,1099);
            Assert.AreEqual(plan.Color,"#787878");
            Assert.AreEqual(plan.InvoiceReminder,3);
            Assert.AreEqual(plan.PaymentMethods, new PaymentMethod[] { PaymentMethod.CreditCard });
        }

    }
}
