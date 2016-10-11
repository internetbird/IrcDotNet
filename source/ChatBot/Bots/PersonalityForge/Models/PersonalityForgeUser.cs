using Newtonsoft.Json;

namespace ChatBot.Bots.PersonalityForge.Models
{
    public class PersonalityForgeUser
    {
        /// <summary>
        /// the speaker's first name or user name (max 32 characters)
        /// </summary>
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }


        /// <summary>
        /// the speaker's last name (max 32 characters)
        /// </summary>
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The user's gender ('m' or 'f')
        /// </summary>
        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        /// <summary>
        /// a unique, consistent ID for the speaker to differentiate him/her from others in your system 
        /// (max 32 characters)
        /// </summary>
        [JsonProperty(PropertyName = "externalID")]
        public string ExternalID { get; set; }
    }
}