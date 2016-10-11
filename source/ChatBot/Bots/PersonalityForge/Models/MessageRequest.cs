using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot.Bots.PersonalityForge.Models
{
    public class MessageRequest
    {
        public PesonalityForgeMessage Message { get; set; }
        public PersonalityForgeUser User { get; set; }
    }
}
