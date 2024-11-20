using ConsoleMessenger;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Messenger;

namespace WindowsFormsClient
{
    public partial class Form1 : Form
    {
        private static int MessageID = 0;
        private static string UserName;
        private static MessangerClientAPI API = new MessangerClientAPI();
        public Form1()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, EventArgs e)
        {
            string UserName = UserNameTB.Text;
            string Message = MessageTB.Text;
            if ((UserName.Length > 1) && (Message.Length > 1))
            {
                Messenger.Message msg = new Messenger.Message(UserName, Message, DateTime.Now);
                API.SendMessageRestSharp(msg);
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            var getMessage = new Func<Task>(async () =>
            {
                Messenger.Message msg = await API.GetMessageHTTPAsync(MessageID);
                while (msg != null)
                {
                    MessagesLB.Items.Add(msg);
                    MessageID++;
                    msg = await API.GetMessageHTTPAsync(MessageID);
                }
            });
            getMessage.Invoke();
        }
    }
}
