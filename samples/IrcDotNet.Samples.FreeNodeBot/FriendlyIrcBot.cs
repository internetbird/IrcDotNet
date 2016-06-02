using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrcDotNet.Samples.FreeNodeBot
{
    public class FriendlyIrcBot : BasicIrcBot
    {
        public override IrcRegistrationInfo RegistrationInfo
        {
            get
            {
                return new IrcUserRegistrationInfo {NickName = "FriendlyBot", UserName = "FriendlyBot", RealName = "Friendly Bot"} ;
            }
        }

        protected override void InitializeChatCommandProcessors()
        {
            base.InitializeChatCommandProcessors();

            this.ChatCommandProcessors.Add("saysomething", ProcessChatCommandSaySomething);
        }

        private void ProcessChatCommandSaySomething(IrcClient client, IIrcMessageSource source, IList<IIrcMessageTarget> targets, string command, IList<string> parameters)
        {
            client.LocalUser.SendMessage(targets, "Hello From Friendly Bot!");

        }
    }
}
