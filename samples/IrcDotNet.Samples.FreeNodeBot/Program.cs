using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IrcDotNet.Samples.FreeNodeBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IrcBot bot = null;

            try
            {
            
                Console.WriteLine("Starting IRC Friendly bot");

                // Create and run bot.
                bot = new FriendlyIrcBot();
                bot.Run();
            }

            catch (Exception ex)
            {
                ConsoleUtilities.WriteError("Fatal error: " + ex.Message);
                Environment.ExitCode = 1;
            }
            finally
            {
                if (bot != null)
                    bot.Dispose();
            }
        }
    }
}
