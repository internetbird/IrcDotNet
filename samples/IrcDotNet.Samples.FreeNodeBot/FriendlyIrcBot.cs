using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace IrcDotNet.Samples.FreeNodeBot
{
    public class FriendlyIrcBot : BasicIrcBot
    {

        private Dictionary<string, int> programmingLangVotes;
        private const string FreeNodeServerAddress = "irc.freenode.net";
        private List<string> userJoinedMessages;
        private List<string> botMentionsMessages;
        private Random random;

        private const string PersonalityForgeUriFormat = "http://www.personalityforge.com/api/chat/?apiKey={0}&hash={1}&message={2}";


        public FriendlyIrcBot() : base()
        {
            programmingLangVotes = new Dictionary<string, int>();
            userJoinedMessages = GetUserJoinedMessages();
            botMentionsMessages = GetBotMentionsMessages();
            random = new Random(DateTime.UtcNow.Millisecond);
        }

        private IrcClient Client
        {
            get
            {
                var serverName = Clients[0].ServerName;

                var client = GetClientFromServerNameMask(serverName);
                return client;
            }
        }

        private List<string> GetBotMentionsMessages()
        {
            var messages = new List<string>
            {
                "What did you mean, {0}?",
                "It's an intersting idea, {0}",
                "Where are you from {0}?",
                "It's a beautiful day outside, {0}",
                "I live inside on a computer, {0}"

            };

            return messages;

        }

        private List<string> GetUserJoinedMessages()
        {
            var messages = new List<string>
            {
                "Howdy, {0}!",
                "Nice to meet you, {0}!",
                "Welcome to the channel, {0}!",
                "A goodday to you, {0}",
                "Hello {0}, my name is FriendlyBot. Type '!help' to see what I can do"

            };

            return messages;
        }

        private string GetRandomMessage(List<string> messages)
        {
            int randomIndex = random.Next(0, messages.Count);

            return messages[randomIndex];
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
            this.CommandProcessors.Add("leavechannel", ProcessCommandLeaveChannel);
        }

     

        private void ProcessCommandLeaveChannel(string command, IList<string> parameters)
        {
           
            var channelName = parameters[0];
            Client.Channels.Leave(channelName);

        }

        private void ProcessCommandJoinChannel(string command, IList<string> parameters)
        {
            var channelName = parameters[0];
            Client.Channels.Join(channelName);

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
           
            foreach (string language in programmingLangVotes.Keys)
            {
                int numOfVotes = programmingLangVotes[language];
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

            if (programmingLangVotes.ContainsKey(favLangKey))
            {
                programmingLangVotes[favLangKey]++;
            }
            else
            {
                programmingLangVotes.Add(favLangKey, 1);
            }

            int langCurrentVotes = programmingLangVotes[favLangKey];

            var votingMessage = $"{source.Name}, thanks for voting for {favLangName} programming language! {favLangName} got {langCurrentVotes} votes so far";

            client.LocalUser.SendMessage(targets, votingMessage);
           
        }

        protected override void OnChannelMessageReceived(IrcChannel channel, IrcMessageEventArgs e)
        {
         
            if (e.Source is IrcUser && e.Text.IndexOf(RegistrationInfo.NickName, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                var client = channel.Client;
                var message = string.Format(GetRandomMessage(botMentionsMessages), e.Source.Name);

                client.LocalUser.SendMessage(e.Targets, message);
            }
        }

        protected override void OnChannelUserJoined(IrcChannel channel, IrcChannelUserEventArgs e)
        {
            var result = CQ.CreateFromUrl("http://www.quotationspage.com/random.php3");

            var quote = result.Select("dt.quote a").FirstOrDefault();
            var author = result.Select("dd.author b a").FirstOrDefault();

            if (quote != null && author != null)
            {
                var message = $"{quote.InnerText} , ({author.InnerText})";

                channel.Client.LocalUser.SendMessage(channel.Name, message);
            }
        }

        protected override void OnCommandNotRecognized(IrcClient client, string command, IList<IIrcMessageTarget> defaultReplyTarget)
        {
          //Do nothing...
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
