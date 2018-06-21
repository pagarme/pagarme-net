using System;
using System.IO;
using System.Net;
using System.Text;
using NUnit.Framework;

namespace PagarMe.Tests
{
	[TestFixture]
	class TLSTests
	{
		private readonly SecurityProtocolType @default = ServicePointManager.SecurityProtocol;

		[TestFixtureSetUp]
		public void ForceOldTls()
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
		}

		[TestFixtureTearDown]
		public void SetDefaultTls()
		{
			ServicePointManager.SecurityProtocol = @default;
		}

		[Test]
		public void TestWithoutTLS12Enforce()
		{
			WebException error = null;
			String response = null;

			try
			{
				response = makeRequest();
			}
			catch (WebException e)
			{
				error = e;
			}

			Assert.IsNull(response);
			Assert.IsNotNull(error);
		}

		[Test]
		public void TestWithTLS12Enforce()
		{
			WebException error = null;
			String response = null;

			try
			{
				response = TLS.Instance.UseTLS12IfAvailable(makeRequest);
			}
			catch (WebException e)
			{
				error = e;
			}

			Assert.IsNull(error);

			var expectedSuccessMessage =
				"Sucesso: sua conexão com a Pagar.me está utilizando o protocolo TLS 1.2.";
			
			Assert.AreEqual(response, expectedSuccessMessage);
		}

		private static String makeRequest()
		{
			WebResponse response;
			var request = getRequest();

			try
			{
				response = request.GetResponse();
			}
			catch (WebException e)
			{
				response = e.Response;

				if (response == null)
					throw;
			}

			var stream = response.GetResponseStream();

			if (stream != null)
			{
				var reader = new StreamReader(stream, Encoding.UTF8);
				return reader.ReadToEnd();
			}

			return null;
		}

		private static HttpWebRequest getRequest()
		{
			var request = WebRequest.CreateHttp("https://tls12.pagar.me");
			request.UserAgent = "pagarme-net/test-tls-12";
			request.Method = "GET";
			return request;
		}

	}
}
