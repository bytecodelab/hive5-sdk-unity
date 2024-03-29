﻿using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class UnsubscribeMessage : SpiderRequestMessage
    {
        public long SubscriptionId { get; set; }

        public UnsubscribeMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.UNSUBSCRIBE;
        }

        public override string ToMessageString()
        {
            List<object> messageObjects = new List<object>();
            messageObjects.Add(this.MessageCode);
            messageObjects.Add(this.RequestId);
            messageObjects.Add(this.SubscriptionId);

            string jsonMessage = JsonMapper.ToJson(messageObjects);
            return jsonMessage;
        }
    }
}
