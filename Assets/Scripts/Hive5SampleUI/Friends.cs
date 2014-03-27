using UnityEngine;
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
		
		Hive5Client.apiCallBack callBack = onUpdateFriends;
		H5.setZone (Hive5APIZone.Beta);
		H5.setDebug ();
		H5.updateFriends (friend_ids, callBack);
	}

/// <summary>
/// Updates the kakao friends.
/// </summary>
public void updateKakaoFriends()
{
	H5 = Hive5Client.Instance;	// Hive5Client 호출

	string kakaoResponseString = "[{\"user_id\":\"88308950770094417\", \"hashed_talk_user_id\":\"AQkNc3MNCQE\", \"message_blocked\":\"false\", \"profile_image_url\":\"http://th-p17.talk.kakao.co.kr/th/talkp/wkdl47x1q9/VzwE5eCc0Vjuzk9xgMEfUk/dhjdcp_110x110_c.jpg\", \"nickname\":\"이승근\"},{\"user_id\":\"88258809308309793\", \"hashed_talk_user_id\":\"AAJGo6NGAgA\", \"message_blocked\":\"false\", \"profile_image_url\":\"http://th-p4.talk.kakao.co.kr/th/talkp/wkbzYhjI5O/oxJZKuK332UZzMAW9YOWXk/dcwz9g_110x110_c.jpg\", \"nickname\":\"쭌\"}]";

	var appFriendsInfo = (JsonMapper.ToObject (kakaoResponseString));

	Hive5Client.apiCallBack callBack = onUpdateKakaoFriends;
	H5.setDebug ();
	H5.updateKakaoFriends (appFriendsInfo, callBack);
	
}
		

	/// <summary>
	/// Ons the update friends.
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
	/// Ons the update kakao friends.
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
