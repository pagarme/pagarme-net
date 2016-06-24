//
// AbstractModelCollection.cs
//
// Author:
//       Rodrigo Amaral <rodrigo.amaral@pagar.me>
//
// Copyright (c) 2016 Rodrigo Amaral
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace PagarMe.Base
{
	public class AbstractModelCollection<TAbstractModel> where TAbstractModel : AbstractModel 
	{

		private PagarMeService _service ;
		private string _endpoint ;
		private string _parentEndpoint ;

		internal AbstractModelCollection (string endpoint, string parentEndpoint)
			: this(null, endpoint, parentEndpoint)
		{

		}

		internal AbstractModelCollection(PagarMeService service, string endpoint, string parentEndpoint)
		{
			if (service == null)
				service = PagarMeService.GetDefaultService();

			_service = service;
			_endpoint = endpoint;
			_parentEndpoint = parentEndpoint;
		}


		public TAbstractModel Find( int idParentModel, int idAbstractModel)
		{
			return Find(idParentModel.ToString(), idAbstractModel.ToString());
		}


		public TAbstractModel Find(string idParentModel, string idAbstractModel = "")
		{
			return FindAll(idParentModel, idAbstractModel).FirstOrDefault();
		}

		public IEnumerable<TAbstractModel> FindAll(string idParentModel, string idAbstractModel = "")
		{
			return FinishFindQuery(BuildFindQuery(idParentModel, idAbstractModel).Execute());
		}


		public PagarMeRequest BuildFindQuery(string idParentModel, string idAbstractModel = "")
		{
			var request = new PagarMeRequest(_service, "GET", string.Concat(_parentEndpoint,"/",idParentModel,
				_endpoint,
				"/",
				idAbstractModel)
			);

			return request;
		}

		public IEnumerable<TAbstractModel> FinishFindQuery(PagarMeResponse response)
		{
			return JArray.Parse(response.Body).Select((x) =>
				{
					var abstractModel = (TAbstractModel)Activator.CreateInstance(
						typeof(TAbstractModel), new object[]{_service});

					abstractModel.LoadFrom((JObject)x);

					return abstractModel;

				});
		} 



	}
}