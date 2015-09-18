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
    /// DataTable에 대한 모든 기능을 포함하는 클래스
    /// </summary>
    public class Hive5DataTable
    {
        /// <summary>
        /// DataTable 내용 가져오기
        /// </summary>
        /// <param name="name">DataTable 이름</param>
        /// <param name="revision">리비전</param>
        /// <param name="callback"></param>
		public void Get(string name, int? revision, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.DataTable.GetDataTable, name));

            var parameter = new Dictionary<string, string>();
            if (revision != null)
            {
                parameter.Add("revision", revision.ToString());
            }
         
            Hive5Http.Instance.GetHttpAsync(url, parameter, GetDataTableResponseBody.Load, callback);
		}	
    }
}
