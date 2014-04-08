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
		long listPrice = 100;
		long purchasePrice = 70;
		string currency = null;
		string purchaseData = @"{""orderId"":""12999763169054705758.1303259400480486"",""packageName"":""com.vespainteractive.BeatMonster"",""productId"":""com.vespainteractive.beatmonster.android.ruby100"",""purchaseTime"":1386754096514,""purchaseState"":0,""developerPayload"":""+id+"",""purchaseToken"":""ydtyoxiavuqzsnpbvstbwgff.AO-J1OwWVF4JhHJju_E2EacgXeYMMTyFwqU8hRiFlvFkNmbQc8ziTSCh1MUv2dgnCzf0I9kQmeftOFvb1zEOeedI-69ux1Wx-4oV7IY93_lVPNWLL8cupg-vNqp_D6pypUttKMx--heNmJYPzhMtAqSmEQLlD3g4EGi44Iid9z4L9kpEhuJcjH9GlIxEE8HXTdmD-se5P4EI""}";
		string signature = "NfhVWI+Gs+ksgw/YivvVvwnisTffSC1ea107THLenWF/9yYpdZTmaSi4/ge9CagSMgknQgFxG0+JDUm5cvYJBcOZP0Gw3c1A7PyILBe1COJu3Gxz/r65bNcL3gdRVOUxXKPDM3bNKhjwsRSvQGiEHNb0z8f4zPWqKV1m3QiofDhKMkDkTNXxo7QfrC710vS9e570mAMLk8Bmu+nK2+lFLbarA/dJ54k8bb597NZnqyBAHdLjIHkWjubBqP7T1zRW5LD/fvj9jg0+b39Os8l8WLsHq5OmKlkt2oZ328OyXcpAqqsmaYbGZiXgnp7HubiGAYLWVc912+uhakNnlX127w==";
		
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
