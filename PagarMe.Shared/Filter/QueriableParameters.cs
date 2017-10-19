using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PagarMe.Filter
{
	public abstract class QueriableParameters : List<Tuple<string,string>>
	{
		public void LessOrEquals(string Parameter, string Value)
		{
			Add (new Tuple<string, string>(Parameter, "<=" + Value));
		}
		public void LessThan(string Parameter, string Value)
		{
			Add (new Tuple<string, string> (Parameter, "<" + Value));
		}
		public void HigherOrEquals(string Parameter, string Value)
		{
			Add (new Tuple<string, string> (Parameter, ">=" + Value));
		}
		public void HigherThan(string Parameter, string Value)
		{
			Add (new Tuple<string, string> (Parameter, ">" + Value));
		}
		public void Equals(string Parameter, string Value)
		{
			Add (new Tuple<string, string> (Parameter, Value));
		}
		public void NotEquals(string Parameter, string Value)
		{
			Add (new Tuple<string, string> (Parameter, Value));
		}
	}
}

