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
		public void CreateWithPlanAndInvalidCardCvv()
		{
			var plan = CreateTestPlan();

			plan.Save();

			Assert.AreNotEqual(plan.Id, 0);

			var subscription = new Subscription
			{
				CardCvv = "651",
				CardExpirationDate = "0921",
				CardHolderName = "Jose da Silva",
				CardNumber = "4242424242424242",
				Customer = new Customer
				{
					Email = "josedasilva@pagar.me"
				},
				Plan = plan
			};

			try
			{
				subscription.Save();
			}
			catch (PagarMeException ex)
			{
				Assert.IsNotNull(ex.Error.Errors.Where(e => e.Type == "action_forbidden").FirstOrDefault());
			}
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

		[Test]
		public void CreateSubscriptionWithSplitRules()
		{
			var plan = CreateTestPlan();
			plan.Save();

			var subscription = new Subscription
			{
				PaymentMethod = PaymentMethod.CreditCard,
				CardHash = GetCardHash(),
				Customer = new Customer()
				{
					Name = "Customer de teste",
					Email = "split@teste.com",
					DocumentNumber = "17583142903",
					Address = new Address()
					{
						Street = "Avenida Brigadeiro Faria Lima",
						StreetNumber = "123",
						Neighborhood = "Jardim Paulistano",
						Zipcode = "04250000"
					},

					Phone = new Phone()
					{
						Ddi = "55",
						Ddd = "11",
						Number = "23456789"
					}
				},
				Plan = plan
			};

			Recipient recipient1 = CreateRecipient();
			recipient1.Save();
			Recipient recipient2 = CreateRecipient();
			recipient2.Save();

			subscription.SplitRules = new[]
			{
				new SplitRule {
					Recipient = recipient1,
					Amount = 660,
					ChargeProcessingFee = true,
					Liable = true
				},
				new SplitRule {
					Recipient = recipient2,
					Amount = 439,
					ChargeProcessingFee = false,
					Liable = false
				}
			};

			subscription.Save();

			Assert.AreEqual(subscription.Status, SubscriptionStatus.Paid);

			List<Payable> subscriptionPayables = subscription.CurrentTransaction.Payables.FindAll(new Payable()).ToList();
			List<Payable> firstRecipientPayable = subscriptionPayables.Where(x => x.RecipientId == recipient1.Id).ToList();
			List<Payable> secondRecipientPayable = subscriptionPayables.Where(x => x.RecipientId == recipient2.Id).ToList();

			Assert.AreEqual(firstRecipientPayable.Count, 1);
			Assert.AreEqual(firstRecipientPayable.ElementAt(0).RecipientId, recipient1.Id);
			Assert.AreEqual(firstRecipientPayable.ElementAt(0).Amount, 660);
			Assert.AreEqual(secondRecipientPayable.Count, 1);
			Assert.AreEqual(secondRecipientPayable.ElementAt(0).RecipientId, recipient2.Id);
			Assert.AreEqual(secondRecipientPayable.ElementAt(0).Amount, 439);
		}
	}
}
