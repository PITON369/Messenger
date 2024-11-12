using Messenger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMessenger
{
    class MessangerClientAPI
    {
        public void TestNewtonsoftJson()
        {
            // Test JSon SerializeObject NewtonSoft
            Message msg = new Message("Alex", "Hi", DateTime.UtcNow);
            string output = JsonConvert.SerializeObject(msg);
            Console.WriteLine(output);
            Message deserializedMsg = JsonConvert.DeserializeObject<Message>(output);
            Console.WriteLine(deserializedMsg);
        }

        public void TestNewtonsoftJsonWithWritingInFile()
        {
            // Test JSon SerializeObject NewtonSoft
            Message msg = new Message("Alex", "Hi", DateTime.UtcNow);
            string output = JsonConvert.SerializeObject(msg);
            Console.WriteLine(output);
            // Create the file.
            string path = @"d:\temp\ser.txt";
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(output);
            }
        }

        public Message GetMessage(int messageId)
        {
            try
            {
                WebRequest request = WebRequest.Create($"http://localhost:5000/api/Messanger/{messageId}");
                request.Method = "GET";

                using (WebResponse response = request.GetResponse())
                {
                    string status = ((HttpWebResponse)response).StatusDescription;

                    using (Stream dataStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string responseFromServer = reader.ReadToEnd();

                        if (status.ToLower() == "ok" && responseFromServer != "Not found")
                        {
                            Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseFromServer);
                            return deserializedMsg;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return null;
        }

        public bool SendMessage(Message msg)
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:5000/api/Messanger");
                request.Method = "POST";

                //msg = new Message("Alex", "Hi", DateTime.Now);

                string postData = JsonConvert.SerializeObject(msg);
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                using (WebResponse response = request.GetResponse())
                using (Stream dataStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(dataStream))
                {
                    string responseFromServer = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            return true;
        }
    }
}