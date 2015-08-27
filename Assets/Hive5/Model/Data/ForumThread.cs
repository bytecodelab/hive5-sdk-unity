using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Model
{
    public class ForumThread
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public JsonData Extras { get; set; }
        public DateTime CreatedAt { get; set; }

        public static ForumThread Load(JsonData jsonData)
        {
            ForumThread thread = new ForumThread();

            try
            {
                if (jsonData.ContainsKey("id")) { 
                    thread.Id = jsonData["id"].ToInt();
                }
                if (jsonData.ContainsKey("title")) { 
                    thread.Title = (string)jsonData["title"];
                }
                if (jsonData.ContainsKey("content")) { 
                    thread.Content = (string)jsonData["content"];
                }
                if (jsonData.ContainsKey("extras")) { 
                    thread.Extras = jsonData["extras"];
                }
                if (jsonData.ContainsKey("created_at")) { 
                    thread.CreatedAt = DateTime.Parse((string)jsonData["created_at"]);
                }   
            }
            catch (Exception ex) {}

            return thread;
        }

        public static List<ForumThread> LoadList(JsonData items)
        {
            if (items == null)
                return new List<ForumThread>();

            var threads = new List<ForumThread>();
            for (int i = 0; i < items.Count; i++)
            {
                threads.Add(ForumThread.Load(items[i]));
            }

            return threads;
        }
    }
}
