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
		string purchaseData = "{\"purchaseToken\":\"ehonnhlnnimhgjdmaeofmbfg.AO-J1Ow_-bdUf8Q5NHNOylWiApwwbg1-kRy5H5ev8MYTKFK3xAjaG5gZoRhEWuwqXdkQm8S_JSEGzoJvGPQqQi8GNJ9TfM5_W3eVwmoxfM4XM6nZ_Bah8VwwbH3r6nwSurEyNgDRsyv9\",\"developerPayload\":\"3159449026239529\",\"packageName\":\"net.mediacanvas.brainbreaker\",\"purchaseState\":0,\"orderId\":\"12999763169054705758.1372533416296071\",\"purchaseTime\":1397006164211,\"productId\":\"google_product_20\"}";
		string signature = "OooXMdywOxkcCRyU7A3NxOsTQJqNz2ZFr7W6k/5tpBGR58bw2bJbJAnv5MAj21YkXGzeFS/FTAFtR7gKOZTKIgffJC7jcqwjMVmX8ulYJ54XCFnw9P2ln3fIHPv/+lT/pgJutQGGVKo878N/VNe4qts8laoZa4xl3MGCRhSR8fqm68EhZemDu2ztvy74IIDD2f1lDMnyGAQ5/B6g+O+AdSxFf2LUGDbVtEiKZGWOSGv9H8azNo4H/79khiq5RqSUJ67I+dmOYviwtv0txkx2udj23nT14sFguukecvpOpSeWztz6nGA51+d46XcXl/XqAPDFZbE0adOyZwOzWD+Yng==";
		
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
