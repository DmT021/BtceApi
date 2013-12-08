// -----------------------------------------------------------------------
// <copyright file="WebApi.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Net;
	using System.IO;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	class WebApi
	{
		public static string Query(string url)
		{
			var request = WebRequest.Create(url);
			request.Proxy = WebRequest.DefaultWebProxy;
			request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
			if (request == null)
				throw new Exception("Non HTTP WebRequest");
			return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
		}
	}
}
