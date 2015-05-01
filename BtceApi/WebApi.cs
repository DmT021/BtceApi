// -----------------------------------------------------------------------
// <copyright file="WebApi.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Net;
using System.Web;

namespace BtcE
{
    /// <summary>
    ///     TODO: Update summary.
    /// </summary>
    internal static class WebApi
    {
        public static string Query(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Proxy = WebRequest.DefaultWebProxy;
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;
            if (request == null)
                throw new HttpException("Non HTTP WebRequest");
            return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
        }
    }
}