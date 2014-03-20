using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;

public class Items : MonoBehaviour {

	Hive5Client H5;

	/// <summary>
	/// Sets the item convert.
	/// </summary>
	public void convertItem()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		
		string itemConvertKey = "buy_gameItem.doubleShot";
		
		H5.convertItem(itemConvertKey,(resultCode, response) => {
			Debug.Log ("onSetUserData");
			
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
			
		});
		
	}
	
	
	/// <summary>
	/// Consumes the item.
	/// </summary>
	public void consumeItem()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		
		var requestBody = new {
			items = new []{
				new {
					key = "money.gold",	// 골드사용
					count = 3	
				}
			}
		};
		
		H5.consumeItem (requestBody,(resultCode, response) => {
			Debug.Log ("onConsumeItem");
			
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
			
		});
		
	}

}
