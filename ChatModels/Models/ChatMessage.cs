using System;

namespace ChatModels.Models
{

    //EntityFramework Test Entity. No real purpose yet.

    public class ChatMessage
    {


        public int Id { get; set; }


        public string Message { get; set; }

        public ChatMessage(string message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public DateTime SentAt { get; set; }
    }
}
