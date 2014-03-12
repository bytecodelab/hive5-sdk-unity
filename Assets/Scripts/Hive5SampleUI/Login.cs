using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Login : MonoBehaviour {

	Hive5Client _hive5;
	
	/// <summary>
	/// Kakao the login.
	/// </summary>
	public void kakaoLogin()
	{
		string userId 		= "88197948207226176";			// 카카오 user id
		string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= Hive5PlatformType.android;	// 'android' 또는 'ios'

		string[] userDataKeys	= new string[] {"player.nickname", "teamselect.turntable"};	// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"moneyinfo.ruby"};		// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"sample", "event"};		// 로그인 후 가져와야할 사용자 configuration의 key

		Hive5Client.apiCallBack callBack = onKakaoLogin;	// 로그인 후 호출 받을 콜백함수 지정
		
		_hive5 = Hive5Client.Instance;	// Hive5Client 호출
		_hive5.kakaoLogin( userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys, callBack );	//카카오 로그인 API 호출
	}

	/// <summary>
	/// Anonymous the login.
	/// </summary>
	public void anonymousLogin()
	{
		string os = Hive5PlatformType.android;	// 'android' 또는 'ios'

		string[] userDataKeys	= new string[] {"player.nickname"};	// 로그인 후 가져와야할 사용자 user data의 key 목록
		string[] itemKeys 		= new string[] {"moneyinfo.ruby"};	// 로그인 후 가져와야할 사용자 item의 key 목록
		string[] configKeys 	= new string[] {"sample"};			// 로그인 후 가져와야할 사용자 configuration의 key

		Hive5Client.apiCallBack callBack = onAnonymousLogin;	// 로그인 후 호출 받을 콜백함수 지정
		
		_hive5 = Hive5Client.Instance;	// Hive5Client 호출
		_hive5.anonymousLogin( os, userDataKeys, itemKeys, configKeys, callBack );	//익명 로그인 API 호출
	}


	/// <summary>
	/// Raises the anonymous login event.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onAnonymousLogin(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("OnAnonymousLogin");
		
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
	private static void onKakaoLogin(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("OnKakaoLogin");
		
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
}
