using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Hive5;

public class SpiderTest : MonoBehaviour {

	Hive5Client hive5Client { get { return Hive5Client.Instance; } }
	Hive5Spider hive5Spider ;

	GUIContent[] TopicKindComboBoxItems;
	private ComboBox TopicKindComboBox = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();

	bool spiderConnected = false;
	string appSecret = "gogogo";

	// Use this for initialization
	void Start () {

		BuildTopicKindComboBox ();

		string userId 		= "88197948207226176";			// 카카오 user id
		//string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
		string sdkVersion 	= "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
		string os 			= OSType.Android;				// 'android' 또는 'ios'
		string appKey = "a40e4122-99d9-44a6-b916-68ed756f79d6";
		string uuid = "747474747";

		string[] objectKeys 	= new string[] {""};		// 로그인 후 가져와야할 사용자 object의 key 목록
		string[] configKeys 	= new string[] {"time_event1"};	// 로그인 후 가져와야할 사용자 configuration의 key

		hive5Client.SetDebug ();
		hive5Client.Init (appKey, uuid, Hive5APIZone.Beta);
		hive5Client.Login(os, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, response => {
			Debug.Log ("login response = "+ response.ResultData);

			hive5Spider = new Hive5Spider (hive5Client);
			
			hive5Spider.Connect ((success) => {
				spiderConnected = success;

				/*
				hive5Spider.MessageReceived += (sender, topicKind, messageContents) =>
				{
					string head = string.Empty;
					switch (topicKind) {
					case TopicKind.Channel:
					{
						head = "[Channel]";
					}
						break;
					case TopicKind.Private:
					{
						head = "[Private]";
					}
						break;
					case TopicKind.System:
					{
						head = "[System]";
					}
						break;
					case TopicKind.Notice:
					{
						head = "[Notice]";
					}
						break;
					}
					
					messages += string.Format("{0} {1} {2}", DateTime.Now, head, messageContents["message"]);
					
				};
				*/

				/*

				*/
			});
		});
	}

	string players = string.Empty;

	void BuildTopicKindComboBox()
	{
		TopicKindComboBoxItems = new GUIContent[4];
		this.TopicKindComboBoxItems [0] = new GUIContent ("Channel");
		this.TopicKindComboBoxItems [1] = new GUIContent ("Private");
		this.TopicKindComboBoxItems [2] = new GUIContent ("Notice");
		this.TopicKindComboBoxItems [3] = new GUIContent ("System");

		listStyle.normal.textColor = Color.white;
		listStyle.onHover.background = 
			listStyle.hover.background = new Texture2D (2, 2);
		listStyle.padding.left = 
			listStyle.padding.right = 
				listStyle.padding.top = 
				listStyle.padding.bottom = 4;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	float[] Columns;
	float[] Rows;

	const float UiGap = 10f; 
	const float GlobalMargin = 10f;

	string inputMessage = "";
	string messages = "";

	Rect GetTextAreaRect(Rect originRect)
	{
		return new Rect (originRect.xMin + UiGap, originRect.yMin + UiGap * 2, originRect.width - UiGap * 2, originRect.height - UiGap * 3);
	}


	void OnGUI() {
		this.Columns = new float[10];
		this.Columns [0] = GetWidth(0.0f);
		this.Columns [1] = this.Columns [0] + UiGap;
		this.Columns [2] = this.Columns [1] + GetWidth (0.3f);
		this.Columns [3] = this.Columns [2] + UiGap;
		this.Columns [4] = this.Columns [3] + GetWidth (0.1f);
		this.Columns [5] = this.Columns [4] + UiGap;
		this.Columns [6] = GetWidth (1.0f) - GetWidth (0.1f) - UiGap * 2;
		this.Columns [7] = this.Columns [6] + UiGap;
		this.Columns [8] = GetWidth (1.0f) - UiGap;
		this.Columns [9] = GetWidth (1.0f);

		this.Rows = new float[10];
		this.Rows[0] = GetHeight(0.0f);
		this.Rows[1] = this.Rows[0] + 30;
		this.Rows[2] = this.Rows[1] + UiGap;
		this.Rows [3] = this.Rows [2] + GetHeight (0.3f);
		this.Rows[4] = this.Rows[3] + UiGap;
		this.Rows [5] = this.Rows [4] + GetHeight (0.3f) - 30 - UiGap;
		this.Rows[6] = this.Rows[5] + UiGap;
		this.Rows [7] = this.Rows[4] + GetHeight (0.3f);
		this.Rows [8] = this.Rows [7] + UiGap;
		this.Rows [9] = GetHeight (1.0f);


		GUI.Box(new Rect(0,0,Screen.width,Screen.height), "Hive5 Spider Tester");
	

		GUI.Box(new Rect(Columns[1],Rows[2],Columns[2] - Columns[1],Rows[3]-Rows[2]), "CHANNELS");

		Rect playersRect = new Rect (Columns [1], Rows [4], Columns [2] - Columns [1], Rows [7] - Rows [4]); 
		GUI.Box(playersRect, "PLAYERS");

		if (GUI.Button (GetSideButtonRect (playersRect), "RUN") == true) {
			hive5Spider.GetPlayers ((successOfGetPlayers, result) => {
				GetPlayersResult castedResult = result as GetPlayersResult;
				players = string.Empty;

				if (castedResult == null)
					Debug.Log("castedResult is null");
				Debug.Log("I got result : " + castedResult.PlatformUserIds.Count.ToString());
				
				foreach (var item in castedResult.PlatformUserIds) {
					players+= item + "\n";
				}
			});
		}

		GUI.enabled = false;
		players = GUI.TextArea (GetTextAreaRect(playersRect), players);
		GUI.enabled = true;

		Rect messagesRect = new Rect(Columns[3],Rows[2],Columns[8] - Columns[3],Rows[5] - Rows[2]);
		GUI.Box(messagesRect, "MESSAGES");

		GUI.enabled = false;
		messages = GUI.TextArea (new Rect (messagesRect.xMin + UiGap, messagesRect.yMin + UiGap * 2, messagesRect.width - UiGap * 2, messagesRect.height - UiGap * 3), messages);
		//messages += "1";
		//Debug.Log (messages.Length);
		GUI.enabled = true;

		GUI.Box(new Rect(Columns[1],Rows[8],Columns[8] - Columns[1],Rows[9] - Rows[8]), "LOGS");

		inputMessage = GUI.TextField (new Rect (Columns [5], Rows [6], Columns [6] - Columns [5], Rows [7] - Rows [6]), inputMessage);

		if (TopicKindComboBox == null)
			Debug.Log ("TopicKindComboBox is null");
		
		if (TopicKindComboBoxItems == null)
			Debug.Log ("TopicKindComboBoxItems is null");

		int selectedIndex = TopicKindComboBox.GetSelectedItemIndex ();
		//Debug.Log ("selectedIndex was " + selectedIndex.ToString ());
		selectedIndex = TopicKindComboBox.List (new Rect (Columns[3], Rows[6], Columns[4] - Columns[3], Rows[7]-Rows[6]), TopicKindComboBoxItems [selectedIndex].text, TopicKindComboBoxItems, listStyle);
		//Debug.Log ("selectedIndex is changed to " + selectedIndex.ToString ());

	

		if (GUI.Button (new Rect (Columns [7], Rows [6], Columns [8] - Columns [7], Rows [7] - Rows [6]), "SEND")) {
			Dictionary<string, string> contents = new Dictionary<string, string>();
			contents.Add("content", inputMessage);

			if (hive5Spider == null)
				Debug.Log ("hive5Spider is Null");

			switch (selectedIndex) {
			case 0:
				//TopicKind = TopicKind.Channel;
				hive5Spider.SendChannelMessage(contents, (success, id) => {});
				break;
			case 1:
				//TopicKind = TopicKind.Private;
				hive5Spider.SendPrivateMessage("88000000000", contents, (success, id) => {});
				break;
			case 2:
				//TopicKind = TopicKind.Notice;
				hive5Spider.SendNoticeMessage(this.appSecret, contents, (success, id) => {});
				break;
			case 3:
				//TopicKind = TopicKind.System;
				hive5Spider.SendSystemMessage(contents, (success, id) => {});
				break;
			}		
		}
	}

	Rect GetSideButtonRect(Rect originRect)
	{
		return new Rect (originRect.xMax - 44, originRect.yMin + 2, 42, 20);
	}

	TopicKind CurrentTopicKind = TopicKind.Channel;

	float GetWidth(float proportion)
	{
		return GlobalMargin + (Screen.width - GlobalMargin * 2) * proportion;
	}

	float GetHeight(float proportion)
	{
		return GlobalMargin + (Screen.height - GlobalMargin * 2) * proportion;
	}
}
