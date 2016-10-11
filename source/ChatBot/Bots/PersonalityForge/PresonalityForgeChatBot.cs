using ChatBot.Bots.PersonalityForge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatBot
{
    public class PersonalityForgeChatBot : IChatBot
    {
        private const string PersonalityForgeUriFormat = "http://www.personalityforge.com/api/chat/?apiKey={0}&hash={1}&message={2}";
        private const string ApiKey = "CT0QRqF4OCULaSsQ";
        private const string ApiSecret = "HIm7ZEm674kimIa55BctuHI37i736z6p";
        private const int ChatBotID = 145863; 
        private const string BotExternalID = "IrcBOT";



        /// <summary>
        /// Returns the chat bot response for the given message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetResponse(string message)
        {

            var messageResponse = new MessageResponse();

            try
            {
                var url = BuildUrl(message);

                var client = new WebClient();

                string response = client.DownloadString(url);

                messageResponse = JsonConvert.DeserializeObject<MessageResponse>(response);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ChatBot error occured {ex.Message}");
            }


            return messageResponse.Success ? messageResponse.Message.Message : string.Empty;
           
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private string BuildUrl(string message)
        {

            var request = CreateMessageRequest(message);

            string requestJson =  JsonConvert.SerializeObject(request);
            string requestHash = GetRequestHash(requestJson);

            var url = string.Format(PersonalityForgeUriFormat,
                                    ApiKey, requestHash, System.Web.HttpUtility.UrlEncode(requestJson));

            return url;

        }


        /// <summary>
        /// Gets the SHA256 hash of the request object
        /// </summary>
        /// <param name="requestJson"></param>
        /// <returns></returns>
        private string GetRequestHash(string requestJson)
        {
            byte[] keyBytes = Encoding.ASCII.GetBytes(ApiSecret);
            byte[] requestBytes = Encoding.ASCII.GetBytes(requestJson);

            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(requestBytes);

                return ByteArrayToHexString(hashmessage);
            }

        }

        private string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
                
            return hex.ToString();
        }


        /// <summary>
        /// Create a pesonality forge request from the given text message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MessageRequest CreateMessageRequest(string message)
        {
            var request = new MessageRequest();

            var requestMessage = new PesonalityForgeRequestMessage
            {
                Message = message,
                ChatBotID = ChatBotID,
                TimeStamp = GetUTCTimeStamp()

            };

            var requestUser = new PersonalityForgeUser
            {
                ExternalID = BotExternalID,
                FirstName = string.Empty,
                LastName = string.Empty,
                Gender = string.Empty
            };
          
            request.Message = requestMessage;
            request.User = requestUser;

            return request;
        }

        /// <summary>
        /// Gets the UTC Unix timestamp
        /// </summary>
        /// <returns></returns>
        private int GetUTCTimeStamp()
        {
            var timeDiff = DateTime.UtcNow - new DateTime(1970, 1, 1);
            var totaltime = (int)timeDiff.TotalSeconds;

            return totaltime;
        }
    }
}
