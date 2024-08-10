using LiteNetLib;
using LiteNetLib.Utils;
using Timer = System.Timers.Timer;

namespace Server;

public abstract class RemoteServer
{
    protected readonly NetManager Manager;
    protected readonly NetDataWriter Writer;

    protected RemoteServer()
    {
        var listener = new EventBasedNetListener();
        Manager = new NetManager(listener) { AutoRecycle = true };
        Writer = new NetDataWriter();

        Manager.AutoRecycle = true;
        listener.ConnectionRequestEvent += OnConnectionRequest;
        listener.PeerConnectedEvent += OnConnected;
        listener.PeerDisconnectedEvent += OnDisconnected;
        listener.NetworkReceiveEvent += (client, reader, _, _) => OnMessage(client, reader);
    }

    public void Start(int port, int tps)
    {
        Manager.Start(port);
        var tickRate = new Timer(1000.0 / tps);
        tickRate.Elapsed += (_, _) =>
        {
            Manager.PollEvents();
            OnTick();
        };
        tickRate.Start();
    }

    protected abstract void OnTick();
    
    protected abstract void OnConnectionRequest(ConnectionRequest request);

    protected abstract void OnConnected(NetPeer client);

    protected abstract void OnDisconnected(NetPeer client, DisconnectInfo info);
    
    protected abstract void OnMessage(NetPeer client, NetDataReader reader);
}