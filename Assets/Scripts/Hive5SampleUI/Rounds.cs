using UnityEngine;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;

public class Rounds : MonoBehaviour {

	Hive5Client H5;
	long roundId;		// Round ID

	/// <summary>
	/// Rounds the start.
	/// </summary>
	public void startRounds()
	{
		H5 = Hive5Client.Instance;	// Hive5Client 호출
		
		long roundRuleId = 5;
		
		H5.startRound(roundRuleId, (response) => {
			Debug.Log ("onRoundStart");
			
			// 성공
			if (response.resultCode == Hive5ResultCode.Success) {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultData = "+ response.resultData);	// 응답 데이터 전체 정보

				var roundInfo = (StartRoundResponseBody)response.resultData;
				this.roundId = roundInfo.id;	// round End 호출을 위한 roundId 저장
			} 
			// 실패
			else {
				Debug.Log ("resultCode =" + response.resultCode);
				Debug.Log ("resultMessage =" + response.resultMessage);	// 상세 에러 메시지
			}	
			
		});
	}
	
	/// <summary>
	/// Rounds the end.
	/// </summary>
	public void endRounds()
	{
		var hive5 = Hive5Client.Instance;	// Hive5Client 호출
		
		long roundId = this.roundId;
		
		var requestBody = new {
			
			// 라운드 종료 시 리더보드에 점수 제출
			score = new {
				leaderboard_id = 3,
				value = 65000
			},
			
			// 라운드 종료 시 item 의 변경 사항
			item_changes = new []{
				new {
					key = "level.exp",	// 플레이어 경험치 키값
					command = "inc",	// 증가
					value = 200	 // 경험치 값
				},
				new {
					key = "money.gold",	// 플레이어 경험치 키값
					command = "inc",	// 증가
					value = 5000	 // 경험치 값
				}
			}
			
		};
		
		hive5.endRound(roundId, requestBody, response => {
			Debug.Log ("onRoundEnd = "+roundId);
			
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
