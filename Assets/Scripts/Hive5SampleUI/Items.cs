using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Items : MonoBehaviour {

	Hive5Client H5;



	public void getItems()
	{
		var hive5 = Hive5Client.Instance;
		var itemKeys = new string[] {"money.gold", "money.heart"};
		hive5.getItems (itemKeys, response => {
			Debug.Log ("onGetItems");

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

		});

	}

	/// <summary>
	/// Sets the item convert.
	/// </summary>
	public void convertItem()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		string itemConvertKey = "buy_gameItem.doubleShot";
		
		hive5.convertItem(itemConvertKey,(response) => {
			Debug.Log ("onSetUserData");

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
			
		});
		
	}
	
	
	/// <summary>
	/// Consumes the item.
	/// </summary>
	public void consumeItem()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		
		var requestBody = new {
			items = new []{
				new {
					key = "money.gold",	// 골드사용
					count = 3	
				}
			}
		};
		
		hive5.consumeItem (requestBody,(response) => {
			Debug.Log ("onConsumeItem");

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
			
		});
		
	}

}
