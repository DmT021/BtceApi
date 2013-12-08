// -----------------------------------------------------------------------
// <copyright file="WebApi.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE
{
  using System.IO;
  using System.Net;
  using System.Web;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  internal static class WebApi
  {
    public static string Query(string url)
    {
      var request = WebRequest.Create(url);
      request.Proxy = WebRequest.DefaultWebProxy;
      request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
      if (request == null)
        throw new HttpException("Non HTTP WebRequest");
      return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
    }
  }
}