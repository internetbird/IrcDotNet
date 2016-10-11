﻿using ChatBot.Bots.PersonalityForge.Models;
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
    public class PresonalityForgeChatBot : IChatBot
    {
        private const string PersonalityForgeUriFormat = "http://www.personalityforge.com/api/chat/?apiKey={0}&hash={1}&message={2}";
        private const string ApiKey = "CT0QRqF4OCULaSsQ";
        private const string ApiSecret = "HIm7ZEm674kimIa55BctuHI37i736z6p";
        private const int ChatBotID = 145863; //Shmulik
        private const string BotExternalID = "ShmulikBOT";



        /// <summary>
        /// Returns the chat bot response for the given message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public string GetResponse(string message)
        {

            var url = BuildUrl(message);

            var client = new WebClient();

            string response =  client.DownloadString(url);

            var responseObj = JsonConvert.DeserializeObject(response);

            return response;
           
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

            HMACSHA256 hmac = new HMACSHA256(keyBytes);


            byte[] hashBytes = hmac.ComputeHash(requestBytes);


            return Encoding.ASCII.GetString(hashBytes);

        }


        /// <summary>
        /// Create a pesonality forge request from the given text message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private MessageRequest CreateMessageRequest(string message)
        {
            var request = new MessageRequest();

            var requestMessage = new PesonalityForgeMessage
            {
                Message = message,
                ChatBotID = ChatBotID,
                TimeStamp = GetUTCTimeStamp()

            };

            var requestUser = new PersonalityForgeUser
            {
                ExternalID = BotExternalID
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
            var totaltime = (int)timeDiff.TotalMilliseconds;

            return totaltime;
        }
    }
}
