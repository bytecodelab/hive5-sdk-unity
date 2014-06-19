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
			Reward API Group
		*********************************************************************************/
		

		/** 
		* @api {GET} GetRewardInfo 보상 정보
		* @apiVersion 1.0.0
		* @apiName GetRewardInfo
		* @apiGroup Reward
		*
		* @apiParam {long} rewardId 상품 코드
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetRewardInfo(rewardId, callback);
		*/
		public void GetRewardInfo(long rewardId, Callback callback)
		{
			var url = InitializeUrl (string.Format (APIPath.Reward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetRewardInfoResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} ApplyReward 보상 받기
		* @apiVersion 1.0.0
		* @apiName ApplyReward
		* @apiGroup Reward
		*
		* @apiParam {long} rewardId 상품 코드
		* @apiParam {bool} deleteMail 보상 받을 시 메일 함께 삭제 여부
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.ApplyReward(rewardId, deleteMail, callback);
		*/
		public void ApplyReward(long rewardId, bool deleteMail, Callback callback)
		{
			var url = InitializeUrl (string.Format (APIPath.ApplyReward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, ApplyRewardResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} ApplyReward 보상 전체 받기
		* @apiVersion 1.0.0
		* @apiName ApplyAllRewards
		* @apiGroup Reward
		*
		* @apiParam {bool} deleteMail 보상 받을 시 메일 함께 삭제 여부
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.ApplyAllRewards(deleteMail, callback);
		*/
		public void ApplyAllRewards(bool deleteMail, Callback callback)
		{
			var url = InitializeUrl (APIPath.ApplyAllReward);
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, ApplyAllRewardsResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} InvalidateReward 보상 무효화
		* @apiVersion 1.0.0
		* @apiName InvalidateReward
		* @apiGroup Reward
		*
		* @apiParam {bool} deleteMail 보상 받을 시 메일 함께 삭제 여부
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.InvalidateReward(rewardId, deleteMail, callback);
		*/
		public void InvalidateReward(long rewardId, bool deleteMail, Callback callback)
		{
			var url = InitializeUrl (string.Format (APIPath.InvalidateReward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, InvalidateRewardResponseBody.Load, callback);
		}


	}

}
