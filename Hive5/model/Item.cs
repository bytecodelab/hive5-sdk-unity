﻿using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace Hive5.model
{
    public class RechargeInfo
    {
        public int max;
        public int rechargesInSec;
        public DateTime nextRechargesAt;

        public RechargeInfo(int max, int rechargesInSec, DateTime nextRechargesAt)
        {
            this.max = max;
            this.rechargesInSec = rechargesInSec;
            this.nextRechargesAt = nextRechargesAt;
        }

        public static RechargeInfo Load(JsonData json)
        {
            if (json == null)
                return null;

            var max = (int)json["max"];
            var rechargesInSec = (int)json["recharges_in_sec"];
            var nextRechargesAt = Util.ParseDateTime((string)json["next_recharges_at"]);

            return new RechargeInfo(max, rechargesInSec, nextRechargesAt);
        }
    }

    public class Item
    {
        public string key;
        public int value;
        public bool? locked;
        public RechargeInfo regargeInfo;

        public Item(string key, int value, bool? locked, RechargeInfo rechargeInfo)
        {
            this.key = key;
            this.value = value;
            this.locked = locked;
            this.regargeInfo = rechargeInfo;
        }

        public static Dictionary<string, Item> Load(JsonData json)
        {
            var items = new Dictionary<string, Item>();

            if (json == null)
                return items;

            if (json.IsObject == false)
                throw new InvalidJsonException("invalid items");

            foreach (string key in (json as System.Collections.IDictionary).Keys)
            {
                var value = (int)json[key]["value"];
                bool? locked = null;

                try
                {
                    locked = (bool)json[key]["locked"];
                }
                catch (KeyNotFoundException e)
                {
                    locked = null;
                }

                RechargeInfo rechargeInfo = null;

                try
                {
                    rechargeInfo = RechargeInfo.Load((JsonData)json[key]["recharge_info"]);
                }
                catch (KeyNotFoundException e)
                {
                    rechargeInfo = null;
                }

                var item = new Item(key, value, locked, rechargeInfo);
                items.Add(key, item);
            }

            return items;
        }
    }
}
