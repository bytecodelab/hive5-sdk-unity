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
		
		/** 
		* @api {GET} GetObjects 오브젝트 리스트
		* @apiVersion 1.0.0
		* @apiName void GetObjects(List<HObject> objects, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {List<HObject>) objects 오브젝트 리스트(class 프로퍼티만 셋팅)
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetObjects(objectKeys, callback);
		*/
		public void GetObjects(List<HObject> objects, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.UserData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			objects.ForEach ( hobject => { parameters.Add( ParameterKey.Key, hobject.@class ); } );
			
			// WWW 호출
			StartCoroutine ( 
	        	GetHttp (url, parameters.data, CommonResponseBody.Load, callback) 
	        );
		}
		
		
		/** 
		* @api {POST} CreateObjects 오브젝트 생성
		* @apiVersion 1.0.0
		* @apiName void CreateObjects(List<HObject> objects,  CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {List<HObject>) objects 오브젝트 리스트(class 프로퍼티만 셋팅)
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateObjects( objects, callback);
		*/
		public void CreateObjects(List<HObject> objects,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateObjects);
			
			var requestBody = new {
				objects = objects
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, requestBody, CommonResponseBody.Load, callback)
			);	
			
		}
		
		/** 
		* @api {POST} SetObjects 오브젝트 저장
		* @apiVersion 1.0.0
		* @apiName void SetObjects(List<Hobject> objects,  CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {List<HObject>) objects 오브젝트 리스트(class 프로퍼티만 셋팅)
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.SetObjects(objects, callback);
		*/
		public void SetObjects(List<HObject> objects,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.SetObjects);
			
			var requestBody = new {
				objects = objects
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, requestBody, CommonResponseBody.Load, callback)
			);	
			
		}
		
		/** 
		* @api {POST} DestroyObjects 오브젝트 제거
		* @apiVersion 1.0.0
		* @apiName void DestroyObjects(List<Hobject> objects,  CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {List<HObject>) objects 오브젝트 리스트(class, id 프로퍼티 셋팅 / Singleton 인경우 id 생략)
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DestroyObjects( objects, callback);
		*/
		public void DestroyObjects(List<HObject> objects,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.DestoryObjects);
			
			var requestBody = new {
				objects = objects
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, requestBody, CommonResponseBody.Load, callback)
			);	
			
		}

	}

}
