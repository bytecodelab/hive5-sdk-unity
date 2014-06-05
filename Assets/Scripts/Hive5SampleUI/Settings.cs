using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Settings : MonoBehaviour {

	public string Nickname = "gilbok";

	public void CheckNicknameAvailability()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.CheckNicknameAvailability(this.Nickname, response => {
			Debug.Log ("onCheckNicknameAvailability");
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
			}
		});
	}


	public void SetNickname()
	{
		Debug.Log ("SetNickname called 0");

		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.SetNickname (this.Nickname,  response => {
			Debug.Log ("onSetNickname");
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
			}
		});
		Debug.Log ("SetNickname called 1");
	}
}