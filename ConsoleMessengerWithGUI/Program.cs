using System;
using Terminal.Gui;
using ConsoleMessenger;
using System.Collections.Generic;
using Messenger;

namespace ConsoleMessengerWithGUI
{
    class Program
    {
        // Message queue
        private static List<Message> messages = new List<Message>();
        private static MessangerClientAPI API = new MessangerClientAPI();

        private static MenuBar menu;
        private static Window winMain;
        private static Window winMessages;
        private static Label labelUser;
        private static TextField fieldUsername;
        private static Label labelMessage;
        private static TextField fieldMessage;
        private static Button btnSend;

        static void Main(string[] args)
        {
            Application.Init();
            // Top-level component
            var top = Application.Top;

            // The menu component
            menu = new MenuBar(new MenuBarItem[]
      {
                new MenuBarItem("_App", new MenuItem[]
                {
                    new MenuItem("_Quit", "Close the app", Application.RequestStop),
                }),
      });
            top.Add(menu);

            // The component of the main form
            winMain = new Window()
            {
                Title = "DotChat",
                // Window start position
                X = 0,
                Y = 1, //< Place under the menu
                       // Auto stretching according to the size of the terminal
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(winMain);

            // The message component
            winMessages = new Window()
            {
                X = 0,
                Y = 0,
                Width = winMain.Width,
                Height = winMain.Height - 2,
            };
            winMain.Add(winMessages);

            // The text "User"
            labelUser = new Label()
            {
                X = 0,
                Y = Pos.Bottom(winMain) - 5,
                Width = 15,
                Height = 1,
                Text = "Username:",
            };
            winMain.Add(labelUser);

            // User Name input field
            fieldUsername = new TextField()
            {
                X = 15,
                Y = Pos.Bottom(winMain) - 5,
                Width = winMain.Width - 14,
                Height = 1,
            };
            winMain.Add(fieldUsername);

            // The text of the "Message"
            labelMessage = new Label()
            {
                X = 0,
                Y = Pos.Bottom(winMain) - 4,
                Width = 15,
                Height = 1,
                Text = "Message:",
            };
            winMain.Add(labelMessage);

            // Message input field
            fieldMessage = new TextField()
            {
                X = 15,
                Y = Pos.Bottom(winMain) - 4,
                Width = winMain.Width - 14,
                Height = 1,
            };
            winMain.Add(fieldMessage);

            // The send button
            btnSend = new Button()
            {
                X = Pos.Right(winMain) - 10 - 5,
                Y = Pos.Bottom(winMain) - 4,
                Width = 10,
                Height = 1,
                Text = "  Send  ",
            };
            winMain.Add(btnSend);

            // Processing a button click
            btnSend.Clicked += OnBtnSendClick;

            // Starting a message update check cycle
            var updateLoop = new System.Timers.Timer();
            updateLoop.Interval = 1000;
            int MessageID = 0;
            updateLoop.Elapsed += (sender, eventArgs) =>
            {
                Message msg = API.GetMessage(MessageID);
                while (msg != null)
                {
                    messages.Add(msg);
                    UpdateMessages();
                    MessageID++;
                    msg = API.GetMessage(MessageID);
                }
            };
            updateLoop.Start();

            Application.Run();
            Console.Clear();
        }
        // When you click on the send button
        static void OnBtnSendClick()
        {
            if (fieldMessage.Text.ToString() != "" && fieldUsername.Text.ToString() != "")
            {
                var msg = new Message(fieldUsername.Text.ToString(), fieldMessage.Text.ToString(), DateTime.Now);
                API.SendMessage(msg);

                // Adding messages locally
                // This is not necessary, because the server will return the message we sent in the update cycle
                // messages.Add(msg);                
                // UpdateMessages();

                fieldMessage.Text = "";
            }
        }
        // Synchronizes the list of messages with the display
        static void UpdateMessages()
        {
            // Deleting all message components
            winMessages.RemoveAll();
            // We go in reverse order and add messages (the newest ones are at the top)
            int offset = 0;
            for (var i = messages.Count - 1; i >= 0; i--)
            {
                var msg = messages[i];
                winMessages.Add(new View()
                {
                    X = 0,
                    Y = offset,
                    Width = winMessages.Width,
                    Height = 1,
                    Text = msg.ToString(),
                });
                offset++;
            }
            // Updating the display
            Application.Refresh();
        }

    }
}