using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Bots.PersonalityForge.Models
{
    /// <summary>
    /// PersonalityForge Message Response Object
    /// </summary>
    public class MessageResponse
    {
        /// <summary>
        /// 1 for a successful call, 0 for an error
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "success")]
        public bool Success { get; set; }

        /// <summary>
        /// empty if success. Otherwise a brief error message.
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "errorType")]
        public string ErrorType { get; set; }

        /// <summary>
        /// empty if success. Otherwise contains details of what went wrong and how to fix it
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The response message
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "message")]
        public PersonalityForgeResponseMessage Message { get; set; }

    }
}
