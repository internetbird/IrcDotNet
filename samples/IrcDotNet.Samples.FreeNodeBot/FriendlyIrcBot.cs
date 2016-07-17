using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcDotNet.Samples.FreeNodeBot
{
    public class FriendlyIrcBot : BasicIrcBot
    {

        private Dictionary<string, int> m_programmingLangVotes;
        private const string FreeNodeServerAddress = "irc.freenode.net";

        public FriendlyIrcBot() : base()
        {
            m_programmingLangVotes = new Dictionary<string, int>();
        }


        public override IrcRegistrationInfo RegistrationInfo
        {
            get
            {
                return new IrcUserRegistrationInfo {NickName = "FriendlyBot", UserName = "FriendlyBot", RealName = "Friendly Bot"} ;
            }
        }

        protected override void InitializeCommandProcessors()
        {
            base.InitializeCommandProcessors();

            this.CommandProcessors.Add("joinchannel", ProcessCommandJoinChannel);
        }

        private void ProcessCommandJoinChannel(string command, IList<string> parameters)
        {
            var serverName = Clients[0].ServerName;

            var client = GetClientFromServerNameMask(serverName);
            var channelName = parameters[0];

            client.Channels.Join(channelName);

        }

        protected override void InitializeChatCommandProcessors()
        {
            base.InitializeChatCommandProcessors();

            ChatCommandProcessors.Add("fav-prog-lang", ProcessChatCommandMyFavoriteProgrammingLanguage);
            ChatCommandProcessors.Add("voteon", ProcessChatCommandMyFavoriteProgrammingLanguage);
            ChatCommandProcessors.Add("results", ProcessChatCommandFavoriteProgrammingLanguageResults);
        }

        private void ProcessChatCommandFavoriteProgrammingLanguageResults(IrcClient client, IIrcMessageSource source, IList<IIrcMessageTarget> targets, string command, IList<string> parameters)
        {

            var sb = new StringBuilder();

            sb.Append("Favorite Programming Language Results: ");
           
            foreach (string language in m_programmingLangVotes.Keys)
            {
                int numOfVotes = m_programmingLangVotes[language];
                sb.Append($"{language}:{numOfVotes} " + (numOfVotes == 1 ? "vote" : "votes") + " ,");
            }

            client.LocalUser.SendMessage(targets, sb.ToString());

        }

        private void ProcessChatCommandMyFavoriteProgrammingLanguage(IrcClient client, IIrcMessageSource source, IList<IIrcMessageTarget> targets, string command, IList<string> parameters)
        {
            if (parameters.Count() != 1)
            {
                client.LocalUser.SendMessage(targets,
                    "Please vote for you favorite programming language like this (for example): my-fav-prog-lang Python");

                return;
            }

            string favLangKey = parameters[0].ToLower();
            string favLangName = favLangKey[0].ToString().ToUpper();

            if (favLangKey.Length > 1)
            {
                favLangName += favLangKey.Substring(1);
            }

            if (m_programmingLangVotes.ContainsKey(favLangKey))
            {
                m_programmingLangVotes[favLangKey]++;
            }
            else
            {
                m_programmingLangVotes.Add(favLangKey, 1);
            }

            int langCurrentVotes = m_programmingLangVotes[favLangKey];

            var votingMessage = $"{source.Name}, thanks for voting for {favLangName} programming language! {favLangName} got {langCurrentVotes} votes so far";

            client.LocalUser.SendMessage(targets, votingMessage);
           
        }

        protected override void OnChannelMessageReceived(IrcChannel channel, IrcMessageEventArgs e)
        {
         
            if (e.Source is IrcUser && e.Text.IndexOf(RegistrationInfo.NickName, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                var client = channel.Client;
                client.LocalUser.SendMessage(e.Targets, $"Hello, {e.Source.Name}!");
            }
        }


        protected override void OnChannelUserJoined(IrcChannel channel, IrcChannelUserEventArgs e)
        {
            channel.Client.LocalUser.SendMessage(channel.Name, $"Hello, {e.ChannelUser.User.NickName}!");
        }


        public override
            void Run()
        {
            //Automatically connnect to freenode
            Connect(FreeNodeServerAddress, RegistrationInfo);

            base.Run();
        }
    }
}
