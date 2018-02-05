using System;

namespace Test.Models
{
    public class ChatModel
    {
        public int Chat_Id { get; set; }

        public int Patient_Id { get; set; }

        public Guid User_Id { get; set; }

        public DateTime Create_Dt { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public bool IsMe { get; set; }

        public bool IsSystem { get; set; }
    }
}