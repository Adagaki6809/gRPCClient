using Grpc.Net.Client;
using SimpleGrpcService;
using System;
using System.Threading.Tasks;

namespace gRPCClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int i = 0;
            try
            {
                // создаем канал для обмена сообщениями с сервером
                // параметр - адрес сервера gRPC
                using var channel = GrpcChannel.ForAddress("http://31.28.242.78:5000");
                // создаем клиента
                var client = new Greeter.GreeterClient(channel);
                Console.Write("Введите имя: ");
                string name = Console.ReadLine();
                // обмениваемся сообщениями с сервером
                while (true)
                {
                    int oldCursorPosition = Console.CursorTop;
                    Console.Write("Введите сообщение: ");
                    string mes = Console.ReadLine();
                    if (mes == "exit" || mes == "учше" || mes == "выход")
                        return;
                    int newCursorPosition = Console.CursorTop;
                    int diff = newCursorPosition - oldCursorPosition;
                    Console.SetCursorPosition(0, Console.CursorTop - diff);
                    while(i < diff)
                    {
                        Console.Write(new string(' ', Console.WindowWidth));
                        i++;
                    }
                    i = 0;
                    Console.SetCursorPosition(0, Console.CursorTop - diff);

                    //Console.WriteLine($"[{name.ToUpper()}][{DateTime.Now:HH:mm}]: {mes}");        тестирование

                    var reply = await client.SayHelloAsync(new HelloRequest { Name = mes });
                    Console.WriteLine($"[{name.ToUpper()}][{DateTime.Now:HH:mm}]: {reply.Message}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
}