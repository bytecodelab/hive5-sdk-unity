using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Model;

public class Login : MonoBehaviour {

	Hive5Client H5;
	Hive5Core HC5;

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
		
		Hive5Core.apiCallBack callBack = onLoginKakao2;		// 로그인 후 호출 받을 콜백함수 지정

		Hive5Auth auth = new Hive5Auth ();
		auth.login( userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys, callBack );	//카카오 로그인 API 호출
	}
	
	/// <summary>
	/// Raises the kakao login event.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onLoginKakao2(Hive5Response response)
	{
		Debug.Log ("onLoginKakao2");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + response.resultCode);
			var loginInfo = (LoginData)response.resultData;
			
			foreach(var userData in loginInfo.userData)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", userData.Key, userData.Value));
			}
			
			foreach(var promotion in loginInfo.promotions)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", promotion.id, promotion.applyUrl));
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


	/// <summary>
	/// Kakao the login.
	/// </summary>
	public void loginKakao3()
	{
		string userId 		= "88197948207226176";			// 카카오 user id
		string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= OSType.android;			// 'android' 또는 'ios'
		
		string[] userDataKeys	= new string[] {"player."};		// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"heart"};		// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key
		
		Hive5Core.apiCallBack callBack = onLoginKakao2;		// 로그인 후 호출 받을 콜백함수 지정
		
		HC5 = Hive5Core.Instance;	// Hive5Client 호출
		HC5.setZone (Hive5APIZone.Real);
		HC5.setDebug ();
		HC5.loginKakao( userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys, callBack );	//카카오 로그인 API 호출
	}

	/// <summary>
	/// Raises the kakao login event.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onLoginKakao3(Hive5Response response)
	{
		Debug.Log ("onLoginKakao3");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + response.resultCode);
			var loginInfo = (LoginData)response.resultData;

			foreach(var userData in loginInfo.userData)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", userData.Key, userData.Value));
			}

			foreach(var promotion in loginInfo.promotions)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", promotion.id, promotion.applyUrl));
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


	/// <summary>
	/// Login
	/// </summary>
	public void login()
	{
		string userId 		= "88197948207226176";			// 카카오 user id
		string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= OSType.android;				// 'android' 또는 'ios'
		string platform 	= PlatformType.facebook;		// Platform 
		
		string[] userDataKeys	= new string[] {"player."};		// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"heart"};		// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key
		
		Hive5Client.apiCallBack callBack = onLogin;		// 로그인 후 호출 받을 콜백함수 지정
		
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		H5.setZone (Hive5APIZone.Beta);
		H5.login( platform, userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys, callBack );	//카카오 로그인 API 호출
	}

	/// <summary>
	/// Kakao the login.
	/// </summary>
	public void loginKakao()
	{
		string userId 		= "88197948207226176";			// 카카오 user id
		string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= OSType.android;			// 'android' 또는 'ios'

		string[] userDataKeys	= new string[] {"player."};		// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"heart"};		// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key

		Hive5Client.apiCallBack callBack = onLoginKakao;		// 로그인 후 호출 받을 콜백함수 지정
		
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		H5.setZone (Hive5APIZone.Real);
		H5.loginKakao( userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys, callBack );	//카카오 로그인 API 호출
	}

	/// <summary>
	/// Anonymous the login.
	/// </summary>
	public void loginAnonymous()
	{
		string os = OSType.android;	// 'android' 또는 'ios'

		string[] userDataKeys	= new string[] {"player.nickname"};	// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"moneyinfo.ruby"};	// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"sample"};			// 로그인 후 가져와야할 사용자 configuration의 key

		Hive5Client.apiCallBack callBack = onLoginAnonymous;	// 로그인 후 호출 받을 콜백함수 지정
		
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		H5.setZone (Hive5APIZone.Beta);
		H5.loginAnonymous( os, userDataKeys, itemKeys, configKeys, callBack );	//익명 로그인 API 호출
	}


	/// <summary>
	/// Ons the login.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onLogin(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onLogin V4");
		
		// 성공
		if (resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
			Debug.Log ("key 'user_id' =" + response["user_id"]);		// 특정 키 참조
			Debug.Log ("config = "+ JsonMapper.ToJson (response["configs"]));
			var time_event1 = JsonMapper.ToObject(((string)response["configs"]["time_event1"]));
			
			Debug.Log ("json="+JsonMapper.ToJson(time_event1));
			Debug.Log ("event_title = "+time_event1[0]["event_title"]);
		} 
		// 실패
		else {
			
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultMessage =" + response ["result_message"]);	// 상세 에러 메시지
			
		}	
	}


	/// <summary>
	/// Raises the anonymous login event.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onLoginAnonymous(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onLoginAnonymous");
		
		// 성공
		if (resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
			Debug.Log ("key 'user_id' =" + response["user_id"]);		// 특정 키 참조
			
		} 
		// 실패
		else {
			
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultMessage =" + response ["result_message"]);	// 상세 에러 메시지
			
		}	
	}

	/// <summary>
	/// Raises the kakao login event.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onLoginKakao(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onLoginKakao");
		
		// 성공
		if (resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
			Debug.Log ("key 'user_id' =" + response["user_id"]);		// 특정 키 참조
			Debug.Log ("config = "+ JsonMapper.ToJson (response["configs"]));
			var time_event1 = JsonMapper.ToObject(((string)response["configs"]["time_event1"]));

			Debug.Log ("json="+JsonMapper.ToJson(time_event1));
			Debug.Log ("event_title = "+time_event1[0]["event_title"]);
		} 
		// 실패
		else {
			
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultMessage =" + response ["result_message"]);	// 상세 에러 메시지
			
		}	
	}
}
