using LiteNetLib;
using LiteNetLib.Utils;
using Timer = System.Timers.Timer;

namespace Server;

public abstract class RemoteServer
{
    protected readonly NetManager Manager;
    private readonly NetDataWriter _writer;

    protected RemoteServer()
    {
        var listener = new EventBasedNetListener();
        Manager = new NetManager(listener);
        _writer = new NetDataWriter();

        listener.ConnectionRequestEvent += OnConnectionRequest;
        listener.PeerConnectedEvent += OnPeerConnected;
        listener.PeerDisconnectedEvent += OnPeerDisconnected;
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

    protected void Send<T>(T obj, NetPeer peer) where T : INetSerializable
    {
        _writer.Put(obj);
        peer.Send(_writer, DeliveryMethod.ReliableOrdered);
        _writer.Reset();
    }

    protected abstract void OnTick();
    
    protected abstract void OnConnectionRequest(ConnectionRequest request);

    protected abstract void OnPeerConnected(NetPeer peer);
    
    protected abstract void OnPeerDisconnected(NetPeer peer, DisconnectInfo info);
}