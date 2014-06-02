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
			Mission API Group
		*********************************************************************************/
		
		/** 
		* @api {public Method} CompleteMission 미션 완료
		* @apiVersion 1.0.0
		* @apiName void CompleteMission(string missionKey, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {string} missionKey 완료할 미션 키
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CompleteMission(missionKey, callback);
		*/
		public void CompleteMission(string missionKey, CallBack callback)
		{
			var url = InitializeUrl (string.Format(APIPath.CompleteMission, missionKey));
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, new {}, CompleteMissionResponseBody.Load, callback)
				);
		}
		
		/** 
		* @api {public Method} BatchCompleteMission 미션 일괄 완료
		* @apiVersion 1.0.0
		* @apiName void BatchCompleteMission(string[] missionKeys, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {string[]} missionKeys 완료할 미션 키 리스트
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.BatchCompleteMission(missionKeys, callback);
		*/
		public void BatchCompleteMission(string[] missionKeys, CallBack callback)
		{
			var url = InitializeUrl (APIPath.BatchCompleteMission);
			
			var requestBody = new {
				keys 	= missionKeys
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, BatchCompleteMissionResponseBody.Load, callback)
				);
		}
		
		/** 
		* @api {public Method} GetCompletedMissions 완료 미션 리스트 가져오기
		* @apiVersion 1.0.0
		* @apiName void BatchCompleteMission(string[] missionKeys, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetCompletedMissions(callback);
		*/
		public void GetCompletedMissions(CallBack callback)
		{
			var url = InitializeUrl (APIPath.GetCompletedMissions);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
                GetHttp (url, parameters.data, GetCompletedMissionsResponseBody.Load, callback) 
           	);
		}


	}

}
