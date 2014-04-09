using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;

public class Mails : MonoBehaviour {
		
	/// <summary>
	/// Gets the user data.
	/// </summary>
	public void getMails()
	{
		var hive5 = Hive5Client.Instance;

		hive5.getMails (10, "dec", 0, "", response => {
			Debug.Log ("onGetMails");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;

				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}

			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}

		});
	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void createMail()
	{
		var hive5 = Hive5Client.Instance;
		hive5.createMail("test mail", null, null, response => {
			Debug.Log ("onCreateMail");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (CreateMailResponseBody)response.resultData;
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
		});
	}


	public void getMailsCount()
	{
		var hive5 = Hive5Client.Instance;
		
		hive5.getMailCount (OrderType.DESC, 0, "", response => {
			Debug.Log ("onGetMailCount");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;
				
				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void updateMail()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		
		hive5.updateMail (mailId, "update content", response => {
			Debug.Log ("onUpdateMail");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;
				
				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void deleteMail()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		hive5.deleteMail (mailId, response => {
			Debug.Log ("onDeleteMail");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;
				
				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void deleteAllMail()
	{
		var hive5 = Hive5Client.Instance;
		var fromMailId = 5;
		var toMailId = 10;

		hive5.deleteAllMail (fromMailId, toMailId, response => {
			Debug.Log ("onDeleteAllMail");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;
				
				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}	
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void AttachMailTag()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		var tags = new string[] {"notice"};

		hive5.attachMailTags (mailId, tags, response => {
			Debug.Log ("onAttachMailTag");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;
				
				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}	
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
			
		});
	}


	public void DetachMailTag()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		var tags = new string[] {"notice"};
		
		hive5.detachMailTags (mailId, tags, response => {
			Debug.Log ("onDetachMailTag");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.resultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.resultData;
				
				foreach(Mail mail in mailInfo.mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}	
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}
			
		});
	}


}
