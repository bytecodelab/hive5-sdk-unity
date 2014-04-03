using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Model;

public class Login : MonoBehaviour {

	/// <summary>
	/// Kakao the login.
	/// </summary>
	public void loginKakao2()
	{
		string userId 		= "88197948207226176";			// 카카오 user id
		string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= OSType.android;			// 'android' 또는 'ios'
		
		string[] userDataKeys	= new string[] {"player."};		// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"heart"};		// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key
		
		Hive5API.CallBack callBack = onLoginKakao2;		// 로그인 후 호출 받을 콜백함수 지정


		Hive5API.Init ("a40e4122-99d9-44a6-b916-68ed756f79d6", "747474747");
		Hive5Auth auth = new Hive5Auth();
		auth.login( userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys, callBack );	//카카오 로그인 API 호출
	}


	/// <summary>
	/// Ons the login kakao2.
	/// </summary>
	/// <param name="response">Response.</param>
	private static void onLoginKakao2(Hive5Response response)
	{
		Debug.Log ("onLoginKakao2");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + response.resultCode);
			var loginInfo = (LoginDataResponseBody)response.resultData;
			
			foreach(var userData in loginInfo.userData)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", userData.Key, userData.Value));
			}
			
			foreach(var promotion in loginInfo.promotions)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", promotion.id, promotion.applyUrl));
			}
			
			foreach(KeyValuePair<string, AgreementData> agreement in loginInfo.agreements)
			{
				
				Debug.Log (string.Format("agreements key : {0}, version : {1} , agreedAt : {2}", agreement.Key, agreement.Value.version, agreement.Value.agreedAt));
			}
			
		} 
		// 실패
		else {			
			Debug.Log ("resultCode =" + response.resultCode);
		}	
	}
	
}
