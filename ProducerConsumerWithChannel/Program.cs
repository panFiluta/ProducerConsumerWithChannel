using ProducerConsumerWithChannel;
using System.Threading.Channels;


var options = new BoundedChannelOptions(10);
options.FullMode = BoundedChannelFullMode.Wait;

var boundedChannel = Channel.CreateBounded<int>(options);

var unboundedChannel = Channel.CreateUnbounded<int>();


var p = Task.Run(() => {
    new Producer(boundedChannel.Writer);
});

var c1 = Task.Run(() => {
    new Consumer(boundedChannel.Reader);
});

var c2 = Task.Run(() => {
    new Consumer(boundedChannel.Reader);
});

Task.WaitAll(p, c1, c2);

//var p = Task.Run(() => {
//    new Producer(unboundedChannel.Writer);    
//});

//var c1 = Task.Run(() => {
//    new Consumer(unboundedChannel.Reader);
//});

//var c2 = Task.Run(() => {
//    new Consumer(unboundedChannel.Reader);
//});

//Task.WaitAll(p, c1, c2);