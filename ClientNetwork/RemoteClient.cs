using LiteNetLib;
using LiteNetLib.Utils;
using Timer = System.Timers.Timer;

namespace ClientNetwork;

public abstract class RemoteClient
{
    protected readonly NetManager Manager;

    public RemoteClient()
    {
        var listener = new EventBasedNetListener();
        Manager = new NetManager(listener);

        listener.NetworkReceiveEvent += (_, reader, _, _) => OnNetworkReceiveEvent(reader);
    }
    
    public void Connect(string address, int port, int serverUpdatesPerSecond)
    {
        Manager.AutoRecycle = true;
        Manager.Start();
        Manager.Connect(address, port, "");
        var serverUpdateRate = new Timer(1000.0 / serverUpdatesPerSecond);
        serverUpdateRate.Elapsed += (_, _) => Manager.PollEvents();
        serverUpdateRate.Start();
    }
    
    protected abstract void OnNetworkReceiveEvent(NetDataReader reader);
}