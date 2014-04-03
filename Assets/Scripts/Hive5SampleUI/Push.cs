﻿using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Push : MonoBehaviour {

	Hive5Client H5;

	/// <summary>
	/// Rounds the start.
	/// </summary>
	public void setToken()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출

		H5.setZone (Hive5APIZone.Real);
		H5.setDebug ();
		H5.registerPushToken ("android", "test_token", (resultCode, response) => {
			Debug.Log ("onregisterPushToken");
			// 성공
			if (resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보

			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + resultCode);
			}
		});

	}	
}
