using Assets.Hive5.Model;
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
        public string BuildResponseWith(Hive5ResultCode code)
        {
            return string.Format("{{\"result_code\":{0}}}", (int)code);
        }
        private void RaiseClientError(Hive5ResultCode resultCode, Hive5Response.dataLoader loader, Callback callback)
        {
            callback(Hive5Response.Load(loader, BuildResponseWith(resultCode)));
        }


#if UNITTEST


		string GetErrorResponseWithError (string error)
		{
			throw new NotImplementedException ();
		}



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
                RaiseClientError(Hive5ResultCode.DuplicatedApiCall, loader, callback);
                return;
            }

            // Hive5 API Header 설정
            var headers = new WebHeaderCollection();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
            headers.Add(HeaderKey.Token, this.AccessToken);
            headers.Add(HeaderKey.SessionKey, this.SessionKey);
            headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            headers.Add(HeaderKey.RequestId, rid.RequestId);

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
                    var requestId = e.UserState != null ? e.UserState.ToString() : "";
                    ApiRequestManager.Instance.RemoveByRequestId(requestId);

                    if (this.isDebug) Logger.Log("WebClient response = " + e.Result);
                    
                    callback(Hive5Response.Load(loader, e.Result));
                };

            wc.DownloadStringAsync(new Uri(newUrl, UriKind.RelativeOrAbsolute), rid.RequestId);
        }



        private void PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            // Hive5 API json body 변환
            string jsonString = requestBody == null ? "" : JsonMapper.ToJson(requestBody);
            var rid = new Rid(url, "", jsonString);

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ResultCode.DuplicatedApiCall, loader, callback);
                return;
            }

            // Hive5 API Header fi설정
            var headers = new WebHeaderCollection();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
            headers.Add(HeaderKey.Token, this.AccessToken);
            headers.Add(HeaderKey.SessionKey, this.SessionKey);
            headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.RequestId, rid.RequestId);
           

          

            if (string.IsNullOrEmpty(jsonString) == false)
            { 
                headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            }

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
                    var requestId = e.UserState != null ? e.UserState.ToString() : "";
                    ApiRequestManager.Instance.RemoveByRequestId(requestId);

                    string responseText = Encoding.UTF8.GetString(e.Result);
                    if (this.isDebug) Logger.Log("www response = " , responseText);

                    callback(Hive5Response.Load(loader, responseText));
                };

            //wc.DownloadDataCompleted += (s, e) =>
            //    {
            //         string responseText = Encoding.UTF8.GetString(e.Result);
            //        if (this.isDebug) Logger.Log("www response = " , responseText);

            //        callback(Hive5Response.Load(loader, responseText));
            //    };

            //if (string.IsNullOrEmpty(jsonString) == true)
            //{
            //    wc.DownloadDataAsync(new Uri(url, UriKind.RelativeOrAbsolute));
            //}
            //else
            //{ 
                wc.UploadDataAsync(new Uri(url, UriKind.RelativeOrAbsolute), "POST", Encoding.UTF8.GetBytes(jsonString));
           // }
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
            string queryString = GetQueryString(parameters);
            var rid = new Rid(url, queryString, "");

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ResultCode.DuplicatedApiCall, loader, callback);
                yield return null;
            }

            // Hive5 API Header 설정
            var headers = new Dictionary<string, string>();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
			headers.Add (HeaderKey.AcceptEncoding, HeaderValue.Gzip);
			if (string.IsNullOrEmpty (this.AccessToken) == false) {
				headers.Add (HeaderKey.Token, this.AccessToken);
			}
            headers.Add(HeaderKey.SessionKey, this.SessionKey);
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

            if (this.isDebug) Logger.Log("www reuqest URL = " + newUrl);
            if (this.isDebug) Logger.Log("www response = " + www.text);

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
				int errorCode = (int)Hive5ResultCode.UnknownError;
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
						errorCode = (int)Hive5ResultCode.NetworkError;
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

        private IEnumerator PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, Callback callback)
        {
            // Hive5 API json body 변환
            string jsonString = requestBody == null ? "" : JsonMapper.ToJson(requestBody);
            var rid = new Rid(url, "", jsonString);

            if (ApiRequestManager.Instance.CheckRequestAllowed(rid) == false)
            {
                RaiseClientError(Hive5ResultCode.DuplicatedApiCall, loader, callback);
                yield return null;
            }

            // Hive5 API Header 설정
            var headers = new Dictionary<string, string>();
            headers.Add(HeaderKey.AppKey, this.AppKey);
            headers.Add(HeaderKey.Uuid, this.Uuid);
			headers.Add(HeaderKey.XPlatformKey, Hive5Config.XPlatformKey);
            headers.Add(HeaderKey.SessionKey, this.SessionKey);
			headers.Add(HeaderKey.AcceptEncoding, HeaderValue.Gzip);
            headers.Add(HeaderKey.RequestId, "42000300");

            if (string.IsNullOrEmpty(this.AccessToken) == false)
            {
                headers.Add(HeaderKey.Token, this.AccessToken);
            }

            if (string.IsNullOrEmpty(jsonString) == false)
            { 
                headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
            }

            var encoding = new System.Text.UTF8Encoding();

            // Hive5 API Request
            WWW www = new WWW(url, encoding.GetBytes(jsonString), headers);
            yield return www;

            if (this.isDebug) Logger.Log("www reuqest URL = " + url);
            if (this.isDebug) Logger.Log("www request jsonBody= " + jsonString);
            if (this.isDebug) Logger.Log("www response = " + www.text);

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
            //Rid rid = new 
            //GetIsDuplicatedCall

#if UNITTEST
            PostHttp(url, parameters, requestBody, loader, callback);
#else
            StartCoroutine(
					PostHttp(url, parameters, requestBody, loader, callback)
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
