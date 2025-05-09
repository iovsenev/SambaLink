using System.IO.Pipes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SambaLink.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private NamedPipeClientStream pipeClient;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void StartServiceButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            pipeClient = new NamedPipeClientStream(".", "SambaLinkPipe", PipeDirection.Out);
            pipeClient.Connect();
            SendMessageToService("Start");
            MessageBox.Show("Service started");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void StopServiceButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (pipeClient != null && pipeClient.IsConnected)
            {
                SendMessageToService("Stop");
                MessageBox.Show("Service Stopped");
                pipeClient.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error: {ex.Message}");
        }
    }

    // Метод для отправки сообщений сервису через Named Pipe
    private void SendMessageToService(string message)
    {
        if (pipeClient != null && pipeClient.IsConnected)
        {
            var messageBytes = Encoding.UTF8.GetBytes(message);
            pipeClient.Write(messageBytes, 0, messageBytes.Length);
        }
    }
}