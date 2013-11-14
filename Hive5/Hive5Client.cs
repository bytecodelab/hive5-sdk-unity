using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using UnityEngine;
using System.Collections;
using Hive5.model;
using LitJson;

namespace Hive5
{
    public class Hive5Client
    {
        private string host = "https://beta.api.hive5.io";
        private string version = "v3";

        private string appKey = "";
        private string uuid = "";
        private string accessToken = "";

        private const string httpHeaderAppKey = "X-APP-KEY";
        private const string httpHeaderUuid = "X-AUTH-UUID";
        private const string httpHeaderToken = "X-AUTH-TOKEN";

        public Hive5Client(string appKey, string uuid)
        {
            this.appKey = appKey;
            this.uuid = uuid;
        }

        public Hive5Client(string appKey, string uuid, string accessToken)
            : this(appKey, uuid)
        {
            this.accessToken = accessToken;
        }

        private Dictionary<string, string> Headers()
        {
            var result = new Dictionary<string, string>();

            result.Add(httpHeaderAppKey, appKey);
            result.Add(httpHeaderUuid, uuid);

            if (accessToken.Length > 0)
                result.Add(httpHeaderToken, accessToken);

            return result;
        }
        public Dictionary<string, Item> GetItems(string[] keys)
        {
            var path = "items";
            var url = String.Join("/", new String[] { host, version, path });

            var parameters = new Dictionary<string, string>();
            foreach (string k in keys)
            {
                parameters.Add("key", k);
            }

            var responseText = httpGet(url, Headers(), parameters);

            var responseJson = JsonMapper.ToObject(responseText);
            
            return Item.Load(responseJson["items"]);
        }

        private string httpGet(string url, Dictionary<string, string> headers, Dictionary<string, string> parameters)
        {
            string queryString = "";

            // WWW의 버그로 get일때 http header 세팅이 안됨
            foreach (KeyValuePair<string, string> x in headers)
            {
                if (queryString.Length > 0)
                    queryString += "&";

                queryString += x.Key + "=" + x.Value;
            }

            foreach (KeyValuePair<string, string> x in parameters)
            {
                if (queryString.Length > 0)
                    queryString += "&";

                queryString += x.Key + "=" + x.Value;
            }

            string newUrl = url;

            if (queryString.Length > 0)
                newUrl = url + "?" + queryString;

#if VS_UNIT_TEST
            var request = (HttpWebRequest)WebRequest.Create(newUrl);
            request.Method = "GET";
            //var requestStream = request.GetRequestStream();
            //var queryStringBytes = UTF8Encoding.UTF8.GetBytes(queryString);
            //requestStream.Write(queryStringBytes, 0, queryStringBytes.Length);
            //requestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.Default);

            return streamReader.ReadToEnd();
#else
            WWW www = new WWW(newUrl);

            return www.text;
#endif

        }




    }
}
