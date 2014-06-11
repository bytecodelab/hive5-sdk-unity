using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class General : MonoBehaviour {

	public string LogData = "hello, this is test log.";

	public void EventLogs()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		hive5.EventLogs("test_log", this.LogData, response => {
			Debug.Log ("onLogs");
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