using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Models
{
    public class PlayerMetadata
    {
        public long? Level { get; set; }
        public long? Exp { get; set; }
        public long? Stage { get; set; }

        public object Hero { get; set; }
        public object Play { get; set; }
        public object Extras { get; set; }

        public PlayerMetadata()
        {

        }

        public PlayerMetadata(long? level, long? exp, long? stage, object hero, object play, object extras)
        {
            this.Level = level;
            this.Exp = exp;
            this.Stage = stage;
            this.Hero = hero;
            this.Play = play;
            this.Extras = extras;
        }

        public object ToRequestBody()
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            if (this.Level != null)
                dict.Add("level", (long)this.Level);

            if (this.Exp != null)
                dict.Add("exp", (long)this.Exp);

            if (this.Stage != null)
                dict.Add("stage", (long)this.Stage);

            if (this.Hero != null)
                dict.Add("hero", this.Hero);

            if (this.Play != null)
                dict.Add("play", this.Play);

            if (this.Extras != null)
                dict.Add("extras", this.Extras);

            return dict;
        }
    }
}
