using System.Net.Sockets;
using System.Text;

namespace ConsoleClient
{
    class Program
    {
        // TODO debug so that there is a connection with the server. Correct the error is HTTP/1.1 408 Request Timeout.
        const int port = 5000;
        const string address = "127.0.0.1";
        static void Main(string[] args)
        {
            Console.Write("Enter your name:");
            string userName = Console.ReadLine();
            TcpClient client = null;
            try
            {
                client = new TcpClient(address, port);
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    Console.Write(userName + ": ");
                    // enter message
                    string message = Console.ReadLine();
                    message = ($"{userName}: {message}");
                    // convert the message into a byte array
                    byte[] data = Encoding.Unicode.GetBytes(message);
                    // sending a message
                    stream.Write(data, 0, data.Length);
                    // get the answer
                    data = new byte[64]; // buffer for received data
                    StringBuilder builder = new StringBuilder();
                    int bytes = 0;
                    do
                    {
                        bytes = stream.Read(data, 0, data.Length);
                        builder.Append(Encoding.UTF8.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);

                    message = builder.ToString();
                    Console.WriteLine($"Server: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }
        }
    }
}