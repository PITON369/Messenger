using Messenger;
using Newtonsoft.Json;

Message msg = new Message("Alex", "Hi!", DateTime.Now);
string output = JsonConvert.SerializeObject(msg);
Console.WriteLine(output);
Message deserializeMsg = JsonConvert.DeserializeObject<Message>(output);
Console.WriteLine(deserializeMsg);
//{ "UserName":"Alex","MessageText":"Hi!","TimeStamp":"2024-08-14T09:29:22.6061978+03:00"}
//Alex < 14.08.2024 9:29:22 >: Hi!
