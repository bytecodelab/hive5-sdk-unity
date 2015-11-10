using Hive5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    /// <summary>
    /// 구독용 Topic 종류
    /// </summary>
    public enum TopicKind
    {
        /// <summary>
        /// 전체 사용자 대상
        /// </summary>
        App,
        /// <summary>
        /// 특정 사용자 대상
        /// </summary>
        User,
        /// <summary>
        /// 특정 존 내 사용자 대상
        /// </summary>
        Zone,
        /// <summary>
        /// 특정 룸 내 사용자 대상
        /// </summary>
        Room,
        /// <summary>
        /// 임시. 확장용
        /// </summary>
        Temp,
        NotTopic,
    };

    public class SpiderTopic
    {
        private const string TopicPrefix = "io.hive5.spider.topic";
        public const string UserTopicPrefix = TopicPrefix + ".user";
        public const string ZoneTopicPrefix = TopicPrefix + ".zone";
        public const string AppTopicPrefix = TopicPrefix + ".app";
        public const string RoomTopicPrefix = TopicPrefix + ".room";

        public string TopicUri { get; private set; }
        public TopicKind TopicKind { get; private set; }

        public SpiderTopic(string topicUri)
        {
            TopicUri = topicUri;
            TopicKind = GetTopicKind(topicUri);
        }

        public static SpiderTopic CreateUserTopic(User user)
        {
            var topicUri = string.Format("{0}.{1}.{2}", UserTopicPrefix, user.platform, user.id);
            return new SpiderTopic(topicUri);
        }

        public static TopicKind GetTopicKind(string topicUri)
        {
            if (string.IsNullOrEmpty(topicUri) == true)
                return TopicKind.NotTopic;

            if (topicUri.StartsWith(TopicPrefix) == false)
                return TopicKind.NotTopic;

            string rest = topicUri.Substring(TopicPrefix.Length);
            if (rest.Length == 0)
                return TopicKind.NotTopic;

            var elements = rest.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length == 0)
                return TopicKind.NotTopic;

            string topicKindHint = elements[0].ToLower();

            switch (topicKindHint)
            {
                default:
                    return TopicKind.Temp;

                case "app":
                    return TopicKind.App;

                case "room":
                    return TopicKind.Room;

                case "zone":
                    return TopicKind.Zone;

                case "user":
                    return TopicKind.User;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is SpiderTopic == false)
                return false;

            SpiderTopic topic = obj as SpiderTopic;
            if (topic.TopicUri == this.TopicUri)
                return true;

            return false;
        }

        /// <summary>
        /// SpiderTopic으로부터 User를 추출하여 반환합니다.
        /// </summary>
        /// <returns>사용자</returns>
        public User TryGetUser()
        {
            if (this.TopicKind != Models.TopicKind.User)
                return null;

            string rest = this.TopicUri.Substring(TopicPrefix.Length);
            var elements = rest.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            if (elements.Length != 3)
                throw new ArgumentException("UserTopicUri is wrong format");

            var user = new User
            {
                platform = elements[1].ToLower().Trim(),
                id = elements[2].ToLower().Trim()
            };

            return user;
        }
    }
}
