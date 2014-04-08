using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Agreements : MonoBehaviour {
	
	public void submitAgreements()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.submitAgreements ("1.1.1", "0.1", response => {
			Debug.Log ("onSubmitAgreements");
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


	public void getAgreements()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.getAgreements ( response => {
			Debug.Log ("onSubmitAgreements");
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

	public void unregister()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.unregister ( response => {
			Debug.Log ("onSubmitAgreements");
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
	
}