using UnityEngine;
using System;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;

public class Purchases : MonoBehaviour {

	Hive5Client H5;
	public long purchaseId = 0;

	public void createGooglePurchase()
	{
		string productCode 		= "google_product_100";
		string receiverPlatformUserId 	= null;
		string mailForReceiver = null;

		Hive5Client hive5 = Hive5Client.Instance;
		hive5.setDebug ();
		hive5.createGooglePurchase(productCode, receiverPlatformUserId, mailForReceiver, (response) => {
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ response.resultData);	// 응답 데이터 전체 정보
				purchaseId = ((CreateGooglePurchaseResponseBody)response.resultData).id;
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
			}	
			
		});
	}
	
	public void completeGooglePurchase()
	{
		long id = purchaseId;
		long listPrice = 1100;
		long purchasePrice = 1100;
		string currency = null;
		string purchaseData = "{\"purchaseToken\":\"\",\"developerPayload\":\"\",\"packageName\":\"\",\"purchaseState\":,\"orderId\":\"\",\"purchaseTime\":,\"productId\":\"\"}";
		string signature = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx==";
		
		Hive5Client hive5 = Hive5Client.Instance;
		hive5.setDebug ();
		hive5.completeGooglePurchase(id, listPrice, purchasePrice, currency, purchaseData, signature, (response) => {
			
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
