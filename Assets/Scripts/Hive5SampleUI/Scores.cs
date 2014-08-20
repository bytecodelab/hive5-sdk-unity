using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Hive5;

public class Scores : MonoBehaviour {

	Hive5Client hive5;

	/// <summary>
	/// Gets the scores.
	/// </summary>
	public void getScores()
	{
		hive5 = Hive5Client.Instance;    // Hive5Client 호출

		hive5.SetZone (Hive5APIZone.Beta);
		hive5.SetDebug ();

		List<string> objectKeys = new List<string> ()
		{
			"sword",
		};

		hive5.GetScores ("3", objectKeys, 1, 10, null, null, response => {
			Debug.Log ("onGetScores");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
			}	
		});
	}

	/// <summary>
	/// Gets the social scores.
	/// </summary>
	public void getSocialScores()
	{
		hive5 = Hive5Client.Instance;    // Hive5Client 호출

		List<string> objectKeys = new List<string> ()
		{
			"sword",
		};

		hive5.SetZone (Hive5APIZone.Beta);
		hive5.SetDebug ();
		hive5.GetSocialScores("3", objectKeys, response => {
			Debug.Log ("onGetSocialScores");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
			}	
		});
	}

}
