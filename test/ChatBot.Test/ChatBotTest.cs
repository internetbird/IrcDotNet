using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ChatBot.Test
{
   [TestFixture]
   public class ChatBotTest
   {
       
        public ChatBotTest()
        {
        }
        

        [Test]
        public void TestPersonalityForgeBot()
        {
            var bot = new PersonalityForgeChatBot();

            var response = bot.GetResponse("How are you doing today?");

        }

        
        
   }
}
