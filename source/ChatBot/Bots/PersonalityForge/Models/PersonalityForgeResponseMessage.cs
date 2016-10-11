using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Bots.PersonalityForge.Models
{
    public class PersonalityForgeResponseMessage
    {
        /// <summary>
        /// the name of the chat bot
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "chatBotName")]
        public string ChatBotName { get; set; }

        /// <summary>
        /// the id of the chat bot
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "chatBotID")]
        public int ChatBotID { get; set; }

        /// <summary>
        /// the chat bot's response
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 	the chat bot's emotion or expression when responding
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "emotion")]
        public string Emotion { get; set; }

    }
}
