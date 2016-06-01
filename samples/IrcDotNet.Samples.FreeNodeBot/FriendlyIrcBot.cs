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


        protected override void OnChannelMessageReceived(IrcChannel channel, IrcMessageEventArgs e)
        {
            IrcClient client = channel.Client;

            if (e.Text == "bot speak!")
            {
                client.LocalUser.SendMessage(, );
            }
        }
    }
}
