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
		Hive5Client.CallBack = onGetMails;
		H5 = Hive5Client.Instance;
	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void postMail()
	{
		Hive5Client.CallBack = onPostMail;
		H5 = Hive5Client.Instance;
	}

	/// <summary>
	/// Ons the get mails.
	/// </summary>
	/// <param name="response">Response.</param>
	private static void onGetMails(Hive5Response response)
	{
		Debug.Log ("onGetMails");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.resultCode);
			Debug.Log ("resultData = "+ response.resultData);	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.resultCode);
			Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
		}		
	}

	/// <summary>
	/// Ons the post mail.
	/// </summary>
	/// <param name="response">Response.</param>
	private static void onPostMail(Hive5Response response)
	{
		Debug.Log ("onPostMail");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.resultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.resultCode);
			Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
		}		
	}

}
