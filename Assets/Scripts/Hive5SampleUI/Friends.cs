using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using System.Collections.Generic;

public class Friends : MonoBehaviour {

	Hive5Client H5;

	public string GroupName = "default";

	/// <summary>
	/// Updates the friends.
	/// </summary>
	public void UpdateFriends()
	{
		H5 = Hive5Client.Instance;    // Hive5Client 호출

        var friends = new List<Friend>() {
            new Friend { platform="kakao", id="881979482072261763"}
        };

		Callback callback = onUpdateFriends;
		H5.SetZone (Hive5APIZone.Beta);
		H5.SetDebug ();
		H5.UpdateFriends (this.GroupName, friends, callback);
	}
	
	/// <summary>
	/// Ons the update friends.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onUpdateFriends(Hive5Response response)
	{
		Debug.Log ("onUpdateFriends");
		
		// 성공
		if (response.ResultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.ResultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.ResultCode);
		}		
	}


	/// <summary>
	/// Adds the friends.
	/// </summary>
	public void AddFriends()
	{
		H5 = Hive5Client.Instance;    // Hive5Client 호출
		
		var friend_ids = new string[] {"881979482072261763"};
		
		Callback callback = onAddFriends;
		H5.SetZone (Hive5APIZone.Beta);
		H5.SetDebug ();
		H5.AddFriends (this.GroupName, friend_ids, callback);
	}

	/// <summary>
	/// On add friends.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onAddFriends(Hive5Response response)
	{
		Debug.Log ("onAddFriends");
		
		// 성공
		if (response.ResultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.ResultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.ResultCode);
		}		
	}

	/// <summary>
	/// Removes the friends.
	/// </summary>
	public void RemoveFriends()
	{
		H5 = Hive5Client.Instance;    // Hive5Client 호출
		
		var friend_ids = new string[] {"881979482072261763"};
		
		Callback callback = onRemoveFriends;
		H5.SetZone (Hive5APIZone.Beta);
		H5.SetDebug ();
		H5.RemoveFriends(this.GroupName, friend_ids, callback);
	}
	
	/// <summary>
	/// On remove friends.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onRemoveFriends(Hive5Response response)
	{
		Debug.Log ("onRemoveFriends");
		
		// 성공
		if (response.ResultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.ResultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.ResultCode);
		}		
	}

	/// <summary>
	/// Gets the friends.
	/// </summary>
	public void GetFriends()
	{
		H5 = Hive5Client.Instance;    // Hive5Client 호출

		Callback callback = onGetFriends;
		H5.SetZone (Hive5APIZone.Beta);
		H5.SetDebug ();
		H5.GetFriends(this.GroupName, callback);
	}
	
	/// <summary>
	/// On remove friends.
	/// </summary>
	/// <param name="resultCode">Result code.</param>
	/// <param name="response">Response.</param>
	private static void onGetFriends(Hive5Response response)
	{
		Debug.Log ("onGetFriends");
		
		// 성공
		if (response.ResultCode == Hive5ResultCode.Success) {
			Debug.Log ("resultCode =" + response.ResultCode);
			Debug.Log ("resultData = "+ JsonMapper.ToJson(response));	// 응답 데이터 전체 정보
		} 
		// 실패
		else {
			Debug.Log ("resultCode =" + response.ResultCode);
		}
	}
}
