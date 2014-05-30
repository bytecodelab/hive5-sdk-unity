using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using Hive5.Util;


namespace Hive5
{
	/// <summary>
	/// Hive5 client.
	/// </summary>
	public partial class Hive5Client : MonoSingleton<Hive5Client> {

		/********************************************************************************
			Objects API Group
		*********************************************************************************/
		
		/// <summary>
		/// Gets the objects.
		/// </summary>
		/// <param name="objectKeys">Object keys.</param>
		/// <param name="callback">Callback.</param>
		public void GetObjects(string[] objectKeys, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.UserData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( objectKeys, key => { parameters.Add( ParameterKey.Key, key ); } );
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, CommonResponseBody.Load, callback) 
			                );
		}
		
		
		/// <summary>
		/// Creates the object.
		/// </summary>
		/// <param name="procedureName">Procedure name.</param>
		/// <param name="objectKeys">Object keys.</param>
		/// <param name="callback">Callback.</param>
		public void CreateObjects(string procedureName, string[] objectKeys,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.CreateObjects,procedureName));
			
			var objects = new List<object>();
			Array.ForEach ( objectKeys, key => { objects.Add( new { @class = key } ); } );
			
			var requestBody = new {
				objects = objects
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, new {}, CommonResponseBody.Load, callback)
				);	
			
		}
		
		/// <summary>
		/// Sets the objects.
		/// </summary>
		/// <param name="procedureName">Procedure name.</param>
		/// <param name="objects">Objects.</param>
		/// <param name="callback">Callback.</param>
		public void SetObjects(string procedureName, List<Hobject> objects,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.SetObjects,procedureName));
			
			var requestBody = new {
				objects = objects
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, new {}, CommonResponseBody.Load, callback)
				);	
			
		}
		
		/// <summary>
		/// Destroies the objects.
		/// </summary>
		/// <param name="procedureName">Procedure name.</param>
		/// <param name="objects">Objects.</param>
		/// <param name="callback">Callback.</param>
		public void DestroyObjects(string procedureName, List<Hobject> objects,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.DestoryObjects,procedureName));
			
			var requestBody = new {
				objects = objects
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, new {}, CommonResponseBody.Load, callback)
				);	
			
		}

	}

}
