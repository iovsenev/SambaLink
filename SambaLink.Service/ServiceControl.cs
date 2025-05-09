using System.IO.Pipes;
using System.Text;

namespace SambaLink.Service;
public class ServiceControl
{
    private NamedPipeServerStream pipeServer;

    public void Start()
    {
        pipeServer = new NamedPipeServerStream("SambaLinkPipe", PipeDirection.In);
        pipeServer.WaitForConnection();
        Console.WriteLine("Service Started");
        ListenForCommands();
    }

    private void ListenForCommands()
    {
        byte[] buffer = new byte[256];
        while (true)
        {
            int bytesRead = pipeServer.Read(buffer, 0, buffer.Length);
            string command = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            if (command == "Start")
            {
                // Логика для запуска сервиса
                Console.WriteLine("Service is running...");
            }
            else if (command == "Stop")
            {
                // Логика для остановки сервиса
                Console.WriteLine("Service Stopped");
                break; // Останавливаем цикл
            }
        }
    }
}
