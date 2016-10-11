using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Bots.PersonalityForge.Models
{
    /// <summary>
    /// PersonalityForge Message Request Object
    /// </summary>
    public class MessageRequest
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "message")]
        public PesonalityForgeRequestMessage Message { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "user")]
        public PersonalityForgeUser User { get; set; }
    }
}
