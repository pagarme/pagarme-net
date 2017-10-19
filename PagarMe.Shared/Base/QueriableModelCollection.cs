using System;
using System.Collections.Generic;
using System.Linq;
using PagarMe.Filter;

namespace PagarMe.Base
{
	public class QueriableModelCollection<TModel, TQueriableParameters> : ModelCollection<TModel>
		where TModel : Model
		where TQueriableParameters : QueriableParameters
	{
		internal QueriableModelCollection(PagarMeService service, string endpoint, string endpointPrefix = "") : base(service, endpoint, endpointPrefix)
		{
		}
		public IEnumerable<TModel> FindAll (TQueriableParameters searchParams)
		{
			return FinishFindQuery (BuildFindQuery (searchParams).Execute ());
		}

		public PagarMeRequest BuildFindQuery (TQueriableParameters queriableFields)
		{
			var request = new PagarMeRequest (_service, "GET", _endpointPrefix + _endpoint);
			request.Query.AddRange (queriableFields);
			return request;
		}
	}
}
