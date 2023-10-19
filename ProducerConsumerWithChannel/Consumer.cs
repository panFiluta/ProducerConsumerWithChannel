using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ProducerConsumerWithChannel;

internal class Consumer
{
    private readonly ChannelReader<int> _reader;

    public Consumer(ChannelReader<int> reader)
    {
        _reader = reader;
        Task.WaitAll(this.Run());
    }

    private async Task Run() 
    {
        while(await _reader.WaitToReadAsync())
        {
            var item = await _reader.ReadAsync();
            Console.WriteLine($"Reading {item}");
            Thread.Sleep(1000);
        }
    }
}
