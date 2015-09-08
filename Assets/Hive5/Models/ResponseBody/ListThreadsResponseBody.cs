using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{
	/// <summary>
	/// Result of ListThreads
	/// </summary>
	public class ListThreadsResponseBody : CommonResponseBody
	{
        public List<ForumThread> Threads { get; set; }

        public static IResponseBody Load(JsonData json)
        {
            if (json == null)
                return null;

            List<ForumThread> threads = new List<ForumThread>();

            try
            {
                threads = ForumThread.LoadList(json["threads"]);
            }
            catch (KeyNotFoundException)
            {
            }

            return new ListThreadsResponseBody()
            {
                Threads = threads,
            };
        }
	}
}

