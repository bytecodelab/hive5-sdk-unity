using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Scores : MonoBehaviour {

	Hive5Client hive5;

	/// <summary>
	/// Gets the scores.
	/// </summary>
	public void getScores()
	{
		hive5 = Hive5Client.Instance;    // Hive5Client 호출

		hive5.SetZone (Hive5APIZone.Beta);
		hive5.SetDebug ();
		hive5.GetScores (3, 1, 10, null, null, response => {
			Debug.Log ("onGetScores");
			
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

	/// <summary>
	/// Gets the social scores.
	/// </summary>
	public void getSocialScores()
	{
		hive5 = Hive5Client.Instance;    // Hive5Client 호출

		hive5.SetZone (Hive5APIZone.Beta);
		hive5.SetDebug ();
		hive5.GetSocialScores(3, response => {
			Debug.Log ("onGetSocialScores");
			
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
