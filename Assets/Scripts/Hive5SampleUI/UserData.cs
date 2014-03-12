using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class UserData : MonoBehaviour {

	Hive5Client H5;



	/// <summary>
	/// Gets the user data.
	/// </summary>
	public void getUserData()
	{
		string dataKeys = "Player.nickname";		// user data keys
		Hive5Client.apiCallBack callBack = onGetUserData;	// api callback
		
		H5 = Hive5Client.Instance;						// Hive5Client 호출
		H5.getUserData( dataKeys, callBack );			//Get UserData API 호출
	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void setUserData()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출

		var requestBody = new {
			data = new []{
				new {
					key 	= "player.nickname",	// 플레이어 닉네임 
					value 	= "nickname3"					
				},
				new {
					key 	= "player.level",		// 플레이어 레벨
					value 	= "3"							 
				},
				new {
					key 	= "invitefriends.count",	// 친구 초대 횟수
					value 	= "5"								 
				},
				new {
					key 	= "invitefriends.list-48793",	// 친구 48793 초대
					value 	= "5"								 
				},
				new {
					key 	= "invitefriends.list-48777",	// 친구 48777 초대
					value 	= "5"								 
				},
				new {
					key 	= "invitefriends.list-48932",	// 친구 48932 초대
					value 	= "5"								 
				},
			}
		};

		Hive5Client.apiCallBack callBack = onSetUserData;
		H5.setUserData (requestBody, callBack);

	}

	/// <summary>
	/// Ons the get user data.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onGetUserData(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onGetUserData");
		
		// 성공
		if (resultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultMessage =" + response ["result_message"]);	// 상세 에러 메시지
		}		
	}

	/// <summary>
	/// Ons the set user data.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onSetUserData(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onSetUserData");
		
		// 성공
		if (resultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + resultCode);
			Debug.Log ("resultMessage =" + response ["result_message"]);	// 상세 에러 메시지
		}		
	}

}
