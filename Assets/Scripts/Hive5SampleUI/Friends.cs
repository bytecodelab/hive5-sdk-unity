﻿using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Friends : MonoBehaviour {

	Hive5Client H5;

	/// <summary>
	/// Updates the friends.
	/// </summary>
	public void updateFriends()
	{
		H5 = Hive5Client.Instance;    // Hive5Client 호출
		
		var friend_ids = new string[] {"881979482072261763"};

		Hive5Client.CallBack callback = onUpdateFriends;
		H5.setZone (Hive5APIZone.Beta);
		H5.setDebug ();
		H5.updateFriends (friend_ids, callback);
	}
	
	/// <summary>
	/// Ons the update friends.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onUpdateFriends(Hive5Response response)
	{
		Debug.Log ("onUpdateFriends");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.resultCode);
		}		
	}

}
