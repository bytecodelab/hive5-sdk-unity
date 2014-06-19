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

		hive5.GetMails (10, OrderType.DESC, 0, "", response => {
			Debug.Log ("onGetMails");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;

				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}

			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}

		});
	}

	/// <summary>
	/// Sets the user data.
	/// </summary>
	public void createMail()
	{
		var hive5 = Hive5Client.Instance;
		hive5.CreateMail("test mail", null, null, response => {
			Debug.Log ("onCreateMail");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (CreateMailResponseBody)response.ResultData;
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
		});
	}


	public void getMailsCount()
	{
		var hive5 = Hive5Client.Instance;
		
		hive5.GetMailCount (OrderType.DESC, 0, "", response => {
			Debug.Log ("onGetMailCount");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;
				
				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void updateMail()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		
		hive5.UpdateMail (mailId, "update content", response => {
			Debug.Log ("onUpdateMail");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;
				
				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void deleteMail()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		hive5.DeleteMail (mailId, response => {
			Debug.Log ("onDeleteMail");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;
				
				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}
				
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void deleteAllMail()
	{
		var hive5 = Hive5Client.Instance;
		var fromMailId = 5;
		var toMailId = 10;

		hive5.DeleteAllMail (fromMailId, toMailId, response => {
			Debug.Log ("onDeleteAllMail");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;
				
				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}	
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
			
		});
	}

	public void AttachMailTag()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		var tags = new string[] {"notice"};

		hive5.AttachMailTags (mailId, tags, response => {
			Debug.Log ("onAttachMailTag");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;
				
				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}	
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
			
		});
	}


	public void DetachMailTag()
	{
		var hive5 = Hive5Client.Instance;
		var mailId = 5;
		var tags = new string[] {"notice"};
		
		hive5.DetachMailTags (mailId, tags, response => {
			Debug.Log ("onDetachMailTag");
			
			// 성공
			if (response.ResultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultData = "+ JsonMapper.ToJson(response.ResultData));	// 응답 데이터 전체 정보
				
				var mailInfo = (GetMailsResponseBody)response.ResultData;
				
				foreach(Mail mail in mailInfo.Mails)
				{
					Debug.Log ("mail = "+ mail.content);
				}	
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.ResultCode);
				Debug.Log ("resultMessage =" + response.ResultMessage);	// 상세 에러 메시지
			}
			
		});
	}


}
