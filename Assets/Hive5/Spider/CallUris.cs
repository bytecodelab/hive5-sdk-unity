using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider
{
    public class CallUris
    {
        private const string BaseUri = "io.hive5.spider.rpc";

        public const string CreateRoom = BaseUri + "." + "room.create";
        public const string JoinRoom = BaseUri + "." + "room.join";
        public const string JoinRandomRoom = BaseUri + "." + "room.join.random";
        public const string LeaveRoom = BaseUri + "." + "room.leave";
        public const string ListRooms = BaseUri + "." + "rooms.list";
        public const string ListMembers = BaseUri + "." + "room.members.list";
    }
}
