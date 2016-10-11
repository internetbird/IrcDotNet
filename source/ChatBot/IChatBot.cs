using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatBot
{
   public interface IChatBot
    {
        string GetResponse(string text);
    }
}
