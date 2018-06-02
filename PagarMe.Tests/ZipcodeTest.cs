using System;
using NUnit.Framework;
namespace PagarMe.Tests
{
    [TestFixture]
    public class ZipcodeTests : PagarMeTestFixture{
        [Test]
        public void ZipcodeTest()
        {
            Zipcode zipcode = new Zipcode();
            zipcode.CheckZipcode("04250000");
            Assert.AreEqual("São Paulo",zipcode.City);
            Assert.AreEqual("SP",zipcode.State);
            Assert.AreEqual("Rua Budapeste",zipcode.Street);
            Assert.AreEqual("Vila Marte", zipcode.Neighborhood);
        } 
    }
      

}
