using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PagarMe.Model;

namespace PagarMe.Tests
{
    [TestFixture]
    public class BankAccountTests : PagarMeTestFixture
    {
        [Test]
        public void CreateCorrenteBankAccount ()
        {
            var bank = CreateTestBankAccount();
            bank.Type = AccountType.Corrente;

            bank.Save ();

            Assert.IsNotNull(bank.Id);
            Assert.IsTrue(bank.Type == AccountType.Corrente);
        }

        [Test]
        public void CreatePoupancaBankAccount()
        {
            var bank = CreateTestBankAccount();
            bank.Type = AccountType.Poupanca;

            bank.Save();

            Assert.IsNotNull(bank.Id);
            Assert.IsTrue(bank.Type == AccountType.Poupanca);
        }

        [Test]
        public void CreateCorrenteConjuntaBankAccount()
        {
            var bank = CreateTestBankAccount();
            bank.Type = AccountType.CorrenteConjunta;

            bank.Save();

            Assert.IsNotNull(bank.Id);
            Assert.IsTrue(bank.Type == AccountType.CorrenteConjunta);
        }

        [Test]
        public void CreatePoupancaConjuntaBankAccount()
        {
            var bank = CreateTestBankAccount();
            bank.Type = AccountType.PoupancaConjunta;

            bank.Save();

            Assert.IsNotNull(bank.Id);
            Assert.IsTrue(bank.Type == AccountType.PoupancaConjunta);
        }
    }
}