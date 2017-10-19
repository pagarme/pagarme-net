using System;
using PagarMe;

namespace PagarMe.Filter
{
	public class BalanceOperationQueriableParameters : QueriableParameters
	{
		public void BeforeThan(DateTime Date)
		{
			Equals("end_date", Utils.ConvertToUnixTimeStamp(Date).ToString());
		}
		public void After(DateTime Date)
		{
			Equals ("start_date", Utils.ConvertToUnixTimeStamp (Date).ToString ());
		}
	}
}

