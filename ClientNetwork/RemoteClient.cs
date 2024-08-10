using LiteNetLib;
using LiteNetLib.Utils;
using Timer = System.Timers.Timer;

namespace ClientNetwork;

public abstract class RemoteClient
{
    protected readonly NetManager Manager;
    protected readonly NetDataWriter Writer;
    protected NetPeer Server = null!;

    public RemoteClient()
    {
        var listener = new EventBasedNetListener();
        Manager = new NetManager(listener) { AutoRecycle = true };
        Writer = new NetDataWriter();
        
        listener.NetworkReceiveEvent += (_, reader, _, _) => OnMessage(reader);
    }

    public void Connect(string address, int port, int tps)
    {
        Manager.Start();
        Server = Manager.Connect(address, port, "");
        var tickRate = new Timer(1000.0 / tps);
        tickRate.Elapsed += (_, _) =>
        {
            Manager.PollEvents();
            OnTick();
        };
        tickRate.Start();
    }

    protected abstract void OnTick();
    
    protected abstract void OnMessage(NetDataReader reader);
}