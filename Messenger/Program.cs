using Messenger;

namespace ConsoleMessenger
{
    class Program
    {
        private static int MessageID;
        private static string UserName;
        private static MessangerClientAPI API = new MessangerClientAPI();

        private static void GetNewMessages()
        {
            Message msg = API.GetMessage(MessageID);
            while (msg != null)
            {
                Console.WriteLine(msg);
                MessageID++;
                msg = API.GetMessage(MessageID);
            }
        }
        static void Main(string[] args)
        {
            MessageID = 1;
            Console.WriteLine("Enter your name:");
            UserName = Console.ReadLine();
            string MessageText = "A new user has connected to the server.";
            Message Sendmsg = new Message(UserName, MessageText, DateTime.Now);
            API.SendMessage(Sendmsg);
            while (MessageText != "exit")
            {
                GetNewMessages();

                Console.WriteLine("Enter your message:");
                MessageText = Console.ReadLine();
                if (MessageText.Length > 1)
                {
                    //{ "UserName":"Alex","MessageText":"Hi!","TimeStamp":"2024-08-14T09:29:22.6061978+03:00"}
                    //Alex < 14.08.2024 9:29:22 >: Hi!
                    Sendmsg = new Message(UserName, MessageText, DateTime.Now);
                    API.SendMessage(Sendmsg);
                }
            }
        }
    }
}