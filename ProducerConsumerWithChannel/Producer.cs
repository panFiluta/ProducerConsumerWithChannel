using System.Threading.Channels;

namespace ProducerConsumerWithChannel;

internal class Producer
{
    private readonly ChannelWriter<int> _writer;
    public Producer(ChannelWriter<int> writer)
    {
        _writer = writer;
        Task.WaitAll(this.Run());
    }

    private async Task Run()
    {
        var r = new Random();

        while (await _writer.WaitToWriteAsync()) 
        {
            var item = r.Next(0, 100);
            await _writer.WriteAsync(item);
            Console.WriteLine($"Writing {item}");
            Thread.Sleep(200);
        }
    }
}
