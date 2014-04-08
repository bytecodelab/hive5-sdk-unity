using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;

public class UserData : MonoBehaviour {

	Hive5Client hive5;

	/// <summary>
	/// Gets the user data.
	/// </summary>
	public void getUserData()
	{
		string[] dataKeys = new string[] {"player.city"};		// user data keys
		Hive5Client.CallBack callBack = onGetUserData;	// api callback
		
		hive5 = Hive5Client.Instance;
		hive5.getUserData( dataKeys, callBack );			//Get UserData API 호출
	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void setUserData()
	{
		Hive5Client.CallBack callback = onSetUserData;

		hive5 = Hive5Client.Instance;
		hive5.setUserData ("player.city", "SEOUL", CommandType.SET, callback);
	}

	private static void onGetUserData(Hive5Response response)
	{
		Debug.Log ("onGetUserData");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + response.resultCode);

			var userDataList = (GetUserDataResponseBody)response.resultData;

			foreach(var userData in userDataList.userData)
			{
				Debug.Log (string.Format("userData key : {0} , value : {1}", userData.key, userData.value));
			}
			
			
		} 
		// 실패
		else {			
			Debug.Log ("resultCode =" + response.resultCode);
		}	
	}

	/// <summary>
	/// Ons the user data4.
	/// </summary>
	/// <param name="response">Response.</param>
	private static void onSetUserData(Hive5Response response)
	{
		Debug.Log ("onSetUserData");
		
		// 성공
		if (response.resultCode == Hive5ResultCode.Success) {
			
			Debug.Log ("resultCode =" + response.resultCode);

			
		} 
		// 실패
		else {			
			Debug.Log ("resultCode =" + response.resultCode);
		}	
	}


}
