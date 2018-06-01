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
		public void CreateWithPlan ()
		{
			var plan = CreateTestPlan ();

			plan.Save ();

			Assert.AreNotEqual (plan.Id, 0);

			var subscription = new Subscription {
				CardHash = GetCardHash (),
				Customer = new Customer {
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			subscription.Save ();

			Assert.AreEqual (subscription.Status, SubscriptionStatus.Paid);
		}

		[Test]
		public void CreateWithPlanAndInvalidCardCvv ()
		{
			var plan = CreateTestPlan ();

			plan.Save ();

			Assert.AreNotEqual (plan.Id, 0);

			var subscription = new Subscription {
				CardCvv = "651",
				CardExpirationDate = "0921",
				CardHolderName = "Jose da Silva",
				CardNumber = "4242424242424242",
				Customer = new Customer {
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			try {
				subscription.Save ();
			} catch (PagarMeException ex) {
				Assert.IsNotNull (ex.Error.Errors.Where (e => e.Type == "action_forbidden").FirstOrDefault ());	
			}
		}

		[Test]
		public void CancelSubscription ()
		{
			var plan = CreateTestPlan ();
			plan.Save ();

			Assert.AreNotEqual (plan.Id, 0);

			var subscription = new Subscription {
				CardHash = GetCardHash (),
				Customer = new Customer {
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			subscription.Save ();
			subscription.Cancel ();

			Assert.AreEqual (subscription.Status, SubscriptionStatus.Canceled);
		}
        [Test]
        public void SettleChargeWithoutParameters()
        {
            var plan = CreateBoletoTestPlan();
            plan.Save();

            Assert.AreNotEqual(plan.Id, 0);

            var subscription = new Subscription
            {
                Customer = new Customer
                {
                    Email = "josedasilva@pagar.me"
                },
                Plan = plan
            };

            subscription.Save();
            subscription.settleCharges();

            Assert.AreEqual(subscription.Status, SubscriptionStatus.Paid);
        } 
	}

}
