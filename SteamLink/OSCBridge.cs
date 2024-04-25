using System.Net;
using Rug.Osc;

namespace Impressive;

public class OSCBridge
{
    public bool Listening { get; private set; }

    public int InPort => Impressive.Config!.GetValue(Impressive.In_Port_Config);
    public int OutPort => Impressive.Config!.GetValue(Impressive.Out_Port_Config);
    public string OutAddress => Impressive.Config!.GetValue(Impressive.Out_Address_Config);

    public EventHandler<OscPacket>? ReceivedPacket;
    private Thread? listenThread;
    private CancellationTokenSource tkSrc = new();

    public bool TryStartListen()
    {
        Impressive.Msg("Trying to start OSC listener");
        if (listenThread != null && listenThread.ThreadState == ThreadState.Running)
            return false;
        
        Impressive.Msg("Starting OSC listening thread");
        tkSrc = new();
        try
        {
            Impressive.Msg("Creating receiver and sender");
            OscReceiver recv = new(IPAddress.Any, InPort);
            OscSender send = new(IPAddress.Parse(OutAddress), OutPort);

            Impressive.Msg("Creating thread loop");
            listenThread = new(new ThreadStart(() => ListenLoop(recv, tkSrc.Token)));

            Impressive.Msg("Connecting receiver and sender");
            recv.Connect();
            send.Connect();

            Impressive.Msg("Forcing VRCFT parameters");
            send.Send(new OscMessage("/vrcft/settings/forceRelevant", true));

            Impressive.Msg("Starting thread");
            listenThread.Start();

            Impressive.Msg("Thread started, listening!");
            Listening = true;

            return true;
        }
        catch (Exception ex)
        {
            Impressive.Msg($"Exception initializing OSCBridge: {ex}");
            Listening = false;
            return false;
        }
    }

    public void StopListen()
    {
        tkSrc.Cancel();
        tkSrc.Dispose();
    }

    void ListenLoop(OscReceiver recv, CancellationToken token)
    {
        Listening = true;
        AutoResetEvent ev = new(false);
        try
        {
            while (recv.State != OscSocketState.Closed)
            {

                if (token.IsCancellationRequested)
                {
                    Impressive.Msg($"OSCListener on {recv.LocalEndPoint} was closed by request.");
                    break;
                }
                var packet = recv.Receive();

                ReceivedPacket?.Invoke(recv, packet);
            }
        }
        catch (Exception ex)
        {
            if (recv.State == OscSocketState.Connected)
            {
                Impressive.Msg($"Exception in listener loop: {ex}");
                Listening = false;
            }
        }
    }
}
