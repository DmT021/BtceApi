using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
namespace BtcE {
	internal static class Helper
	{
		private static Lazy<CultureInfo> _InvariantCulture = new Lazy<CultureInfo>(() => CultureInfo.InvariantCulture);
		internal static CultureInfo InvariantCulture { get { return _InvariantCulture.Value; } }
		internal static string ByteArrayToString( byte[] ba ) { return BitConverter.ToString( ba ).Replace( "-", "" ); }
		internal static string BuildPostData( Dictionary<string, string> d ) {
			var s = new StringBuilder();
			foreach ( var item in d ) s.AppendFormat( "{0}={1}&", item.Key, HttpUtility.UrlEncode( item.Value ) );
			if ( s.Length > 0 ) s.Remove( s.Length - 1, 1 );
			return s.ToString();
		}
		internal static string DecimalToString( decimal d ) { return d.ToString( CultureInfo.InvariantCulture ); }
		internal static HttpWebRequest PrepareRequest( string url ) {
			var request = WebRequest.CreateHttp( url );
			request.Proxy = WebRequest.DefaultWebProxy;
			request.Proxy.Credentials = CredentialCache.DefaultCredentials;
			if ( request == null ) throw new Exception( "Non HTTP WebRequest" );
			return request;
		}

        internal static T SyncTask<T>( Task<T> task ) {
            VSyncTask( task );
            return task.Result;
        }
        internal static void VSyncTask( Task task ) {
            //not started
            if ( task.Status == TaskStatus.Created )
                task.Start();
            //not finished
            if ( task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted && task.Status != TaskStatus.RanToCompletion )
                task.Wait();
        }
	}
}