using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Models;
using Hive5.Util;

namespace Hive5
{
	/// <summary>
	/// Hive5 클라우드스크립트에 대한 모든 것을 포함한 클래스
	/// </summary>
    public class Hive5Script
    {
        /// <summary>
        /// 스크립트를 실행합니다.
        /// </summary>
        /// <param name="name">클라우드 스크립트 이름</param>
        /// <param name="parameters">전달할 파라미터 오브젝트</param>
        /// <param name="callback">콜백함수</param>
        /// <code language="cs">
        /// var parameters = new {
        ///   exp_gained = 100
        /// };
        /// Hive5.Script.RunScript("levelup", parameters, (response) =>  {
        ///     // your code here
        /// });
        /// </code>
        public void RunScript(string name, object parameters,  Callback callback)
		{
            string paramsString = parameters == null ? "{}" : LitJson.JsonMapper.ToJson(parameters);
            string scriptParams = string.Format("{{\"params\":{0} }}", paramsString);

			var url = Hive5Client.ComposeRequestUrl(String.Format(ApiPath.Script.RunScript, Hive5Http.EscapeData(name)));

			Hive5Http.Instance.PostHttpAsync(url, null, scriptParams, RunScriptResponseBody.Load, callback);	
		}

        /// <summary>
        /// 인증정보없이 스크립트를 실행합니다.
        /// </summary>
        /// <remarks>로그인 프로세스를 진행하기 전에 실행할 필요가 있는 스크립트를 실행합니다.</remarks>
        /// <param name="name">클라우드 스크립트 이름</param>
        /// <param name="parameters">전달할 파라미터 오브젝트</param>
        /// <param name="callback">콜백함수</param>
        /// <code language="cs">
        /// Hive5.Script.CheckScript("checkUpdates", null, (response) =>  {
        ///     // your code here
        /// });
        /// </code>
        public void CheckScript(string name, object parameters,  Callback callback)
		{
            string paramsString = parameters == null ? "{}" : LitJson.JsonMapper.ToJson(parameters);
            string scriptParams = string.Format("{{\"params\":{0} }}", paramsString);

			var url = Hive5Client.ComposeRequestUrl(String.Format(ApiPath.Script.CheckScript, Hive5Http.EscapeData(name)));

			Hive5Http.Instance.PostHttpAsync(url, null, scriptParams, RunScriptResponseBody.Load, callback);	
		}
    }
}
