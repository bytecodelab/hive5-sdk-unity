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
			Reward API Group
		*********************************************************************************/
		
		/// <summary>
		/// Get the specified rewardId and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="callback">Callback.</param>
		public void GetRewardInfo(long rewardId, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.Reward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, GetRewardInfoResponseBody.Load, callback) 
			                );
		}
		
		/// <summary>
		/// Apply the specified rewardId, deleteMail and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void ApplyReward(long rewardId, bool deleteMail, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.ApplyReward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, ApplyRewardResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Applies all.
		/// </summary>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void ApplyAllRewards(bool deleteMail, CallBack callback)
		{
			var url = InitializeUrl (APIPath.ApplyAllReward);
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, ApplyAllRewardsResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Invalidate the specified rewardId, deleteMail and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void InvalidateReward(long rewardId, bool deleteMail, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.InvalidateReward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, InvalidateRewardResponseBody.Load, callback)
				);
		}


	}

}
