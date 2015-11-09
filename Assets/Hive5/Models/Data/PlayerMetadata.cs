using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Models
{
    public class PlayerMetadata
    {
        public long Level { get; set; }
        public long Exp { get; set; }
        public long Stage { get; set; }

        public object Hero { get; set; }
        public object Play { get; set; }
        public object Extras { get; set; }

        public PlayerMetadata()
        {

        }

        public PlayerMetadata(long level, long exp, long stage, object hero, object play, object extras)
        {
            this.Level = level;
            this.Exp = exp;
            this.Stage = stage;
            this.Hero = hero;
            this.Play = play;
            this.Extras = extras;
        }

        public string ToJson()
        {
            return LitJson.JsonMapper.ToJson(this);
        }
    }
}
