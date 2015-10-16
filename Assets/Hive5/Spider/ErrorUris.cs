using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider
{
    public class ErrorUris
    {
        //Peer is not authorized to access the given resource. This might be triggered by a session trying to join a realm, a publish, subscribe, register or call.
        public static string NotAuthorized = "wamp.error.not_authorized";

        //Peer wanted to join a non-existing realm (and the Router did not allow to auto-create the realm).
        public static string NoSuchRealm = "wamp.error.no_such_realm";

        //The Peer is shutting down completely - used as a GOODBYE (or ABORT) reason.
        public static string SystemShutdown = "wamp.error.system_shutdown";

        //The Peer want to leave the realm - used as a GOODBYE reason.
        public static string CloseRealm = "wamp.error.close_realm";
        
        //A Peer acknowledges ending of a session - used as a GOOBYE reply reason.
        public static string GoodbyeAndOut = "wamp.error.goodbye_and_out";

        //A Dealer could not perform a call, since the procedure called does not exist.
        public static string NoSuchProcedure = "wamp.error.no_such_procedure";

        //A Broker could not perform a unsubscribe, since the given subscription is not active.
        public static string NoSuchSubscription = "wamp.error.no_such_subscription";

        //A Dealer could not perform a unregister, since the given registration is not active.
        public static string NoSuchRegistration = "wamp.error.no_such_registration";

        //A call failed, since the given argument types or values are not acceptable to the called procedure.
        public static string InvalidArgument = "wamp.error.invalid_argument";

        //A publish failed, since the given topic is not acceptable to the Broker.
        public static string InvalidTopic = "wamp.error.invalid_topic";

        //A procedure could not be registered, since a procedure with the given URI is already registered (and the Dealer is not able to set up a distributed registration).
        public static string ProcedureAlreadyExists = "wamp.error.procedure_already_exists";
    };
}
