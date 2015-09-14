using Hive5.Models;
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
    public enum HttpVerbs
    {
        GET = 0,
        POST = 1, 
        PUT = 2, 
        DELETE = 3
    }

#if UNITTEST
    public class Hive5Http : MockMonoSingleton<Hive5Http> {
#else
	public class Hive5Http : MonoSingleton<Hive5Http> {
#endif
        public string BuildResponseWith(Hive5ErrorCode code)
        {
            return string.Format("{{\"result_code\":{0}}}", (int)code);
        }
        private void RaiseClientError(Hive5ErrorCode resultCode, Hive5Response.dataLoader loader, Callback callback)
        {
            callback(Hive5Response.Load(loader, BuildResponseWith(resultCode)));
        }


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
            string queryString = GetQueryString(parameters);
            var rid = new Rid(url, queryString, "");

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ErrorCode.DuplicatedApiCall, loader, callback);
                return;
            }

            // Hive5 API Header 설정
            var headers = new WebHeaderCollection();
            headers.Add(HeaderKey.AppKey, Hive5Client.AppKey);
            headers.Add(HeaderKey.Uuid, Hive5Client.Uuid);
            headers.Add(HeaderKey.Token, Hive5Client.Auth.AccessToken);
            headers.Add(HeaderKey.SessionKey, Hive5Client.Auth.SessionKey == null ? string.Empty : Hive5Client.Auth.SessionKey);
            headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            headers.Add(HeaderKey.RequestId, rid.RequestId);

            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }
            if (Hive5Client.IsDebugMode) Logger.Log("WebClient reuqest URL = " + newUrl);

            WebClient wc = new WebClient() 
            {
                Encoding = Encoding.UTF8,
                Headers = headers,
            };
            wc.DownloadStringCompleted += (s, e) =>
                {
                    var requestId = e.UserState != null ? e.UserState.ToString() : "";
                    ApiRequestManager.Instance.RemoveByRequestId(requestId);

                    if (Hive5Client.IsDebugMode) Logger.Log("WebClient response = " + e.Result);
                    
                    callback(Hive5Response.Load(loader, e.Result));
                };

            wc.DownloadStringAsync(new Uri(newUrl, UriKind.RelativeOrAbsolute), rid.RequestId);
        }

       

        private void PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback, HttpVerbs verb = HttpVerbs.POST)
        {
            // Hive5 API json body 변환
            string jsonString = ObjectToJson(requestBody);
            var rid = new Rid(url, "", jsonString);

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ErrorCode.DuplicatedApiCall, loader, callback);
                return;
            }

            // Hive5 API Header fi설정
            var headers = new WebHeaderCollection();
            headers.Add(HeaderKey.AppKey, Hive5Client.AppKey);
            headers.Add(HeaderKey.Uuid, Hive5Client.Uuid);
            headers.Add(HeaderKey.Token, Hive5Client.Auth.AccessToken);
            headers.Add(HeaderKey.SessionKey, Hive5Client.Auth.SessionKey == null ? string.Empty : Hive5Client.Auth.SessionKey);
            headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.RequestId, rid.RequestId);

            if (string.IsNullOrEmpty(jsonString) == false)
            { 
                headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            }

            if (Hive5Client.IsDebugMode) Logger.Log("www reuqest URL = " + url);
            if (Hive5Client.IsDebugMode) Logger.Log("www request jsonBody= " + jsonString);

            // Hive5 API Request
            WebClient wc = new WebClient() 
            {
                Encoding = Encoding.UTF8,
                Headers = headers,
            };
            wc.UploadDataCompleted+= (s, e) => 
                {
                    var requestId = e.UserState != null ? e.UserState.ToString() : "";
                    ApiRequestManager.Instance.RemoveByRequestId(requestId);

                    string responseText = Encoding.UTF8.GetString(e.Result);
                    if (Hive5Client.IsDebugMode) Logger.Log("www response = " , responseText);

                    callback(Hive5Response.Load(loader, responseText));
                };

                wc.UploadDataAsync(new Uri(url, UriKind.RelativeOrAbsolute), verb.ToString(), Encoding.UTF8.GetBytes(jsonString), rid.RequestId);
        }

         /// <summary>
        /// Https the post.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        private void PostHttp(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, Callback callback, HttpVerbs verb = HttpVerbs.POST)
        {
            string queryString = GetQueryString(parameters);

            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }

            PostHttp(newUrl, requestBody, loader, callback, verb);
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
            string queryString = GetQueryString(parameters);
            var rid = new Rid(url, queryString, "");

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ErrorCode.DuplicatedApiCall, loader, callback);
                yield return null;
            }

            // Hive5 API Header 설정
            var headers = new Dictionary<string, string>();
            headers.Add(HeaderKey.AppKey, Hive5Client.AppKey);
            headers.Add(HeaderKey.Uuid, Hive5Client.Uuid);
			headers.Add (HeaderKey.AcceptEncoding, HeaderValue.Gzip);
			if (string.IsNullOrEmpty (Hive5Client.Auth.AccessToken) == false) {
				headers.Add (HeaderKey.Token, Hive5Client.Auth.AccessToken);
			}
            headers.Add(HeaderKey.SessionKey, Hive5Client.Auth.SessionKey == null ? string.Empty : Hive5Client.Auth.SessionKey);
            headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            headers.Add(HeaderKey.RequestId, rid.RequestId);
            
            string newUrl = url;

            if (queryString.Length > 0)
            {
                newUrl = url + "?" + queryString;
            }


			WWW www = new WWW(newUrl, null, headers);
			yield return www;

            if (Hive5Client.IsDebugMode) Logger.Log("www reuqest URL = " + newUrl);
            if (Hive5Client.IsDebugMode) Logger.Log("www response = " + www.text);

			string httpResponse = www.text;

            var requestId = www.responseHeaders.ContainsKey(HeaderKey.RequestId) == true ? www.responseHeaders[HeaderKey.RequestId] : "";
            ApiRequestManager.Instance.RemoveByRequestId(requestId);

			if (string.IsNullOrEmpty(www.error) == false) {
					httpResponse = GetErrorResponseWithError(www.error);
					Logger.Log(httpResponse);
			}

			callback(Hive5Response.Load(loader, httpResponse));
        }

		string GetErrorResponseWithError (string error)
		{
				string trimmedError = error.Trim();
				int errorCode = (int)Hive5ErrorCode.UnknownError;
				string resultMessage = string.Empty;
				int index = trimmedError.IndexOf(" ");
				if (index != -1)
				{
					string codePart = trimmedError.Substring(0, index);
					int parsedErrorCode = 0;
					if (int.TryParse(codePart, out parsedErrorCode) == true)
					{
						errorCode = parsedErrorCode;
						resultMessage = trimmedError.Substring(index+1);
					}
					else
					{
						errorCode = (int)Hive5ErrorCode.NetworkError;
						resultMessage = trimmedError;
					}
				}
				else
				{
					resultMessage = trimmedError;
				}
				
				string httpResponse = string.Format("{{\"result_code\":{0}, \"result_message\":\"{1}\" }}", errorCode, resultMessage);
				return httpResponse;
		}

        private IEnumerator PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback, HttpVerbs verb = HttpVerbs.POST)
        {
            switch (verb)
            {
                case HttpVerbs.PUT:
                case HttpVerbs.DELETE:
                    {
                        url += url.Contains("?") ? "&" : "?";
                        url += "_method=" + verb.ToString();
                    }
                    break;
            }

            // Hive5 API json body 변환
            string jsonString =ObjectToJson(requestBody);
            var rid = new Rid(url, "", jsonString);

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ErrorCode.DuplicatedApiCall, loader, callback);
                yield return null;
            }

            // Hive5 API Header 설정
            var headers = new Dictionary<string, string>();
            headers.Add(HeaderKey.AppKey, Hive5Client.AppKey);
            headers.Add(HeaderKey.Uuid, Hive5Client.Uuid);
			headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.SessionKey, Hive5Client.Auth.SessionKey == null ? string.Empty : Hive5Client.Auth.SessionKey);
			headers.Add(HeaderKey.AcceptEncoding, HeaderValue.Gzip);
            headers.Add(HeaderKey.RequestId, rid.RequestId);

            if (string.IsNullOrEmpty(Hive5Client.Auth.AccessToken) == false)
            {
                headers.Add(HeaderKey.Token, Hive5Client.Auth.AccessToken);
            }

            if (string.IsNullOrEmpty(jsonString) == false)
            { 
                headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            }

            var encoding = new System.Text.UTF8Encoding();

            // Hive5 API Request
            WWW www = new WWW(url, encoding.GetBytes(jsonString), headers);
            yield return www;

            if (Hive5Client.IsDebugMode) Logger.Log("www reuqest URL = " + url);
            if (Hive5Client.IsDebugMode) Logger.Log("www request jsonBody= " + jsonString);
            if (Hive5Client.IsDebugMode) Logger.Log("www response = " + www.text);

				string httpResponse = www.text;
			
	            var requestId = www.responseHeaders.ContainsKey(HeaderKey.RequestId) == true ? www.responseHeaders[HeaderKey.RequestId] : "";
                ApiRequestManager.Instance.RemoveByRequestId(requestId);

				if (string.IsNullOrEmpty(www.error) == false) {
					httpResponse = GetErrorResponseWithError(www.error);
					Logger.Log(httpResponse);
				}
				
				callback(Hive5Response.Load(loader, httpResponse));
        }

         /// <summary>
        /// Https the post.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="url">URL.</param>
        /// <param name="parameters">Parameters.</param>
        private IEnumerator PostHttp(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, Callback callback, HttpVerbs verb = HttpVerbs.POST)
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


        internal void GetHttpAsync(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, Callback callback)
        {
#if UNITTEST
            GetHttp(url, parameters, loader, callback);
#else
            StartCoroutine(
                GetHttp(url, parameters, loader, callback)
            );
#endif
        }

        internal void PostHttpAsync(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            if (!Hive5Client.IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");

#if UNITTEST
            PostHttp(url, requestBody, loader, callback);
#else
            StartCoroutine(
                PostHttp(url, requestBody, loader, callback)
            );
#endif
        }

        internal void PostHttpAsync(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
             if (!Hive5Client.IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");

#if UNITTEST
            PostHttp(url, parameters, requestBody, loader, callback);
#else
            StartCoroutine(
					PostHttp(url, parameters, requestBody, loader, callback)
            );
#endif
        }

        internal void PutHttpAsync(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            if (!Hive5Client.IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");

#if UNITTEST
            PostHttp(url, requestBody, loader, callback, HttpVerbs.PUT);
#else
            StartCoroutine(
                PostHttp(url, requestBody, loader, callback, HttpVerbs.PUT)
            );
#endif
        }

        internal void DeleteHttpAsync(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            if (!Hive5Client.IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");

#if UNITTEST
            PostHttp(url, requestBody, loader, callback, HttpVerbs.DELETE);
#else
            StartCoroutine(
                PostHttp(url, requestBody, loader, callback, HttpVerbs.DELETE)
            );
#endif
        }

        /// <summary>
		/// Build QueryString
		/// </summary>
		/// <returns>The query string.</returns>
		/// <param name="parameters">Parameters.</param>
		public static string GetQueryString(List<KeyValuePair<string, string>> parameters)
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
				
				sb.Append(parameter.Key + "=" + EscapeData(parameter.Value));
			}

			return sb.ToString ();
		}

        public static string EscapeData(string data)
        {
#if UNITTEST
            return Uri.EscapeDataString(data);
#else
            return WWW.EscapeURL(data);
#endif

        }

        public static string ObjectToJson(object obj)
        {
            if (obj == null )
                return string.Empty;

            string jsonString = obj is string ? (string)obj : JsonMapper.ToJson(obj);
            return jsonString;
        }
    }
}
