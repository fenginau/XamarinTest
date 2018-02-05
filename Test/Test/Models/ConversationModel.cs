using System.Collections.Generic;

namespace Test.Models
{
    public class ConversationModel
    {
        public int ConvId{ get; set; }

        public List<ChatModel> Chats { get; set; }

    }
}