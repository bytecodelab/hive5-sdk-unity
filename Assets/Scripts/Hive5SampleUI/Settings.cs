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
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
			}
		});
	}


	public void SetNickname()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.SetNickname (this.Nickname,  response => {
			Debug.Log ("onSetNickname");
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
			}
		});
	}
}