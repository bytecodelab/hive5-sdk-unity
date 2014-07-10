using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Hive5;

public class SpiderTest : MonoBehaviour
{

    Hive5Client hive5Client { get { return Hive5Client.Instance; } }
    Hive5Spider hive5Spider;

    GUIContent[] TopicKindComboBoxItems;
    private ComboBox TopicKindComboBox = new ComboBox();
    private GUIStyle listStyle = new GUIStyle();

    bool spiderConnected = false;
    string appSecret = "gogogo";

    // Use this for initialization
    void Start()
    {
        Logger.LogOutput += Logger_LogOutput;

        BuildTopicKindComboBox();

        string userId = "88197948207226176";			// 카카오 user id
        //string accessToken 	= "bcl_token";					// 카카오 로그인 후 발급 받은 access token
        string sdkVersion = "3";							// 클라이언트에서 사용하고 있는 카카오 sdk의 버전
        string os = OSType.Android;				// 'android' 또는 'ios'
        string appKey = "a40e4122-99d9-44a6-b916-68ed756f79d6";
        string uuid = "46018";

        string[] objectKeys = new string[] { "" };		// 로그인 후 가져와야할 사용자 object의 key 목록
        string[] configKeys = new string[] { "time_event1" };	// 로그인 후 가져와야할 사용자 configuration의 key

        hive5Client.SetDebug();
        hive5Client.Init(appKey, uuid, Hive5APIZone.Beta);
        hive5Client.Login(os, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, response =>
        {
            Debug.Log("login response = " + response.ResultData);

            hive5Spider = new Hive5Spider(hive5Client);

            hive5Spider.Connect((success) =>
            {
                spiderConnected = success;

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
					
                    messages += string.Format("{0} {1} {2}\n", DateTime.Now, head, messageContents["message"]);
					
                };
                
                /*

                */
            });
        });
    }

    void Logger_LogOutput(string log)
    {
        logs += log + "\n";
    }

    string players = string.Empty;
    string channels = string.Empty;

    void BuildTopicKindComboBox()
    {
        TopicKindComboBoxItems = new GUIContent[4];
        this.TopicKindComboBoxItems[0] = new GUIContent("Channel");
        this.TopicKindComboBoxItems[1] = new GUIContent("Private");
        this.TopicKindComboBoxItems[2] = new GUIContent("Notice");
        this.TopicKindComboBoxItems[3] = new GUIContent("System");

        listStyle.normal.textColor = Color.white;
        listStyle.onHover.background =
            listStyle.hover.background = new Texture2D(2, 2);
        listStyle.padding.left =
            listStyle.padding.right =
                listStyle.padding.top =
                listStyle.padding.bottom = 4;
    }

    // Update is called once per frame
    void Update()
    {

    }

    float[] Columns;
    float[] Rows;

    const float UiGap = 10f;
    const float GlobalMargin = 10f;

    string inputMessage = "";
    string messages = "";

    bool CanSubscribeChannel = false;
    bool CanSubscribePrivate = false;
    bool CanSubscribeNotice = false;
    bool CanSubscribeSystem = false;

    long channelSubscriptionId = 0;
    long privateSubscriptionId = 0;
    long noticeSubscriptionId = 0;
    long systemSubscriptionId = 0;

    string logs = "";

    Rect GetTextAreaRect(Rect originRect)
    {
        return new Rect(originRect.xMin + UiGap, originRect.yMin + UiGap * 2, originRect.width - UiGap * 2, originRect.height - UiGap * 3);
    }

	Vector2 scrollPosition = Vector2.zero;

    void OnGUI()
    {
        this.Columns = new float[10];
        this.Columns[0] = GetWidth(0.0f);
        this.Columns[1] = this.Columns[0] + UiGap;
        this.Columns[2] = this.Columns[1] + GetWidth(0.3f);
        this.Columns[3] = this.Columns[2] + UiGap;
        this.Columns[4] = this.Columns[3] + GetWidth(0.1f);
        this.Columns[5] = this.Columns[4] + UiGap;
        this.Columns[6] = GetWidth(1.0f) - GetWidth(0.1f) - UiGap * 2;
        this.Columns[7] = this.Columns[6] + UiGap;
        this.Columns[8] = GetWidth(1.0f) - UiGap;
        this.Columns[9] = GetWidth(1.0f);

        this.Rows = new float[10];
        this.Rows[0] = GetHeight(0.0f);
        this.Rows[1] = this.Rows[0] + 30;
        this.Rows[2] = this.Rows[1] + UiGap;
        this.Rows[3] = this.Rows[2] + GetHeight(0.3f);
        this.Rows[4] = this.Rows[3] + UiGap;
        this.Rows[5] = this.Rows[4] + GetHeight(0.3f) - 30 - UiGap;
        this.Rows[6] = this.Rows[5] + UiGap;
        this.Rows[7] = this.Rows[4] + GetHeight(0.3f);
        this.Rows[8] = this.Rows[7] + UiGap;
        this.Rows[9] = GetHeight(1.0f);

        // HEADER
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Hive5 Spider Tester");

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // CHANNELS
        Rect channelsRect = new Rect(Columns[1], Rows[2], Columns[2] - Columns[1], Rows[3] - Rows[2]);
        GUI.Box(channelsRect, "CHANNELS");

        // CHANNELS BUTTON
        if (GUI.Button(GetSideButtonRect(channelsRect), "RUN") == true)
        {
            hive5Spider.GetChannels((successOfGetChannels, result) =>
            {
                GetChannelsResult castedResult = result as GetChannelsResult;
                channels = string.Empty;

                if (castedResult == null)
                    Debug.Log("castedResult is null");
                Debug.Log("I got result : " + castedResult.Channels.Count.ToString());

                foreach (var item in castedResult.Channels)
                {
                    channels += string.Format("[{0}]{1}({2})", item.app_id, item.channel_number, item.session_count);
                }
			});
        }

        // CHANNELS LIST
        GUI.enabled = false;
        channels = GUI.TextArea(GetTextAreaRect(channelsRect), channels);
        GUI.enabled = true;

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // PLAYERS
        Rect playersRect = new Rect(Columns[1], Rows[4], Columns[2] - Columns[1], Rows[7] - Rows[4]);
        GUI.Box(playersRect, "PLAYERS");

        // PLAYERS BUTTON
        if (GUI.Button(GetSideButtonRect(playersRect), "RUN") == true)
        {
            hive5Spider.GetPlayers((successOfGetPlayers, result) =>
            {
                GetPlayersResult castedResult = result as GetPlayersResult;
                players = string.Empty;

                if (castedResult == null)
                    Debug.Log("castedResult is null");
                Debug.Log("I got result : " + castedResult.PlatformUserIds.Count.ToString());

                foreach (var item in castedResult.PlatformUserIds)
                {
                    players += item + "\n";
                }
            });
        }

        // PLAYER LIST
        GUI.enabled = false;
        players = GUI.TextArea(GetTextAreaRect(playersRect), players);
        GUI.enabled = true;

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // SUBSCRIBES
        Rect subscribesRect = new Rect(Columns[1], Rows[8], Columns[2] - Columns[1], Rows[9] - Rows[8]);
        GUI.Box(subscribesRect, "SUBSCRIBES");

        #region Subscribes Checkboxes

        float yStep = subscribesRect.yMin + UiGap + 20;
        bool newCanSubscribeChannel = GUI.Toggle(new Rect(subscribesRect.xMin + UiGap, yStep, subscribesRect.width - UiGap * 2, 20), CanSubscribeChannel, "Topic.Channel");
        if (newCanSubscribeChannel != CanSubscribeChannel)
        {
            CanSubscribeChannel = newCanSubscribeChannel;
            if (newCanSubscribeChannel == true)
            {
                hive5Spider.Subscribe(TopicKind.Channel, (success, subscriptionId) =>
                    {
                        if (success == false)
                        {
                            CanSubscribeChannel = false;
                        }
                        else
                        {
                            channelSubscriptionId = subscriptionId;
                        }
                    });
            }
            else
            {
                if (channelSubscriptionId != 0)
                {
                    hive5Spider.Unsubscribe(channelSubscriptionId, (success) =>
                        {
                            if (success == false)
                            {
                                CanSubscribeChannel = true;
                            }
                            else
                            {
                                channelSubscriptionId = 0;
                            }

                        });
                }
            }
        }


        yStep += 20;
        bool newCanSubscribePrivate = GUI.Toggle(new Rect(subscribesRect.xMin + UiGap, yStep, subscribesRect.width - UiGap * 2, 20), CanSubscribePrivate, "Topic.Private");
        if (newCanSubscribePrivate != CanSubscribePrivate)
        {
            CanSubscribePrivate = newCanSubscribePrivate;
            if (newCanSubscribePrivate == true)
            {
                hive5Spider.Subscribe(TopicKind.Private, (success, subscriptionId) =>
                    {
                        if (success == false)
                        {
                            CanSubscribePrivate = false;
                        }
                        else
                        {
                            privateSubscriptionId = subscriptionId;
                        }
                    });
            }
            else
            {
                if (privateSubscriptionId != 0)
                {
                    hive5Spider.Unsubscribe(privateSubscriptionId, (success) =>
                        {
                            if (success == false)
                            {
                                CanSubscribePrivate = true;
                            }
                            else
                            {
                                privateSubscriptionId = 0;
                            }

                        });
                }
            }
        }

        yStep += 20;
        bool newCanSubscribeNotice = GUI.Toggle(new Rect(subscribesRect.xMin + UiGap, yStep, subscribesRect.width - UiGap * 2, 20), CanSubscribeNotice, "Topic.Notice");
        if (newCanSubscribeNotice != CanSubscribeNotice)
        {
            CanSubscribeNotice = newCanSubscribeNotice;
            if (newCanSubscribeNotice == true)
            {
                hive5Spider.Subscribe(TopicKind.Notice, (success, subscriptionId) =>
                    {
                        if (success == false)
                        {
                            CanSubscribeNotice = false;
                        }
                        else
                        {
                            noticeSubscriptionId = subscriptionId;
                        }
                    });
            }
            else
            {
                if (noticeSubscriptionId != 0)
                {
                    hive5Spider.Unsubscribe(noticeSubscriptionId, (success) =>
                        {
                            if (success == false)
                            {
                                CanSubscribeNotice = true;
                            }
                            else
                            {
                                noticeSubscriptionId = 0;
                            }

                        });
                }
            }
        }

        yStep += 20;
        bool newCanSubscribeSystem = GUI.Toggle(new Rect(subscribesRect.xMin + UiGap, yStep, subscribesRect.width - UiGap * 2, 20), CanSubscribeSystem, "Topic.System");
        if (newCanSubscribeSystem != CanSubscribeSystem)
        {
            CanSubscribeSystem = newCanSubscribeSystem;
            if (newCanSubscribeSystem == true)
            {
                hive5Spider.Subscribe(TopicKind.System, (success, subscriptionId) =>
                    {
                        if (success == false)
                        {
                            CanSubscribeSystem = false;
                        }
                        else
                        {
                            systemSubscriptionId = subscriptionId;
                        }
                    });
            }
            else
            {
                if (systemSubscriptionId != 0)
                {
                    hive5Spider.Unsubscribe(systemSubscriptionId, (success) =>
                        {
                            if (success == false)
                            {
                                CanSubscribeSystem = true;
                            }
                            else
                            {
                                systemSubscriptionId = 0;
                            }
                        });
                }
            }
        }
        
        #endregion Subscribes Checkboxes

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // MESSAGES
        Rect messagesRect = new Rect(Columns[3], Rows[2], Columns[8] - Columns[3], Rows[5] - Rows[2]);
        GUI.Box(messagesRect, "MESSAGES");

        GUI.enabled = false;
        messages = GUI.TextArea(new Rect(messagesRect.xMin + UiGap, messagesRect.yMin + UiGap * 2, messagesRect.width - UiGap * 2, messagesRect.height - UiGap * 3), messages);
        //messages += "1";
        //Debug.Log (messages.Length);
        GUI.enabled = true;


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // LOGS
        Rect logsRect = new Rect(Columns[3], Rows[8], Columns[8] - Columns[3], Rows[9] - Rows[8]);
		Rect logsTextAreaRect = GetTextAreaRect(logsRect);
		Rect relativeLogsRect = new Rect(0,0, logsTextAreaRect.width-UiGap, logsTextAreaRect.height -UiGap);
        GUI.Box(logsRect, "LOGS");

		scrollPosition = GUI.BeginScrollView (logsTextAreaRect,
		                                      scrollPosition, relativeLogsRect);

		logs = GUI.TextArea(relativeLogsRect, logs);

		GUI.EndScrollView ();


        //////////////////////////////////////////////////////////////////////////////////////////////////////
        // INPUT
        inputMessage = GUI.TextField(new Rect(Columns[5], Rows[6], Columns[6] - Columns[5], Rows[7] - Rows[6]), inputMessage);

        if (TopicKindComboBox == null)
            Debug.Log("TopicKindComboBox is null");

        if (TopicKindComboBoxItems == null)
            Debug.Log("TopicKindComboBoxItems is null");

        int selectedIndex = TopicKindComboBox.GetSelectedItemIndex();
        //Debug.Log ("selectedIndex was " + selectedIndex.ToString ());
        selectedIndex = TopicKindComboBox.List(new Rect(Columns[3], Rows[6], Columns[4] - Columns[3], Rows[7] - Rows[6]), TopicKindComboBoxItems[selectedIndex].text, TopicKindComboBoxItems, listStyle);
        //Debug.Log ("selectedIndex is changed to " + selectedIndex.ToString ());



        if (GUI.Button(new Rect(Columns[7], Rows[6], Columns[8] - Columns[7], Rows[7] - Rows[6]), "SEND"))
        {
            Dictionary<string, string> contents = new Dictionary<string, string>();
            contents.Add("message", inputMessage);

            if (hive5Spider == null)
                Debug.Log("hive5Spider is Null");

            switch (selectedIndex)
            {
                case 0:
                    //TopicKind = TopicKind.Channel;
					hive5Spider.SendChannelMessage(contents, (success, id) => { });
                    break;
                case 1:
                    //TopicKind = TopicKind.Private;
                    hive5Spider.SendPrivateMessage("88000000000", contents, (success, id) => { });
                    break;
                case 2:
                    //TopicKind = TopicKind.Notice;
                    hive5Spider.SendNoticeMessage(this.appSecret, contents, (success, id) => { });
                    break;
                case 3:
                    //TopicKind = TopicKind.System;
                    hive5Spider.SendSystemMessage(contents, (success, id) => { });
                    break;
            }

			// Initialize InputBox
			inputMessage = string.Empty;
        }
    }

    Rect GetSideButtonRect(Rect originRect)
    {
        return new Rect(originRect.xMax - 44, originRect.yMin + 2, 42, 20);
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
