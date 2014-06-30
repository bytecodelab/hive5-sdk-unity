using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace Hive5
{
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif

#if UNITTEST
         /// <summary>
        /// Https the get.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="callback">Callback.</param>
        private void GetHttp(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, Callback callback)
        {
            // Hive5 API Header 설정
            var headers = new WebHeaderCollection();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
            headers.Add(HeaderKey.Token, this.AccessToken);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

            string queryString = GetQueryString(parameters);
            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }
            if (this.isDebug) Logger.Log("WebClient reuqest URL = " + newUrl);

            WebClient wc = new WebClient() 
            {
                Encoding = Encoding.UTF8,
                Headers = headers,
            };
            wc.DownloadStringCompleted += (s, e) =>
                {
                    if (this.isDebug) Logger.Log("WebClient response = " + e.Result);

                    callback(Hive5Response.Load(loader, e.Result));
                };

            wc.DownloadStringAsync(new Uri(newUrl, UriKind.RelativeOrAbsolute));
        }

        private void PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            // Hive5 API Header 설정
            var headers = new WebHeaderCollection();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
            headers.Add(HeaderKey.Token, this.AccessToken);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

            // Hive5 API json body 변환
            string jsonString = JsonMapper.ToJson(requestBody);

            if (this.isDebug) Logger.Log("www reuqest URL = " + url);
            if (this.isDebug) Logger.Log("www request jsonBody= " + jsonString);

            // Hive5 API Request
            WebClient wc = new WebClient() 
            {
                Encoding = Encoding.UTF8,
                Headers = headers,
            };
            wc.UploadDataCompleted+= (s, e) => 
                {
                    string responseText = Encoding.UTF8.GetString(e.Result);
                    if (this.isDebug) Logger.Log("www response = " , responseText);

                    callback(Hive5Response.Load(loader, responseText));
                };

            wc.UploadDataAsync(new Uri(url, UriKind.RelativeOrAbsolute), Encoding.UTF8.GetBytes(jsonString));
        }

         /// <summary>
        /// Https the post.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        private void PostHttp(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            string queryString = GetQueryString(parameters);

            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }

            PostHttp(newUrl, requestBody, loader, callback);
        }
#else
         /// <summary>
        /// Https the get.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="callback">Callback.</param>
        private IEnumerator GetHttp(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, Callback callback)
        {
            // Hive5 API Header 설정
            var headers = new Hashtable();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
            headers.Add(HeaderKey.Token, this.AccessToken);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

            string queryString = GetQueryString(parameters);
            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }

            WWW www = new WWW(newUrl, null, headers);
            yield return www;

            if (this.isDebug) Logger.Log("www reuqest URL = " + newUrl);
            if (this.isDebug) Logger.Log("www response = " + www.text);

            callback(Hive5Response.Load(loader, www.text));
        }

        private IEnumerator PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            // Hive5 API Header 설정
            var headers = new Hashtable();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
            headers.Add(HeaderKey.Token, this.AccessToken);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

            // Hive5 API json body 변환
            string jsonString = JsonMapper.ToJson(requestBody);

            var encoding = new System.Text.UTF8Encoding();

            // Hive5 API Request
            WWW www = new WWW(url, encoding.GetBytes(jsonString), headers);
            yield return www;

            if (this.isDebug) Logger.Log("www reuqest URL = " + url);
            if (this.isDebug) Logger.Log("www request jsonBody= " + jsonString);
            if (this.isDebug) Logger.Log("www response = " + www.text);

            callback(Hive5Response.Load(loader, www.text));
        }

         /// <summary>
        /// Https the post.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        private IEnumerator PostHttp(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            string queryString = GetQueryString(parameters);

            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }

            return PostHttp(newUrl, requestBody, loader, callback);
        }
#endif

        	
        private void GetHttpAsync(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, Callback callback)
        {
#if UNITTEST
            GetHttp(url, parameters, loader, callback);
#else
            StartCoroutine(
                GetHttp(url, parameters, loader, callback)
            );
#endif
        }

        private void PostHttpAsync(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {

#if UNITTEST
            PostHttp(url, requestBody, loader, callback);
#else
            StartCoroutine(
                PostHttp(url, requestBody, loader, callback)
            );
#endif
        }

        private void PostHttpAsync(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {

#if UNITTEST
            PostHttp(url, parameters, requestBody, loader, callback);
#else
            StartCoroutine(
                PostHttp(url, requestBody, loader, callback)
            );
#endif
        }


        		/// <summary>
		/// Build QueryString
		/// </summary>
		/// <returns>The query string.</returns>
		/// <param name="parameters">Parameters.</param>
		private string GetQueryString(List<KeyValuePair<string, string>> parameters)
		{
			if (parameters == null)
			    return string.Empty;

			// Using StringBuilder is faster than concating string by + operator repeatly 
			StringBuilder sb = new StringBuilder ();
			foreach (KeyValuePair<string, string> parameter in parameters)
			{
				if (sb.Length > 0)	
				{
					sb.Append("&");
				}
				
				sb.Append(parameter.Key + "=" + WWW.EscapeURL(parameter.Value));
			}

			return sb.ToString ();
		}

    }
}
