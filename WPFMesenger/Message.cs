using System;

namespace Messenger
{
    [Serializable]
    public class Message
    {
        public Message()
        {
            UserName = "System";
            MessageText = "Server is running...";
            TimeStamp = DateTime.Now;
        }

        public Message(string userName, string messageText, DateTime timeStamp)
        {
            UserName = userName;
            MessageText = messageText;
            TimeStamp = timeStamp;
        }

        public string UserName { get; set; }
        public string MessageText{ get; set; }
        public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
            string output = String.Format($"{UserName} <{TimeStamp}>: {MessageText}");
            return output;
        }
    }
}