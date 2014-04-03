using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net;
using System.IO;
using LitJson;
using Hive5;
using Hive5.Core;
using Hive5.Model;
using Hive5.Util;

namespace Hive5
{

	/// <summary>
	/// Hive5 user data.
	/// </summary>
	public class Hive5UserData : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Set the specified key, value and callback.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		/// <param name="callback">Callback.</param>
		public void set(string key, string value, string command, CallBack callback)
		{
			Debug.Log ("set LoginState=" + hive5.LoginState);

			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.userData);

			var requestBody = new SetUserDataRequest ();
			var data = new List<KeyValueCommand> ();
			var userData = new KeyValueCommand () { key = key, value = value, command = command };
			data.Add (userData);
			requestBody.condition = new List<Condition> ();
			requestBody.data = data;

			hive5.setDebug ();
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);

		}

		/// <summary>
		/// Get the specified dataKeys and callback.
		/// </summary>
		/// <param name="dataKeys">Data keys.</param>
		/// <param name="callback">Callback.</param>
		public void get(string[] dataKeys, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.userData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( dataKeys, key => { parameters.Add( ParameterKey.key, key ); } );
			
			hive5.asyncRoutine ( 
				hive5.getHTTP (url, parameters.data, GetUserDataResponseBody.Load, callback) 
			);
		}


		/// <summary>
		/// Sets the batch.
		/// </summary>
		/// <param name="userData">User data.</param>
		/// <param name="callback">Callback.</param>
		/// 
		/// 
		/*
		public void setBatch(Dictionary<string, string> userData, Hive5Core.apiCallBack callback)
		{
			Debug.Log ("LoginState=" + hive5.LoginState);
			
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.userData);

			var dataList = new List<UserData>();
			foreach (KeyValuePair<string, string> rowData in userData) 
			{
				dataList.Add (new UserData(rowData.Key, rowData.Value));
			}

			Array data = dataList.ToArray ();

			var requestBody = new {
				data = dataList;
			};
			
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, callback)
			);
			
		}
		*/

	}

}
