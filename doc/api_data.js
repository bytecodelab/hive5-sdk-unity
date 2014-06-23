define({ api: [
  {
    "type": "GET",
    "url": "GetAgreements",
    "title": "약관 동의 내역보기",
    "version": "1.0.0",
    "name": "GetAgreements",
    "group": "Auth",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetAgreements(callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientAuth.cs"
  },
  {
    "type": "GET",
    "url": "Login",
    "title": "로그인",
    "version": "1.0.0",
    "name": "Login",
    "group": "Auth",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "os",
            "optional": false,
            "description": "OSType"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "objectKeys",
            "optional": false,
            "description": "object key 리스트"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "configKeys",
            "optional": false,
            "description": "config key 리스트"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "platform",
            "optional": false,
            "description": "플랫폼 Type"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "platformUserId",
            "optional": false,
            "description": "플랫폼 UserId(카카오 ID, GOOGLE ID, FACEBOOK ID ....)"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "platformSDKVersion",
            "optional": false,
            "description": "플랫폼 Version( 3, 4, 5...)"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "string userId \t\t= \"88197xxxx07226176\";\nstring sdkVersion \t= \"3\";\nvar objectKeys \t= new string[] {\"\"};\nvar configKeys \t= new string[] {\"time_event\",\"last_version\"};\nHive5Client hive5 = Hive5Client.Instance;\nhive5.Login (OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, response => {\nLogger.Log (\"response = \"+ response.ResultData);\n});\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientAuth.cs"
  },
  {
    "type": "post",
    "url": "Logout",
    "title": "로그아웃",
    "version": "1.0.0",
    "name": "Logout",
    "group": "Auth",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "field": "userId",
            "optional": false,
            "description": "유저 ID"
          },
          {
            "group": "Parameter",
            "type": "String",
            "field": "accessToken",
            "optional": false,
            "description": "Login SDK 에서 응답 받은 accessToken"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.Logout(userId, accessToken, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientAuth.cs"
  },
  {
    "type": "POST",
    "url": "SubmitAgreements",
    "title": "약관 동의",
    "version": "1.0.0",
    "name": "SubmitAgreements",
    "group": "Auth",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "generalVersion",
            "optional": false,
            "description": "약관 버전"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "partnershipVersion",
            "optional": false,
            "description": "파트너쉽 버전"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.SubmitAgreements(generalVersion, partnershipVersion, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientAuth.cs"
  },
  {
    "type": "POST",
    "url": "SwitchPlatform",
    "title": "로그인 플랫폼 바꾸기",
    "version": "1.0.0",
    "name": "SwitchPlatform",
    "group": "Auth",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "platformType",
            "optional": false,
            "description": "플랫폼타입 PlatformType.Kakao 등"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "platformUserId",
            "optional": false,
            "description": "플랫폼 사용자 아이디"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.SwitchPlatform(PlatformType.Kakao, platformUserId, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientAuth.cs"
  },
  {
    "type": "POST",
    "url": "Unregister",
    "title": "탈퇴",
    "version": "1.0.0",
    "name": "Unregister",
    "group": "Auth",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.Unregister(callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientAuth.cs"
  },
  {
    "type": "POST",
    "url": "Init",
    "title": "SDK 초기화",
    "version": "2.0.0",
    "name": "Init",
    "group": "Init",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "field": "appKey",
            "optional": false,
            "description": "Hive5 발급 AppKey"
          },
          {
            "group": "Parameter",
            "type": "String",
            "field": "uuid",
            "optional": false,
            "description": "디바이스 고유 UUID"
          },
          {
            "group": "Parameter",
            "type": "Hive5APIZone",
            "field": "zone",
            "optional": false,
            "description": "접속 서버 선택(Beta OR Real)"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.Init ( \"a40e4122-99d9-44a6-xxxx-68ed756f79d6\", \"747474747\", Hive5APIZone.Beta );\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5Client.cs"
  },
  {
    "type": "GET",
    "url": "GetMyScore",
    "title": "내 랭킹 확인",
    "version": "1.0.0",
    "name": "GetMyScore",
    "group": "Leaderboard",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "leaderboardId",
            "optional": false,
            "description": "리더보드 ID"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rankMin",
            "optional": false,
            "description": "랭킹 최저"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rankMax",
            "optional": false,
            "description": "랭킹 최고"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rangeMin",
            "optional": false,
            "description": "점수 범위 최저"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rangeMax",
            "optional": false,
            "description": "점수 범위 최고"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetMyScore(leaderboardId, rangeMin, rangeMax, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientLeaderboard.cs"
  },
  {
    "type": "GET",
    "url": "GetScores",
    "title": "랭킹 가져오기",
    "version": "1.0.0",
    "name": "GetScores",
    "group": "Leaderboard",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "leaderboardId",
            "optional": false,
            "description": "리더보드 ID"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rankMin",
            "optional": false,
            "description": "랭킹 최저"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rankMax",
            "optional": false,
            "description": "랭킹 최고"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rangeMin",
            "optional": false,
            "description": "점수 범위 최저"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "rangeMax",
            "optional": false,
            "description": "점수 범위 최고"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetScores(leaderboardId, rankMin, rankMax, rangeMin, rangeMax, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientLeaderboard.cs"
  },
  {
    "type": "GET",
    "url": "GetSocialScores",
    "title": "친구 랭킹 가져오기",
    "version": "1.0.0",
    "name": "GetSocialScores",
    "group": "Leaderboard",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "leaderboardId",
            "optional": false,
            "description": "리더보드 ID"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetSocialScores(leaderboardId, CallBack callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientLeaderboard.cs"
  },
  {
    "type": "POST",
    "url": "SubmitScore",
    "title": "점수 기록",
    "version": "1.0.0",
    "name": "SubmitScore",
    "group": "Leaderboard",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "leaderboardId",
            "optional": false,
            "description": "리더보드 ID"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "score",
            "optional": false,
            "description": "기록 점수"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.SubmitScore(leaderboardId, score, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientLeaderboard.cs"
  },
  {
    "type": "POST",
    "url": "AttachMailTags",
    "title": "메일 TAG 추가",
    "version": "1.0.0",
    "name": "AttachMailTags",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "mailId",
            "optional": false,
            "description": "메일 ID"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "tags",
            "optional": false,
            "description": "추가할 TAG"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.AttachMailTags(mailId, tags, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "POST",
    "url": "CreateMail",
    "title": "메일 생성하기",
    "version": "1.0.0",
    "name": "CreateMail",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "content",
            "optional": false,
            "description": "메일 본문"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "friendPlatformUserId",
            "optional": false,
            "description": "받는사람 플랫폼 UserId"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "tags",
            "optional": false,
            "description": "메일 Tags"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CreateMail(content, friendPlatformUserId, tags, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "POST",
    "url": "DeleteAllMail",
    "title": "메일 전체(특정범위) 삭제",
    "version": "1.0.0",
    "name": "DeleteAllMail",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "fromMailId",
            "optional": false,
            "description": "삭제 메일 시작점 ID"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "toMailId",
            "optional": false,
            "description": "삭제 메일 끝점 ID"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.DeleteAllMail(fromMailId, toMailId, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "POST",
    "url": "DeleteMail",
    "title": "메일 삭제",
    "version": "1.0.0",
    "name": "DeleteMail",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "mailId",
            "optional": false,
            "description": "특정 메일 이후의 리스트 받기 위한 mail id"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "content",
            "optional": false,
            "description": "메일 본문"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.DeleteMail(mailId, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "POST",
    "url": "DetachMailTags",
    "title": "메일 TAG 제거",
    "version": "1.0.0",
    "name": "DetachMailTags",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "mailId",
            "optional": false,
            "description": "메일 ID"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "tags",
            "optional": false,
            "description": "제거할 TAGS"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.DetachMailTags(mailId, tags, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "GET",
    "url": "GetMailCount",
    "title": "메일 갯수 확인",
    "version": "1.0.0",
    "name": "GetMailCount",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "OrderType",
            "field": "order",
            "optional": false,
            "description": "메일 순서"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "afterMailId",
            "optional": false,
            "description": "특정 메일 이후의 리스트 받기 위한 mail id"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "tags",
            "optional": false,
            "description": "메일 Tag"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetMailCount(order, afterMailId, tag, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "GET",
    "url": "GetMails",
    "title": "메일 리스트 가져오기",
    "version": "1.0.0",
    "name": "GetMails",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "int",
            "field": "limit",
            "optional": false,
            "description": "받을 메일 갯수"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "order",
            "optional": false,
            "description": "메일 순서"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "afterMailId",
            "optional": false,
            "description": "특정 메일 이후의 리스트 받기 위한 mail id"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "tags",
            "optional": false,
            "description": "메일 Tag"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetMails(limit, order, afterMailId, tag, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "POST",
    "url": "UpdateMail",
    "title": "메일 수정",
    "version": "1.0.0",
    "name": "UpdateMail",
    "group": "Mail",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "mailId",
            "optional": false,
            "description": "특정 메일 이후의 리스트 받기 위한 mail id"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "content",
            "optional": false,
            "description": "메일 본문"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.UpdateMail(mailId, content, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMail.cs"
  },
  {
    "type": "POST",
    "url": "GetCompletedMissions",
    "title": "완료 미션 리스트 가져오기",
    "version": "1.0.0",
    "name": "BatchCompleteMission",
    "group": "Mission",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetCompletedMissions(callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMission.cs"
  },
  {
    "type": "POST",
    "url": "CompleteMission",
    "title": "미션 완료",
    "version": "1.0.0",
    "name": "CompleteMission",
    "group": "Mission",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "missionKey",
            "optional": false,
            "description": "완료할 미션 키"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CompleteMission(missionKey, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientMission.cs"
  },
  {
    "type": "POST",
    "url": "CreateObjects",
    "title": "오브젝트 생성",
    "version": "1.0.0",
    "name": "CreateObjects",
    "group": "Object",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "List&#60HObject&#62",
            "field": "objects",
            "optional": false,
            "description": "오브젝트 리스트(class 프로퍼티만 셋팅)"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CreateObjects( objects, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientObject.cs"
  },
  {
    "type": "POST",
    "url": "DestroyObjects",
    "title": "오브젝트 제거",
    "version": "1.0.0",
    "name": "DestroyObjects",
    "group": "Object",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "List&#60HObject&#62",
            "field": "objects",
            "optional": false,
            "description": "오브젝트 리스트(class, id 프로퍼티 셋팅 / Singleton 인경우 id 생략)"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.DestroyObjects( objects, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientObject.cs"
  },
  {
    "type": "GET",
    "url": "GetObjects",
    "title": "오브젝트 리스트",
    "version": "1.0.0",
    "name": "GetObjects",
    "group": "Object",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "List&#60HObject&#62",
            "field": "objects",
            "optional": false,
            "description": "오브젝트 리스트(class 프로퍼티만 셋팅)"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetObjects(objectKeys, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientObject.cs"
  },
  {
    "type": "POST",
    "url": "SetObjects",
    "title": "오브젝트 저장",
    "version": "1.0.0",
    "name": "SetObjects",
    "group": "Object",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "List&#60HObject&#62",
            "field": "objects",
            "optional": false,
            "description": "오브젝트 리스트(class 프로퍼티만 셋팅)"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.SetObjects(objects, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientObject.cs"
  },
  {
    "type": "POST",
    "url": "CallProcedure",
    "title": "Procedure 호출",
    "version": "1.0.0",
    "name": "CallProcedure",
    "group": "Procedure",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "field": "procedureName",
            "optional": false,
            "description": "호출 Procedure 이름"
          },
          {
            "group": "Parameter",
            "type": "TupleList&#60;string, string&#62;",
            "field": "parameters",
            "optional": false,
            "description": "파라미터"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CallProcedure(procedureName, parameters, callback)\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5Client.cs"
  },
  {
    "type": "POST",
    "url": "CompleteApplePurchase",
    "title": "애플 결제 완료",
    "version": "1.0.0",
    "name": "CompleteApplePurchase",
    "group": "Purchase",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "id",
            "optional": false,
            "description": "결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "listPrice",
            "optional": false,
            "description": "표시 가격"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "purchasedPrice",
            "optional": false,
            "description": "결제 가격"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "currency",
            "optional": false,
            "description": "화폐"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "receipt",
            "optional": false,
            "description": "영수증 데이터"
          },
          {
            "group": "Parameter",
            "type": "bool",
            "field": "isSandbox",
            "optional": false,
            "description": "sandbox 여부"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CompleteApplePurchase(id, listPrice, purchasedPrice, currency, receipt, isSandbox, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientPurchase.cs"
  },
  {
    "type": "POST",
    "url": "CompleteGooglePurchase",
    "title": "구글 결제 완료",
    "version": "1.0.0",
    "name": "CompleteGooglePurchase",
    "group": "Purchase",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "id",
            "optional": false,
            "description": "결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "listPrice",
            "optional": false,
            "description": "표시 가격"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "purchasedPrice",
            "optional": false,
            "description": "결제 가격"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "currency",
            "optional": false,
            "description": "화폐"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "purchaseData",
            "optional": false,
            "description": "결제 데이터"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "signature",
            "optional": false,
            "description": "결제 데이터 검증용 sign"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CompleteGooglePurchase(id, listPrice, purchasedPrice, currency, purchaseData, signature, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientPurchase.cs"
  },
  {
    "type": "POST",
    "url": "CompleteNaverPurchase",
    "title": "네이버 결제 완료",
    "version": "1.0.0",
    "name": "CompleteNaverPurchase",
    "group": "Purchase",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "id",
            "optional": false,
            "description": "결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "listPrice",
            "optional": false,
            "description": "표시 가격"
          },
          {
            "group": "Parameter",
            "type": "long",
            "field": "purchasedPrice",
            "optional": false,
            "description": "결제 가격"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "currency",
            "optional": false,
            "description": "화폐"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CreateNaverPurchase(productCode, paymentSequence, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientPurchase.cs"
  },
  {
    "type": "POST",
    "url": "CreateApplePurchase",
    "title": "애플 결제 시작",
    "version": "1.0.0",
    "name": "CreateApplePurchase",
    "group": "Purchase",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "productCode",
            "optional": false,
            "description": "상품 코드"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "receiverPlatformUserId",
            "optional": false,
            "description": "선물 받을 플랫폼 User ID"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "mailForReceiver",
            "optional": false,
            "description": "메일로 받을 경우"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CreateApplePurchase(productCode, receiverPlatformUserId, mailForReceiver, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientPurchase.cs"
  },
  {
    "type": "POST",
    "url": "CreateGooglePurchase",
    "title": "구글 결제 시작",
    "version": "1.0.0",
    "name": "CreateGooglePurchase",
    "group": "Purchase",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "productCode",
            "optional": false,
            "description": "상품 코드"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "receiverPlatformUserId",
            "optional": false,
            "description": "선물 받을 플랫폼 User ID"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "mailForReceiver",
            "optional": false,
            "description": "메일로 받을 경우"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CreateGooglePurchase(productCode, receiverPlatformUserId, mailForReceiver, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientPurchase.cs"
  },
  {
    "type": "POST",
    "url": "CreateNaverPurchase",
    "title": "네이버 결제 시작",
    "version": "1.0.0",
    "name": "CreateNaverPurchase",
    "group": "Purchase",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "productCode",
            "optional": false,
            "description": "상품 코드"
          },
          {
            "group": "Parameter",
            "type": "string",
            "field": "paymentSequence",
            "optional": false,
            "description": "결제 시퀀스"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CreateNaverPurchase(productCode, paymentSequence, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientPurchase.cs"
  },
  {
    "type": "POST",
    "url": "UpdatePushToken",
    "title": "Push 토큰 등록 및 업데이트",
    "version": "1.0.0",
    "name": "UpdatePushToken",
    "group": "Push",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "field": "platform",
            "optional": false,
            "description": "플랫폼 Type( Android, iOS )"
          },
          {
            "group": "Parameter",
            "type": "String",
            "field": "token",
            "optional": false,
            "description": "push 토큰"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.UpdatePushToken( platform, token, callback)\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5Client.cs"
  },
  {
    "type": "POST",
    "url": "ApplyReward",
    "title": "보상 전체 받기",
    "version": "1.0.0",
    "name": "ApplyAllRewards",
    "group": "Reward",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "bool",
            "field": "deleteMail",
            "optional": false,
            "description": "보상 받을 시 메일 함께 삭제 여부"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.ApplyAllRewards(deleteMail, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientReward.cs"
  },
  {
    "type": "POST",
    "url": "ApplyReward",
    "title": "보상 받기",
    "version": "1.0.0",
    "name": "ApplyReward",
    "group": "Reward",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "rewardId",
            "optional": false,
            "description": "상품 코드"
          },
          {
            "group": "Parameter",
            "type": "bool",
            "field": "deleteMail",
            "optional": false,
            "description": "보상 받을 시 메일 함께 삭제 여부"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.ApplyReward(rewardId, deleteMail, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientReward.cs"
  },
  {
    "type": "GET",
    "url": "GetRewardInfo",
    "title": "보상 정보",
    "version": "1.0.0",
    "name": "GetRewardInfo",
    "group": "Reward",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "long",
            "field": "rewardId",
            "optional": false,
            "description": "상품 코드"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetRewardInfo(rewardId, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientReward.cs"
  },
  {
    "type": "POST",
    "url": "InvalidateReward",
    "title": "보상 무효화",
    "version": "1.0.0",
    "name": "InvalidateReward",
    "group": "Reward",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "bool",
            "field": "deleteMail",
            "optional": false,
            "description": "보상 받을 시 메일 함께 삭제 여부"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.InvalidateReward(rewardId, deleteMail, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientReward.cs"
  },
  {
    "type": "GET",
    "url": "CheckNicknameAvailability",
    "title": "닉네임 사용 가능여부 확인",
    "version": "1.0.0",
    "name": "CheckNicknameAvailability",
    "group": "Settings",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "nickname",
            "optional": false,
            "description": "닉네임"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.CheckNicknameAvailability(\"gilbert\", callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSettings.cs"
  },
  {
    "type": "POST",
    "url": "SetNickname",
    "title": "닉네임 설정",
    "version": "1.0.0",
    "name": "SetNickname",
    "group": "Settings",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "nickname",
            "optional": false,
            "description": "닉네임"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.SetNickname(\"gilbert\", callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSettings.cs"
  },
  {
    "type": "POST",
    "url": "AddFriends",
    "title": "친구 리스트 add",
    "version": "1.0.0",
    "name": "AddFriends",
    "group": "SocialGraph",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "groupName",
            "optional": false,
            "description": "A group name which add friends into"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "friend_ids",
            "optional": false,
            "description": "친구 ID 리스트"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.AddFriends(groupName, friend_ids, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSocialgraph.cs"
  },
  {
    "type": "GET",
    "url": "GetFriends",
    "title": "친구 리스트 가져오기 from a group",
    "version": "1.0.0",
    "name": "GetFriends",
    "group": "SocialGraph",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "groupName",
            "optional": false,
            "description": "a group name which retrieve friends from"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetFriends(groupName, CallBack callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSocialgraph.cs"
  },
  {
    "type": "GET",
    "url": "GetFriendsInfo",
    "title": "친구 리스트 가져오기",
    "version": "1.0.0",
    "name": "GetFriendsInfo",
    "group": "SocialGraph",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "friend_ids",
            "optional": false,
            "description": "친구 ID 리스트"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.GetFriendsInfo(platformUserIds, CallBack callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSocialgraph.cs"
  },
  {
    "type": "POST",
    "url": "RemoveFriends",
    "title": "친구 리스트 remove",
    "version": "1.0.0",
    "name": "RemoveFriends",
    "group": "SocialGraph",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "groupName",
            "optional": false,
            "description": "A group name which remove friends from"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "friend_ids",
            "optional": false,
            "description": "친구 ID 리스트"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.RemoveFriends(groupName, friend_ids, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSocialgraph.cs"
  },
  {
    "type": "POST",
    "url": "UpdateFriends",
    "title": "친구 리스트 업데이트",
    "version": "1.0.0",
    "name": "UpdateFriends",
    "group": "SocialGraph",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "string",
            "field": "groupName",
            "optional": false,
            "description": "A group name to be updated"
          },
          {
            "group": "Parameter",
            "type": "string[]",
            "field": "friend_ids",
            "optional": false,
            "description": "친구 ID 리스트"
          },
          {
            "group": "Parameter",
            "type": "Callback",
            "field": "callback",
            "optional": false,
            "description": "콜백 함수"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultCode",
            "optional": false,
            "description": "Error Code 참고"
          },
          {
            "group": "Success 200",
            "type": "String",
            "field": "resultMessage",
            "optional": false,
            "description": "요청 실패시 메시지"
          }
        ]
      }
    },
    "examples": [
      {
        "title": "Example usage:",
        "content": "Hive5Client hive5 = Hive5Client.Instance;\nhive5.UpdateFriends(friend_ids, callback);\n"
      }
    ],
    "filename": "Assets/Hive5/Hive5ClientSocialgraph.cs"
  }
] });