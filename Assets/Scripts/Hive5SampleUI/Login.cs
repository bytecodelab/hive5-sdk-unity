using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Model;

public class Login : MonoBehaviour {

	/// <summary>
	/// Login this instance.
	/// </summary>
	public void login()
	{

		string userId 		= "88197948207226176";			// 카카오 user id
		string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= OSType.Android;				// 'android' 또는 'ios'
		
		string[] objectKeys 	= new string[] {""};		// 로그인 후 가져와야할 사용자 object의 key 목록
		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key
	
		Hive5Client hive5 = Hive5Client.Instance;
		hive5.Init ("a40e4122-99d9-44a6-b916-68ed756f79d6", "747474747",Hive5APIZone.Beta);
		hive5.setDebug ();
		hive5.login (os, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, response => {
			Debug.Log ("response = "+ response.resultData);
		});
	}

}
