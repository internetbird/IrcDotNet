using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrcDotNet.Samples.FreeNodeBot
{
    public class FriendlyIrcBot : BasicIrcBot
    {

        private Dictionary<string, int> m_programmingLangVotes;

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

        protected override void InitializeChatCommandProcessors()
        {
            base.InitializeChatCommandProcessors();

            ChatCommandProcessors.Add("my-fav-prog-lang", ProcessChatCommandMyFavoriteProgrammingLanguage);
            ChatCommandProcessors.Add("results", ProcessChatCommandFavoriteProgrammingLanguageResults);
        }

        private void ProcessChatCommandFavoriteProgrammingLanguageResults(IrcClient client, IIrcMessageSource source, IList<IIrcMessageTarget> targets, string command, IList<string> parameters)
        {

            client.LocalUser.SendMessage(targets, "-------------------------------------------");
            client.LocalUser.SendMessage(targets, "Favorite Programming Language Results");
            client.LocalUser.SendMessage(targets, "-------------------------------------------");
          
            foreach (string language in m_programmingLangVotes.Keys)
            {
                int numOfVotes = m_programmingLangVotes[language];
                client.LocalUser.SendMessage(targets, $"{language}\t:{numOfVotes} " + (numOfVotes == 1 ? "vote" : "votes"));
            }
          
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

            client.LocalUser.SendMessage(targets, $"{source.Name}, thanks for voting for {favLangName} programming language!");
            client.LocalUser.SendMessage(targets, $"{favLangName} got {langCurrentVotes} votes so far");
           
        }

        
    }
}
