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
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
		/********************************************************************************
			Mission API Group
		*********************************************************************************/
		
		/** 
		* @api {POST} CompleteMission 미션 완료
		* @apiVersion 1.0.0-alpha
		* @apiName CompleteMission
		* @apiGroup Mission
		*
		* @apiParam {string} missionKey 완료할 미션 키
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CompleteMission(missionKey, callback);
		*/
		public void CompleteMission(string missionKey, Callback callback)
		{
			var url = InitializeUrl (string.Format(APIPath.CompleteMission, WWW.EscapeURL(missionKey)));
			
			// WWW 호출
            PostHttpAsync(url, new { }, CompleteMissionResponseBody.Load, callback);
		}
			
		/** 
		* @api {POST} GetCompletedMissions 완료 미션 리스트 가져오기
		* @apiVersion 1.0.0-alpha
		* @apiName BatchCompleteMission
		* @apiGroup Mission
		*
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetCompletedMissions(callback);
		*/
		public void GetCompletedMissions(Callback callback)
		{
			var url = InitializeUrl (APIPath.GetCompletedMissions);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetCompletedMissionsResponseBody.Load, callback);
		}


	}

}
