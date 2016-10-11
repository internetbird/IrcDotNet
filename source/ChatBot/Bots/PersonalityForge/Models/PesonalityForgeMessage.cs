using Newtonsoft.Json;

namespace ChatBot.Bots.PersonalityForge.Models
{
    public class PesonalityForgeMessage
    {
        /// <summary>
        /// what you're saying to the chat bot
        /// </summary>
        [JsonProperty(PropertyName ="message")]
        public string Message { get; set; }

        /// <summary>
        /// the ID of the chat bot you're talking to
        /// </summary>
        [JsonProperty(PropertyName = "chatBotID")]
        public int ChatBotID { get; set; }


        /// <summary>
        /// the current UTC timestamp
        /// </summary>
        [JsonProperty(PropertyName = "chatBotID")]
        public int TimeStamp { get; set; }


    }
}