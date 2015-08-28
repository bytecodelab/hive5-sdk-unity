using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using Hive5.Util;

namespace Hive5
{
   	/// <summary>
	/// Hive5 client.
	/// </summary>
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
        /** 
		* @api {POST} RunScript 구글 결제 상태확인
		* @apiVersion 0.3.11-beta
		* @apiName RunScript
		* @apiGroup Script
		*
		* @apiParam {string} name 스크립트 이름
        * @apiParam {object} parameters 스크립트 파라미터
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
        * var parameters = new { id = 2 };
		* hive5.RunScript("your-script-name", parameters,  callback);
		*/
        public void RunScript(string name, object parameters,  Callback callback)
		{
            string paramsString = LitJson.JsonMapper.ToJson(parameters);
            string scriptParams = string.Format("{{\"params\":{0} }}", paramsString);

			var url = Hive5Client.Instance.ComposeRequestUrl(String.Format(ApiPath.Script.RunScript, Hive5.Hive5Client.EscapeData(name)));

			PostHttpAsync(url, null, scriptParams, RunScriptResponseBody.Load, callback);	
		}

        /** 
		* @api {POST} CallProcedureWithoutAuth 로그인하지 않고 Procedure 호출
		* @apiVersion 0.3.11-beta
		* @apiName CallProcedure
		* @apiGroup Procedure
		*
		* @apiParam {string} procedureName 호출 Procedure 이름
		* @apiParam {object} parameters 파라미터 오브젝트(내부적으로 JSON화 됨)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CheckScript("your-script-name", "", callback)
		*/
        public void CheckScript(string name, object parameters,  Callback callback)
		{
            string paramsString = LitJson.JsonMapper.ToJson(parameters);
            string scriptParams = string.Format("{{\"params\":{0} }}", paramsString);

			var url = Hive5Client.Instance.ComposeRequestUrl(String.Format(ApiPath.Script.CheckScript, Hive5Client.EscapeData(name)));

			PostHttpAsync(url, null, scriptParams, RunScriptResponseBody.Load, callback);	
		}
    }
}
