using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Friends : MonoBehaviour {

	Hive5Client H5;
	
	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void updateFriends()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출

		var requestBody = new {
			app_friends_info = new[] {
				new {
					user_id = "88197948207226176",
					nickname = "heesung",
					profile_image_url = "",
					friend_nickname = "heesung", 
					message_blocked = false,
					hashed_talk_user_id = "123123"
				}
			}
		};

		Hive5Client.apiCallBack callBack = onUpdateFriends;
		H5.updateKakaoFriends (requestBody, callBack);

	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void updateKakaoFriends()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		
		var requestBody = new {
			app_friends_info = new[] {
				new {
					user_id = "88197948207226176",
					nickname = "heesung",
					profile_image_url = "",
					friend_nickname = "heesung", 
					message_blocked = false,
					hashed_talk_user_id = "123123"
				}
			}
		};
		
		Hive5Client.apiCallBack callBack = onUpdateKakaoFriends;
		H5.updateKakaoFriends (requestBody, callBack);
		
	}
	

	/// <summary>
	/// Ons the set user data.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onUpdateFriends(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onUpdateFriends");
		
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
	private static void onUpdateKakaoFriends(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onUpdateKakaoFriends");
		
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
