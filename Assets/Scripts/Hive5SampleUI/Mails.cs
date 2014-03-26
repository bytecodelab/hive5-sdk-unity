using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Mails : MonoBehaviour {

	Hive5Client H5;

	
	/// <summary>
	/// Gets the user data.
	/// </summary>
	public void getMails()
	{
		Hive5Client.apiCallBack callBack = onGetMails;	// api callback
		H5 = Hive5Client.Instance;						// Hive5Client 호출

	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void postMail()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출

		Hive5Client.apiCallBack callBack = onPostMail;

	}

	/// <summary>
	/// Ons the get user data.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onGetMails(Hive5ResultCode resultCode, JsonData response)
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
	private static void onPostMail(Hive5ResultCode resultCode, JsonData response)
	{
		Debug.Log ("onPostMail");
		
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
